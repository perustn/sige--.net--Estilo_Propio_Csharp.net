using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Estilo_Propio_Csharp
{
    public class ResultadoEjecucion
    {
        public bool Exitoso { get; set; }
        public string MensajeError { get; set; }
        public string CodigoError { get; set; }

        public string ModuloConError { get; set; }
        public List<LogEntry> LogDetallado { get; set; } = new List<LogEntry>();

        public object Excel { get; set; }
        public TimeSpan TiempoEjecucion { get; set; }

       
    }

    public class ConfiguracionCeldaControl
    {
        public string CeldaControl { get; set; }
        public string NombreHoja { get; set; }
        public bool UsarHojaActiva { get; set; } = false;
        public int? IndiceHoja { get; set; } // 1-based
        public List<string> PatronesError { get; set; } = new List<string> { "ERROR", "FAIL", "EXCEPTION" };
        public List<string> PatronesExito { get; set; } = new List<string> { "SUCCESS", "OK", "COMPLETED" };
        public int Reintentos { get; set; } = 1;
        public int DelayEntreReintentos { get; set; } = 500; // ms
        public bool EsOpcional { get; set; } = true; // Si no se encuentra, continuar sin error
    }

    public class ConfiguracionCeldaControlAvanzada : ConfiguracionCeldaControl
    {
        public string CeldaModuloActual { get; set; } = "B1";
        public string CeldaError { get; set; } = "C1";
        public string HojaLog { get; set; } = "Log";
        public bool LeerLogCompleto { get; set; } = false;
        public int MaxFilasLog { get; set; } = 100;
    }

    public class LogEntry
    {
        public string Timestamp { get; set; }
        public string Modulo { get; set; }
        public string Evento { get; set; }
        public string Tipo { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp}] {Tipo}: {Modulo} - {Evento}";
        }
    }

    //public class ResultadoMacro
    //{
    //    public bool Exitoso { get; set; }
    //    public string MensajeError { get; set; }
    //    public string ModuloConError { get; set; }
    //    public List<LogEntry> LogDetallado { get; set; } = new List<LogEntry>();
    //}


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
            bool mostrarExcel = false,
            ConfiguracionCeldaControl config = null,
            ConfiguracionCeldaControlAvanzada configAvanzada = null
            )
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
                            resultado.Excel = EjecutarRunConParametrosIndividuales(excelApp, nombreMacro, parametros);
                        }
                        else
                        {
                            resultado.Excel = excelApp.Run(nombreMacro);
                        }

                        if (config != null)
                        {
                            resultado = VerificarCeldaControlConfigurable(workbook, config,resultado);
                            return;
                        }
                        else if (configAvanzada != null)
                        {
                            resultado = VerificarCeldaControlAvanzada(workbook, configAvanzada, resultado);
                            return;
                        }
                        else if (!string.IsNullOrEmpty(celdaControl))
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

        private ResultadoEjecucion VerificarCeldaControlConfigurable(Workbook workbook,
        ConfiguracionCeldaControl config, ResultadoEjecucion resultado)
        {
            if (config == null || string.IsNullOrEmpty(config.CeldaControl))
                return resultado;

            Worksheet hoja = null;
            string referenciaHoja = "";

            try
            {
                // Determinar qué hoja usar según configuración
                hoja = ObtenerHojaSegunConfiguracion(workbook, config, out referenciaHoja);

                if (hoja == null)
                {
                    string mensaje = $"No se pudo obtener la hoja especificada: {referenciaHoja}";
                    if (config.EsOpcional)
                    {
                        Console.WriteLine($"Advertencia: {mensaje}");
                        return resultado;
                    }
                    else
                    {
                        resultado.MensajeError = mensaje;
                        return resultado;
                    }
                }

                // Intentar leer la celda con reintentos
                string valorControl = LeerCeldaConReintentos(hoja, config);

                if (!string.IsNullOrEmpty(valorControl))
                {
                    string referenciaCompleta = $"{hoja.Name}!{config.CeldaControl}";
                    Console.WriteLine($"Estado de celda control [{referenciaCompleta}]: {valorControl}");

                    // Verificar si es error
                    if (EsErrorSegunPatrones(valorControl, config.PatronesError))
                    {
                        //resultado.MensajeError = $"Error reportado en {referenciaCompleta}: {valorControl}";
                        resultado.MensajeError = valorControl;
                        resultado.Exitoso = false;
                        return resultado;
                    }

                    // Verificar si es éxito
                    if (EsExitoSegunPatrones(valorControl, config.PatronesExito))
                    {
                        Console.WriteLine($"Macro ejecutada exitosamente según {referenciaCompleta}");
                        resultado.Exitoso = true;
                    }
                }
                else
                {
                    string mensaje = $"Celda de control {hoja.Name}!{config.CeldaControl} está vacía";
                    if (config.EsOpcional)
                    {
                        Console.WriteLine($"Advertencia: {mensaje}");
                    }
                    else
                    {
                        resultado.MensajeError = $"Error: {mensaje}";
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje = $"Error al verificar celda control {referenciaHoja}!{config.CeldaControl}: {ex.Message}";
                if (config.EsOpcional)
                {
                    Console.WriteLine($"Advertencia: {mensaje}");
                }
                else
                {
                    resultado.MensajeError = mensaje;
                }
            }
            return resultado;
        }

        private static Worksheet ObtenerHojaSegunConfiguracion(Workbook workbook,
    ConfiguracionCeldaControl config, out string referencia)
        {
            referencia = "";

            try
            {
                // Prioridad 1: Usar hoja activa
                if (config.UsarHojaActiva)
                {
                    referencia = "HojaActiva";
                    return workbook.ActiveSheet;
                }

                // Prioridad 2: Usar índice de hoja
                if (config.IndiceHoja.HasValue)
                {
                    referencia = $"Indice{config.IndiceHoja.Value}";
                    if (config.IndiceHoja.Value > 0 && config.IndiceHoja.Value <= workbook.Sheets.Count)
                    {
                        return workbook.Sheets[config.IndiceHoja.Value];
                    }
                }

                // Prioridad 3: Usar nombre de hoja
                if (!string.IsNullOrEmpty(config.NombreHoja))
                {
                    referencia = config.NombreHoja;
                    foreach (Worksheet ws in workbook.Sheets)
                    {
                        if (ws.Name.Equals(config.NombreHoja, StringComparison.OrdinalIgnoreCase))
                        {
                            return ws;
                        }
                    }
                }

                // Por defecto: hoja activa
                referencia = "HojaActiva(Default)";
                return workbook.ActiveSheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string LeerCeldaConReintentos(Worksheet hoja, ConfiguracionCeldaControl config)
        {
            string valorControl = null;

            for (int intento = 0; intento < config.Reintentos; intento++)
            {
                try
                {
                    if (intento > 0)
                    {
                        System.Threading.Thread.Sleep(config.DelayEntreReintentos);
                        Console.WriteLine($"Reintentando lectura de celda (intento {intento + 1}/{config.Reintentos})");
                    }

                    valorControl = hoja.Range[config.CeldaControl].Value?.ToString()?.Trim();

                    if (!string.IsNullOrEmpty(valorControl))
                        break;
                }
                catch (Exception ex)
                {
                    if (intento == config.Reintentos - 1) // Último intento
                        throw;

                    Console.WriteLine($"Error en intento {intento + 1}: {ex.Message}");
                }
            }

            return valorControl;
        }

        private static bool EsErrorSegunPatrones(string valor, List<string> patrones)
        {
            if (string.IsNullOrEmpty(valor) || patrones == null || !patrones.Any())
                return false;

            string valorUpper = valor.ToUpper();
            return patrones.Any(patron => valorUpper.Contains(patron.ToUpper()));
        }

        private static bool EsExitoSegunPatrones(string valor, List<string> patrones)
        {
            if (string.IsNullOrEmpty(valor) || patrones == null || !patrones.Any())
                return false;

            string valorUpper = valor.ToUpper();
            return patrones.Any(patron => valorUpper.Equals(patron.ToUpper()) ||
                                         valorUpper.StartsWith(patron.ToUpper() + ":"));
        }

      

        private ResultadoEjecucion VerificarCeldaControlAvanzada(Workbook workbook,
    ConfiguracionCeldaControlAvanzada config, ResultadoEjecucion resultado)
        {
            if (config == null || string.IsNullOrEmpty(config.CeldaControl))
                return resultado;

            try
            {
                Worksheet hoja = ObtenerHojaSegunConfiguracion(workbook, config, out string referenciaHoja);

                if (hoja == null)
                {
                    resultado.MensajeError = $"No se pudo obtener la hoja: {referenciaHoja}";
                    return resultado;
                }

                // Leer estado principal
                string valorControl = LeerCeldaConReintentos(hoja, config);

                if (!string.IsNullOrEmpty(valorControl))
                {
                    Console.WriteLine($"Estado principal: {valorControl}");

                    if (EsErrorSegunPatrones(valorControl, config.PatronesError))
                    {
                        // Leer información adicional del error
                        string moduloActual = LeerCeldaSiExiste(hoja, config.CeldaModuloActual);
                        string detalleError = LeerCeldaSiExiste(hoja, config.CeldaError);

                        // Construir mensaje de error detallado
                        resultado.MensajeError = ConstruirMensajeErrorDetallado(
                            valorControl, moduloActual, detalleError, hoja.Name, config.CeldaControl);

                        // Leer log completo si está configurado
                        if (config.LeerLogCompleto)
                        {
                            var logCompleto = LeerLogCompleto(workbook, config);
                            resultado.LogDetallado = logCompleto;
                        }

                        resultado.Exitoso = false;
                        resultado.ModuloConError = moduloActual;
                        return resultado;
                    }

                    if (EsExitoSegunPatrones(valorControl, config.PatronesExito))
                    {
                        resultado.Exitoso = true;
                        Console.WriteLine("Macro ejecutada exitosamente");
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.MensajeError = $"Error verificando control: {ex.Message}";
            }
            return resultado;
        }

        private static string LeerCeldaSiExiste(Worksheet hoja, string celda)
        {
            try
            {
                return hoja.Range[celda].Value?.ToString()?.Trim() ?? "";
            }
            catch
            {
                return "";
            }
        }

        private static string ConstruirMensajeErrorDetallado(string valorControl, string modulo,
            string detalleError, string nombreHoja, string celdaControl)
        {
            var mensaje = new StringBuilder();
            //mensaje.AppendLine($"Error detectado en {nombreHoja}!{celdaControl}");
            mensaje.AppendLine($"Estado: {valorControl}");

            if (!string.IsNullOrEmpty(modulo))
                mensaje.AppendLine($"Módulo con error: {modulo}");

            if (!string.IsNullOrEmpty(detalleError))
                mensaje.AppendLine($"Detalle del error: {detalleError}");

            return mensaje.ToString().Trim();
        }

        private static List<LogEntry> LeerLogCompleto(Workbook workbook, ConfiguracionCeldaControlAvanzada config)
        {
            var logs = new List<LogEntry>();

            try
            {
                Worksheet hojaLog = workbook.Sheets[config.HojaLog];

                for (int fila = 2; fila <= Math.Min(hojaLog.UsedRange.Rows.Count, config.MaxFilasLog + 1); fila++)
                {
                    var timestamp = hojaLog.Cells[fila, 1].Value?.ToString();
                    var modulo = hojaLog.Cells[fila, 2].Value?.ToString();
                    var evento = hojaLog.Cells[fila, 3].Value?.ToString();
                    var tipo = hojaLog.Cells[fila, 4].Value?.ToString();

                    if (!string.IsNullOrEmpty(modulo))
                    {
                        logs.Add(new LogEntry
                        {
                            Timestamp = timestamp,
                            Modulo = modulo,
                            Evento = evento,
                            Tipo = tipo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error leyendo log completo: {ex.Message}");
            }

            return logs;
        }

        

    }
}
