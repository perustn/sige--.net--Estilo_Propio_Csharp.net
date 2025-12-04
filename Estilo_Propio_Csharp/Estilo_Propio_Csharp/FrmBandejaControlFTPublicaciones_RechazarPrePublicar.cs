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
    public partial class FrmBandejaControlFTPublicaciones_RechazarPrePublicar : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        GeneradorFichaTecnica GenFT = new GeneradorFichaTecnica();
        private Color colEmpresa;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public Boolean IsCambioOK;
        public int FilaSeleccionado;
        public string TipoCambioStatus;
        public string DesRptstatus;
        public int IDPublicacion;

        public string EstiloPropioSel;
        public string Versionsel;
        public int IdFichaTecnicaSel;
        public string CodigoClienteSel;

        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        #endregion

        public FrmBandejaControlFTPublicaciones_RechazarPrePublicar()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_RechazarPrePublicar_Load(object sender, EventArgs e)
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

                this.ActiveControl = TxtObservacion;
                CargarGrillaSecciones();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TxtCodMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivo(1);
            }
        }

        private void TxtDesMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivo(1);
            }
        }

        public void BuscarMotivo(short tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "Exec ft_muestra_motivos_rechazo_publicacion";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodMotivo.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Motivo"]);
                    TxtDesMotivo.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodMotivo.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Motivo"].Value);
                        TxtDesMotivo.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }
                BtnAceptar.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMantMotivoRechazo_Click(object sender, EventArgs e)
        {
            FrmMantMotivoRechazo oMant = new FrmMantMotivoRechazo();
            oMant.ShowDialog();
        }

        public void CargarGrillaSecciones()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC ft_muestra_Secciones_publicaciones_a_mostrar";

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                grxLista.RootTable.Columns.Clear();
                grxLista.DataSource = oDt;
                oHp.CheckLayoutGridEx(grxLista);

                {
                    var withBlock = grxLista;
                    withBlock.FilterMode = FilterMode.None;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;

                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 1;
                        withBlock1.RowHeight = 20;
                        withBlock1.PreviewRow = true;
                        withBlock1.PreviewRowLines = 15;
                        withBlock1.PreviewRowMember = "OBSERVACIONES";

                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }
                        {
                            var withBlock2 = withBlock1.Columns["ID_SECCION"];
                            withBlock2.Caption = "ID SECCION";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.EditType = EditType.NoEdit;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.Width = 90;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["DESCRIPCION_SECCION"];
                            withBlock2.Caption = "DESCRIPCION SECCION";
                            withBlock2.EditType = EditType.NoEdit;
                            withBlock2.Width = 220;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["NUMERO_OCURRENCIAS"];
                            withBlock2.Caption = "N° OCURRENCIAS";
                            withBlock2.Width = 140;
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
                Boolean RecorreGrilla = false;
                DialogResult rpt;

                foreach (Janus.Windows.GridEX.GridEXRow row in grxLista.GetRows())
                {
                    if (row.RowType == Janus.Windows.GridEX.RowType.Record)
                    {
                        if ((int)row.Cells["NUMERO_OCURRENCIAS"].Value != 0)
                        {
                            RecorreGrilla = true;
                        }
                    }
                }

                if (RecorreGrilla == false)
                {
                    MessageBox.Show("Favor de colocar al menos una ocurrencia para alguna Sección", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                strSQL = string.Empty;
                strSQL += Environment.NewLine + "DECLARE @FT_TablaSectores_Rechazo AS FT_TablaSectores_Rechazo";
                strSQL += Environment.NewLine;
                foreach (Janus.Windows.GridEX.GridEXRow row in grxLista.GetRows())
                {
                    if (row.RowType == Janus.Windows.GridEX.RowType.Record)
                    {
                        if ((int)row.Cells["NUMERO_OCURRENCIAS"].Value != 0)
                        {
                            strSQL += Environment.NewLine + string.Format(
                            "INSERT @FT_TablaSectores_Rechazo VALUES('{0}','{1}')",
                            row.Cells["ID_SECCION"].Value,
                            row.Cells["NUMERO_OCURRENCIAS"].Value
                            );
                        }                       
                    }
                }

                DesRptstatus = "Rechazar la Publicación de";
                strSQL += "\n" + "EXEC FT_Cambia_Status_a_PrePublicada_Rechazada";
                strSQL += "\n" + string.Format(" @Id_Publicacion            = {0} ", TxtIdPublicacion.Text);
                strSQL += "\n" + string.Format(",@cod_usuario               ='{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(",@ComentariosDePublicacion  ='{0}'", TxtObservacion.Text);
                strSQL += "\n" + string.Format(",@cod_estacion              ='{0}'", Environment.MachineName);
                strSQL += "\n" + string.Format(",@Cod_Motivo_Rechazo        ='{0}'", TxtCodMotivo.Text);
                strSQL += "\n" + string.Format(",@FT_TablaSectores_Rechazo = {0}", "@FT_TablaSectores_Rechazo");
                rpt = MessageBox.Show("¿Está seguro de " + DesRptstatus + " la FT seleccionada?", "Pregunta", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == rpt)
                {
                    if (oHp.EjecutarOperacion(strSQL) == true)
                    {
                        IsCambioOK = true;
                        MessageBox.Show("El Proceso Se Ha Generado Correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            IsCambioOK = false;
            DialogResult = DialogResult.Cancel;
        }
    }
}
