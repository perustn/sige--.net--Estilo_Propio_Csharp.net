/**/using System;
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
    public partial class FrmBandejaBibliotecaGraficos :  ProyectoBase.frmBase
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

        public Boolean LlamadaDesdeGraficosEP = false;
        public string vCodEstpro;
        public string vCodVersion;
        public string vCodigoCliente;
        public string vCodSecuencia;

        enum ModoGUIVerPreciosProveedores
        {
            PreciosProveedoresTelasCombinaciones = 2
        }

        #endregion

        public FrmBandejaBibliotecaGraficos()
        {
            InitializeComponent();
        }

        private void FrmBandejaSeguimientoCotizacionPreciosTela_Load(object sender, EventArgs e)
        {

           ////// oSeg.GetBotonesJanus(VariablesGenerales.pCodPerfil, VariablesGenerales.pCodEmpresa, this.Name, buttonBar1, "");

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

                bolSW_LoadGUI = false;
                if(LlamadaDesdeGraficosEP == false)
                {
                    buttonBar1.Groups[0].Items["TRASLADAREPV"].Visible = false;
                    grpProcesoGrafico.Visible = true;
                    OpcionFiltro = "1";
                    ActiveControl = TxtProceso;
                }
                else
                {
                    buttonBar1.Groups[0].Items["TRASLADAREPV"].Visible = true;
                    OpcionFiltro = "4";
                    OptEstiloPropio.Checked = true;
                    grpEstiloPropio.Visible = true;
                    ActiveControl = TxtEstiloPropio;
                    CargaGrilla();
                }               
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
                        grpProcesoGrafico.Visible = true;
                        TxtProceso.Focus();
                        break;
                    }

                case "2":
                    {                                                
                        OpcionFiltro = "2";
                        grpClienteTemporada.Visible = true;
                        TxtCliente.Select();
                        TxtCliente.Focus();
                        break;
                    }

                case "3":
                    {
                        OpcionFiltro = "3";
                        grpEstiloCliente.Visible = true;
                        TxtClienteEstCli.Focus();
                        break;
                    }

                case "4":
                    {
                        OpcionFiltro = "4";
                        grpEstiloPropio.Visible = true;
                        TxtEstiloPropio.Focus();
                        break;
                    }

                case "5":
                    {
                        OpcionFiltro = "5";
                        grpTipoPrenda.Visible = true;
                        TxtTipoPrenda.Focus();
                        break;
                    }               
           }
        }

        private void VisibilidadObj(bool vbool)
        {
            TxtCliente.Text = "";
            TxtCliente.Tag = "";
            TxtTemporada.Text = "";
            TxtTemporada.Tag = "";

            TxtProceso.Text = "";
            TxtProceso.Tag = "";
            TxtTipoGrafico.Text = "";
            TxtTipoGrafico.Tag = "";

            TxtEstiloCliente.Text = "";
            TxtEstiloCliente.Tag = "";

            TxtEstiloPropio.Text = "";
            TxtEstiloPropio.Tag = "";

            TxtTipoPrenda.Text = "";
            TxtTipoPrenda.Tag = "";


            grpClienteTemporada.Visible = vbool;
            grpProcesoGrafico.Visible = vbool;
            grpEstiloCliente.Visible = vbool;
            grpEstiloPropio.Visible = vbool;
            grpTipoPrenda.Visible = vbool;
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
                    if (flgEstcli==true)
                    {
                        TxtEstiloCliente.Tag = oTipo.dtResultados.Rows[0]["cod_cliente"];
                        TxtEstiloCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cliente"]);
                    }
                    else
                    {
                        TxtCliente.Tag = oTipo.dtResultados.Rows[0]["cod_cliente"];
                        TxtCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cliente"]);
                    }
                    
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        if (flgEstcli == true)
                        {
                            TxtClienteEstCli.Tag = oTipo.RegistroSeleccionado.Cells["cod_cliente"].Value;
                            TxtClienteEstCli.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cliente"].Value);
                        }else
                        {
                            TxtCliente.Tag = oTipo.RegistroSeleccionado.Cells["cod_cliente"].Value;
                            TxtCliente.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cliente"].Value);
                        }
                    }
                }


                if (flgEstcli == true)
                { TxtEstiloCliente.Focus(); }
                else { TxtTemporada.Focus(); }
                    
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

        private void DtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                DtpFechaFinal.Focus();
            }
        }

        private void DtpFechaFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnBuscar.Focus();
            }
        }

        private void TxtCodTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (TxtCodTela.Text == "")
                    BUSCATELA(1);
                else
                {
                    if (TxtCodTela.Text.Trim().Length >= 2 & TxtCodTela.Text.Trim().Length < 8)
                        TxtCodTela.Text = oMod.CompletaCodigo(TxtCodTela.Text.Trim(), 8, 2);
                    BUSCATELA(2);
                }
            }
        }

        public void BUSCATELA(short Tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                if (Tipo == 1)
                    oTipo.sQuery = "SELECT Cod_Tela AS CODIGO, Des_Tela_Comercial AS DESCRIPCION, Des_Tela AS DESCRIPCION_COMPLETA FROM tx_tela";
                else
                    oTipo.sQuery = "SELECT Cod_Tela AS CODIGO, Des_Tela_Comercial AS DESCRIPCION, Des_Tela AS DESCRIPCION_COMPLETA FROM tx_tela WHERE Cod_Tela LIKE '%" + TxtCodTela.Text + "%'";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Codigo"]);
                    TxtDesTela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Codigo"].Value);
                        TxtDesTela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
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

        private void TxtCodTipoTejido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaTipoTejido();
            }
        }

        private void TxtDesTipoTejido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaTipoTejido();
            }
        }

        public void BuscaTipoTejido()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "select Tipo_Tela Codigo, Des_Tipo_Tela Descripcion from TX_Tipo_Tela";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTipoTejido.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Codigo"]);
                    TxtDesTipoTejido.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTipoTejido.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Codigo"].Value);
                        TxtDesTipoTejido.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }

                TxtCodFamiliaTela.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCodFamiliaTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaFamiliaTela();
            }
        }

        private void TxtDesFamiliaTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaFamiliaTela();
            }
        }

        public void BuscaFamiliaTela()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "SELECT cod_famtela as Codigo, des_famtela as Descripcion  FROM TX_FAMTELA WHERE Tipo_Familia_Tela = '2' and Cod_TipFamTela_Sist_Ant = '" + TxtCodTipoTejido.Text.Trim() + "'";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodFamiliaTela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Codigo"]);
                    TxtDesFamiliaTela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodFamiliaTela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Codigo"].Value);
                        TxtDesFamiliaTela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
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

        private void TxtSMT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnBuscar.Focus();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {              
            CargaGrilla();
        }

        object IIf(bool expression, object truePart, object falsePart) { return expression ? truePart : falsePart; }
        private object Val(object Expression)
        {
            throw new NotImplementedException();
        }

        public void CargaGrilla()
        {
            string codCliente="";
            
            try
            {
                if (TxtCliente.Text == "")
                {
                    TxtCliente.Tag = "";
                }

                if (OpcionFiltro=="3") { codCliente = TxtClienteEstCli.Tag.ToString(); }
                if (OpcionFiltro == "2") { codCliente = TxtCliente.Tag.ToString(); }


                strSQL = string.Empty;
                strSQL += "\n" + "EXEC UP_MAN_Es_Proceso_Imagen_Bandeja";
                strSQL += "\n" + string.Format("  @opcion                   = '{0}'", OpcionFiltro);
                strSQL += "\n" + string.Format(", @Cod_proceso			    = '{0}'", TxtProceso.Tag);
                strSQL += "\n" + string.Format(", @tipo_grafico			    = '{0}'", TxtTipoGrafico.Tag);               
                strSQL += "\n" + string.Format(", @cod_cliente			    = '{0}'", codCliente);
                strSQL += "\n" + string.Format(", @cod_temcli			    = '{0}'", TxtTemporada.Tag);
                strSQL += "\n" + string.Format(", @cod_estcli			    = '{0}'", TxtEstiloCliente.Tag);
                strSQL += "\n" + string.Format(", @cod_estpro			    = '{0}'", TxtEstiloPropio.Text);
                strSQL += "\n" + string.Format(", @cod_tippre			    = '{0}'", TxtTipoPrenda.Tag);


                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                gridEX1.RootTable.Columns.Clear();
                gridEX1.DataSource = oDt;

                
                //////////////////oDt.Columns.Add(new DataColumn(K_strColLink_Imagen, typeof(string)));

                //////////////////foreach (DataRow fila in oDt.Rows)
                //////////////////{
                //////////////////    fila[K_strColLink_Imagen] = "Ver Archivo";
                //////////////////}

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
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }

                        //////{
                        //////    var withBlock2 = withBlock1.Columns[K_strColLink_Imagen];
                        //////    //withBlock2.Image = GE_Aplicaciones.NET.Resources.ic_art_palette_16x16;
                        //////    withBlock2.Caption = string.Empty;
                        //////    withBlock2.ColumnType = ColumnType.Text;
                        //////    withBlock2.ButtonDisplayMode = CellButtonDisplayMode.Always;
                        //////    withBlock2.ButtonStyle = ButtonStyle.ButtonCell;
                        //////    withBlock2.AutoSizeMode = ColumnAutoSizeMode.AllCells;
                        //////}

                        {
                            var withBlock2 = withBlock1.Columns["Id_Proceso_Imagen"];
                            withBlock2.Caption = "ID PROC. IMAGEN";
                            withBlock2.Width = 70;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_proceso"];
                            withBlock2.Caption = "COD. PROCESO";
                            withBlock2.Width = 70;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Des_Proceso"];
                            withBlock2.Caption = "PROCESO";
                            withBlock2.Width = 120;
                        }                       
                        {
                            var withBlock2 = withBlock1.Columns["Descripcion"];
                            withBlock2.Caption = "SUB TITULO";
                            withBlock2.Width = 150;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Descripcion_Det"];
                            withBlock2.Caption = "DESCRIPCIÓN DETALLADA";
                            withBlock2.Width = 170;
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
                            var withBlock2 = withBlock1.Columns["tipo_grafico"];
                            withBlock2.Caption = "TIPO GRAFICO";
                            withBlock2.Width = 80;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Des_Tipo_Grafico"];
                            withBlock2.Caption = "DESCRIPCIÓN TIPO GRAFICO";
                            withBlock2.Width = 170;
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
                //////////////if (gridEX1.CurrentRow.RowType != RowType.Record)
                //////////////{
                //////////////    return;
                //////////////}
                DialogResult rpt;
                switch (e.Item.Key)
                {
                    case "ADICIONAR":                       
                        FrmBandejaBibliotecaGraficos_MAN oFrm = new FrmBandejaBibliotecaGraficos_MAN();
                        oFrm.sOpcion = "I";
                        oFrm.TxtRutaImagen.Text = "";
                        oFrm.rutaArchivo = "";
                        oFrm.nombreArchivo = "";
                        oFrm.ShowDialog();
                        if (oFrm.IsCambioOK == true)
                        {
                            CargaGrilla();                            
                        }
                        break;

                    case "MODIFICAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        FilaSeleccionado = gridEX1.CurrentRow.Position;
                        FrmBandejaBibliotecaGraficos_MAN oFrmMod = new FrmBandejaBibliotecaGraficos_MAN();
                        oFrmMod.sOpcion = "U";
                        oFrmMod.TxtIdProceso.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Proceso_Imagen"].Index).ToString();
                        oFrmMod.TxtCodProceso.Text= gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_proceso"].Index).ToString();
                        oFrmMod.TxtDesProceso.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Des_Proceso"].Index).ToString();
                        oFrmMod.TxtCodTipoGrafico.Text= gridEX1.GetValue(gridEX1.RootTable.Columns["tipo_grafico"].Index).ToString();
                        oFrmMod.TxtDesTipoGrafico.Text= gridEX1.GetValue(gridEX1.RootTable.Columns["Des_Tipo_Grafico"].Index).ToString();
                        oFrmMod.TxtDescripcion.Text= gridEX1.GetValue(gridEX1.RootTable.Columns["Descripcion"].Index).ToString();
                        oFrmMod.TxtDescripcionDetallada.Text= gridEX1.GetValue(gridEX1.RootTable.Columns["Descripcion_Det"].Index).ToString();
                        oFrmMod.TxtRutaImagen.Text= gridEX1.GetValue(gridEX1.RootTable.Columns["Imagen"].Index).ToString();
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
                        rpt = MessageBox.Show("¿Está seguro de eliminar el(los) registro(s) seleccionado(s)?", "Pregunta", MessageBoxButtons.YesNo);
                        if (DialogResult.Yes == rpt)
                        {

                            EliminarMasiva();
                            ////////////////////////Eliminar();                            
                            //////////FrmBandejaSolicitudAplicacionesBDESTF_DELETE oFrmDel = new FrmBandejaSolicitudAplicacionesBDESTF_DELETE();                            
                            //////////oFrmDel.TxtNroReq.Text= gridEX1.GetValue(gridEX1.RootTable.Columns["Nro_Req"].Index).ToString();
                            //////////oFrmDel.ShowDialog();
                            //////////if (oFrmDel.IsCambioOK == true)
                            //////////{
                            //////////    CargaGrilla();                                
                            //////////}
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
                        ListaIDProceso = "";
                        DataRow oDr;
                        if (LlamadaDesdeGraficosEP == true)
                        {
                            if (gridEX1.GetCheckedRows().Length != 0)
                            {
                                rpt = MessageBox.Show("¿Está seguro de copiar las imágenes al EPV de los registros seleccionados?", "Pregunta", MessageBoxButtons.YesNo);
                                if (DialogResult.Yes == rpt)
                                {
                                    foreach (GridEXRow oGridEXRow in gridEX1.GetCheckedRows())
                                    {
                                        if(ListaIDProceso == "")
                                        {
                                            ListaIDProceso = oGridEXRow.Cells["Id_Proceso_Imagen"].Value.ToString();
                                        }
                                        else
                                        {
                                            ListaIDProceso = ListaIDProceso + "," + oGridEXRow.Cells["Id_Proceso_Imagen"].Value.ToString();
                                        }
                                    }
                                    strSQL = string.Empty;
                                    strSQL += "\n" + "EXEC Up_Es_EstProVer_Rutas_Proceso_Imagen_Copiar_Desde_Biblioteca";
                                    strSQL += "\n" + string.Format(" @List_Id_Proceso_Imagen ='{0}'", ListaIDProceso);
                                    strSQL += "\n" + string.Format(",@Cod_estpro             ='{0}'", vCodEstpro);
                                    strSQL += "\n" + string.Format(",@Cod_version            ='{0}'", vCodVersion);
                                    strSQL += "\n" + string.Format(",@cod_cliente            ='{0}'", vCodigoCliente);
                                    strSQL += "\n" + string.Format(",@cod_usuario			 ='{0}'", VariablesGenerales.pUsuario);
                                    strSQL += "\n" + string.Format(",@cod_estacion			 ='{0}'", SystemInformation.ComputerName);

                                    DataTable oDtDatosRetorna = oHp.EjecutaOperacionRetornaDatos2(strSQL, VariablesGenerales.pConnect);
                                    if (oDtDatosRetorna != null)
                                    {
                                        if (oDtDatosRetorna.Rows.Count > 0)
                                        {
                                            foreach (DataRow oDrDatoElimina in oDtDatosRetorna.Rows)
                                            {
                                                if (CopiarFoto(oDrDatoElimina["ruta_imagen_origen"].ToString(), oDrDatoElimina["ruta_imagen_destino"].ToString()) == true)
                                                {
                                                    ValidaCopia = true;
                                                }
                                            }

                                            if (ValidaCopia == true)
                                            {
                                                rpt = MessageBox.Show("La copia se ha realizado satisfactoriamente , desea copiar más?", "Pregunta", MessageBoxButtons.YesNo);
                                                if (DialogResult.Yes == rpt)
                                                {
                                                    CargaGrilla();
                                                }
                                                else
                                                {
                                                    this.Close();
                                                }
                                            }
                                        }
                                    }
                                }                                   
                            }
                            else
                            {
                                MessageBox.Show("Debe Seleccionar Al Menos un Registro", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }                       
                        }
                        break;

                    case "SOLICREAJUSTE":
                        if (gridEX1.RecordCount == 0) { return; }
                        SolicitudPrecio("R");
                        break;

                    case "SOLICVALIDA":
                        if (gridEX1.RecordCount == 0) { return; }
                        SolicitudPrecio("V");
                        break;

                    case "VEREPV":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaBibliotecaGraficos_VerEPV overEPV = new FrmBandejaBibliotecaGraficos_VerEPV();
                        overEPV.TxtIdProceso.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Proceso_Imagen"].Index).ToString();
                        overEPV.CargaGrilla();
                        overEPV.ShowDialog();
                        break;

                    case "FECSOLICPROV":
                        //////////if (gridEX1.RecordCount == 0) { return; }
                        //////////FilaSeleccionado = gridEX1.CurrentRow.Position;
                        //////////FrmBandejaSeguimientoCotizacionPreciosTela_FecSolicProveedor oFrm = new FrmBandejaSeguimientoCotizacionPreciosTela_FecSolicProveedor();
                        //////////oFrm.oDrDataGrilla = oHp.ObtenerDr_DeGridEx(gridEX1);
                        //////////oFrm.TxtProveedor.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Proveedor"].Index).ToString();
                        //////////oFrm.TxtVersion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Version Cotizacion"].Index).ToString();
                        //////////oFrm.DtpFechaSolicitud.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Fecha Solicitud Cot Proveedor"].Index).ToString();
                        //////////oFrm.TxtObservaciones.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Observaciones Envio Proveedor"].Index).ToString();
                        //////////oFrm.ShowDialog();
                        //////////if (oFrm.IsCambioOK == true)
                        //////////{
                        //////////    CargaGrilla();
                        //////////    gridEX1.Row = FilaSeleccionado;
                        //////////}
                        //////////break;

                    case "FECATENCIONPROV":
                        ////////////////////////if (gridEX1.RecordCount == 0) { return; }
                        ////////////////////////FilaSeleccionado = gridEX1.CurrentRow.Position;
                        ////////////////////////FrmBandejaSeguimientoCotizacionPreciosTela_FecAtencionProveedor oAten = new FrmBandejaSeguimientoCotizacionPreciosTela_FecAtencionProveedor();
                        ////////////////////////oAten.oDrDataGrilla = oHp.ObtenerDr_DeGridEx(gridEX1);
                        ////////////////////////oAten.TxtProveedor.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Proveedor"].Index).ToString();
                        ////////////////////////oAten.TxtVersion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Version Cotizacion"].Index).ToString();
                        ////////////////////////oAten.DtpFechaSolicitud.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Fecha Solicitud Cot Proveedor"].Index).ToString();
                        ////////////////////////oAten.TxtObservaciones.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Observaciones Envio Proveedor"].Index).ToString();
                        ////////////////////////oAten.ShowDialog();
                        ////////////////////////if (oAten.IsCambioOK == true)
                        ////////////////////////{
                        ////////////////////////    CargaGrilla();
                        ////////////////////////    gridEX1.Row = FilaSeleccionado;
                        ////////////////////////}
                        ////////////////////////break;

                    case "OBSERVACION":
                        //////////////////if (gridEX1.RecordCount == 0) { return; }
                        //////////////////FilaSeleccionado = gridEX1.CurrentRow.Position;

                        //////////////////if (gridEX1.GetValue(gridEX1.RootTable.Columns["Proveedor"].Index).ToString().Trim() == "")
                        //////////////////{
                        //////////////////    MessageBox.Show("Registro no tiene Proveedor asignado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //////////////////    return;
                        //////////////////}

                        //////////////////FrmBandejaSeguimientoCotizacionPreciosTela_Observaciones oFrm2 = new FrmBandejaSeguimientoCotizacionPreciosTela_Observaciones();
                        //////////////////oFrm2.oDrDataGrilla = oHp.ObtenerDr_DeGridEx(gridEX1);
                        //////////////////oFrm2.TxtProveedor.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Proveedor"].Index).ToString();
                        //////////////////oFrm2.TxtVersion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Version Cotizacion"].Index).ToString();
                        //////////////////oFrm2.TxtObservaciones.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Observacion"].Index).ToString();
                        //////////////////oFrm2.ShowDialog();
                        //////////////////if (oFrm2.IsCambioOK == true)
                        //////////////////{
                        //////////////////    CargaGrilla();
                        //////////////////    gridEX1.Row = FilaSeleccionado;
                        //////////////////}
                        //////////////////break;

                    case "ESTADOSCOM":
                        ////////////if (gridEX1.RecordCount == 0) { return; }
                        ////////////FilaSeleccionado = gridEX1.CurrentRow.Position;

                        ////////////if (gridEX1.GetValue(gridEX1.RootTable.Columns["Proveedor"].Index).ToString().Trim() == "")
                        ////////////{
                        ////////////    MessageBox.Show("Registro no tiene Proveedor asignado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ////////////    return;
                        ////////////}

                        ////////////FrmBandejaSeguimientoCotizacionPreciosTela_EstadosComercial oEst = new FrmBandejaSeguimientoCotizacionPreciosTela_EstadosComercial();
                        ////////////oEst.oDrDataGrilla = oHp.ObtenerDr_DeGridEx(gridEX1);
                        ////////////oEst.TxtProveedor.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Proveedor"].Index).ToString();
                        ////////////oEst.TxtVersion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Version Cotizacion"].Index).ToString();
                        ////////////oEst.TxtCodEstado.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Status_Aprobacion_Precio"].Index).ToString();
                        ////////////oEst.TxtDesEstado.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Nombre_Aprobacion_Precio"].Index).ToString();
                        ////////////oEst.ShowDialog();
                        ////////////if (oEst.IsCambioOK == true)
                        ////////////{
                        ////////////    CargaGrilla();
                        ////////////    gridEX1.Row = FilaSeleccionado;
                        ////////////}
                        ////////////break;

                    case "BITACORAESTADO":
                        ////////////if (gridEX1.RecordCount == 0) { return; }
                        ////////////FrmBandejaSeguimientoCotizacionPreciosTela_BitacoraEstado oBtEst = new FrmBandejaSeguimientoCotizacionPreciosTela_BitacoraEstado();
                        ////////////oBtEst.oDrDataGrilla = oHp.ObtenerDr_DeGridEx(gridEX1);
                        ////////////oBtEst.TxtTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString() + " - " + gridEX1.GetValue(gridEX1.RootTable.Columns["DESC TELA COMERCIAL"].Index).ToString();
                        ////////////oBtEst.TxtCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD COMB"].Index).ToString();
                        ////////////oBtEst.CargaGrilla();
                        ////////////oBtEst.ShowDialog();
                        ////////////break;                    

                    case "REGISPRECIO":
                        if (gridEX1.RecordCount == 0) { return; }
                        FilaSeleccionado = gridEX1.CurrentRow.Position;
                        string AgrupacionPrecio;

                        if (gridEX1.GetValue(gridEX1.RootTable.Columns["Proveedor"].Index).ToString().Trim() == "")
                        {
                            MessageBox.Show("SMT sin Proveedor asignado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (gridEX1.GetValue(gridEX1.RootTable.Columns["Agrupacion Precios"].Index).ToString().Trim() != "")
                        {
                            AgrupacionPrecio = gridEX1.GetValue(gridEX1.RootTable.Columns["Agrupacion Precios"].Index).ToString().Trim().Substring(0, 1);

                            if (AgrupacionPrecio == "1")
                            {
                                object oForm1 = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "FrmShowTelas_PreciosProveedoresClienteTemporada");
                                ((dynamic)oForm1).TxtCodTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                                ((dynamic)oForm1).TxtDesTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["DESC TELA COMERCIAL"].Index).ToString();
                                ((dynamic)oForm1).TxtCodRuta.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD RUTA"].Index).ToString();
                                ((dynamic)oForm1).TxtDesRuta.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["NOMBRE RUTA"].Index).ToString();
                                ((dynamic)oForm1).CodClienteDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["CLIENTE"].Index).ToString();
                                ((dynamic)oForm1).DesClienteDesdePrecios = oHp.DevuelveDato("select Nom_Cliente from  TG_Cliente where cod_cliente = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString() + "'", VariablesGenerales.pConnect).ToString();
                                ((dynamic)oForm1).CodTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TEMCLI"].Index).ToString();
                                ((dynamic)oForm1).DesTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["TEMPORADA"].Index).ToString();
                                ((dynamic)oForm1).CodProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm1).DesProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm1).LlamadoDesdePreciosTela = true;
                                ((dynamic)oForm1).ShowDialog();
                            }
                            else if (AgrupacionPrecio == "2")
                            {
                                object oForm2 = oHp.GetFormDesdeOtroProyecto("CotizacionComercial", ".exe", "FrmPreciosCompraTelasHeather");
                                ((dynamic)oForm2).TxtCodTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                                ((dynamic)oForm2).TxtDesTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["DESC TELA COMERCIAL"].Index).ToString();
                                ((dynamic)oForm2).TxtCodCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD COMB"].Index).ToString();
                                ((dynamic)oForm2).TxtDesCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["NOMBRE COMB."].Index).ToString();
                                ((dynamic)oForm2).BusquedaDesdeLlamadaTela = true;
                                ((dynamic)oForm2).CodClienteDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["CLIENTE"].Index).ToString();
                                ((dynamic)oForm2).DesClienteDesdePrecios = oHp.DevuelveDato("select Nom_Cliente from  TG_Cliente where cod_cliente = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString() + "'", VariablesGenerales.pConnect).ToString();
                                ((dynamic)oForm2).CodTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TEMCLI"].Index).ToString();
                                ((dynamic)oForm2).DesTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["TEMPORADA"].Index).ToString();
                                ((dynamic)oForm2).CodProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm2).DesProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm2).LlamadoDesdePreciosTela = true;
                                ((dynamic)oForm2).ShowDialog();
                            }
                            else if (AgrupacionPrecio == "3")
                            {
                                object oForm3 = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "frmShowTX_Rapport_PreciosPrvoveedores");
                                ((dynamic)oForm3).txtCod_Tela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                                ((dynamic)oForm3).txtDsc_Tela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["DESC TELA COMERCIAL"].Index).ToString();
                                ((dynamic)oForm3).TxtRapport.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["RAPPORT"].Index).ToString();
                                ((dynamic)oForm3).CodClienteDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["CLIENTE"].Index).ToString();
                                ((dynamic)oForm3).DesClienteDesdePrecios = oHp.DevuelveDato("select Nom_Cliente from  TG_Cliente where cod_cliente = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString() + "'", VariablesGenerales.pConnect).ToString();
                                ((dynamic)oForm3).CodTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TEMCLI"].Index).ToString();
                                ((dynamic)oForm3).DesTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["TEMPORADA"].Index).ToString();
                                ((dynamic)oForm3).CodProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm3).DesProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm3).LlamadoDesdePreciosTela = true;
                                ((dynamic)oForm3).ShowDialog();
                            }
                            else if (AgrupacionPrecio == "4")
                            {
                                object oForm4 = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "FrmManTela_DisEstampado_PreciosMT");
                                ((dynamic)oForm4).sCodCliente = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString();
                                ((dynamic)oForm4).TxtCliente.Text = oHp.DevuelveDato("select Nom_Cliente from  TG_Cliente where cod_cliente = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString() + "'", VariablesGenerales.pConnect).ToString();
                                ((dynamic)oForm4).TxtItem.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD ESTAMPADO"].Index).ToString();
                                ((dynamic)oForm4).CodClienteDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["CLIENTE"].Index).ToString();
                                ((dynamic)oForm4).DesClienteDesdePrecios = oHp.DevuelveDato("select Nom_Cliente from  TG_Cliente where cod_cliente = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString() + "'", VariablesGenerales.pConnect).ToString();
                                ((dynamic)oForm4).CodTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TEMCLI"].Index).ToString();
                                ((dynamic)oForm4).DesTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["TEMPORADA"].Index).ToString();
                                ((dynamic)oForm4).CodProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm4).DesProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm4).LlamadoDesdePreciosTela = true;
                                ((dynamic)oForm4).ShowDialog();
                            }
                            else if (AgrupacionPrecio == "5")
                            {
                                object oForm1 = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "FrmShowTelas_PreciosProveedoresClienteTemporada");
                                ((dynamic)oForm1).TxtCodTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                                ((dynamic)oForm1).TxtDesTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["DESC TELA COMERCIAL"].Index).ToString();
                                ((dynamic)oForm1).TxtCodCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD COMB"].Index).ToString();
                                ((dynamic)oForm1).TxtDesCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["NOMBRE COMB."].Index).ToString();
                                ((dynamic)oForm1).CodClienteDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["CLIENTE"].Index).ToString();
                                ((dynamic)oForm1).DesClienteDesdePrecios = oHp.DevuelveDato("select Nom_Cliente from  TG_Cliente where cod_cliente = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString() + "'", VariablesGenerales.pConnect).ToString();
                                ((dynamic)oForm1).CodTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TEMCLI"].Index).ToString();
                                ((dynamic)oForm1).DesTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["TEMPORADA"].Index).ToString();
                                ((dynamic)oForm1).CodProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm1).DesProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["PROVEEDOR"].Index).ToString();
                                ((dynamic)oForm1).LlamadoDesdePreciosTela = true;
                                ((dynamic)oForm1).LlamadoDesdePreciosTelaCombinacion = true;
                                ((dynamic)oForm1).ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("SMT no tiene una agrupacion de precios", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "IMPRIMIR":
                        if (gridEX1.RowCount == 0)
                            return;
                        string Ruta_ArchivoEx;

                        Ruta_ArchivoEx = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        Random numAleatorioEx = new Random(System.Convert.ToInt32(DateTime.Now.Ticks & int.MaxValue));
                        string RutaUniArchivoEx = string.Format(Ruta_ArchivoEx + @"\Export_{0}.xls", System.Convert.ToString(numAleatorioEx.Next()));
                        System.IO.FileStream fsEx = new System.IO.FileStream(RutaUniArchivoEx, System.IO.FileMode.Create);

                        gridEXExporter1.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows;
                        gridEXExporter1.GridEX = gridEX1;
                        gridEXExporter1.Export(fsEx);
                        fsEx.Close();
                        Process.Start(RutaUniArchivoEx);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SolicitudPrecio(string Opcion)
        {
            ////////////FrmBandejaSeguimientoCotizacionPreciosTela_SolicitudPrecio oSolP = new FrmBandejaSeguimientoCotizacionPreciosTela_SolicitudPrecio();
            ////////////oSolP.oDrDataGrilla = oHp.ObtenerDr_DeGridEx(gridEX1);
            ////////////oSolP.OpcionSolicitud = Opcion;
            ////////////if (Opcion == "S")
            ////////////{

            ////////////}
            ////////////else if(Opcion == "R")
            ////////////{
            ////////////    oSolP.lblReajuste.Visible = true;
            ////////////    oSolP.TxtCodMotivo.Visible = true;
            ////////////    oSolP.TxtDesMotivo.Visible = true;
            ////////////    oSolP.BtnMantMotivo.Visible = true;
            ////////////}
            ////////////else if(Opcion == "V")
            ////////////{
            ////////////    oSolP.lblTemporada.Visible = true;
            ////////////    oSolP.TxtCodTemporada.Visible = true;
            ////////////    oSolP.TxtDesTemporada.Visible = true;
            ////////////}
            ////////////oSolP.CargaGrillaProveedores();
            ////////////oSolP.ShowDialog();
            ////////////if (oSolP.IsCambioOK == true)
            ////////////{
            ////////////    CargaGrilla();
            ////////////    gridEX1.Row = FilaSeleccionado;
            ////////////}
        }

        private void Eliminar()
        {
            try
            {
                int contador = 0;
                int contBusqueda = 0;                
                    strSQL = string.Empty;
                    strSQL += "\n" + "EXEC UP_MAN_Es_Proceso_Imagen";
                    strSQL += "\n" + string.Format(" @accion                 ='{0}'", "D");
                    strSQL += "\n" + string.Format(",@Id_Proceso_Imagen      ='{0}'", gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Proceso_Imagen"].Index).ToString());
                    strSQL += "\n" + string.Format(",@cod_usuario            ='{0}'", VariablesGenerales.pUsuario);
                    strSQL += "\n" + string.Format(",@cod_estacion           ='{0}'", Environment.MachineName);

    

                if (oHp.EjecutarOperacion(strSQL) == false)
                    {
                        contador += 1;
                    }
                
                if (contador == 0)
                {
                    MessageBox.Show("Registro eliminado correctamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargaGrilla();
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
                    strSQL += "\n" + "EXEC UP_MAN_Es_Proceso_Imagen";
                    strSQL += "\n" + string.Format(" @accion                 ='{0}'", "D");
                    strSQL += "\n" + string.Format(",@Id_Proceso_Imagen      ='{0}'", item.Cells["Id_Proceso_Imagen"].Value.ToString());                    
                    strSQL += "\n" + string.Format(",@cod_usuario            ='{0}'", VariablesGenerales.pUsuario);
                    strSQL += "\n" + string.Format(",@cod_estacion           ='{0}'", Environment.MachineName);

                    if (oHp.EjecutarOperacion(strSQL) == false)
                    {
                        contador += 1;
                    }else { contBusqueda += 1; }
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



        private GridEXFormatStyle oGridEXFormatStyle_SI = new GridEXFormatStyle() { ForeColor = Color.Blue, BackColor = Color.AliceBlue, TextAlignment = TextAlignment.Center };

        private void gridEX1_FormattingRow(object sender, RowLoadEventArgs e)
        {
            ////////try
            ////////{
            ////////    foreach (GridEXCell oGridEXCell in e.Row.Cells)
            ////////    {
            ////////        switch (oGridEXCell.Column.Key.ToUpper())
            ////////        {
            ////////            case "HEATHER":
            ////////                {
            ////////                    switch (oGridEXCell.Value)
            ////////                    {
            ////////                        case "SI":
            ////////                            {
            ////////                                oGridEXCell.FormatStyle = oGridEXFormatStyle_SI;
            ////////                                break;
            ////////                            }
            ////////                    }

            ////////                    break;
            ////////                }
            ////////        }
            ////////    }

            ////////    switch (e.Row.Cells["HEATHER"].Value)
            ////////    {
            ////////        case "SI":
            ////////            {
            ////////                e.Row.Cells["HEATHER"].CellDisplayType = ColumnType.Link;
            ////////                break;
            ////////            }
            ////////    }

            ////////    switch (e.Row.Cells["ESTADO PRODUCCION"].Value)
            ////////    {
            ////////        case "SI":
            ////////            {
            ////////                e.Row.Cells["ESTADO PRODUCCION"].CellDisplayType = ColumnType.Link;
            ////////                break;
            ////////            }
            ////////    }
            ////////}
            ////////catch (Exception ex)
            ////////{
            ////////}
        }

        private void gridEX1_LinkClicked(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (gridEX1.CurrentColumn == null) { return; }
                if (gridEX1.CurrentColumn.Key == null) { return; }

                switch (gridEX1.CurrentColumn.Key.ToUpper())
                {
                    case "SMTSEC":
                        {
                            DataRow oDr;

                            if (gridEX1.CurrentRow.Cells["SMTSec"].Value.ToString() == "") { return; }

                            strSQL = "EXEC tx_muestra_solicitud_telas_SEC '" + gridEX1.GetValue(gridEX1.RootTable.Columns["SMT"].Index).ToString() + "','" + gridEX1.GetValue(gridEX1.RootTable.Columns["SEC"].Index).ToString() + "'";
                            DataTable oDtDatosRetorna = oHp.EjecutaOperacionRetornaDatos2(strSQL, VariablesGenerales.pConnect);
                            if (oDtDatosRetorna != null)
                            {
                                if (oDtDatosRetorna.Rows.Count > 0)
                                {
                                    oDr = oDtDatosRetorna.Rows[0];

                                    object oForm = oHp.GetFormDesdeOtroProyecto("Planeamiento", ".exe", "frmAddSolicitudTelas");
                                    short vOut = Convert.ToInt16(oDr["Num_Solicitud"]);
                                    ((dynamic)oForm).ItNumero_Sol = vOut;
                                    ((dynamic)oForm).sCod_Motivo_Solicitud = oDr["Cod_Motivo_Solicitud"].ToString();

                                    if (oDr["Num_Secuencia"].ToString().Equals(""))
                                    { }
                                    else {
                                        short vOut2 = Convert.ToInt16(oDr["Num_Secuencia"]);
                                        ((dynamic)oForm).sNum_Secuencia = vOut2; 
                                    }

                                    ((dynamic) oForm).sCod_Tela = oDr["Cod_Tela_ASOCIADA"].ToString();
                                    ((dynamic)oForm).sAccion = "U";
                                    ((dynamic)oForm).Text = "Modifica Solicitud Nro :" + oDr["Num_Solicitud"].ToString();
                                    ((dynamic)oForm).GrbColorTenido.Visible = true;
                                    ((dynamic)oForm).TxtObservaciones.Text = oDr["Observacion_detalle"].ToString();
                                    ((dynamic)oForm).Txt_Carta.Text = oDr["corr_carta"].ToString();
                                    ((dynamic)oForm).txtCod_Color.Text = oDr["Cod_Color_ASOCIADO"].ToString();
                                    ((dynamic)oForm).txtCod_Color.Enabled = true;
                                    ((dynamic)oForm).txtDes_Color.Text = oDr["Des_Color_ASOCIADO"].ToString();
                                    ((dynamic)oForm).txtDes_Color.Enabled = true;
                                    ((dynamic)oForm).sCodigoCliente = oDr["Codigo_Cliente"].ToString();
                                    ((dynamic)oForm).sCodigoTemporada = oDr["Cod_TemCli"].ToString();
                                    ((dynamic)oForm).TxtCodModoSolicitud.Text = oDr["Modo_Solicitud"].ToString();
                                    ((dynamic)oForm).TxtDesModoSolicitud.Text = oHp.DevuelveDato("select Des_Modo_Solicitud from  TX_Modo_Solicitud_Desarrollo_Telas where Modo_Solicitud = '" + oDr["Modo_Solicitud"].ToString() + "'", VariablesGenerales.pConnect).ToString();
                                    ((dynamic)oForm).TxtTipoAprobacion.Text = oDr["Tipos_Aprobacion"].ToString();
                                    ((dynamic)oForm).TxpTipAprob.ciTXT_COD.Text = oDr["Cod_Tipo_Aprobacion"].ToString();
                                    ((dynamic)oForm).TxpTipAprob.ciTXT_DES.Text = oDr["Des_Tipo_Aprobacion"].ToString();
                                    ((dynamic)oForm).TxpPresentacionEnviado.ciTXT_COD.Text = oDr["Tipo_Muestra_Cliente_Recibido"].ToString();
                                    if(oDr["Tipo_Muestra_Cliente_Recibido"].ToString() != "   ")
                                    {
                                        ((dynamic)oForm).TxpPresentacionEnviado.ciTXT_DES.Text = oHp.DevuelveDato("SELECT Tipo_Muestra_Cliente FROM TG_Tipo_Muestra where Tipo_Muestra_Cliente = '" + oDr["Tipo_Muestra_Cliente_Recibido"].ToString() + "'", VariablesGenerales.pConnect).ToString();
                                    }
                                    ((dynamic)oForm).TxpPresentacionRequerido.ciTXT_COD.Text = oDr["Tipo_Muestra_Cliente_Requerido"].ToString();
                                    if(oDr["Tipo_Muestra_Cliente_Requerido"].ToString() .Trim()!= "")
                                    {
                                        ((dynamic)oForm).TxpPresentacionRequerido.ciTXT_DES.Text = oHp.DevuelveDato("SELECT Des_Corta_Tipo_Muestra_Cliente FROM TG_Tipo_Muestra where Tipo_Muestra_Cliente = '" + oDr["Tipo_Muestra_Cliente_Requerido"].ToString() + "'", VariablesGenerales.pConnect).ToString();
                                    }
                                    ((dynamic)oForm).vTitulo = oDr["Cod_Titulo"].ToString();
                                    ((dynamic)oForm).vDesTitulo = oDr["Nombre_titulo"].ToString();
                                    ((dynamic)oForm).vClaseHilado = oDr["Cod_ClaseHilado"].ToString();
                                    ((dynamic)oForm).vDesClaseHilado = oDr["Nombre_ClaseHilado"].ToString();
                                    ((dynamic)oForm).vProceso = oDr["COD_PROCESO_HILADO_DESARROLLO_TELA"].ToString();
                                    if (oDr["flg_es_complemento"].ToString() == "S") 
                                    {
                                        ((dynamic)oForm).chkcomplemento.CheckState = System.Windows.Forms.CheckState.Checked;
                                    }
                                    else
                                    {
                                        ((dynamic)oForm).chkcomplemento.CheckState = System.Windows.Forms.CheckState.Unchecked;
                                    }
                                    ((dynamic)oForm).vFlgConSinLavado = oDr["Flg_ConLavado"].ToString();
                                    ((dynamic)oForm).Txt_Carta.Enabled = false;
                                    ((dynamic)oForm).txtCod_Tela.Text = oDr["Cod_Tela_ASOCIADA"].ToString();
                                    ((dynamic)oForm).txtCod_Tela.Enabled = true;
                                    ((dynamic)oForm).txtDes_Tela.Text = oDr["Des_Tela_ASOCIADA"].ToString();
                                    ((dynamic)oForm).txtDes_Tela.Enabled = true;
                                    ((dynamic)oForm).txtKgs_Reg.Text = oDr["Kgs_Requeridos"].ToString();
                                    ((dynamic)oForm).txtUni_Req.Text = oDr["Uni_Requeridos"].ToString();
                                    ((dynamic)oForm).txtCod_Color.Text = oDr["Cod_Color_ASOCIADO"].ToString();
                                    ((dynamic)oForm).txtCod_Color.Enabled = true;
                                    ((dynamic)oForm).txtDes_Color.Text = oDr["Des_Color_ASOCIADO"].ToString();
                                    ((dynamic)oForm).txtDes_Color.Enabled = true;
                                    ((dynamic)oForm).txtruta.Text = oDr["Ruta"].ToString();
                                    ((dynamic)oForm).TXTdesruta.Text = oDr["DesRuta"].ToString();
                                    ((dynamic)oForm).TxtCodMedida.Text = oDr["cod_medida"].ToString();
                                    ((dynamic)oForm).TxtDesMedida.Text = oDr["DES_MEDIDA"].ToString();
                                    ((dynamic)oForm).TxtCodCombinacion.Text = oDr["cod_comb"].ToString();
                                    ((dynamic)oForm).TxtDesCombinacion.Text = oDr["Des_Comb"].ToString();

                                    if (oDr["Fec_Entrega_Prevista"].ToString().Equals(""))
                                    { }
                                    else { ((dynamic)oForm).DtFchEntr.Value = Convert.ToDateTime( oDr["Fec_Entrega_Prevista"]); }

                                    if (oDr["Flg_Cliente_Envio_Muestra_Original"].ToString() == "S")
                                    {
                                        ((dynamic)oForm).chkFlg_Cliente_Envio_Muestra_Original.CheckState = System.Windows.Forms.CheckState.Checked;
                                    }
                                    else
                                    {
                                        ((dynamic)oForm).chkFlg_Cliente_Envio_Muestra_Original.CheckState = System.Windows.Forms.CheckState.Unchecked;
                                    }

                                    ((dynamic)oForm).BtnAceptar.Visible = false;
                                    ((dynamic)oForm).TxtObservacionComplemento.Text = oDr["Observacion_Complemento"].ToString();
                                    ((dynamic)oForm).TxtRutaEstampado.Text = oDr["Ruta_Estampado"].ToString();
                                    ((dynamic)oForm).TxtNumPdasProyectadas.Text = oDr["Num_Prendas_Proyectadas"].ToString();
                                    ((dynamic)oForm).TxtTargetTela.Text = oDr["PrecioObjetivoTelaParaCotizacion"].ToString();

                                    if (oDr["Flg_Estampado"].ToString() == "S")
                                    {
                                        ((dynamic)oForm).rbSiEstampado.Checked = true;
                                        ((dynamic)oForm).rbNoEstampado.Checked = false;
                                        ((dynamic)oForm).PanelProcesoEstampado.Enabled = true;
                                    }
                                    else
                                    {
                                        ((dynamic)oForm).rbSiEstampado.Checked = false;
                                        ((dynamic)oForm).rbNoEstampado.Checked = true;
                                        ((dynamic)oForm).PanelProcesoEstampado.Enabled = false;
                                    }

                                    if (System.Convert.ToBoolean(oDr["Flg_UDTColocaElProcesoDeEstampado"]) == true)
                                    {
                                        ((dynamic)oForm).rbSiIndicadoUDT.Checked = true;
                                        ((dynamic)oForm).rbNoIndicadoUDT.Checked = false;
                                        ((dynamic)oForm).Label25.Enabled = true;
                                        ((dynamic)oForm).TxtCodProcesoEstampado.Enabled = true;
                                        ((dynamic)oForm).TxtDesProcesoEstampado.Enabled = true;
                                        ((dynamic)oForm).TxtCodProcesoEstampado.Text = oDr["cod_proceso_tex_estampado"].ToString();
                                        ((dynamic)oForm).TxtDesProcesoEstampado.Text = oDr["DESCRIPCION_PROCESO_ESTAMPADO"].ToString();
                                    }
                                    else
                                    {
                                        ((dynamic)oForm).rbSiIndicadoUDT.Checked = false;
                                        ((dynamic)oForm).rbNoIndicadoUDT.Checked = true;
                                        ((dynamic)oForm).Label25.Enabled = false;
                                        ((dynamic)oForm).TxtCodProcesoEstampado.Enabled = false;
                                        ((dynamic)oForm).TxtDesProcesoEstampado.Enabled = false;
                                        ((dynamic)oForm).TxtCodProcesoEstampado.Text = "";
                                        ((dynamic)oForm).TxtDesProcesoEstampado.Text = "";
                                    }

                                    if (System.Convert.ToBoolean(oDr["Flg_ElUsuarioIndicaCodigoDeEstampadoDeTela"]) == true)
                                    {
                                        ((dynamic)oForm).rbSiCodigoEstampado.Checked = true;
                                        ((dynamic)oForm).rbNoCodigoEstampado.Checked = false;
                                        ((dynamic)oForm).Label35.Enabled = true;
                                        ((dynamic)oForm).TxtCodInternoEstampado.Enabled = true;
                                        ((dynamic)oForm).TxtDesInternoEstampado.Enabled = true;
                                        ((dynamic)oForm).Label26.Enabled = false;
                                        ((dynamic)oForm).TxtCAD.Enabled = false;
                                        ((dynamic)oForm).TxtCodInternoEstampado.Text = oDr["cod_estampadoentela"].ToString();
                                        ((dynamic)oForm).TxtDesInternoEstampado.Text = oDr["DESCRIPCIONITEM"].ToString();
                                        ((dynamic)oForm).TxtCAD.Text = oDr["CodigoCAD_Estampado"].ToString();
                                    }
                                    else
                                    {
                                        ((dynamic)oForm).rbSiCodigoEstampado.Checked = false;
                                        ((dynamic)oForm).rbNoCodigoEstampado.Checked = true;
                                        ((dynamic)oForm).Label35.Enabled = false;
                                        ((dynamic)oForm).TxtCodInternoEstampado.Enabled = false;
                                        ((dynamic)oForm).TxtDesInternoEstampado.Enabled = false;
                                        ((dynamic)oForm).Label26.Enabled = true;
                                        ((dynamic)oForm).TxtCAD.Enabled = true;
                                        ((dynamic)oForm).TxtCodInternoEstampado.Text = "";
                                        ((dynamic)oForm).TxtDesInternoEstampado.Text = "";
                                        ((dynamic)oForm).TxtCAD.Text = oDr["CodigoCAD_Estampado"].ToString();
                                    }

                                    if (System.Convert.ToBoolean(oDr["Flg_LaTelaEnManufacturaLLevaProcesoEnPrenda"]) == true)
                                    {
                                        ((dynamic)oForm).PanelProcesoPrenda.Enabled = true;
                                        ((dynamic)oForm).rbSiProcesoPrenda.Checked = true;
                                        ((dynamic)oForm).rbNoProcesoPrenda.Checked = false;
                                        ((dynamic)oForm).Carga_Proceso();
                                        ((dynamic)oForm).TxtObservacionPrenda.Text = oDr["Obs_ProcesoEnPrenda"].ToString();
                                    }
                                    else
                                    {
                                        ((dynamic)oForm).PanelProcesoPrenda.Enabled = false;
                                        ((dynamic)oForm).rbSiProcesoPrenda.Checked = false;
                                        ((dynamic)oForm).rbNoProcesoPrenda.Checked = true;
                                        ((dynamic)oForm).Carga_Proceso();
                                        ((dynamic)oForm).TxtObservacionPrenda.Text = "";
                                    }

                                    if (System.Convert.ToBoolean(oDr["Flg_LaTelaLLevaLavadoEnPanos"]) == true)
                                    {
                                        ((dynamic)oForm).PanelProcesoPanos.Enabled = true;
                                        ((dynamic)oForm).rbSiProcesoPanos.Checked = true;
                                        ((dynamic)oForm).rbNoProcesoPanos.Checked = false;
                                        ((dynamic)oForm).TxtObservacionLavadoPanos.Text = oDr["observacion_lavado_en_panos"].ToString();
                                    }                                    
                                    else
                                    {
                                        ((dynamic)oForm).PanelProcesoPanos.Enabled = false;
                                        ((dynamic)oForm).rbSiProcesoPanos.Checked = false;
                                        ((dynamic)oForm).rbNoProcesoPanos.Checked = true;
                                        ((dynamic)oForm).TxtObservacionLavadoPanos.Text = "";
                                    }
                                    ((dynamic)oForm).OpcionSoloConsulta = true;
                                    ((dynamic)oForm).TxtObservacionEstampadoPrenda.Text = oDr["Observacion_Estampado_Prenda"].ToString();
                                    ((dynamic)oForm).ShowDialog();
                                }
                            }
                            break;
                        }

                    case "COD_TELA":
                        {
                            if(gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString() == "") { return; }

                            object oForm = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "frmManTela");
                            ((dynamic)oForm).RbTela.Checked = true;
                            ((dynamic)oForm).CodigoTela = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                            ((dynamic)oForm).Busqueda_Desde_EP = true;
                            ((dynamic)oForm).Opcion = 2;
                            ((dynamic)oForm).GrpDescripcionComercial.Visible = false;
                            ((dynamic)oForm).GrpFamilia.Visible = false;
                            ((dynamic)oForm).GrpTela.Visible = true;
                            ((dynamic)oForm).TxpFiltroTela.ciTXT_COD.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                            ((dynamic)oForm).TxpFiltroTela.ciTXT_DES.Text = oHp.DevuelveDato("select des_tela from tx_tela  where cod_tela = '" + gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString() + "'", VariablesGenerales.pConnect).ToString();
                            ((dynamic)oForm).CargaCombos();
                            ((dynamic)oForm).Carga_Grid();
                            ((dynamic)oForm).Estado();
                            ((dynamic)oForm).panCabecera.Enabled = true;
                            ((dynamic)oForm).ShowDialog();
                            break;
                        }

                    case "RAPPORT":
                        {
                            object oForm = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "frmShowTX_Rapport");
                           ((dynamic)oForm).TxpTela.ciTXT_COD.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                           ((dynamic)oForm).TxpTela.ciTXT_DES.Text = oHp.DevuelveDato("select des_tela from tx_tela  where cod_tela = '" + gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString() + "'", VariablesGenerales.pConnect).ToString();
                            ((dynamic)oForm).TxtRapport.Text = gridEX1.CurrentRow.Cells["RAPPORT"].Value.ToString();
                           ((dynamic)oForm).Option1.Checked = true;
                           ((dynamic)oForm).Opcion = 1;
                           ((dynamic)oForm).PanTela.Visible = false;
                           ((dynamic)oForm).PanRN.Visible = true;
                           ((dynamic)oForm).PanCliente.Visible = false;
                           ((dynamic)oForm).CargaRapport();
                           ((dynamic)oForm).ShowDialog();
                            break;
                        }

                    case "COD ESTAMPADO":
                        {

                            if(gridEX1.CurrentRow.Cells["COD ESTAMPADO"].Value.ToString() == "")
                                return;
                            DataTable oDTConsultaEstampado;
                            DataRow oDrEstampado;

                            strSQL = string.Empty;
                            strSQL += "\n" + "EXEC Es_EstampadosFullCobertura_Mostrar";
                            strSQL += "\n" + string.Format("  @Cod_Item                   = '{0}'", gridEX1.CurrentRow.Cells["COD ESTAMPADO"].Value.ToString());

                            oDTConsultaEstampado = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                            oDrEstampado = oDTConsultaEstampado.Rows[0];

                            object oForm = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "FrmManTela_DisEstampado_Mant_Add");
                            ((dynamic)oForm).Text = "Modificar Item";
                            ((dynamic)oForm).sCodTela = gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString();
                            ((dynamic)oForm).Opcion = "1";
                            ((dynamic)oForm).sCodCliente = gridEX1.CurrentRow.Cells["COD_CLIENTE"].Value.ToString();
                            ((dynamic)oForm).TxpCliente.ciTXT_COD.Text = oHp.DevuelveDato("select Abr_Cliente from tg_cliente where Cod_Cliente = '" + gridEX1.CurrentRow.Cells["COD_CLIENTE"].Value.ToString() + "'", VariablesGenerales.pConnect).ToString();
                            ((dynamic)oForm).TxpCliente.ciTXT_DES.Text = gridEX1.CurrentRow.Cells["Cliente"].Value.ToString();  
                            ((dynamic)oForm).TxpTemporada.ciTXT_COD.Text = gridEX1.CurrentRow.Cells["Cod_TemCli"].Value.ToString(); 
                            ((dynamic)oForm).TxpTemporada.ciTXT_DES.Text = gridEX1.CurrentRow.Cells["Temporada"].Value.ToString();  
                            ((dynamic)oForm).TxtNombreProtegido.Text = oHp.DevuelveDato("select dbo.es_obtiene_descripcion_automatica_estampado_tela('" + oDrEstampado["cod_tecnica"].ToString() + "','" + gridEX1.CurrentRow.Cells["COD_CLIENTE"].Value.ToString() + "','" + oDrEstampado["cod_cliente_estampado_cut"].ToString() + "')", VariablesGenerales.pConnect).ToString();
                            ((dynamic)oForm).txtCodProveedor.Text = oDrEstampado["Cod_Proveedor"].ToString();
                            ((dynamic)oForm).txtNombreProveedor.Text = oDrEstampado["Proveedor"].ToString();
                            ((dynamic)oForm).txtCodItemProv.Text = oDrEstampado["Cod_ItemProv"].ToString();
                            ((dynamic)oForm).txtPrecio.Text = oDrEstampado["Pre_Cotizado"].ToString();
                            ((dynamic)oForm).txtUniMedProv.Text = oDrEstampado["Cod_UniMedProv"].ToString();
                            ((dynamic)oForm).txtObservacionesProv.Text = oDrEstampado["ObservacionesProveedor"].ToString();
                            ((dynamic)oForm).txtcoditem.Text = oDrEstampado["CODIGO"].ToString();
                            ((dynamic)oForm).txtDesItem.Text = oDrEstampado["DESCRIPCION"].ToString();
                            ((dynamic)oForm).txtCodFamilia.Text = oDrEstampado["cod_FamItem"].ToString();
                            ((dynamic)oForm).txtDesFamilia.Text = oDrEstampado["des_famitem"].ToString();
                            ((dynamic)oForm).txtCodUM.Text = oDrEstampado["cod_UniMed"].ToString();
                            ((dynamic)oForm).txtDesUM.Text = oDrEstampado["Des_UniMed"].ToString();
                            ((dynamic)oForm).TxtModo.Text = oDrEstampado["Flg_ModoProceso"].ToString();
                            ((dynamic)oForm).TxtDes_modo.Text = oDrEstampado["Des_ModoProceso"].ToString();
                            ((dynamic)oForm).txtCodTecnica.Text = oDrEstampado["cod_tecnica"].ToString();
                            ((dynamic)oForm).TxtDesTecnica.Text = oDrEstampado["descripcion_tecnica"].ToString();
                            ((dynamic)oForm).txtGradoDif.Text = oDrEstampado["Tipo_Grado_Dificultad"].ToString();
                            ((dynamic)oForm).txtGradodifDes.Text = oDrEstampado["descripcion_Grado_Dificultad"].ToString();
                            ((dynamic)oForm).txtPrecioComercial.Text = oDrEstampado["PRECIO_COTIZACION_ARTES"].ToString();
                            ((dynamic)oForm).TxtCodCUT.Text = oDrEstampado["cod_cliente_estampado_cut"].ToString();
                            ((dynamic)oForm).TtColorBase.Text = oDrEstampado["color_base"].ToString();
                            ((dynamic)oForm).TxtCantidadColores.Text = oDrEstampado["Num_Colores_Bordado"].ToString();
                            ((dynamic)oForm).BtnAceptar.Visible = false;
                            ((dynamic)oForm).BtnMantTecnica.Visible = false;
                            ((dynamic)oForm).BtnArchivo.Visible = false;
                            ((dynamic)oForm).ShowDialog();
                            break;
                        }

                    case "JACQUARD":
                        {
                            object oForm = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "FrmTx_Jacquard");
                            ((dynamic)oForm).CodigoTela = gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString();
                            ((dynamic)oForm).PanJacquard.Visible = true;
                            ((dynamic)oForm).PanCliente.Visible = false;
                            ((dynamic)oForm).RbJacquard.Checked = true;
                            ((dynamic)oForm).nUpJacquard.Value = System.Convert.ToDecimal(gridEX1.CurrentRow.Cells["JACQUARD"].Value); 
                            ((dynamic)oForm).OpBusqueda = 1;
                            ((dynamic)oForm).Carga();
                            ((dynamic)oForm).ShowDialog();
                            break;
                        }

                    case "COD RUTA":
                        {
                            object oForm = oHp.GetFormDesdeOtroProyecto("TablasEst", ".exe", "FrmShowTelas");
                            ((dynamic)oForm).TxtCodTela.Text = gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString();
                            ((dynamic)oForm).TxtDesTela.Text = oHp.DevuelveDato("select des_tela from tx_tela  where cod_tela = '" + gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString() + "'", VariablesGenerales.pConnect).ToString();
                            ((dynamic)oForm).vCod_Tela = gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString();
                            ((dynamic)oForm).vDes_Tela = oHp.DevuelveDato("select des_tela from tx_tela  where cod_tela = '" + gridEX1.CurrentRow.Cells["cod_tela"].Value.ToString() + "'", VariablesGenerales.pConnect).ToString();
                            ((dynamic)oForm).BarraOpciones.Visible = false;
                            ((dynamic)oForm).ShowDialog();
                            break;
                        }

                    //////////////////case "ESTADO PRODUCCION":
                    //////////////////    {
                    //////////////////        FrmBandejaSeguimientoCotizacionPreciosTela_EstadoProd oFrm = new FrmBandejaSeguimientoCotizacionPreciosTela_EstadoProd();
                    //////////////////        oFrm.TxtTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString() + " - " + gridEX1.GetValue(gridEX1.RootTable.Columns["DESC TELA COMERCIAL"].Index).ToString();
                    //////////////////        oFrm.TxtCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD COMB"].Index).ToString();
                    //////////////////        oFrm.CodTelaSel = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                    //////////////////        oFrm.CodCombSel = gridEX1.GetValue(gridEX1.RootTable.Columns["COD COMB"].Index).ToString();
                    //////////////////        oFrm.CodRutaSel = gridEX1.GetValue(gridEX1.RootTable.Columns["COD RUTA"].Index).ToString();
                    //////////////////        oFrm.CodEstampadoSel = gridEX1.GetValue(gridEX1.RootTable.Columns["COD ESTAMPADO"].Index).ToString();
                    //////////////////        oFrm.CargaGrilla();
                    //////////////////        oFrm.ShowDialog();
                    //////////////////        break;
                    //////////////////    }

                    case "HEATHER":
                        {
                            object oForm2 = oHp.GetFormDesdeOtroProyecto("CotizacionComercial", ".exe", "FrmPreciosCompraTelasHeather");
                            ((dynamic)oForm2).TxtCodTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TELA"].Index).ToString();
                            ((dynamic)oForm2).TxtDesTela.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["DESC TELA COMERCIAL"].Index).ToString();
                            ((dynamic)oForm2).TxtCodCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["COD COMB"].Index).ToString();
                            ((dynamic)oForm2).TxtDesCombo.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["NOMBRE COMB."].Index).ToString();
                            ((dynamic)oForm2).BusquedaDesdeLlamadaTela = true;
                            ((dynamic)oForm2).CodClienteDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["CLIENTE"].Index).ToString();
                            ((dynamic)oForm2).DesClienteDesdePrecios = oHp.DevuelveDato("select Nom_Cliente from  TG_Cliente where cod_cliente = '" + gridEX1.GetValue(gridEX1.RootTable.Columns["COD_CLIENTE"].Index).ToString() + "'", VariablesGenerales.pConnect).ToString();
                            ((dynamic)oForm2).CodTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_TEMCLI"].Index).ToString();
                            ((dynamic)oForm2).DesTemporadaDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["TEMPORADA"].Index).ToString();
                            ((dynamic)oForm2).CodProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["COD_PROVEEDOR"].Index).ToString();
                            ((dynamic)oForm2).DesProveedorDesdePrecios = gridEX1.GetValue(gridEX1.RootTable.Columns["PROVEEDOR"].Index).ToString();
                            ((dynamic)oForm2).LlamadoDesdePreciosTela = false;
                            ((dynamic)oForm2).ShowDialog();
                            break;
                        }

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

        private void OptTipoSolicitudPrecio_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void BuscarProceso()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                
                oTipo.sQuery = "ES_muestra_Rutas_Proceso '2','','" + TxtProceso.Text + "'"; 


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtProceso.Tag = oTipo.dtResultados.Rows[0]["codigo"];
                    TxtProceso.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtProceso.Tag = oTipo.RegistroSeleccionado.Cells["codigo"].Value;
                        TxtProceso.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["descripcion"].Value);
                    }
                }
                TxtTipoGrafico.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void BuscarTipoGrafico()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();

                oTipo.sQuery = "Es_muestra_TiposImagenes_por_Proceso_Ruta_Manufactura '2','" + TxtProceso.Tag +"',0,'" + TxtTipoGrafico.Text + "'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtTipoGrafico.Tag = oTipo.dtResultados.Rows[0]["Tipo_Grafico"];
                    TxtTipoGrafico.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtTipoGrafico.Tag = oTipo.RegistroSeleccionado.Cells["Tipo_Grafico"].Value;
                        TxtTipoGrafico.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["descripcion"].Value);
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

        public void BuscarEstiloCliente()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();

                oTipo.sQuery = "Tg_muestra_ayuda_estcli '2','" + TxtClienteEstCli.Tag + "','','" + TxtEstiloCliente.Text + "'";
                    
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtEstiloCliente.Tag = oTipo.dtResultados.Rows[0]["Cod_EstCli"];
                    TxtEstiloCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Des_EstCli"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtEstiloCliente.Tag = oTipo.RegistroSeleccionado.Cells["Cod_EstCli"].Value;
                        TxtEstiloCliente.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Des_EstCli"].Value);
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




        public void BuscarTipoPrenda()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();

                oTipo.sQuery = "tg_muestra_ayuda_TipPre '2','','" +  TxtTipoPrenda.Text + "'";

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

        private void TxtProceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarProceso();
            }
        }

        private void TxtTipoGrafico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoGrafico();
            }
        }

        private void TxtEstiloCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarEstiloCliente();
            }
        }

        private void TxtTipoPrenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoPrenda();
            }
        }

        private void TxtClienteEstCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaCliente(true);
            }

        }

        private void TxtEstiloPropio_KeyPress(object sender, KeyPressEventArgs e)
        {           
            if (e.KeyChar == (char)13)
            {                
                
                TxtEstiloPropio.Text= TxtEstiloPropio.Text.PadLeft(5, '0');
                BtnBuscar.Focus();
            }
        }

        private void gridEX1_DoubleClick(object sender, EventArgs e)
        {
            //if (gridEX1.RowCount > 0)
            //{
            //    if (gridEX1.Col == gridEX1.RootTable.Columns["Imagen"].Index + 1)
            //    {
            //        DataRow odr;
            //        odr = oHp.ObtenerDr_DeGridEx(gridEX1);
            //        if (odr != null)
            //        {
            //            FrmBandejaBibliotecaGraficos_AddImagen oFrm = new FrmBandejaBibliotecaGraficos_AddImagen();
            //            oFrm.CodRutaImagen = odr["Imagen"].ToString();
            //            oFrm.ShowDialog();
            //        }
            //    }
            //}
        }

        public bool CopiarFoto(string vRutaOriginal, string vRutaDestino)
        {
            try
            {
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
                        System.IO.File.Copy(vRutaOriginal, vRutaDestino, true);
                    return true;
                }
            }
            catch (SqlException xSQLErr)
            {
                MessageBox.Show(xSQLErr.Message, "SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }
}
