using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;

namespace Estilo_Propio_Csharp
{
    public partial class FrmMantMotivoRechazo : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        private Color colEmpresa;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public oParent oParent;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        private string OpcionMantenimiento;

        public string TipoCaracteristicaPreview;

        #endregion

        public FrmMantMotivoRechazo()
        {
            InitializeComponent();
        }

        private void FrmMantMotivoRechazo_Load(object sender, EventArgs e)
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
            CargarGrilla();
        }

        public void CargarGrilla()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_UP_MAN_Motivos_Rechazo_Publicacion";
                strSQL += "\n" + string.Format(" @opcion        = '{0}'", "V");

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                gridEX1.RootTable.Columns.Clear();
                gridEX1.DataSource = oDt;
                oHp.CheckLayoutGridEx(gridEX1);

                {
                    var withBlock = gridEX1;
                    withBlock.FilterMode = FilterMode.Automatic;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;

                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 2;
                        withBlock1.RowHeight = 30;
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
                            var withBlock2 = withBlock1.Columns["Cod_Motivo_Rechazo"];
                            withBlock2.Caption = "CODIGO";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 60;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Descripcion_Motivo_Rechazo"];
                            withBlock2.Caption = "DESCRIPCION";
                            withBlock2.Width = 220;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["cod_usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 70;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Fec_Actuaalizacion"];
                            withBlock2.Caption = "FECHA ACTUALIZACION";
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

        private void BarraOpciones_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            try
            {
                int vRow;
                int cRow;
                switch (e.Item.Key)
                {
                    case "ADICIONAR":
                        {
                            OpcionMantenimiento = "I";
                            LIMPIA_DATOS();
                            HABILITA_DATOS();
                            this.TxtDesCaracteristica.Focus();
                            {
                                var withBlock = BarraOpciones.Groups[0];
                                withBlock.Items["MODIFICAR"].Enabled = false;
                                withBlock.Items["ELIMINAR"].Enabled = false;
                                withBlock.Items["GRABAR"].Enabled = true;
                                withBlock.Items["DESHACER"].Enabled = true;
                            }
                            break;
                        }

                    case "MODIFICAR":
                        {
                            OpcionMantenimiento = "U";
                            HABILITA_DATOS();
                            this.TxtDesCaracteristica.Focus();
                            {
                                var withBlock1 = BarraOpciones.Groups[0];
                                withBlock1.Items["ADICIONAR"].Enabled = false;
                                withBlock1.Items["ELIMINAR"].Enabled = false;
                                withBlock1.Items["GRABAR"].Enabled = true;
                                withBlock1.Items["DESHACER"].Enabled = true;
                            }
                            break;
                        }

                    case "ELIMINAR":
                        {
                            var Mensaje = MessageBox.Show("¿Esta usted seguro de eliminar el registro seleccionado?", "Eliminar Codición de pago", MessageBoxButtons.YesNo);
                            if (Mensaje == DialogResult.Yes)
                            {
                                OpcionMantenimiento = "D";
                                SALVAR_DATOS();
                                CargarGrilla();
                                OpcionMantenimiento = "";
                            }
                            break;
                        }

                    case "GRABAR":
                        {
                            vRow = gridEX1.Row;
                            cRow = gridEX1.RowCount;
                            SALVAR_DATOS();
                            CargarGrilla();
                            INHABILITA_DATOS();
                            {
                                var withBlock2 = BarraOpciones.Groups[0];
                                withBlock2.Items["ADICIONAR"].Enabled = true;
                                withBlock2.Items["MODIFICAR"].Enabled = true;
                                withBlock2.Items["ELIMINAR"].Enabled = true;
                                withBlock2.Items["GRABAR"].Enabled = false;
                                withBlock2.Items["DESHACER"].Enabled = false;
                            }

                            if (OpcionMantenimiento == "U")
                            {
                                gridEX1.Row = vRow;
                            }
                            else
                            {
                                // GrdMntMedidas.Row = cRow
                            }
                            OpcionMantenimiento = "";
                            break;
                        }

                    case "DESHACER":
                        {
                            LIMPIA_DATOS();
                            CargarGrilla();
                            INHABILITA_DATOS();
                            {
                                var withBlock3 = BarraOpciones.Groups[0];
                                withBlock3.Items["ADICIONAR"].Enabled = true;
                                withBlock3.Items["MODIFICAR"].Enabled = true;
                                withBlock3.Items["ELIMINAR"].Enabled = true;
                                withBlock3.Items["GRABAR"].Enabled = false;
                                withBlock3.Items["DESHACER"].Enabled = false;
                            }
                            OpcionMantenimiento = "";
                            break;
                        }

                    case "SALIR":
                        {
                            this.Close();
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void HABILITA_DATOS()
        {
            TxtDesCaracteristica.Enabled = true;
        }

        public void INHABILITA_DATOS()
        {
            TxtCodCaracteristica.Enabled = false;
            TxtDesCaracteristica.Enabled = false;
        }

        public void LIMPIA_DATOS()
        {
            TxtCodCaracteristica.Text = string.Empty;
            TxtDesCaracteristica.Text = string.Empty;
        }

        object IIf(bool expression, object truePart, object falsePart) { return expression ? truePart : falsePart; }

        private void SALVAR_DATOS()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_UP_MAN_Motivos_Rechazo_Publicacion";
                strSQL += "\n" + string.Format(" @opcion                ='{0}'", OpcionMantenimiento);
                strSQL += "\n" + string.Format(",@cod_motivo_rechazo    ='{0}'", TxtCodCaracteristica.Text);
                strSQL += "\n" + string.Format(",@descripcion           ='{0}'", TxtDesCaracteristica.Text);
                strSQL += "\n" + string.Format(",@cod_usuario           ='{0}'", VariablesGenerales.pUsuario);

                oHp.EjecutarOperacion(strSQL);
                CargarGrilla();
                LIMPIA_DATOS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridEX1_SelectionChanged(object sender, EventArgs e)
        {
            Carga_Datos();
        }

        public void Carga_Datos()
        {
            if (gridEX1.RowCount > 0)
            {
                DataRow odr;
                odr = oHp.ObtenerDr_DeGridEx(gridEX1);
                if (odr != null)
                {
                    this.TxtCodCaracteristica.Text = odr["Cod_Motivo_Rechazo"].ToString();
                    this.TxtDesCaracteristica.Text = odr["Descripcion_Motivo_Rechazo"].ToString();
                }
            }
            else
            {
                LIMPIA_DATOS();
            }
        }
    }
}
