using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Janus.Windows.GridEX;

namespace Estilo_Propio_Csharp
{
    public partial class FrmMaestroAprobadores : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        private Color colEmpresa;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public oParent oParent;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        private string OpcionMantenimiento;

        public int IDUsuario;

        #endregion

        public FrmMaestroAprobadores()
        {
            InitializeComponent();
        }

        private void FrmMaestroAprobadores_Load(object sender, EventArgs e)
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
                strSQL += "\n" + "EXEC UP_MAN_FT_AI_Usuarios";
                strSQL += "\n" + string.Format(" @opcion            = '{0}'", "V");
                strSQL += "\n" + string.Format(",@Cod_Usuario_Mant  = '{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(",@Cod_Estacion      = '{0}'", Environment.MachineName);

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
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
                            var withBlock2 = withBlock1.Columns["Id"];
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 50;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 80;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Nom_Usuario"];
                            withBlock2.Caption = "NOMBRE USUARIO";
                            withBlock2.Width = 165;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Fec_Creacion"];
                            withBlock2.Caption = "FECHA CREACION";
                            withBlock2.FormatString = "dd/MM/yyyy";
                            withBlock2.Width = 90;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario_Creacion"];
                            withBlock2.Caption = "USUARIO CREACION";
                            withBlock2.Width = 90;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Estacion_Creacion"];
                            withBlock2.Caption = "PC CREACION";
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
                            HABILITA_DATOS(true);
                            {
                                var withBlock = BarraOpciones.Groups[0];
                                withBlock.Items["ELIMINAR"].Enabled = false;
                                withBlock.Items["GRABAR"].Enabled = true;
                                withBlock.Items["DESHACER"].Enabled = true;
                            }
                            TxtCodUsuario.Focus();
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
                            HABILITA_DATOS(false);
                            {
                                var withBlock2 = BarraOpciones.Groups[0];
                                withBlock2.Items["ADICIONAR"].Enabled = true;
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
                            HABILITA_DATOS(false);
                            {
                                var withBlock3 = BarraOpciones.Groups[0];
                                withBlock3.Items["ADICIONAR"].Enabled = true;
                                withBlock3.Items["ELIMINAR"].Enabled = true;
                                withBlock3.Items["GRABAR"].Enabled = false;
                                withBlock3.Items["DESHACER"].Enabled = false;
                            }
                            OpcionMantenimiento = "";
                            break;
                        }

                    case "EXPORTAR":
                        {
                            if (gridEX1.RecordCount == 0) { return; }
                            string Ruta_Archivo;

                            Ruta_Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                            Random numAleatorio = new Random(System.Convert.ToInt32(DateTime.Now.Ticks & int.MaxValue));
                            string RutaUniArchivo = string.Format(Ruta_Archivo + @"\Export_{0}.xls", System.Convert.ToString(numAleatorio.Next()));
                            System.IO.FileStream fs = new System.IO.FileStream(RutaUniArchivo, System.IO.FileMode.Create);

                            gridEXExporter1.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows;
                            gridEXExporter1.GridEX = gridEX1;
                            gridEXExporter1.Export(fs);
                            fs.Close();
                            Process.Start(RutaUniArchivo);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void HABILITA_DATOS(bool vBool)
        {
            TxtCodUsuario.Enabled = vBool;
            TxtDesUsuario.Enabled = vBool;
        }

        public void LIMPIA_DATOS()
        {
            TxtCodUsuario.Text = string.Empty;
            TxtDesUsuario.Text = string.Empty;
        }


        private void TxtCodUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaUsuario(1);
            }
        }

        private void TxtDesUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaUsuario(2);
            }
        }

        public void BuscaUsuario(int tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                switch (tipo)
                {
                    case 1:
                        oTipo.sQuery = "select cod_usuario, nom_usuario from SEGURIDAD..seg_usuarios where cod_usuario LIKE '%" + TxtCodUsuario.Text + "%'";
                        break;
                    case 2:
                        oTipo.sQuery = "select cod_usuario, nom_usuario from SEGURIDAD..seg_usuarios where nom_usuario LIKE '%" + TxtDesUsuario.Text + "%'";
                        break;
                }

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodUsuario.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_usuario"]);
                    TxtDesUsuario.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["nom_usuario"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodUsuario.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["cod_usuario"].Value);
                        TxtDesUsuario.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["nom_usuario"].Value);
                    }
                }
                BarraOpciones.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
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
                    this.TxtCodUsuario.Text = odr["Cod_Usuario"].ToString();
                    this.TxtDesUsuario.Text = odr["Nom_Usuario"].ToString();
                    IDUsuario = (int)odr["Id"];
                }
            }
            else
            {
                LIMPIA_DATOS();
            }
        }

        private void SALVAR_DATOS()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC UP_MAN_FT_AI_Usuarios";
                strSQL += "\n" + string.Format(" @opcion            ='{0}'", OpcionMantenimiento);
                strSQL += "\n" + string.Format(",@Id                ='{0}'", IDUsuario);
                strSQL += "\n" + string.Format(",@Cod_Usuario       ='{0}'", TxtCodUsuario.Text);
                strSQL += "\n" + string.Format(",@Cod_Usuario_Mant  ='{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(",@Cod_Estacion      ='{0}'", Environment.MachineName);

                oHp.EjecutarOperacion(strSQL);
                CargarGrilla();
                LIMPIA_DATOS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
