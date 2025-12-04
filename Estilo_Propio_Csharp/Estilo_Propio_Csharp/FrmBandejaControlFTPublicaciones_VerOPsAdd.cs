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
    public partial class FrmBandejaControlFTPublicaciones_VerOPsAdd : Form
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
        public int IDPublicacion;
        public Boolean IsCambioOK;
        #endregion

        public FrmBandejaControlFTPublicaciones_VerOPsAdd()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_VerOPsAdd_Load(object sender, EventArgs e)
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
                    panel2.BackColor = colEmpresa;
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
                strSQL += "\n" + "EXEC FT_busca_Ops_EPV_Sin_FT_Vinculada";
                strSQL += "\n" + string.Format(" @cod_estpro        ='{0}'", TxtEstiloPropio.Text);
                strSQL += "\n" + string.Format(",@cod_version       ='{0}'", TxtVersion.Text);
                strSQL += "\n" + string.Format(",@id_fichatecnica   ='{0}'", IdFichaTecnicaSel);

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
                            var withBlock2 = withBlock1.Columns["Flg"];
                            withBlock2.Caption = string.Empty;
                            withBlock2.Width = 35;
                            withBlock2.ColumnType = ColumnType.CheckBox;
                            withBlock2.EditType = EditType.CheckBox;
                            withBlock2.ActAsSelector = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["OP"];
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 70;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Ex_Fact"];
                            withBlock2.Caption = "FECHA EX FACT";
                            withBlock2.Width = 90;
                            withBlock2.FormatString = "dd/MM/yyyy";
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Pdas"];
                            withBlock2.Width = 80;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["PO"];
                            withBlock2.Width = 90;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Temporada"];
                            withBlock2.Width = 120;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rpt;
                if (GrdLista.RecordCount == 0) { return; }
                if (GrdLista.GetCheckedRows().Length == 0)
                {
                    MessageBox.Show("Es necesario seleccione un registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                rpt = MessageBox.Show("¿Está seguro de Vincular las Ops seleccionadas?", "Pregunta", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == rpt)
                {
                    int contador = 0;
                    int contBusqueda = 0;
                    foreach (GridEXRow item in GrdLista.GetCheckedRows())
                    {
                        strSQL = string.Empty;
                        strSQL += "\n" + "EXEC FT_Vincula_OP_Ficha_Tecnica";
                        strSQL += "\n" + string.Format(" @cod_ordpro        ='{0}'", item.Cells["OP"].Value);
                        strSQL += "\n" + string.Format(",@id_publicacion    = {0} ", IDPublicacion);
                        strSQL += "\n" + string.Format(",@cod_usuario       ='{0}'", VariablesGenerales.pUsuario);

                        if (oHp.EjecutarOperacion(strSQL) == false)
                        {
                            contador += 1;
                        }
                        else { contBusqueda += 1; }
                    }
                    if (contador == 0)
                    {
                        MessageBox.Show("Proceso realizado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IsCambioOK = true;
                        DialogResult = DialogResult.Cancel;
                    }
                    if (contBusqueda > 0)
                    {
                        CargaGrilla();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            IsCambioOK = false;
            DialogResult = DialogResult.Cancel;
        }
    }
}
