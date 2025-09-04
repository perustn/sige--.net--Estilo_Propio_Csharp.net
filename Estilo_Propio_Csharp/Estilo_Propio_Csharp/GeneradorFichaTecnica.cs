using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Estilo_Propio_Csharp.FormularioProgreso;

namespace Estilo_Propio_Csharp
{
    class GeneradorFichaTecnica
    {
        ClsHelper oHp = new ClsHelper();

        // Método público que recibe los 3 parámetros
        public async Task<bool> GenerarFtPDFAsync(string codEstpro, string codVersion, int IDFichaTecnica, int IdPublicacion, string CodigoClienteSel)
        {
            bool isGeneroOK = false;
           
            return isGeneroOK;
        }

        private void SetPendienteGenerarFT(string codEstpro, string codVersion, int IDFichaTecnica, int IdPublicacion)
        {
            try
            {
                string strSQL = "EXEC FT_Recovery_Generacion_PDF_Incompleta " + Environment.NewLine;
                strSQL += string.Format(" @ID_Publicacion   = '{0}'", IdPublicacion) + Environment.NewLine;
                strSQL += string.Format(",@COD_USUARIO      = '{0}'", VariablesGenerales.pUsuario) + Environment.NewLine;
                
                oHp.EjecutarOperacion(strSQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string GuardarRutaPDFenBD(string codEstpro, string codVersion, int IDFichaTecnica, string ArchivoDestino, int IdPublicacion)
        {
            string executionOk = "";
            // Guardar en base de datos
     
            string strSQL = string.Empty;
            strSQL += Environment.NewLine + "EXEC ES_MANT_ESTPROVER_FICHA_TECNICA" + Environment.NewLine;
            strSQL += string.Format(" @OPCION           = '{0}'", "V") + Environment.NewLine;
            strSQL += string.Format(",@COD_ESTPRO       = '{0}'", codEstpro) + Environment.NewLine;
            strSQL += string.Format(",@COD_VERSION      = '{0}'", codVersion) + Environment.NewLine;
            strSQL += string.Format(",@SEC              =  {0} ", IDFichaTecnica) + Environment.NewLine;
            strSQL += string.Format(",@DESCRIPCION      = '{0}'", "") + Environment.NewLine;
            strSQL += string.Format(",@OBSERVACION      = '{0}'", "") + Environment.NewLine;
            strSQL += string.Format(",@FEC_PUBLICACION  =  {0} ", "NULL") + Environment.NewLine;
            strSQL += string.Format(",@NOMBRE_ADJUNTO   = '{0}'", ArchivoDestino) + Environment.NewLine;
            strSQL += string.Format(",@COD_USUARIO      = '{0}'", VariablesGenerales.pUsuario) + Environment.NewLine;
            strSQL += string.Format(",@PC_CREACION      = '{0}'", Environment.MachineName) + Environment.NewLine;
            strSQL += string.Format(",@ID_Publicacion_Tx=  {0} ", IdPublicacion) + Environment.NewLine;
            if (oHp.EjecutarOperacion(strSQL))
            {
                executionOk = "Error en ejecucion de SP ES_MANT_ESTPROVER_FICHA_TECNICA";
            };
            return executionOk;
        }

        public string CreaCarpetaLocal()
        {
            string vCarpetaFichaTecnicaCliente = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FichaTecnicaTMP\\";
            if (!Directory.Exists(vCarpetaFichaTecnicaCliente))
            {
                Directory.CreateDirectory(vCarpetaFichaTecnicaCliente);
            }
            return vCarpetaFichaTecnicaCliente;
        }

        public string ValidaCarpetaCompartida(string CodigoClienteSel)
        {
            string RutaCompartida = oHp.DevuelveDato("Select Ruta_Ficha_Tecnica from tg_cliente where cod_cliente = '" + CodigoClienteSel + "'",
                        VariablesGenerales.pConnect).ToString();
            if (!Directory.Exists(RutaCompartida))
            {
                Directory.CreateDirectory(RutaCompartida);
            }
            return RutaCompartida;
        }

        public bool EliminaPDF(string NombreArchivo)
        {
            if (File.Exists(NombreArchivo))
            {
                File.Delete(NombreArchivo);
            }
            return true;
        }

        public string CopiaPDFLocalCompartido(string CodigoClienteSel,string RutaArchivoCompleto, string NombreArchivo)
        {
            string RutaCompartida;
            RutaCompartida = ValidaCarpetaCompartida(CodigoClienteSel);
            string ArchivoDestino = Path.Combine(RutaCompartida, RutaArchivoCompleto + ".PDF");

            if (System.IO.File.Exists(ArchivoDestino))
            {
                File.Delete(ArchivoDestino);
                File.Copy(NombreArchivo, ArchivoDestino);
                Process.Start(NombreArchivo);
            }
            else
            {
                File.Copy(NombreArchivo, ArchivoDestino);
                Process.Start(NombreArchivo);
            }

            return ArchivoDestino;
        }

        public string GenerarPDF(string codEstpro, string codVersion, int IDFichaTecnica, string vCarpetaFichaTecnicaCliente, string RutaArchivoCompleto)
        {
            dynamic oXL = null;
            Process xproc = null;
            var resultado = new ResultadoEjecucion();
            string executionOk  = "";
            try
            {                
                string RouteFileXLT = VariablesGenerales.pRuta;
                string RouteLogo = oHp.DevuelveDato("SELECT Ruta_Logo = ISNULL(Ruta_Logo, '') From SEGURIDAD..SEG_EMPRESAS WHERE Cod_Empresa = '" +
                    VariablesGenerales.pCodEmpresa + "'", VariablesGenerales.pConnect).ToString();

                string MethodVBA = "App.Run";
                RouteFileXLT = Path.Combine(RouteFileXLT, "RptFichaTecnicaPrendaV2.xltm");
                oXL = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
                int xlHWND = oXL.Hwnd;
                GetWindowThreadProcessId((IntPtr)xlHWND, out int procIdXL);
                xproc = Process.GetProcessById(procIdXL);

                //oXL.Workbooks.Open(RouteFileXLT);
                var wb = oXL.Workbooks.Open(RouteFileXLT);
                oXL.DisplayAlerts = false;
                oXL.Visible = false;
                oXL.EnableEvents = false;

                // Ejecutar macro con parámetros
                resultado.ResultadoMacro = oXL.Run(MethodVBA,
                    VariablesGenerales.pConnectVB6,
                    RouteLogo,
                    codEstpro,
                    codVersion,
                    IDFichaTecnica,
                    true,
                    vCarpetaFichaTecnicaCliente,
                    RutaArchivoCompleto,
                    "PDF");


                // ✅ Cerrar workbook explícitamente
                //wb.Close(false);

                // ✅ Cerrar Excel
                oXL.Quit();

                // Liberar COM
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);

                wb = null;
                oXL = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            //catch (System.Runtime.InteropServices.COMException comEx)
            //{
            //    MessageBox.Show($"Error COM ejecutando macro: {comEx.Message}");
            //    MessageBox.Show($"HRESULT: {comEx.HResult:X}");
            //}
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
                executionOk = ex.Message;

                //MessageBox.Show(ex.Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oXL != null)
                {
                    oXL.Quit();
                    ReleaseCOM(oXL);
                }

                if (!xproc.HasExited)
                {
                    xproc.Kill();
                }                
            }
            return executionOk;
        }

        public class ResultadoEjecucion
        {
            public bool Exitoso { get; set; }
            public string MensajeError { get; set; }
            public string CodigoError { get; set; }
            public object ResultadoMacro { get; set; }
            public TimeSpan TiempoEjecucion { get; set; }
        }

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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);

        static void ReleaseCOM(object com)
        {
            try
            {
                if (com != null && Marshal.IsComObject(com))
                {
                    Marshal.ReleaseComObject(com);
                }
                com = null;
            }
            catch (Exception)
            {
                com = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private async Task<string> ProtegerPdf(string carpetaDestino)
        {
            string executionOk = "";
            using var pdfService = new PDFProtectionPdfSharp.PdfProtectionService();

            var config = new PDFProtectionPdfSharp.PdfProtectionConfig
            {
                InputPath = carpetaDestino,
                OutputPath = carpetaDestino, // mismo archivo → se reemplaza
                OwnerPassword = PDFProtectionPdfSharp.PdfProtectionService.GenerateSecurePassword(),
                UserPassword = "",
                Permissions = new PDFProtectionPdfSharp.PdfPermissions
                {
                    AllowPrint = false,  // 🚫 bloquear impresión
                    AllowExtractContent = true,
                    AllowModifyDocument = false,
                    AllowAnnotations = false,
                    AllowFormsFill = false,
                    AllowAccessibilityExtractContent = true,
                    AllowAssembleDocument = false
                },
                OverwriteIfExists = true,
                CreateBackup = false
            };

            var result = await pdfService.ProtectPdfAsync(config);

            if (!result.Success)
            {
                executionOk = result.Message;
            }

            return executionOk;
        }

        // Método público que permite llevar el control del proceso
        public async Task<bool> GenerarPDFAsync(string codEstpro, string codVersion, int IDFichaTecnica, int IdPublicacion, string CodigoClienteSel,
            IProgress<ProgresoInfo> progress, CancellationToken cancellationToken)
        {
            int porcentaje = 0;
            progress?.Report(new ProgresoInfo
            {
                Porcentaje = 0,
                Mensaje = "Inicializando proceso...",
                Detalle = "Preparando recursos y validando parámetros"
            });

            bool isGeneroOK = false;
            string vCarpetaFichaTecnicaCliente;
            vCarpetaFichaTecnicaCliente = CreaCarpetaLocal();

            string RutaArchivoCompleto = string.Format("FT[{2}]EP{0}-{1}", codEstpro, codVersion, IDFichaTecnica);
            string NombreArchivo = string.Format("{0}FT[{3}]EP{1}-{2}{4}", vCarpetaFichaTecnicaCliente, codEstpro, codVersion, IDFichaTecnica, ".PDF");

            try
            {
                porcentaje = 5;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Eliminando PDF existente"
                });

                EliminaPDF(NombreArchivo);

                porcentaje = 10;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Inicio de Extraccion informacion de base de datos y construyendo PDF"
                });

                string mensajePDF = "";

                var task = Task.Run(() =>
                {
                    return GenerarPDF(codEstpro, codVersion, IDFichaTecnica, vCarpetaFichaTecnicaCliente, RutaArchivoCompleto);
                }, cancellationToken);

                // *** ESPERAR Y RECIBIR EL RESULTADO ***
                mensajePDF = await task;
                if (!mensajePDF.Equals(""))
                {
                    throw new ProcessingException($"Error al generar PDF: {mensajePDF}");
                }

                porcentaje = 80;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Protegiendo PDF"
                });

