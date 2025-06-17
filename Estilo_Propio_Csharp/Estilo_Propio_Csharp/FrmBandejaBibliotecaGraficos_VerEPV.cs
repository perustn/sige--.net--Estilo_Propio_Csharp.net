using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Janus.Windows.GridEX;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBandejaBibliotecaGraficos_VerEPV : Form
    {

        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        private Color colEmpresa;
        private string OpcionFiltro;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        private Boolean bolSW_LoadGUI = true;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        #endregion

        public FrmBandejaBibliotecaGraficos_VerEPV()
        {
            InitializeComponent();
        }

        private void FrmBandejaBibliotecaGraficos_VerEPV_Load(object sender, EventArgs e)
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
            string codCliente = "";

            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC UP_MAN_Es_Proceso_Imagen_Ver_EPV_Donde_SeUsa";
                strSQL += "\n" + string.Format("@Id_Proceso_Imagen    ={0}", TxtIdProceso.Text);

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
                            var withBlock2 = withBlock1.Columns["Cod_estpro"];
                            withBlock2.Caption = "EP";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 80;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_version"];
                            withBlock2.Caption = "VERSION";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 70;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Pri_Fec_Asociacion"];
                            withBlock2.Caption = "PRIMER ASOCIACION";
                            withBlock2.Width = 120;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Ult_Fec_Asociacion"];
                            withBlock2.Caption = "ULTIMA ASOCIACION ";
                            withBlock2.Width = 150;
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
