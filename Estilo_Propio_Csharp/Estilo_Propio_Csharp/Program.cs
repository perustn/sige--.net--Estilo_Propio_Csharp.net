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
                Application.Run(new FrmCargaUPCdesdeExcel());
            }else
            {
                Application.Run(new MenuPrincipal());
            }

           
        }
    }
}