                mensajePDF = await ProtegerPdf(vCarpetaFichaTecnicaCliente + RutaArchivoCompleto + ".PDF");
                if (!mensajePDF.Equals(""))
                {
                    throw new ProcessingException($"Error al Proteger PDF: {mensajePDF}");
                }

                porcentaje = 90;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Copiando a ruta compartida"
                });

                task = Task.Run(() =>
                {
                    return CopiaPDFLocalCompartido(CodigoClienteSel, RutaArchivoCompleto, NombreArchivo);
                }, cancellationToken);

                string ArchivoDestino;
                ArchivoDestino = await task;

                porcentaje = 95;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Guardando Ruta de PDF en base de datos"
                });

                task = Task.Run(() =>
                {
                    return GuardarRutaPDFenBD(codEstpro, codVersion, IDFichaTecnica, ArchivoDestino, IdPublicacion);
                }, cancellationToken);

                mensajePDF = await task;
                if (!mensajePDF.Equals(""))
                {
                    throw new ProcessingException($"Error al guardar PDF en BD: {mensajePDF}");
                }

                // Finalización
                porcentaje = 100;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Proceso completado",
                    Detalle = "Todos los pasos ejecutados correctamente"
                });

                isGeneroOK = true;
            }
            catch (OperationCanceledException)
            {
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Proceso cancelado",
                    Detalle = "La operación fue cancelada por el usuario",
                    EsError = true
                });

                SetPendienteGenerarFT(codEstpro, codVersion, IDFichaTecnica, IdPublicacion);
                isGeneroOK = false;
            }
            catch (Exception ex)
            {
               
                // Esto captura cualquier error no manejado
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "💥 Error crítico",
                    Detalle = ex.Message,
                    EsError = true
                });
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
                SetPendienteGenerarFT(codEstpro, codVersion, IDFichaTecnica, IdPublicacion);
                isGeneroOK = false;

                //throw; // Re-lanza para que el formulario lo maneje
            }
            return isGeneroOK;
        }
    }
}
