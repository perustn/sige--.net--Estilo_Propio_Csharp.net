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
    public partial class FrmBandejaBibliotecaGraficos_AddImagen : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        private Color colEmpresa;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";

        private String s_varbmp;
        public string CodRutaImagen;

        #endregion

        public FrmBandejaBibliotecaGraficos_AddImagen()
        {
            InitializeComponent();
        }

        private void FrmBandejaCotizacionFlashCost_AddImagen_Load(object sender, EventArgs e)
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

            //Scripting.FileSystemObject BuscaArchivo = new Scripting.FileSystemObject();
            //if (BuscaArchivo.FileExists(s_varbmp))
            //{
            //    Open_Imagen(CodRutaImagen);
            //}

            Open_Imagen(CodRutaImagen);
            //BuscaArchivo = null/* TODO Change to default(_) if this is not a reference type */;

            this.ActiveControl = pctIMG_CAB1;
        }

        public void Open_Imagen(string StrRuta)
        {
            try
            {
                Image img = Image.FromFile(StrRuta);
                Image imgCopy = new Bitmap(img, new Size(350, 350));
                img.Dispose();
                pctIMG_CAB1.Image = imgCopy;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
