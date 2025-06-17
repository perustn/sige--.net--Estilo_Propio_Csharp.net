using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBandejaBibliotecaFormaMedir : ProyectoBase.frmBase
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        private Color colEmpresa;
        private string OpcionFiltro;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        private Boolean bolSW_LoadGUI = true;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        private const string K_strColCheck = "FLG";
        private int FilaSeleccionado;
        private string ListaIDProceso;
        public Boolean ValidaCopia = false;
        private DataRow oDr;
        private string strRutaOriginal;
        string K_strColLink_Imagen = "lnkImagen";
        clsBtnSeguridadJanus oSeg = new clsBtnSeguridadJanus();
        public Boolean IsCambioOK;

        public Boolean LlamadaDesdeModificarEP = false;
        public string vImagenFormaMedir;
        public string vresult;

        public string RutaOriginal
        {
            set
            {
                strRutaOriginal = value;
            }
            get
            {
                return strRutaOriginal;
            }
        }

        enum ModoGUIVerPreciosProveedores
        {
            PreciosProveedoresTelasCombinaciones = 2
        }

        #endregion

        public FrmBandejaBibliotecaFormaMedir()
        {
            InitializeComponent();
        }

        private void FrmBandejaBibliotecaFormaMedir_Load(object sender, EventArgs e)
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
                    //panel1.BackColor = colEmpresa;
                }

                bolSW_LoadGUI = false;
                if (LlamadaDesdeModificarEP == false)
                {
                    buttonBar1.Groups[0].Items["TRASLADAREPV"].Visible = false;
                    ActiveControl = gridEX1;
                }
                else
                {
                    buttonBar1.Groups[0].Items["TRASLADAREPV"].Visible = true;
                    ActiveControl = gridEX1;
                    CargaGrilla();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TxtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaCliente(false);
            }
        }

        public void BuscaCliente(bool flgEstcli)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "Tg_PROMPT_Clientes '" + TxtCliente.Text + "'";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCliente.Tag = oTipo.dtResultados.Rows[0]["cod_cliente"];
                    TxtCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cliente"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCliente.Tag = oTipo.RegistroSeleccionado.Cells["cod_cliente"].Value;
                        TxtCliente.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cliente"].Value);
                    }
                }
                TxtTemporada.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtTemporada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (TxtCliente.Text == "") { TxtCliente.Tag = ""; }
                BuscaTemporada();
            }
        }

        public void BuscaTemporada()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                //////////////oTipo.sQuery = "exec Sm_Flash_Cost_Busca_Cliente_Temporada '" + TxtCliente.Tag + "','','" + TxtTemporada.Text + "'";
                oTipo.sQuery = "SELECT Cod_TemCli, Nom_TemCli FROM tg_temcli WHERE cod_cliente = '" + TxtCliente.Tag + "'  and Cod_TemCli LIKE '%" + TxtTemporada.Text + "%'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtTemporada.Tag = oTipo.dtResultados.Rows[0]["Cod_TemCli"];
                    TxtTemporada.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Nom_TemCli"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtTemporada.Tag = oTipo.RegistroSeleccionado.Cells["Cod_TemCli"].Value;
                        TxtTemporada.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Nom_TemCli"].Value);
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

        private void TxtTipoPrenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoPrenda();
            }
        }

        public void BuscarTipoPrenda()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();

                oTipo.sQuery = "tg_muestra_ayuda_TipPre '2','','" + TxtTipoPrenda.Text + "'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtTipoPrenda.Tag = oTipo.dtResultados.Rows[0]["Cod_TipPre"];
                    TxtTipoPrenda.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Des_TipPre"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtTipoPrenda.Tag = oTipo.RegistroSeleccionado.Cells["Cod_TipPre"].Value;
                        TxtTipoPrenda.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Des_TipPre"].Value);
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
                if (TxtCliente.Text == "") {TxtCliente.Tag = "";}
                if (TxtTemporada.Text == "") { TxtTemporada.Tag = "";}
                if (TxtTipoPrenda.Text == "") { TxtTipoPrenda.Tag = "";}

                strSQL = string.Empty;
                strSQL += "\n" + "EXEC UP_MAN_Es_Biblioteca_Forma_Medir_Bandeja";
                strSQL += "\n" + string.Format(" @opcion            ='{0}'", "1");
                strSQL += "\n" + string.Format(",@cod_cliente		='{0}'", TxtCliente.Tag);
                strSQL += "\n" + string.Format(",@cod_temcli		='{0}'", TxtTemporada.Tag);
                strSQL += "\n" + string.Format(",@cod_tippre		='{0}'", TxtTipoPrenda.Tag);
                strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(",@cod_estacion		='{0}'", SystemInformation.ComputerName);

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
                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Id_Biblioteca"];
                            withBlock2.Caption = "ID";
                            withBlock2.Width = 50;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Cliente"];
                            withBlock2.Caption = "COD. CLIENTE";
                            withBlock2.Width = 70;
                            withBlock2.Visible = false;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Abr_Cliente"];
                            withBlock2.Caption = "ABR. CLIENTE";
                            withBlock2.Width = 70;
                            withBlock2.Visible = false;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Nom_Cliente"];
                            withBlock2.Caption = "NOMBRE CLIENTE";
                            withBlock2.Width = 150;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_TemCli"];
                            withBlock2.Caption = "COD TEMCLI";
                            withBlock2.Width = 70;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Nom_TemCli"];
                            withBlock2.Caption = "TEMPORADA";
                            withBlock2.Width = 120;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_TipPre"];
                            withBlock2.Caption = "TIPO PRENDA";
                            withBlock2.Width = 80;
                            withBlock2.Visible = false;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Des_TipPre"];
                            withBlock2.Caption = "TIPO PRENDA";
                            withBlock2.Width = 140;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Imagen"];
                            withBlock2.Caption = "RUTA DE IMAGEN";
                            withBlock2.Width = 150;
                            withBlock2.ColumnType = ColumnType.Link;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.CellStyle.ForeColor = Color.Blue;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Descripcion"];
                            withBlock2.Width = 140;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Creacion"];
                            withBlock2.Caption = "FECHA CREACIÓN";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario_Creacion"];
                            withBlock2.Caption = "USUARIO CREACIÓN";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Estacion_Creacion"];
                            withBlock2.Caption = "ESTACIÓN CREACIÓN";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Modificacion"];
                            withBlock2.Caption = "FECHA MODIFICACIÓN";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario_Modificacion"];
                            withBlock2.Caption = "USUARIO MODIFICACIÓN";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Estacion_Modificacion"];
                            withBlock2.Caption = "ESTACIÓN MODIFICACIÓN";
                        }

                        withBlock1.Columns.Add(new GridEXColumn()
                        {
                            Key = K_strColCheck,
                            Caption = string.Empty,
                            Width = 35,
                            ColumnType = ColumnType.CheckBox,
                            EditType = EditType.CheckBox,
                            ActAsSelector = true,
                            Visible = true
                        });
                    }
                }
                gridEX1.FrozenColumns = 8;
                gridEX1.RootTable.Columns[K_strColCheck].Position = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonBar1_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            try
            {
                DialogResult rpt;
                switch (e.Item.Key)
                {
                    case "ADICIONAR":
                        FrmBandejaBibliotecaFormaMedir_Man oFrm = new FrmBandejaBibliotecaFormaMedir_Man();
                        oFrm.sOpcion = "I";
                        oFrm.TxtRutaImagen.Text = "";
                        oFrm.rutaArchivo = "";
                        oFrm.nombreArchivo = "";
                        if(TxtCliente.Text != "")
                        {
                            oFrm.TxtCodCliente.Tag = TxtCliente.Tag;
                            oFrm.TxtCodCliente.Text = oHp.DevuelveDato("select Abr_Cliente from  TG_Cliente where cod_cliente = '" + TxtCliente.Tag + "'", VariablesGenerales.pConnect).ToString();
                            oFrm.TxtDesCliente.Text = TxtCliente.Text;
                        }
                        if (TxtTemporada.Text != "")
                        {
                            oFrm.TxtCodTemporada.Text = TxtTemporada.Tag.ToString();
                            oFrm.TxtDesTemporada.Text = TxtTemporada.Text;
                        }
                        if (TxtTipoPrenda.Text != "")
                        {
                            oFrm.TxtCodTipoPrenda.Text = TxtTipoPrenda.Tag.ToString();
                            oFrm.TxtDesTipoPrenda.Text = TxtTipoPrenda.Text;
                        }
                        oFrm.ShowDialog();
                        if (oFrm.IsCambioOK == true)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "MODIFICAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        FilaSeleccionado = gridEX1.CurrentRow.Position;
                        FrmBandejaBibliotecaFormaMedir_Man oFrmMod = new FrmBandejaBibliotecaFormaMedir_Man();
                        oFrmMod.sOpcion = "U";
                        oFrmMod.TxtIdBiblioteca.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Biblioteca"].Index).ToString();
                        oFrmMod.TxtCodCliente.Tag = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Cliente"].Index).ToString();
                        oFrmMod.TxtCodCliente.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Abr_Cliente"].Index).ToString();
                        oFrmMod.TxtDesCliente.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Nom_Cliente"].Index).ToString();
                        oFrmMod.TxtCodTemporada.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_TemCli"].Index).ToString();
                        oFrmMod.TxtDesTemporada.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Nom_TemCli"].Index).ToString();
                        oFrmMod.TxtCodTipoPrenda.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_TipPre"].Index).ToString();
                        oFrmMod.TxtDesTipoPrenda.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Des_TipPre"].Index).ToString();
                        oFrmMod.TxtRutaImagen.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Imagen"].Index).ToString();
                        oFrmMod.TxtDescripcion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Descripcion"].Index).ToString();
                        oFrmMod.nombreArchivo = "";
                        oFrmMod.ShowDialog();
                        if (oFrmMod.IsCambioOK == true)
                        {
                            CargaGrilla();
                            gridEX1.Row = FilaSeleccionado;
                        }
                        break;

                    case "ELIMINAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        if (gridEX1.GetCheckedRows().Length != 0)
                        {
                            rpt = MessageBox.Show("¿Está seguro de eliminar el(los) registro(s) seleccionado(s)?", "Pregunta", MessageBoxButtons.YesNo);
                            if (DialogResult.Yes == rpt)
                            {
                                EliminarMasiva();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe Seleccionar Al Menos un Registro", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }                       
                        break;

                    case "EXPORTAR":
                        if (gridEX1.RowCount == 0)
                            return;
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

                    case "TRASLADAREPV":
                        if (gridEX1.RecordCount == 0) { return; }
                        if (LlamadaDesdeModificarEP == true)
                        {
                            vImagenFormaMedir = gridEX1.GetValue(gridEX1.RootTable.Columns["Imagen"].Index).ToString();
                            IsCambioOK = true;
                            DialogResult = DialogResult.OK;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarMasiva()
        {
            try
            {
                int contador = 0;
                int contBusqueda = 0;
                foreach (GridEXRow item in gridEX1.GetCheckedRows())
                {
                    strSQL = string.Empty;
                    strSQL += "\n" + "EXEC UP_MAN_Es_Biblioteca_Forma_Medir";
                    strSQL += "\n" + string.Format(" @accion            ='{0}'", "D");
                    strSQL += "\n" + string.Format(",@Id_Biblioteca     ='{0}'", item.Cells["Id_Biblioteca"].Value.ToString());
                    strSQL += "\n" + string.Format(",@cod_usuario       ='{0}'", VariablesGenerales.pUsuario);
                    strSQL += "\n" + string.Format(",@cod_estacion      ='{0}'", Environment.MachineName);

                    if (oHp.EjecutarOperacion(strSQL) == false)
                    {
                        contador += 1;
                    }
                    else { contBusqueda += 1; }
                }
                if (contador == 0)
                {
                    MessageBox.Show("Registro eliminado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargaGrilla();
                }
                if (contBusqueda > 0)
                {
                    CargaGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridEX1_LinkClicked(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (gridEX1.CurrentColumn == null) { return; }
                if (gridEX1.CurrentColumn.Key == null) { return; }

                switch (gridEX1.CurrentColumn.Key.ToUpper())
                {
                    case "IMAGEN":
                        {
                            int xfilas;
                            xfilas = gridEX1.Row;
                            if (gridEX1.RowCount == 0)
                                return;
                            string sRutaTemp;
                            sRutaTemp = @"C:\\TEMP\\BibliotecaGraficos\\";

                            if (!System.IO.Directory.Exists(sRutaTemp))
                                System.IO.Directory.CreateDirectory(sRutaTemp);
                            RutaOriginal = gridEX1.GetValue(gridEX1.RootTable.Columns["IMAGEN"].Index).ToString();


                            // Obtener el nombre del Archivo
                            string Namefiles = System.IO.Path.GetFileName(RutaOriginal);

                            // Obtener el Tamaño del Archivo
                            System.IO.FileInfo fi = new System.IO.FileInfo(RutaOriginal);
                            double xTamFile = fi.Length;

                            string sRutaTempNomFile;
                            sRutaTempNomFile = sRutaTemp + Namefiles;

                            if (System.IO.File.Exists(RutaOriginal))
                            {
                                if (System.IO.File.Exists(sRutaTempNomFile))
                                    DelFichaTecnicaVista(sRutaTempNomFile);

                                if (!System.IO.File.Exists(sRutaTempNomFile))
                                    System.IO.File.Copy(RutaOriginal, sRutaTempNomFile, true);

                                bool rtnvalue = false;

                                // Abrir archivo
                                try
                                {
                                    System.IO.FileStream fs = System.IO.File.OpenWrite(sRutaTempNomFile);
                                    fs.Close();
                                }
                                catch (Exception exx)
                                {
                                    rtnvalue = true;
                                }

                                // CargarDocumentosComercial()
                                gridEX1.Row = xfilas;

                                if (!rtnvalue)
                                {
                                    System.Diagnostics.Process Arc;
                                    Arc = Process.Start(sRutaTempNomFile);
                                }
                                else
                                    MessageBox.Show("Archivo ya se encuentra Abierto por Usted. Favor de Revisar", "Mensaje");
                            }
                            else
                                MessageBox.Show("Archivo no existe", "Mensaje");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DelFichaTecnicaVista(string vRutaFT)
        {
            try
            {
                if (!string.IsNullOrEmpty((vRutaFT)))
                    System.IO.File.Delete(vRutaFT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
