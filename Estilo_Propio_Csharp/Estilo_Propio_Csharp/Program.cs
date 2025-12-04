using System;
using System.Collections.Generic;
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
    }
}
