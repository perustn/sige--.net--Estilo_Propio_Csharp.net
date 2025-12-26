using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    public partial class FrmSolicitudesAutorizacionImpFT : ProyectoBase.frmBase
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        GeneradorFichaTecnica GenFT = new GeneradorFichaTecnica();
        private Color colEmpresa;
        private string OpcionFiltro;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        private const string K_strColCheck = "FLG";
        private int FilaSeleccionado;
        public Boolean ValidaCopia = false;
        clsBtnSeguridadJanus oSeg = new clsBtnSeguridadJanus();
        private object Cad1;
        private DataTable oDT_StatusFT;

        private string FiltroClienteSel;
        private string FiltroTemporadaSel;
        private string FiltroEPSel;
        private string FiltroVersionSel;
        private int FiltroIDFichaTecnicaSel;
        private DataTable oDtDataGlobal;
        private DataTable oDtFiltraRegistros;

        public DataTable oDtEstructuraFTecnica;
        public string ListaOpsSel;
        public bool EvaluaSeleccionReg;
        private bool isExecutionGeneracionFT = false;
        #endregion

        public FrmSolicitudesAutorizacionImpFT()
        {
            InitializeComponent();
        }

        private void FrmSolicitudesAutorizacionImpFT_Load(object sender, EventArgs e)
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
                }

                DtpFechaDesde.Value = DateTime.Now;
                DtpFechaHasta.Value = DateTime.Now;

                OpcionFiltro = "1";
                CargaGrilla();
                ActiveControl = gridEX1;

                //oSeg.GetBotonesJanus(VariablesGenerales.pCodPerfil, VariablesGenerales.pCodEmpresa, this.Name, buttonBar1, "");
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
                        OpcionFiltro = "1";
                        CargaGrilla();
                        break;
                    }

                case "2":
                    {
                        OpcionFiltro = "2";
                        grpNroSolicitud.Visible = true;
                        TxtNroSolicitud.Focus();
                        break;
                    }

                case "3":
                    {
                        OpcionFiltro = "3";
                        grpFechaEstado.Visible = true;
                        DtpFechaDesde.Focus();
                        break;
                    }

                case "4":
                    {
                        OpcionFiltro = "4";
                        grpOP.Visible = true;
                        TxtOP.Focus();
                        break;
                    }

                case "5":
                    {
                        OpcionFiltro = "5";
                        grpEstiloVersion.Visible = true;
                        TxtEP.Focus();
                        break;
                    }
            }
        }

        private void VisibilidadObj(bool vbool)
        {
            TxtNroSolicitud.Text = "";
            DtpFechaDesde.Value = DateTime.Now;
            DtpFechaHasta.Value = DateTime.Now;
            TxtOP.Text = "";
            TxtEP.Text = "";
            TxtVersion.Text = "";

            grpNroSolicitud.Visible = vbool;
            grpFechaEstado.Visible = vbool;
            grpOP.Visible = vbool;
            grpEstiloVersion.Visible = vbool;
        }

        private void TxtNroSolicitud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnBuscar.Focus();
            }
        }

        private void DtpFechaDesde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DtpFechaHasta.Focus();
            }
        }

        private void DtpFechaHasta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtCodEstado.Focus();
            }
        }

        private void TxtCodEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaEstado(1);
            }
        }

        private void TxtDesEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaEstado(2);
            }
        }

        public void BuscaEstado(int tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                switch (tipo)
                {
                    case 1:
                        oTipo.sQuery = "select cod_estado, nom_estado, Id_Estado from FT_AI_Estado where Cod_Estado LIKE '%" + TxtCodEstado.Text + "%'";
                        break;
                    case 2:
                        oTipo.sQuery = "select cod_estado, nom_estado, Id_Estado from FT_AI_Estado where nom_estado LIKE '%" + TxtDesEstado.Text + "%'";
                        break;
                }               

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodEstado.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_estado"]);
                    TxtDesEstado.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["nom_estado"]);
                    TxtCodEstado.Tag = oTipo.dtResultados.Rows[0]["Id_Estado"];
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodEstado.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["cod_estado"].Value);
                        TxtDesEstado.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["nom_estado"].Value);
                        TxtCodEstado.Tag = oTipo.RegistroSeleccionado.Cells["Id_Estado"].Value;
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
                strSQL += "\n" + "EXEC FT_AI_Solicitud_Bandeja";
                strSQL += "\n" + string.Format("  @opcion           = '{0}'", OpcionFiltro);
                strSQL += "\n" + string.Format(", @desde			= '{0}'", DtpFechaDesde.Value.ToShortDateString());
                strSQL += "\n" + string.Format(", @hasta			= '{0}'", DtpFechaHasta.Value.ToShortDateString());
                strSQL += "\n" + string.Format(", @id_solicitud		= '{0}'", TxtNroSolicitud.Text);
                strSQL += "\n" + string.Format(", @id_estado		= '{0}'", TxtCodEstado.Tag);
                strSQL += "\n" + string.Format(", @cod_ordpro		= '{0}'", TxtOP.Text);
                strSQL += "\n" + string.Format(", @cod_estpro	    = '{0}'", TxtEP.Text);
                strSQL += "\n" + string.Format(", @cod_version  	= '{0}'", TxtVersion.Text);
                strSQL += "\n" + string.Format(", @cod_usuario		= '{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(", @cod_estacion     = '{0}'", SystemInformation.ComputerName);

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                gridEX1.RootTable.Columns.Clear();
                oDtDataGlobal = oDt;
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
                        //////////////withBlock1.PreviewRowMember = "OBSERVACIONES";

                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.Visible = false;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }

                        //{
                        //    var withBlock2 = withBlock1.Columns[K_strColCheck];
                        //    withBlock2.HeaderImage = global::Estilo_Propio_Csharp.Properties.Resources.accept_16X16;
                        //    withBlock2.AllowDrag = false;
                        //    withBlock2.Caption = string.Empty;
                        //    withBlock2.Width = 60;
                        //    withBlock2.Visible = true;
                        //    withBlock2.CellStyle.BackColor = Color.AliceBlue;
                        //    withBlock2.HeaderStyle = new GridEXFormatStyle() { FontSize = 1, TextAlignment = TextAlignment.Center, ForeColor = BackColor, ImageHorizontalAlignment = ImageHorizontalAlignment.Center, ImageVerticalAlignment = ImageVerticalAlignment.Center };
                        //}

                        {
                            var withBlock2 = withBlock1.Columns["Id_Solicitud"];
                            withBlock2.Caption = "ID";
                            withBlock2.Width = 70;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Estado"];
                            withBlock2.Width = 70;
                            withBlock2.Visible = false;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Nom_Estado"];
                            withBlock2.Caption = "ESTADO";
                            withBlock2.Width = 120;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Estado"];
                            withBlock2.Caption = "FECHA ESTADO";
                            withBlock2.Width = 90;
                            withBlock2.Visible = true;
                            withBlock2.FormatString = "dd/MM/yyyy";
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Estacion"];
                            withBlock2.Caption = "ESTACION";
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Id_Publicacion"];
                            withBlock2.Caption = "ID PUBLICACIÓN";
                            withBlock2.Width =100;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Estpro"];
                            withBlock2.Caption = "ESTILO PROPIO";
                            withBlock2.Width = 80;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Version"];
                            withBlock2.Caption = "VERSION";
                            withBlock2.Visible = true;
                            withBlock2.Width =70;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Pedidos"];
                            withBlock2.Visible = true;
                            withBlock2.Width = 200;
                        }

                    }
                }
                //gridEX1.RootTable.Columns[K_strColCheck].Position = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void buttonBar1_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            try
            {
                DialogResult rpt;
                switch (e.Item.Key)
                {
                    case "GENERAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmSolicitudesAutorizacionImpFT_Generar oFrm = new FrmSolicitudesAutorizacionImpFT_Generar();
                        if (oFrm.ShowDialog() == DialogResult.OK)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "CANCELAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        ActualizaEstado("C");
                        break;

                    case "APROBAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        ActualizaEstado("A");
                        break;

                    case "RECHAZAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        ActualizaEstado("R");
                        break;

                    case "IMPRIMIR":
                        if (gridEX1.RecordCount == 0) { return; }
                        if ((string)gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Estado"].Index) != "A")
                        {
                            MessageBox.Show("Usuario no está aprobado para la Impresión", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        string rutaArchivoPDF = GenFT.CreaCarpetaLocal();
                        string ArchivoOrigen = (string)oHp.DevuelveDato("select Nombre_Adjunto_Publicacion from es_estprover_ficha_tecnica_publicaciones where ID_Publicacion = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["ID_Publicacion"].Index).ToString() + "'", VariablesGenerales.pConnect);
                        string ArchivoDestino = rutaArchivoPDF + Path.GetFileName(ArchivoOrigen);
                        string Password = (string)oHp.DevuelveDato("select Password_PDF_Protection from es_estprover_ficha_tecnica_publicaciones where ID_Publicacion = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["ID_Publicacion"].Index).ToString() + "'", VariablesGenerales.pConnect);

                        GenFT.OnProcesoCompleto += () => {
                            // Esto se ejecutará SOLO cuando le den "Aceptar" en la impresión
                            this.Invoke((MethodInvoker)delegate {
                                CargaGrilla(); // Tu método que refresca el DataGridView
                            });
                        };

                        GenFT.DesbloquedaPDF(ArchivoOrigen, ArchivoDestino, Password,(int)gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Solicitud"].Index));

                        break;

                    case "VERLOGEST":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmSolicitudesAutorizacionImpFT_VerLogEstados oRec = new FrmSolicitudesAutorizacionImpFT_VerLogEstados();
                        oRec.TxtIdSolicitud.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Solicitud"].Index).ToString();
                        oRec.CargaGrilla();
                        oRec.ShowDialog();
                        break;

                    case "EXPORTAR":
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

                    case "MANTAPROBADORES":
                        FrmMaestroAprobadores oMant = new FrmMaestroAprobadores();
                        oMant.ShowDialog();
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActualizaEstado(string CodigoEstado)
        {
            try
            {
                DialogResult rpt;
                rpt = MessageBox.Show("¿Está seguro de cambiar el estado al registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == rpt)
                {
                    strSQL = string.Empty;
                    strSQL += "\n" + "EXEC UP_MAN_FT_AI_Solicitud_Estado";
                    strSQL += "\n" + string.Format(" @Opcion            ='{0}'", "S");
                    strSQL += "\n" + string.Format(",@Id_Solicitud		= {0} ", gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Solicitud"].Index));
                    strSQL += "\n" + string.Format(",@Cod_Estado		='{0}'", CodigoEstado);
                    strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                    strSQL += "\n" + string.Format(",@cod_estacion		='{0}'", Environment.MachineName);

                    if (oHp.EjecutarOperacion(strSQL) == true)
                    {
                        MessageBox.Show("Estado cambiado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaGrilla();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
