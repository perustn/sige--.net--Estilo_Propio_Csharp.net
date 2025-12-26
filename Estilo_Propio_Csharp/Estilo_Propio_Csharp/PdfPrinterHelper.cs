using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    class PdfPrinterHelper
    {
        public event Action OnImpresionFinalizada;

        public void PrintWithUserConfiguration(string pdfPath, int idSolicitud)
        {
            if (!System.IO.File.Exists(pdfPath)) return;

            using (PrintDialog printDialog = new PrintDialog())
            {
                // 1. DESACTIVAR el diálogo extendido para eliminar la vista previa vacía
                printDialog.UseEXDialog = false;

                // 2. Permitir que el usuario elija impresora y copias
                printDialog.AllowSelection = false;
                printDialog.AllowSomePages = false;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    string nombreImpresora = printDialog.PrinterSettings.PrinterName;

                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = pdfPath,
                            Verb = "PrintTo",
                            Arguments = $"\"{nombreImpresora}\"",
                            CreateNoWindow = true,
                            WindowStyle = ProcessWindowStyle.Hidden,
                            UseShellExecute = true
                        };

                        Process.Start(startInfo);
                        EjecutarSPAutorizacion(idSolicitud);
                        OnImpresionFinalizada?.Invoke();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al imprimir: " + ex.Message);
                    }
                }
            }
        }

        private void EjecutarSPAutorizacion(int idSolicitud)
        {
            using (SqlConnection conn = new SqlConnection(VariablesGenerales.pConnect))
            {
                using (SqlCommand cmd = new SqlCommand("UP_MAN_FT_AI_Solicitud_Estado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Opcion", "S");
                    cmd.Parameters.AddWithValue("@ID_Solicitud", idSolicitud);
                    cmd.Parameters.AddWithValue("@Cod_Estado", "I");
                    cmd.Parameters.AddWithValue("@Cod_Usuario", VariablesGenerales.pUsuario);
                    cmd.Parameters.AddWithValue("@Cod_Estacion", Environment.MachineName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
