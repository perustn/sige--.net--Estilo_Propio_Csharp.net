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
    public partial class FrmBandejaControlFTPublicaciones_VerOPs : Form
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
        public int IdFichaTecnicaSel;
        public string EPSel;
        public string VersionSel;
        #endregion

        public FrmBandejaControlFTPublicaciones_VerOPs()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_VerOPs_Load(object sender, EventArgs e)
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
                strSQL += "\n" + "EXEC FT_Muestra_OPS_FT_SEC";
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
                            var withBlock2 = withBlock1.Columns["ID_Publicacion"];
                            withBlock2.Visible = false;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["cod_Estpro"];
                            withBlock2.Visible = false;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Version_Asociada"];
                            withBlock2.Visible = false;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["OP"];
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 70;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["EPV"];
                            withBlock2.Width =70;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Temporada"];
                            withBlock2.Width = 140;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Pdas_Solic"];
                            withBlock2.Caption = "PRENDAS SOLIC.";
                            withBlock2.Width = 90;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["ExFact"];
                            withBlock2.Width =90;
                            withBlock2.FormatString = "dd/MM/yyyy";
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["PO"];
                            withBlock2.Width =90;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["LOTE"];
                            withBlock2.Width = 70;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonBar1_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            try
            {
                DialogResult rpt;
                switch (e.Item.Key)
                {
                    case "VINCULAR":
                        FrmBandejaControlFTPublicaciones_VerOPsAdd oFrm = new FrmBandejaControlFTPublicaciones_VerOPsAdd();
                        oFrm.TxtEstiloPropio.Text = EPSel;
                        oFrm.TxtVersion.Text = VersionSel;
                        oFrm.IdFichaTecnicaSel = IdFichaTecnicaSel;
                        oFrm.IDPublicacion = Convert.ToInt32(TxtIdPublicacion.Text);
                        oFrm.CargaGrilla();
                        oFrm.ShowDialog();
                        if (oFrm.IsCambioOK == true)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "DESVINCULAR":
                        if (GrdLista.RecordCount == 0) { return; }
                        rpt = MessageBox.Show("¿Está seguro de Desvincular la OP seleccionada de la FT?", "Pregunta", MessageBoxButtons.YesNo);
                        if (DialogResult.Yes == rpt)
                        {
                            strSQL = string.Empty;
                            strSQL += "\n" + "EXEC FT_DesVincula_OP_Ficha_Tecnica";
                            strSQL += "\n" + string.Format(" @cod_ordpro        ='{0}'", GrdLista.GetValue(GrdLista.RootTable.Columns["OP"].Index));
                            strSQL += "\n" + string.Format(",@id_publicacion    = {0} ", TxtIdPublicacion.Text);
                            strSQL += "\n" + string.Format(",@cod_usuario       ='{0}'", VariablesGenerales.pUsuario);

                            if (oHp.EjecutarOperacion(strSQL) == true)
                            {
                                MessageBox.Show("Proceso realizado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargaGrilla();
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
    }
}
