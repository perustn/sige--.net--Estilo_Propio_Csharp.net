using Janus.Windows.GridEX;
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
    public partial class FrmBandejaControlFTPublicaciones_VerModificaciones : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        private Color colEmpresa;
        private string OpcionFiltro;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        private Boolean bolSW_LoadGUI = true;
        public int FilaSeleccionado;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        #endregion

        public FrmBandejaControlFTPublicaciones_VerModificaciones()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_VerModificaciones_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable oDt = oHp.DevuelveDatos(string.Format("SELECT * FROM SEG_Empresas where cod_empresa = '{0}'", VariablesGenerales.pCodEmpresa), VariablesGenerales.pConnectSeguridad);
                if (!(oDt == null))
                {
                    int ColorFondo_R = Convert.ToInt32(oDt.Rows[0]["ColorFondo_R"]);
                    int ColorFondo_G = Convert.ToInt32(oDt.Rows[0]["ColorFondo_G"]);
                    int ColorFondo_B = Convert.ToInt32(oDt.Rows[0]["ColorFondo_B"]);
                    colEmpresa = Color.FromArgb(ColorFondo_R, ColorFondo_G, ColorFondo_B);
                    Panel1.BackColor = colEmpresa;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CargaGrilla()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_Muestra_Cambios_Publicaciones";
                strSQL += "\n" + string.Format("@Id_Publicacion    ={0}", TxtIdPublicacion.Text);

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                GrdLista.RootTable.Columns.Clear();
                GrdLista.DataSource = oDt;
                oHp.CheckLayoutGridEx(GrdLista);

                {
                    var withBlock = GrdLista;
                    withBlock.FilterMode = FilterMode.Automatic;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;

                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 2;
                        withBlock1.RowHeight = 30;
                        withBlock1.PreviewRow = true;
                        withBlock1.PreviewRowLines = 15;

                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Secuencia"];
                            withBlock2.Caption = "SEC";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 50;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cambio"];
                            withBlock2.Width = 120;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 90;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Actualizacion"];
                            withBlock2.Caption = "FECHA ACTUALIZACION";
                            withBlock2.Width = 120;
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.FormatString = "dd/MM/yyyy";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Status"];
                            withBlock2.Width = 90;
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
