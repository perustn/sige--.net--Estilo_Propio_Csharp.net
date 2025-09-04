using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading.Tasks;


namespace Estilo_Propio_Csharp
{
    public class ResultadoEjecucion
    {
        public bool Exitoso { get; set; }
        public string MensajeError { get; set; }
        public string CodigoError { get; set; }
        public object ResultadoMacro { get; set; }
        public TimeSpan TiempoEjecucion { get; set; }
    }

    public class ExcelMacroExecutor
    {
       

        /// <summary>
        /// Ejecuta una macro de Excel con manejo completo de errores
        /// </summary>
        /// <param name="rutaArchivo">Ruta completa al archivo Excel (.xlsm)</param>
        /// <param name="nombreMacro">Nombre de la macro a ejecutar</param>
        /// <param name="parametros">Parámetros opcionales para la macro</param>
        /// <param name="timeoutSegundos">Timeout en segundos (0 = sin timeout)</param>
        /// <param name="celdaControl">Celda para verificar estado (ej: "A1")</param>
        /// <param name="mostrarExcel">Si mostrar Excel durante la ejecución</param>
        /// <returns>Resultado de la ejecución con detalles del error si ocurre</returns>
        public async Task<ResultadoEjecucion> EjecutarMacroAsync(
            string rutaArchivo,
            string nombreMacro,
            object[] parametros = null,
            int timeoutSegundos = 300,
            string celdaControl = null,
            bool mostrarExcel = false)
        {
            var resultado = new ResultadoEjecucion();
            var inicioTiempo = DateTime.Now;

            Application excelApp = null;
            Workbook workbook = null;

            try
            {
                // Validaciones iniciales
                if (!File.Exists(rutaArchivo))
                {
                    resultado.MensajeError = $"El archivo no existe: {rutaArchivo}";
                    return resultado;
                }

                if (string.IsNullOrEmpty(nombreMacro))
                {
                    resultado.MensajeError = "El nombre de la macro no puede estar vacío";
                    return resultado;
                }

                // Configurar timeout si se especifica
                Task tareaEjecucion = Task.Run(() =>
                {
                    try
                    {
                        // Inicializar Excel
                        excelApp = new Application();
                        excelApp.Visible = mostrarExcel;
                        excelApp.DisplayAlerts = false;
                        excelApp.EnableEvents = false;
                        excelApp.ScreenUpdating = false;
                        //excelApp.Calculation = XlCalculation.xlCalculationManual;

                        Console.WriteLine($"Abriendo archivo: {rutaArchivo}");
                        workbook = excelApp.Workbooks.Open(
                            rutaArchivo,
                            UpdateLinks: 0,
                            ReadOnly: false,
                            Format: 5,
                            Password: "",
                            WriteResPassword: "",
                            IgnoreReadOnlyRecommended: true,
                            Origin: XlPlatform.xlWindows,
                            Delimiter: "",
                            Editable: false,
                            Notify: false,
                            Converter: 0,
                            AddToMru: false,
                            Local: false,
                            CorruptLoad: XlCorruptLoad.xlNormalLoad
                        );

                        //// Verificar si la macro existe
                        //if (!VerificarMacroExiste(workbook, nombreMacro))
                        //{
                        //    resultado.MensajeError = $"La macro '{nombreMacro}' no existe en el archivo";
                        //    return;
                        //}

                        // Limpiar celda de control si se especifica
                        if (!string.IsNullOrEmpty(celdaControl))
                        {
                            try
                            {
                                workbook.ActiveSheet.Range[celdaControl].Value = "";
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Advertencia: No se pudo limpiar celda de control {celdaControl}: {ex.Message}");
                            }
                        }

                        Console.WriteLine($"Ejecutando macro: {nombreMacro}");

                        // Ejecutar la macro
                        if (parametros != null && parametros.Length > 0)
                        {
                            //resultado.ResultadoMacro = excelApp.Run(nombreMacro, parametros);
                            resultado.ResultadoMacro = EjecutarRunConParametrosIndividuales(excelApp, nombreMacro, parametros);
                            //resultado.ResultadoMacro = excelApp.Run(nombreMacro, parametros[0]);
                        }
                        else
                        {
                            resultado.ResultadoMacro = excelApp.Run(nombreMacro);
                        }

                        // Verificar celda de control si se especifica
                        if (!string.IsNullOrEmpty(celdaControl))
                        {
                            try
                            {
                                var valorControl = workbook.ActiveSheet.Range[celdaControl].Value?.ToString();
                                if (!string.IsNullOrEmpty(valorControl))
                                {
                                    if (valorControl.ToUpper().StartsWith("ERROR"))
                                    {
                                        resultado.MensajeError = $"Error reportado por macro: {valorControl}";
                                        return;
                                    }
                                    Console.WriteLine($"Estado de celda control: {valorControl}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Advertencia: No se pudo leer celda de control: {ex.Message}");
                            }
                        }

                        //workbook.Save();
                        resultado.Exitoso = true;
                        Console.WriteLine("Macro ejecutada exitosamente");
                    }
                    catch (Exception ex)
                    {
                        if (ex is COMException comEx)
                        {
                            ManejarErrorCOM(comEx, resultado);
                        }
                        else
                        {
                            resultado.MensajeError = $"Error general: {ex.Message}";
                            resultado.CodigoError = ex.GetType().Name;
                        }
                    }
                });

                // Aplicar timeout si se especifica
                if (timeoutSegundos > 0)
                {
                    if (await Task.WhenAny(tareaEjecucion, Task.Delay(timeoutSegundos * 1000)) != tareaEjecucion)
                    {
                        resultado.MensajeError = $"La ejecución excedió el timeout de {timeoutSegundos} segundos";
                        resultado.CodigoError = "TIMEOUT";
                    }
                    else
                    {
                        await tareaEjecucion;
                    }
                }
                else
                {
                    await tareaEjecucion;
                }
            }
            catch (Exception ex)
            {
                resultado.MensajeError = $"Error en el wrapper: {ex.Message}";
                resultado.CodigoError = ex.GetType().Name;
            }
            finally
            {
                resultado.TiempoEjecucion = DateTime.Now - inicioTiempo;

                // Liberar recursos COM
                await LiberarRecursosCOM(workbook, excelApp);
            }

            return resultado;
        }

        private object EjecutarRunConParametrosIndividuales(Application excelApp, string nombreMacro, object[] parametros)
        {
            // Excel.Run puede aceptar hasta 30 parámetros
            switch (parametros.Length)
            {
                case 1:
                    return excelApp.Run(nombreMacro, parametros[0]);
                case 2:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1]);
                case 3:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2]);
                case 4:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2], parametros[3]);
                case 5:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2], parametros[3], parametros[4]);
                case 6:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2], parametros[3], parametros[4], parametros[5]);
                case 7:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2], parametros[3], parametros[4], parametros[5], parametros[6]);
                case 8:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2], parametros[3], parametros[4], parametros[5], parametros[6], parametros[7]);
                case 9:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2], parametros[3], parametros[4], parametros[5], parametros[6], parametros[7], parametros[8]);
                case 10:
                    return excelApp.Run(nombreMacro, parametros[0], parametros[1], parametros[2], parametros[3], parametros[4], parametros[5], parametros[6], parametros[7], parametros[8], parametros[9]);
                default:
                    throw new ArgumentException($"No se soportan más de 10 parámetros. Se recibieron: {parametros.Length}");
            }
        }

        /// <summary>
        /// Versión sincrónica de la función
        /// </summary>
        public ResultadoEjecucion EjecutarMacro(
            string rutaArchivo,
            string nombreMacro,
            object[] parametros = null,
            int timeoutSegundos = 300,
            string celdaControl = null,
            bool mostrarExcel = false)
        {
            return EjecutarMacroAsync(rutaArchivo, nombreMacro, parametros, timeoutSegundos, celdaControl, mostrarExcel).Result;
        }

        //private bool VerificarMacroExiste(Workbook workbook, string nombreMacro)
        //{
        //    try
        //    {
        //        // Intentar acceder a la macro directamente
        //        foreach (VBComponent component in workbook.VBProject.VBComponents)
        //        {
        //            if (component.CodeModule.CountOfLines > 0)
        //            {
        //                string codigo = component.CodeModule.Lines[1, component.CodeModule.CountOfLines];
        //                if (codigo.Contains($"Sub {nombreMacro}") || codigo.Contains($"Function {nombreMacro}"))
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        // Si no podemos verificar, asumimos que existe
        //        return true;
        //    }
        //}

        private void ManejarErrorCOM(COMException comEx, ResultadoEjecucion resultado)
        {
            resultado.CodigoError = $"{comEx.HResult:X}";

            Console.WriteLine("=== ERROR COM DETALLADO ===");
            Console.WriteLine($"HRESULT: {comEx.HResult:X} ({comEx.HResult})");
            Console.WriteLine($"Mensaje original: {comEx.Message}");

            // Intentar obtener descripción más detallada
            try
            {
                Exception detalleEx = Marshal.GetExceptionForHR(comEx.HResult);
                if (detalleEx != null && detalleEx.Message != comEx.Message)
                {
                    Console.WriteLine($"Descripción detallada: {detalleEx.Message}");
                }
            }
            catch { }

            // Intentar descripción Win32
            try
            {
                var win32Ex = new Win32Exception(comEx.HResult);
                if (!string.IsNullOrEmpty(win32Ex.Message) && win32Ex.Message != comEx.Message)
                {
                    Console.WriteLine($"Descripción Win32: {win32Ex.Message}");
                }
            }
            catch { }

            // Descripción personalizada según código de error
            string descripcionPersonalizada = ObtenerDescripcionErrorExcel(comEx.HResult);
            Console.WriteLine($"Descripción: {descripcionPersonalizada}");

            if (!string.IsNullOrEmpty(comEx.Source))
                Console.WriteLine($"Fuente: {comEx.Source}");

            Console.WriteLine("==========================");

            resultado.MensajeError = $"{descripcionPersonalizada} (Código: {comEx.HResult:X})";

            // Sugerencias específicas
            AgregarSugerenciasError(comEx.HResult, resultado);
        }

        private string ObtenerDescripcionErrorExcel(int hresult)
        {
            switch ((uint)hresult)
            {
                case 0x800A03EC: // -2146827284
                    return "No se puede ejecutar la macro. Puede estar deshabilitada o no existir";
                case 0x800A01A8: // -2146827864
                    return "El objeto no admite esta propiedad o método";
                case 0x800A0009: // -2146828279
                    return "El subíndice está fuera del intervalo";
                case 0x800A000D: // -2146828275
                    return "No coinciden los tipos de datos";
                case 0x800A0414: // -2146827244
                    return "Error en tiempo de ejecución de la aplicación";
                case 0x800A03E4: // -2146827292
                    return "Error de automatización. El objeto se ha desconectado";
                case 0x80020009: // -2147352567
                    return "Excepción durante la ejecución de la macro VBA";
                case 0x800A01B6: // -2146827850
                    return "El objeto no admite esta acción";
                case 0x800A01E2: // -2146827806
                    return "Archivo no válido o dañado";
                case 0x800A03EA: // -2146827286
                    return "Error de sintaxis en la macro";
                case 0x800A0005: // -2146828283
                    return "Llamada a procedimiento no válida o argumento incorrecto";
                case 0x800A000E: // -2146828274
                    return "Sin memoria suficiente";
                case 0x800A0011: // -2146828271
                    return "División por cero";
                case 0x800A001C: // -2146828260
                    return "Argumento o llamada a procedimiento no válida";
                case 0x800A002F: // -2146828241
                    return "Error en tiempo de ejecución DLL";
                case 0x80004005: // -2147467259
                    return "Error no especificado";
                case 0x80070005: // -2147024891
                    return "Acceso denegado";
                default:
                    return $"Error COM no identificado (HRESULT: {hresult:X})";
            }
        }

        private void AgregarSugerenciasError(int hresult, ResultadoEjecucion resultado)
        {
            string sugerencia = "";

            switch ((uint)hresult)
            {
                case 0x800A03EC:
                    sugerencia = "Verifica que las macros estén habilitadas en Excel y que el nombre de la macro sea exacto.";
                    break;
                case 0x800A01A8:
                    sugerencia = "Revisa que el método o propiedad exista en la versión de Excel instalada.";
                    break;
                case 0x80020009:
                    sugerencia = "Error dentro del código VBA de la macro. Revisa el código de la macro en Excel.";
                    break;
                case 0x800A0009:
                    sugerencia = "Verifica que los índices de arrays y rangos estén dentro de los límites válidos.";
                    break;
                case 0x800A000D:
                    sugerencia = "Verifica que los tipos de datos de los parámetros sean correctos.";
                    break;
                case 0x80070005:
                    sugerencia = "El archivo puede estar protegido o abierto por otra aplicación.";
                    break;
            }

            if (!string.IsNullOrEmpty(sugerencia))
            {
                resultado.MensajeError += $"\n\nSugerencia: {sugerencia}";
            }
        }

        private async Task LiberarRecursosCOM(Workbook workbook, Application excelApp)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (workbook != null)
                    {
                        try
                        {
                            workbook.Close(false);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error cerrando workbook: {ex.Message}");
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(workbook);
                        }
                    }

                    if (excelApp != null)
                    {
                        try
                        {
                            excelApp.ScreenUpdating = true;
                            //excelApp.Calculation = XlCalculation.xlCalculationAutomatic;
                            excelApp.EnableEvents = true;
                            excelApp.DisplayAlerts = true;
                            excelApp.Quit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error cerrando Excel: {ex.Message}");
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(excelApp);
                        }
                    }

                    // Forzar garbage collection
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error liberando recursos COM: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Método de conveniencia para ejecutar macro simple
        /// </summary>
        public bool EjecutarMacroSimple(string rutaArchivo, string nombreMacro)
        {
            var resultado = EjecutarMacro(rutaArchivo, nombreMacro);

            if (!resultado.Exitoso)
            {
                Console.WriteLine($"Error ejecutando macro: {resultado.MensajeError}");
            }
            else
            {
                Console.WriteLine($"Macro ejecutada en {resultado.TiempoEjecucion.TotalSeconds:F2} segundos");
            }

            return resultado.Exitoso;
        }

    }
}
