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
    public partial class FrmBandejaControlFTPublicaciones_Publicar : Form
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

        public FrmBandejaControlFTPublicaciones_Publicar()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_Publicar_Load(object sender, EventArgs e)
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
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rpt;
                switch (TipoCambioStatus)
                {
                    case "PUBLICAR":
                        DesRptstatus = "Publicar";

                        strSQL = string.Empty;
                        strSQL += "\n" + "EXEC FT_Cambia_Status_a_Publicado";
                        strSQL += "\n" + string.Format(" @Id_Publicacion            = {0} ", TxtIdPublicacion.Text);
                        strSQL += "\n" + string.Format(",@cod_usuario               ='{0}'", VariablesGenerales.pUsuario);
                        strSQL += "\n" + string.Format(",@ComentariosDePublicacion  ='{0}'", TxtObservacion.Text);
                        strSQL += "\n" + string.Format(",@cod_estacion              ='{0}'", Environment.MachineName);
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
                        break;

                    case "MODPREPUBLICAR":
                        DesRptstatus = "Modificar por Pre Publicación ";

                        strSQL = string.Empty;
                        strSQL += "\n" + "EXEC FT_Cambia_Status_a_Modificada_por_PrePublicar";
                        strSQL += "\n" + string.Format(" @Id_Publicacion            = {0} ", TxtIdPublicacion.Text);
                        strSQL += "\n" + string.Format(",@cod_usuario               ='{0}'", VariablesGenerales.pUsuario);
                        strSQL += "\n" + string.Format(",@ComentariosDePublicacion  ='{0}'", TxtObservacion.Text);
                        strSQL += "\n" + string.Format(",@cod_estacion              ='{0}'", Environment.MachineName);
                        rpt = MessageBox.Show("¿Está seguro de cambiar Status a " + DesRptstatus + " de la FT seleccionada?", "Pregunta", MessageBoxButtons.YesNo);
                        if (DialogResult.Yes == rpt)
                        {
                            if (oHp.EjecutarOperacion(strSQL) == true)
                            {
                                IsCambioOK = true;
                                MessageBox.Show("El Proceso Se Ha Generado Correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                DialogResult = DialogResult.OK;
                            }
                        }
                        break;
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
