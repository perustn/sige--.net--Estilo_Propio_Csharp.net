using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    public partial class FrmCargaUPCdesdeExcel_Main : ProyectoBase.frmBase
    {
        public FrmCargaUPCdesdeExcel_Main()
        {
            InitializeComponent();
        }

        private void btnVer_Datos_Extraidos_Click(object sender, EventArgs e)
        {
            string nameProy = "Estilo_Propio_Csharp";
            string tipo = ".exe";
            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            string sDllName = sPath + @"\" + nameProy + tipo;
            string executablePath = sDllName;
            string arguments = $"\"{VariablesGenerales.pConnect}\"";
            try
            {
                System.Diagnostics.Process.Start(executablePath, arguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al iniciar el proceso: " + ex.Message);
            }
        }
    }
}
