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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Puedes realizar acciones basadas en los argumentos
            if (args.Length > 0)
            {
                VariablesGenerales.pConnect = args[0];
                VariablesGenerales.pConnectSeguridad = args[1];
                VariablesGenerales.pConnectVB6 = args[2];
                VariablesGenerales.pCodEmpresa = args[3];
                VariablesGenerales.pUsuario = args[4];
                VariablesGenerales.pRuta = args[5];
                VariablesGenerales.pCodPerfil = args[6];

                Application.Run(new FrmCargaUPCdesdeExcel());
            }else
            {
                Application.Run(new MenuPrincipal());
            }

           
        }
    }
}
