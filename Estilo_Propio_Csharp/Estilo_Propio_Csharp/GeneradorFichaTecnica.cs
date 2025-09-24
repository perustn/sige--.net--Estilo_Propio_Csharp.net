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
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

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

        private string GuardarRutaPDFenBD(string codEstpro, string codVersion, int IDFichaTecnica, string ArchivoDestino, 
            int IdPublicacion, string Password_PDF_Protection = "")
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
            strSQL += string.Format(",@Password_PDF_Protection= '{0}'", Password_PDF_Protection) + Environment.NewLine;

            if (!oHp.EjecutarOperacion(strSQL))
            {
                executionOk = "Error en ejecucion de SP ES_MANT_ESTPROVER_FICHA_TECNICA";
            };
            return executionOk;
        }

        public string CreaCarpetaLocal()
        {
            string carpetaLocal = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\FichaTecnicaTMP\\";
            if (!Directory.Exists(carpetaLocal))
            {
                Directory.CreateDirectory(carpetaLocal);
            }
            return carpetaLocal;
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

        public void CopiarArchivo(string pathOrigen,string pathDestino)
        {

            if (System.IO.File.Exists(pathDestino))
            {
                File.Delete(pathDestino);
                File.Copy(pathOrigen, pathDestino);
            }
            else
            {
                File.Copy(pathOrigen, pathDestino);
            }
        }

        private async Task<ResultadoEjecucion> GenerarPDFAsync_V2(string codEstpro, string codVersion, int IDFichaTecnica, string rutaFile, string nombreFile,
            IProgress<ProgresoInfo> progress, CancellationToken cancellationToken)
        {
            string RouteFileXLT = VariablesGenerales.pRuta;
            string RouteLogo = oHp.DevuelveDato("SELECT Ruta_Logo = ISNULL(Ruta_Logo, '') From SEGURIDAD..SEG_EMPRESAS WHERE Cod_Empresa = '" +
                VariablesGenerales.pCodEmpresa + "'", VariablesGenerales.pConnect).ToString();

            string MethodVBA = "App.RunSave";
            RouteFileXLT = Path.Combine(RouteFileXLT, "RptFichaTecnicaPrendaV2.xltm");

            var executor = new ExcelMacroExecutor();

            // Ejemplo 2: Configuración por índice con reintentos
            //var config = new ConfiguracionCeldaControl
            //{
            //    CeldaControl = "R2",
            //    IndiceHoja = 1,
            //    Reintentos = 3,
            //    DelayEntreReintentos = 1000
            //};

            // Configuración para máximo rendimiento
            var config = new ConfiguracionCeldaControlAvanzada
            {
                CeldaControl = "A1",           // Estado principal
                NombreHoja = "Control",     // Hoja de control     

                CeldaModuloActual = "A2",      // Módulo actual
                CeldaError = "A3",             // Detalle del error

                HojaLog = "Log",               // Hoja de log detallado

                SoloLeerLogEnError = true,     // Solo leer log detallado si hay error
                IgnorarLogDetallado = false,    // No leer log detallado nunca
                                               
                NivelMinimoLog = LogLevelExcel.Info, 

                MaxFilasLog = 100,              // Máximo 50 entradas de log
                Reintentos = 1,
                EsOpcional = false,
                MonitoreoTiempoReal = true,
                CeldaMetaData = "A7"
            };

            string RouteFileLog = rutaFile;
            RouteFileLog = Path.Combine(RouteFileLog, nombreFile,"_log.csv");

            var configCSV = new ConfiguracionLogCSV
            {
                RutaArchivoLog = RouteFileLog,
                CeldaControl = "A4",
                NombreHoja = "Control",
                MonitoreoTiempoReal = true,
                IntervaloMonitoreoMs = 100,
                CrearArchivoSiNoExiste = true,
                LimpiarLogAnterior = true,
                AgregarTimestampAlNombre = true,
                NivelesPermitidos = new List<LogLevelExcel> { LogLevelExcel.Error, LogLevelExcel.Warning, LogLevelExcel.Info }
            };

            // Configuración para depuración completa
            // SoloLeerEnError = false,       // Siempre leer log
            //IgnorarLogDetallado = false,   // Leer log detallado
            //NivelMinimo = LogLevel.Debug,  // Todo el detalle

            ResultadoEjecucion resultado = await executor.EjecutarMacroAsync(
            rutaArchivo: RouteFileXLT,
            nombreMacro: MethodVBA,
            parametros: new object[] {VariablesGenerales.pConnectVB6,
                        RouteLogo,
                        codEstpro,
                        codVersion,
                        IDFichaTecnica,
                        true,
                        rutaFile,
                        nombreFile,
                        "PDF"
                        },
            timeoutSegundos: 1000,
            celdaControl: null,
            mostrarExcel: true,
            config: null,
            configAvanzada: config,
            progress, 
            cancellationToken,
            configCSV: configCSV
            );;

         return resultado;

        }

        public string GenerarPDF(string codEstpro, string codVersion, int IDFichaTecnica, string vCarpetaFichaTecnicaCliente, string RutaArchivoCompleto)
        {
            dynamic oXL = null;
            Process xproc = null;
            string executionOk  = "";
            object ResultadoMacro;
            try
            {                
                string RouteFileXLT = VariablesGenerales.pRuta;
                string RouteLogo = oHp.DevuelveDato("SELECT Ruta_Logo = ISNULL(Ruta_Logo, '') From SEGURIDAD..SEG_EMPRESAS WHERE Cod_Empresa = '" +
                    VariablesGenerales.pCodEmpresa + "'", VariablesGenerales.pConnect).ToString();

                string MethodVBA = "Run";
                RouteFileXLT = Path.Combine(RouteFileXLT, "RptFichaTecnicaPrendaV2_2.xltm");
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
                ResultadoMacro = oXL.Run(MethodVBA,
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

        public async Task<string> ProtegerPdf(string carpetaDestino, string OwnerPassword ="")
        {
            string executionOk = "";
            using var pdfService = new PDFProtectionPdfSharp.PdfProtectionService();

            if (OwnerPassword.Equals("")){
                OwnerPassword = PDFProtectionPdfSharp.PdfProtectionService.GenerateSecurePassword();
            }

            var config = new PDFProtectionPdfSharp.PdfProtectionConfig
            {
                InputPath = carpetaDestino,
                OutputPath = carpetaDestino, // mismo archivo → se reemplaza
                OwnerPassword = OwnerPassword,
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
        public async Task<bool> GenerarPDFAsync(string codEstpro, 
            string codVersion, int IDFichaTecnica, int IdPublicacion, string CodigoClienteSel,
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

            string rutaArchivoPDF = CreaCarpetaLocal();
            string nombreArchivoPDF = string.Format("FT-EP{0}-{1}-{2}_T", codEstpro, codVersion, IdPublicacion);
            string nombreArchivoPDF_ConIndice = string.Format("FT-EP{0}-{1}-{2}", codEstpro, codVersion, IdPublicacion);

            string pathPDF = string.Format("{0}{1}{2}", rutaArchivoPDF, nombreArchivoPDF, ".PDF");
            string pathPDF_ConIndice = string.Format("{0}{1}{2}", rutaArchivoPDF, nombreArchivoPDF_ConIndice, ".PDF");

            string rutaArchivoPDF_Compartido = ValidaCarpetaCompartida(CodigoClienteSel);
            string pathPDF_Compartido = Path.Combine(rutaArchivoPDF_Compartido, nombreArchivoPDF_ConIndice + ".PDF");

            try
            {
                porcentaje = 5;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Eliminando PDF existente"
                });

                EliminaPDF(pathPDF);

                porcentaje = 10;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Inicio de Invocacion de plantilla XLT"
                });

                string mensajePDF = "";
                ResultadoEjecucion resultado = await GenerarPDFAsync_V2(codEstpro, codVersion, IDFichaTecnica, rutaArchivoPDF, nombreArchivoPDF,
                    progress, cancellationToken);

                if (resultado.Exitoso)
                {
                    porcentaje = 79;
                    progress?.Report(new ProgresoInfo
                    {
                        Porcentaje = porcentaje,
                        Mensaje = "Generando PDF",
                        Detalle = ($"✓ Macro ejecutada exitosamente en {resultado.TiempoEjecucion.TotalSeconds:F2}s")
                    });
                }
                else
                {
                    if (resultado.LogDetallado.Any())
                    {
                        foreach (var log in resultado.LogDetallado)
                        {
                            progress?.Report(new ProgresoInfo
                            {
                                Porcentaje = porcentaje,
                                Mensaje = "Generando PDF",
                                Detalle = log.ToString()
                            });
                        }
                    }
                    throw new ProcessingException($"Error al Generar PDF: {resultado.MensajeError}");
                }

                var procesador = new PdfIndexProcessor();
                procesador.ProcesarPdfConIndice(pathPDF, resultado.RutaArchivoMetaData, pathPDF_ConIndice);

                porcentaje = 80;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Protegiendo PDF"
                });

                ResultadoProteccionPDF proteccionPDF = await ProtegerPdfExe(pathPDF_ConIndice);

                if (!proteccionPDF.Exitoso)
                {
                    throw new ProcessingException($"Error al Proteger PDF");
                };

                porcentaje = 90;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Copiando a ruta compartida"
                });

                CopiarArchivo(pathPDF_ConIndice, pathPDF_Compartido);

                porcentaje = 95;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Guardando Ruta de PDF en base de datos"
                });

                var task = Task.Run(() =>
                {
                    return GuardarRutaPDFenBD(codEstpro, codVersion, IDFichaTecnica, pathPDF_Compartido, IdPublicacion, proteccionPDF.OwnerPassword);
                }, cancellationToken);

                mensajePDF = await task;
                if (!mensajePDF.Equals(""))
                {
                    throw new ProcessingException($"Error al guardar PDF en BD: {mensajePDF}");
                }

                porcentaje = 97;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Generando PDF",
                    Detalle = "Eliminando PDF Local Temporal"
                });

                EliminaPDF(pathPDF);

                // Finalización
                porcentaje = 100;
                progress?.Report(new ProgresoInfo
                {
                    Porcentaje = porcentaje,
                    Mensaje = "Proceso completado",
                    Detalle = "Todos los pasos ejecutados correctamente"
                });

                Process.Start(pathPDF_ConIndice);

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

        public class ResultadoProteccionPDF
        {
            public bool Exitoso { get; set; }
            public string OwnerPassword { get; set; }
        }
        private async Task<ResultadoProteccionPDF> ProtegerPdfExe(string carpetaDestino)
        {
            ResultadoProteccionPDF resultado = new ResultadoProteccionPDF();
            string nameProy = "Estilo_Propio_Csharp";
            string tipo = ".exe";
            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            string sDllName = sPath + @"\" + nameProy + tipo;
            string executablePath = sDllName;

            // Define tus parámetros en una lista
            List<string> parameters = new List<string>
            {
                "PROTECCIONPDF",
                VariablesGenerales.pConnect,
                VariablesGenerales.pConnectSeguridad,
                VariablesGenerales.pConnectVB6,
                VariablesGenerales.pCodEmpresa,
                VariablesGenerales.pUsuario,
                VariablesGenerales.pRuta,
                VariablesGenerales.pCodPerfil,
                carpetaDestino
            };

            // Prepara los argumentos: Si un parámetro contiene espacios, lo encierra en comillas.
            // string.Join une todos los elementos con un espacio entre ellos.
            string allArguments = string.Join(" ", parameters.Select(p => p.Contains(" ") ? $"\"{p}\"" : p));

            using (var process = new Process())
            {
                process.StartInfo.FileName = executablePath;
                process.StartInfo.Arguments = allArguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();

                // Esperar a que termine el proceso
                await Task.Run(() => process.WaitForExit());

                // Obtener código de salida
                if (process.ExitCode == 0)
                {
                    string[] lineas = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    if (lineas.Length > 0)
                    {
                        resultado.OwnerPassword = lineas[lineas.Length - 1];
                        resultado.Exitoso = true;
                    }
                }
            }
            return resultado;
        }
    }
}
