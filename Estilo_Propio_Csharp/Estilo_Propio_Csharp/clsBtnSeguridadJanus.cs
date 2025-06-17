using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.Data;

namespace Estilo_Propio_Csharp
{
    class clsBtnSeguridadJanus
    {
        public string strSQL = string.Empty;
        ClsHelper oHp = new ClsHelper();

        public void GetBotonesJanus(string xPerfil, string xEmpresa, string xFormulario, Janus.Windows.ButtonBar.ButtonBar NombreBarra, string KeyNames)
        {
            try
            {
                if (string.IsNullOrEmpty(KeyNames))
                {
                    // Consultar en la BD los botones que tiene acceso el perfil
                    short b;
                    strSQL = "SEG_MUESTRA_ACCESOS_FORMULARIOS_FUNCION '" + xPerfil + "','" + xEmpresa + "','" + xFormulario + "'";
                    DataTable DtBotones = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnectSeguridad);
                    for (b = 0; b <= DtBotones.Rows.Count - 1; b++)
                        KeyNames += DtBotones.Rows[b][0] + "/";
                }

                // Habilitar los Botones
                HabilitaBotonesBar(NombreBarra, KeyNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void HabilitaBotonesBar(Janus.Windows.ButtonBar.ButtonBar NombreBarra, string KeyNames)
        {
            try
            {
                short y, x, i;
                // Obtener la cantidad de grupos de la barra
                int totalgrupos = NombreBarra.Groups.Count;
                for (y = 0; y <= totalgrupos - 1; y++)
                {
                    // Inhabilitar todos los botones de la barra de cada grupo
                    int totalbotones = NombreBarra.Groups[y].Items.Count;
                    for (x = 0; x <= totalbotones - 1; x++)
                        NombreBarra.Groups[y].Items[x].Enabled = false;
                }

                // Habilitar los botones (KeyNames)
                //object Matriz;
                int Mayor;

                string[] Matriz = KeyNames.Split('/');
                //split[KeyNames, "/"];
                Mayor = Matriz.GetUpperBound(0);
                //UBound[Matriz];
                for (y = 0; y <= totalgrupos - 1; y++)
                {
                    // Habilitar todos los botones que concidan con los nombres de la matriz
                    int totalbotones = NombreBarra.Groups[y].Items.Count;
                    for (x = 0; x <= totalbotones - 1; x++)
                    {
                        for (i = 0; i <= Mayor; i++)
                        {
                            if (NombreBarra.Groups[y].Items[x].Key == Matriz[i].ToString())
                            {
                                NombreBarra.Groups[y].Items[Matriz[i].ToString()].Enabled = true;
                                break;
                            }
                            else if (NombreBarra.Groups[y].Items[x].Key == "SALIR")
                            {
                                // Salir siempre estará activo
                                NombreBarra.Groups[y].Items[x].Enabled = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
