using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Puedes realizar acciones basadas en los argumentos
            if (args.Length > 0)
            {
                VariablesGenerales.pConnect = args[1];
                VariablesGenerales.pConnectSeguridad = args[2];
                VariablesGenerales.pConnectVB6 = args[3];
                VariablesGenerales.pCodEmpresa = args[4];
                VariablesGenerales.pUsuario = args[5];
                VariablesGenerales.pRuta = args[6];
                VariablesGenerales.pCodPerfil = args[7];

                switch (args[0].ToString())
                {

                    case "PROTECCIONPDF":
                        EjecutarProteccionPdf(args[8]);
                        break;
                    case "EVALUASEGURIDAD":
                        EvaluaSeguridadPDF(args[8]).GetAwaiter().GetResult();
                        break;
                    case "UPC":
                        Application.Run(new FrmCargaUPCdesdeExcel());
                        break;
                    default:
                        break;
                }
            }else
            {
                Application.Run(new MenuPrincipal());
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1); // Código de salida con error
            }
        }

        private static void EjecutarProteccionPdf(string archivoDestino)
        {
            try
            {
                GeneradorFichaTecnica ft = new GeneradorFichaTecnica();

                string OwnerPassword = PDFProtectionPdfSharp.PdfProtectionService.GenerateSecurePassword();

                // Ejecutar de forma síncrona usando GetAwaiter().GetResult()
                ft.ProtegerPdf(archivoDestino, OwnerPassword).GetAwaiter().GetResult();

                Console.WriteLine("Protección PDF completada exitosamente");
                Console.WriteLine(OwnerPassword);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en protección PDF: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private static async Task EvaluaSeguridadPDF(string archivoDestino)
        {
            try
            {
                // 1. Configurar el Factory de Logs (Requiere NuGet Microsoft.Extensions.Logging.Console)
                using var loggerFactory = LoggerFactory.Create(builder => {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                });

                // 2. Instanciar el servicio pasando el logger
                using var pdfService = new PDFProtectionPdfSharp.PdfProtectionService(
                    loggerFactory.CreateLogger<PDFProtectionPdfSharp.PdfProtectionService>()
                );

                // 3. Obtener la información de seguridad
                var info = await pdfService.GetSecurityInfoAsync(archivoDestino);

                if (info.HasSecurity && info.CurrentPermissions != null)
                {
                    // Imprime True si tiene permiso, False si está bloqueado
                    Console.WriteLine($"PERMISO_IMPRESION: {info.CurrentPermissions.AllowPrint}");
                }
                else
                {
                    // Si no tiene seguridad, por defecto se puede imprimir
                    Console.WriteLine("PERMISO_IMPRESION: True");
                }
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en evaluación PDF: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}
