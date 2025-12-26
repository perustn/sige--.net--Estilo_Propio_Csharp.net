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
    public partial class FrmSolicitudesAutorizacionImpFT_Generar : Form
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
        private const string K_strColCheck = "FLG";
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        #endregion

        public FrmSolicitudesAutorizacionImpFT_Generar()
        {
            InitializeComponent();
        }

        private void FrmSolicitudesAutorizacionImpFT_Generar_Load(object sender, EventArgs e)
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

                OpcionFiltro = "4";
                grpNPublicadas.Visible = true;
                TxtNPublicadas.Text = "10";
                CargaGrilla();
                this.ActiveControl = TxtNPublicadas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void OpcionFiltroBusqueda(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            VisibilidadObj(false);
            switch (button.Tag.ToString())
            {
                case "1":
                    {
                        OpcionFiltro = "4";
                        grpNPublicadas.Visible = true;
                        TxtNPublicadas.Focus();
                        break;
                    }

                case "2":
                    {
                        OpcionFiltro = "2";
                        grpOP.Visible = true;
                        TxtOP.Focus();
                        break;
                    }

                case "3":
                    {
                        OpcionFiltro = "3";
                        grpEstiloVersion.Visible = true;
                        TxtEP.Focus();
                        break;
                    }
            }
        }

        private void VisibilidadObj(bool vbool)
        {
            TxtNPublicadas.Text = "";
            TxtOP.Text = "";
            TxtEP.Text = "";
            TxtVersion.Text = "";

            grpNPublicadas.Visible = vbool;
            grpOP.Visible = vbool;
            grpEstiloVersion.Visible = vbool;
        }

        private void TxtNPublicadas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnBuscar.Focus();
            }
        }

        private void TxtOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtOP.Text = oHp.RellenaDeCerosEnIzquierda(TxtOP.Text, 5);
                BtnBuscar.Focus();
            }
        }

        private void TxtEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtEP.Text = oHp.RellenaDeCerosEnIzquierda(TxtEP.Text, 5);
                TxtVersion.Focus();
            }
        }

        private void TxtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaVersionEP();
            }
        }

        public void BuscaVersionEP()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "select Cod_Version as Codigo , Des_Version as Descripcion from ES_EstProVer where Cod_EstPro = '" + TxtEP.Text + "'  and Cod_Version LIKE '%" + TxtVersion.Text + "%'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtVersion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Codigo"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtVersion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Codigo"].Value);
                    }
                }
                BtnBuscar.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargaGrilla();
        }

        public void CargaGrilla()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_AI_Solicitud_FT_Disponibles";
                strSQL += "\n" + string.Format("  @opcion           = '{0}'", OpcionFiltro);
                strSQL += "\n" + string.Format(", @cod_ordpro		= '{0}'", TxtOP.Text);
                strSQL += "\n" + string.Format(", @cod_estpro	    = '{0}'", TxtEP.Text);
                strSQL += "\n" + string.Format(", @cod_version  	= '{0}'", TxtVersion.Text);
                strSQL += "\n" + string.Format(", @cod_usuario		= '{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(", @nro_publicaciones= '{0}'", TxtNPublicadas.Text);

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                grxLista.RootTable.Columns.Clear();
                grxLista.DataSource = oDt;

                oHp.CheckLayoutGridEx(grxLista);

                {
                    var withBlock = grxLista;
                    withBlock.FilterMode = FilterMode.Automatic;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;

                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 2;
                        withBlock1.RowHeight = 30;
                        withBlock1.PreviewRow = true;
                        withBlock1.PreviewRowLines = 15;
                        //////////////withBlock1.PreviewRowMember = "OBSERVACIONES";

                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }

                        {
                            var withBlock2 = grxLista.RootTable.Columns;
                            withBlock2.Add(new GridEXColumn() { 
                                Key = K_strColCheck, 
                                ColumnType = ColumnType.CheckBox,
                                EditType = EditType.CheckBox,
                                ActAsSelector = true,
                                Width = 25,
                                Caption = string.Empty });
                        }

                        {
                            var withBlock2 = withBlock1.Columns["OP"];
                            withBlock2.Width = 70;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                    }
                }
                grxLista.RootTable.Columns[K_strColCheck].Position = 0;
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
                if (grxLista.GetCheckedRows().Length != 0)
                {
                    int contador = 0;
                    int contBusqueda = 0;
                    var idsUnicos = grxLista.GetCheckedRows()
                        .Select(row => row.Cells["Id_Publicacion"].Value.ToString())
                        .Distinct()
                        .ToList();

                    foreach (string idPublicacion in idsUnicos)
                    {
                        strSQL = string.Empty;
                        strSQL += "\n" + "EXEC UP_MAN_FT_AI_Solicitud";
                        strSQL += "\n" + string.Format(" @Opcion            ='{0}'", "I");
                        strSQL += "\n" + string.Format(",@Id_Solicitud		= {0} ", 0);
                        strSQL += "\n" + string.Format(",@Id_Publicacion	= {0} ", idPublicacion);
                        strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                        strSQL += "\n" + string.Format(",@cod_estacion		='{0}'", Environment.MachineName);
                        if (oHp.EjecutarOperacion(strSQL) == false)
                        {
                            contador += 1;
                        }
                        else { contBusqueda += 1; }
                    }
                    if (contador == 0 && idsUnicos.Count > 0)
                    {
                        MessageBox.Show("Proceso realizado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                    if (contBusqueda > 0)
                    {
                        CargaGrilla();
                    }
                }
                else
                {
                    MessageBox.Show("Debe Seleccionar Al Menos un Registro", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }       
    }
}
