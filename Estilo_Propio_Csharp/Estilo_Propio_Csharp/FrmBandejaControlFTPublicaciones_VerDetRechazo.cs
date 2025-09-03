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
    public partial class FrmBandejaControlFTPublicaciones_VerDetRechazo : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        private Color colEmpresa;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public int FilaSeleccionado;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        public DataTable oDtEstructura;
        public DataRow oDr;
        #endregion

        public FrmBandejaControlFTPublicaciones_VerDetRechazo()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_VerDetRechazo_Load(object sender, EventArgs e)
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

                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_Muestra_Errores_por_Sector";
                strSQL += "\n" + string.Format(" @Id_Publicacion    = {0} ", TxtIdPublicacion.Text);
                strSQL += "\n" + string.Format(",@opcion            ='{0}'", "1");
                oDtEstructura = oHp.EjecutaOperacionRetornaDatos2(strSQL, VariablesGenerales.pConnect);
                if (oDtEstructura != null)
                {
                    if (oDtEstructura.Rows.Count > 0)
                    {
                        oDr = oDtEstructura.Rows[0];
                        TxtCodMotivo.Text = oDr["Cod_Motivo_Rechazo"].ToString();
                        TxtDesMotivo.Text = oDr["Descripcion_Motivo_Rechazo"].ToString();
                    }
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
                strSQL += "\n" + "EXEC FT_Muestra_Errores_por_Sector";
                strSQL += "\n" + string.Format(" @Id_Publicacion    = {0} ", TxtIdPublicacion.Text);
                strSQL += "\n" + string.Format(",@opcion            ='{0}'", "2");

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                grxLista.RootTable.Columns.Clear();
                grxLista.DataSource = oDt;
                oHp.CheckLayoutGridEx(grxLista);

                {
                    var withBlock = grxLista;
                    withBlock.FilterMode = FilterMode.None;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;
                    withBlock.AllowEdit = InheritableBoolean.False;

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
                            var withBlock2 = withBlock1.Columns["DESCRIPCION_SECCION"];
                            withBlock2.Caption = "DESCRIPCION SECCION";
                            withBlock2.EditType = EditType.NoEdit;
                            withBlock2.Width = 170;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["NUMERO_OCURRENCIAS"];
                            withBlock2.Caption = "N° OCURR.";
                            withBlock2.Width = 80;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 90;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Fec_Actualizacion"];
                            withBlock2.Caption = "FECHA ACTUALIZACIÓN";
                            withBlock2.Width = 120;
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.FormatString = "dd/MM/yyyy";
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
