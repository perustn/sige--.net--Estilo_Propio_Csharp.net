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
            GetFormCargaUPCDesdeExcel();
        }

        private void FrmCargaUPCdesdeExcel_Main_Load(object sender, EventArgs e)
        {
            GetFormCargaUPCDesdeExcel();
        }

        private void GetFormCargaUPCDesdeExcel()
        {
            string nameProy = "Estilo_Propio_Csharp";
            string tipo = ".exe";
            string sPath = AppDomain.CurrentDomain.BaseDirectory;
            string sDllName = sPath + @"\" + nameProy + tipo;
            string executablePath = sDllName;

            // Define tus parámetros en una lista
            List<string> parameters = new List<string>
            {
                VariablesGenerales.pConnect,
                VariablesGenerales.pConnectSeguridad,
                VariablesGenerales.pConnectVB6,
                VariablesGenerales.pCodEmpresa,
                VariablesGenerales.pUsuario,
                VariablesGenerales.pRuta,
                VariablesGenerales.pCodPerfil
            };

            // Prepara los argumentos: Si un parámetro contiene espacios, lo encierra en comillas.
            // string.Join une todos los elementos con un espacio entre ellos.
            string allArguments = string.Join(" ", parameters.Select(p => p.Contains(" ") ? $"\"{p}\"" : p));

            try
            {
                System.Diagnostics.Process.Start(executablePath, allArguments);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al iniciar el proceso: " + ex.Message);
            }
        }
    }
}
