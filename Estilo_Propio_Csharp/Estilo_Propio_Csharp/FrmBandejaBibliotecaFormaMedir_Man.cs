using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Estilo_Propio_Csharp
{
    public partial class FrmBandejaBibliotecaFormaMedir_Man : Form
    {

        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = ""; string TipoAdd5 = ""; string TipoAdd6 = ""; string TipoAdd7 = "";
        public Boolean IsCambioOK;
        string strSQL;
        ClsHelper oHp = new ClsHelper();
        public string sOpcion = "";
        public string sNro_Req = "";
        public new DataTable oDt;
        public DataRow oDr;
        public string rutaArchivo;
        private Color colEmpresa;
        public string nombreArchivo;
        public string result;
        private readonly string[] FormatImageValid = { ".jpg", ".jpeg", ".png", ".bmp" };

        public Boolean LlamadaDesdeModificarEP = false;
        public string RutaArchivoDesdeEPV;
        public string NombreArchivoDesdeEPV;
        public string ResultArchivoDesdeEPV;

        public FrmBandejaBibliotecaFormaMedir_Man()
        {
            InitializeComponent();
        }

        private void FrmBandejaBibliotecaFormaMedir_Man_Load(object sender, EventArgs e)
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

                if (LlamadaDesdeModificarEP == true)
                {
                    rutaArchivo = RutaArchivoDesdeEPV;
                    nombreArchivo = NombreArchivoDesdeEPV;
                    result = ResultArchivoDesdeEPV;
                    string strSQL = "SELECT RutaGrafico_Forma_Medir_Biblioteca FROM TG_CONTROL";
                    object resultado = oHp.DevuelveDato(strSQL, VariablesGenerales.pConnect);
                    string rutaBase = resultado != null ? resultado.ToString() : string.Empty;
                    string newRuta = rutaBase + "_" + nombreArchivo + ".jpg";
                    TxtRutaImagen.Text = newRuta;
                    this.ActiveControl = TxtDescripcion;
                }
                else
                {
                    this.ActiveControl = TxtCodCliente;
                }                
            }
        }

        private void TxtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaCliente(1);
            }
        }

        private void TxtDesCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaCliente(2);
            }
        }

        public void BuscaCliente(short Tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                if (Tipo == 1)
                    oTipo.sQuery = "SELECT Abr_Cliente AS CODIGO, Nom_Cliente AS DESCRIPCION, Cod_cliente FROM TG_CLIENTE WHERE Abr_Cliente like '%" + TxtCodCliente.Text + "%'";
                else
                    oTipo.sQuery = "SELECT Abr_Cliente AS CODIGO, Nom_Cliente AS DESCRIPCION, Cod_cliente FROM TG_CLIENTE WHERE Nom_Cliente LIKE '%" + TxtDesCliente.Text + "%'";
                oTipo.Cargar_Datos();
                oTipo.DGridLista.RootTable.Columns["Cod_cliente"].Visible = false;
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["CODIGO"]);
                    TxtDesCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["DESCRIPCION"]);
                    TxtCodCliente.Tag = oTipo.dtResultados.Rows[0]["Cod_cliente"];
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodCliente.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["CODIGO"].Value);
                        TxtDesCliente.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["DESCRIPCION"].Value);
                        TxtCodCliente.Tag = oTipo.RegistroSeleccionado.Cells["Cod_cliente"].Value;
                    }
                }
                TxtCodTemporada.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCodTemporada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (TxtCodCliente.Text == "") { TxtCodCliente.Tag = ""; }
                BuscaTemporada(1);
            }
        }

        private void TxtDesTemporada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (TxtCodCliente.Text == "") { TxtCodCliente.Tag = ""; }
                BuscaTemporada(2);
            }
        }

        public void BuscaTemporada(short Tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                if (Tipo == 1)
                    oTipo.sQuery = "SELECT  Cod_TemCli as Codigo, Nom_TemCli as Descripcion FROM  TG_TemCli WHERE cod_cliente ='" + TxtCodCliente.Tag + "' and Cod_TemCli like '%" + TxtCodTemporada.Text + "%'";
                else
                    oTipo.sQuery = "SELECT  Cod_TemCli as Codigo, Nom_TemCli as Descripcion FROM  TG_TemCli WHERE cod_cliente ='" + TxtCodCliente.Tag + "' and Nom_TemCli like '%" + TxtDesTemporada.Text + "%'";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTemporada.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Codigo"]);
                    TxtDesTemporada.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTemporada.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Codigo"].Value);
                        TxtDesTemporada.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }
                TxtCodTipoPrenda.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCodTipoPrenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaTipoPrenda(1);
            }
        }

        private void TxtDesTipoPrenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaTipoPrenda(2);
            }
        }

        public void BuscaTipoPrenda(short Tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                if (Tipo == 1)
                    oTipo.sQuery = "tg_muestra_ayuda_TipPre '1','" + TxtCodTipoPrenda.Text + "',''";
                else
                    oTipo.sQuery = "tg_muestra_ayuda_TipPre '2','','" + TxtDesTipoPrenda.Text + "'";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTipoPrenda.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_TipPre"]);
                    TxtDesTipoPrenda.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Des_TipPre"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTipoPrenda.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_TipPre"].Value);
                        TxtDesTipoPrenda.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Des_TipPre"].Value);
                    }
                }
                BtnImagen.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImagen_Click(object sender, EventArgs e)
        {
            Foto1();
        }

        public void Foto1()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog
                {
                    ////Filter = vDialogFilterImage,
                    Filter = "Imágenes (*.bmp;*.jpeg;*.jpg;*.png)|*.bmp;*.jpeg;*.jpg;*.png| All Files|*.*",
                    FileName = ""

                };

                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    string img1 = openFileDialog1.FileName;

                    rutaArchivo = openFileDialog1.FileName;

                    if (!IsValidImage(img1))
                    {
                        MessageBox.Show("NO es una imagen permitida", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    nombreArchivo = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    result = Path.GetExtension(openFileDialog1.FileName);

                    FileInfo infoReader = new FileInfo(openFileDialog1.FileName);

                    string strSQL = "SELECT RutaGrafico_Forma_Medir_Biblioteca FROM TG_CONTROL";
                    object resultado = oHp.DevuelveDato(strSQL, VariablesGenerales.pConnect);
                    string rutaBase = resultado != null ? resultado.ToString() : string.Empty;
                    string newRuta = rutaBase + "_" + nombreArchivo + ".jpg";

                    TxtRutaImagen.Text = newRuta;
                    TxtDescripcion.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool IsValidImage(string path)
        {
            string extensionFile = Path.GetExtension(path);
            foreach (string clave in FormatImageValid)
            {
                if (clave.Equals(extensionFile, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private void TxtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            string rutaDestino = string.Empty;
            try
            {
                if (TxtCodCliente.Text == "") { TxtCodCliente.Tag = ""; }
                if (TxtCodCliente.Text == "")
                {
                    MessageBox.Show("Favor de colocar el cliente", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCodCliente.Focus();
                    return;
                }
                if (TxtCodTemporada.Text == "")
                {
                    MessageBox.Show("Favor de colocar la temporada", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCodTemporada.Focus();
                    return;
                }

                DialogResult rpt;
                rpt = MessageBox.Show("¿Está seguro de guardar?", "Pregunta", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == rpt)
                {
                    strSQL = string.Empty;
                    strSQL += "\n" + "EXEC UP_MAN_Es_Biblioteca_Forma_Medir";
                    strSQL += "\n" + string.Format(" @accion            ='{0}'", sOpcion);
                    strSQL += "\n" + string.Format(",@Id_Biblioteca     ='{0}'", TxtIdBiblioteca.Text);
                    strSQL += "\n" + string.Format(",@Cod_Cliente       ='{0}'", TxtCodCliente.Tag);
                    strSQL += "\n" + string.Format(",@Cod_TemCli        ='{0}'", TxtCodTemporada.Text);
                    strSQL += "\n" + string.Format(",@Cod_TipPre        ='{0}'", TxtCodTipoPrenda.Text);
                    strSQL += "\n" + string.Format(",@descripcion       ='{0}'", TxtDescripcion.Text);                    
                    if (nombreArchivo != "")
                    {
                        strSQL += "\n" + string.Format(",@imagen            ='{0}'", nombreArchivo);
                    }
                    strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                    strSQL += "\n" + string.Format(",@cod_estacion      ='{0}'", Environment.MachineName);
                    strSQL += "\n" + string.Format(",@result            ='{0}'", result);

                    DataTable oDtDatosRetorna = oHp.EjecutaOperacionRetornaDatos2(strSQL, VariablesGenerales.pConnect);
                    if (nombreArchivo != "")
                    {
                        if (oDtDatosRetorna != null)
                        {
                            if (oDtDatosRetorna.Rows.Count > 0)
                            {
                                oDr = oDtDatosRetorna.Rows[0];
                                if (oDr["grafico"] != "")
                                {
                                    TxtRutaImagen.Text = oDr["grafico"].ToString();
                                    IsCambioOK = true;

                                    CopiarFoto(rutaArchivo, TxtRutaImagen.Text);

                                    MessageBox.Show("Cambios realizados correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult = DialogResult.OK;
                                }
                            }
                        }
                    }
                    else
                    {
                        IsCambioOK = true;
                        MessageBox.Show("Cambios realizados correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CopiarFoto(string vRutaOriginal, string vRutaDestino)
        {
            try
            {

                if (string.IsNullOrEmpty(vRutaOriginal)) return false;
                if (string.IsNullOrEmpty(vRutaDestino)) return false;


                string directorioArchivo = Path.GetDirectoryName(vRutaDestino);

                if (!Directory.Exists(directorioArchivo))
                {
                    try
                    {
                        Directory.CreateDirectory(directorioArchivo);
                        Console.WriteLine("Carpeta creada correctamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al crear la carpeta: " + ex.Message);
                    }
                }

                if (!string.IsNullOrEmpty(vRutaOriginal.Trim()))
                {
                    // Si la imagen no ha cambiado (vOrigen) entonces queda la misma imagen al grabar
                    if (vRutaOriginal != vRutaDestino)

                        if (!File.Exists(vRutaDestino))
                        {
                            System.IO.File.Copy(vRutaOriginal, vRutaDestino, true);
                        }
                    return true;
                }
            }
            catch (Exception xSQLErr)
            {
                MessageBox.Show(xSQLErr.Message, "SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            IsCambioOK = false;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
