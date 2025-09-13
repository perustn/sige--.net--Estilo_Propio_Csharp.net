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
    public partial class FrmEstiloVersionMadreDependencia_Adicionar : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        private Color colEmpresa;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public int FilaSeleccionado;

        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        #endregion

        public FrmEstiloVersionMadreDependencia_Adicionar()
        {
            InitializeComponent();
        }

        private void FrmEstiloVersionMadreDependencia_Adicionar_Load(object sender, EventArgs e)
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
                    panel1.BackColor = colEmpresa;
                    panel2.BackColor = colEmpresa;
                }

                this.ActiveControl = TxtEP;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TxtEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (TxtEP.Text.Length > 0)
                {
                    TxtEP.Text = oHp.RellenaDeCerosEnIzquierda(TxtEP.Text, 5);
                }
                BuscaEstiloPropio();
            }
        }

        private void TxtEPDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaEstiloPropio();
            }
        }

        public void BuscaEstiloPropio()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "select cod_estpro as Codigo , des_estpro as Descripcion from es_estpro where cod_estpro LIKE '%" + TxtEP.Text + "%'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtEP.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Codigo"]);
                    TxtEPDescripcion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtEP.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Codigo"].Value);
                        TxtEPDescripcion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }
                TxtVersion.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaVersion();
            }
        }

        private void TxtVersionDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaVersion();
            }
        }

        public void BuscaVersion()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "exec es_ayuda_estprover_dependientes '" + TxtEP.Text + "'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtVersion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Version"]);
                    TxtVersionDescripcion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Des_Version"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtVersion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Version"].Value);
                        TxtVersionDescripcion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Des_Version"].Value);
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

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                strSQL += "\n" + "EXEC ES_CREA_Es_EstProVer_Madres";
                strSQL += "\n" + string.Format(" @COD_ESTPRO        ='{0}'", TxtEP.Text);
                strSQL += "\n" + string.Format(",@COD_VERSION       ='{0}'", TxtVersion.Text);
                strSQL += "\n" + string.Format(",@COD_USUARIO       ='{0}'", VariablesGenerales.pUsuario);
                if (oHp.EjecutarOperacion(strSQL) == true)
                {
                    MessageBox.Show("El Proceso Se Ha Generado Correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }        
    }
}
