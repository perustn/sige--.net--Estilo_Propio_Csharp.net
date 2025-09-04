using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using static Estilo_Propio_Csharp.FormularioProgreso;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBandejaControlFTPublicaciones : ProyectoBase.frmBase
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
        private bool isExecutionGeneracionFT = false;
        #endregion

        public FrmBandejaControlFTPublicaciones()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_Load(object sender, EventArgs e)
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

                DtpFechaInicio.Value = DateTime.Now;
                DtpFechaFinal.Value = DateTime.Now.AddDays(+30);

                oDT_StatusFT = oHp.DevuelveDatos("FT_Muestra_Status_FT_Publicacion", VariablesGenerales.pConnect);
                if (oDT_StatusFT.Rows.Count > 0)
                {
                    cboStatusFT.DataSource = oDT_StatusFT;
                    cboStatusFT.DisplayMember = "Des_Status";
                    cboStatusFT.ValueMember = "Status_FT_Publicacion";
                    cboStatusFT.SelectedIndex = 0;
                }
                else
                {
                    cboStatusFT.DataSource = null;
                    cboStatusFT.Text = "";
                }

                grpClienteTemporada.Visible = true;
                OpcionFiltro = "1";
                ActiveControl = TxtCliente;

                CreateTableFichasTecnicas();

                oSeg.GetBotonesJanus(VariablesGenerales.pCodPerfil, VariablesGenerales.pCodEmpresa, this.Name, buttonBar1, "");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateTableFichasTecnicas()
        {
            oDtEstructuraFTecnica = new DataTable("EstructuraFTecnica");
            oDtEstructuraFTecnica.Columns.Add("Cod_Cliente", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("Cod_TemCli", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("Cod_EstCli", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("Cod_EstPro", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("Cod_Version", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("AlternativaDeConsumo", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("Num_Cotizacion", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("OpcionCotizacion", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("PO", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("Cod_Fabrica", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("OP", Type.GetType("System.String"));
            oDtEstructuraFTecnica.Columns.Add("OM", Type.GetType("System.String"));
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
                        grpClienteTemporada.Visible = true;
                        TxtCliente.Select();
                        TxtCliente.Focus();
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
                        grpRangoFecha.Visible = true;
                        DtpFechaInicio.Focus();
                        break;
                    }

                case "4":
                    {
                        OpcionFiltro = "4";
                        grpIDPublicacion.Visible = true;
                        TxtIDPublicacion.Focus();
                        break;
                    }

                case "5":
                    {
                        OpcionFiltro = "5";
                        grpEstiloVersion.Visible = true;
                        TxtEP.Focus();
                        break;
                    }
                case "6":
                    {
                        OpcionFiltro = "6";
                        BtnBuscar.Focus();
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
            TxtOP.Text = "";
            DtpFechaInicio.Value = DateTime.Now;
            DtpFechaFinal.Value = DateTime.Now.AddDays(+30);
            TxtIDPublicacion.Text = "";
            TxtEP.Text = "";
            TxtVersion.Text = "";

            grpClienteTemporada.Visible = vbool;
            grpOP.Visible = vbool;
            grpRangoFecha.Visible = vbool;
            grpIDPublicacion.Visible = vbool;
            grpEstiloVersion.Visible = vbool;
        }

        private void TxtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaCliente();
            }
        }

        public void BuscaCliente()
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

        private void TxtOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtOP.Text = oHp.RellenaDeCerosEnIzquierda(TxtOP.Text, 5);
                BtnBuscar.Focus();
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

        private void TxtIDPublicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
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
            string codCliente = "";

            try
            {
                if (TxtCliente.Text == "")
                {
                    TxtCliente.Tag = "";
                }

                string ValorFt = "";
                if (cboStatusFT.SelectedValue != null)
                {
                    ValorFt = cboStatusFT.SelectedValue.ToString().Trim();
                }
                else
                {
                    ValorFt = "";
                }

                if (OpcionFiltro == "1") { codCliente = TxtCliente.Tag.ToString(); }

                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_Ver_Bandeja_por_OP_Status";
                strSQL += "\n" + string.Format("  @opcion               = '{0}'", OpcionFiltro);
                strSQL += "\n" + string.Format(", @cod_cliente			= '{0}'", codCliente);
                strSQL += "\n" + string.Format(", @cod_temcli			= '{0}'", TxtTemporada.Tag);
                strSQL += "\n" + string.Format(", @cod_ordpro			= '{0}'", TxtOP.Text);
                strSQL += "\n" + string.Format(", @desde			    = '{0}'", DtpFechaInicio.Value.ToShortDateString());
                strSQL += "\n" + string.Format(", @hasta			    = '{0}'", DtpFechaFinal.Value.ToShortDateString());
                strSQL += "\n" + string.Format(", @id_publicacion		= '{0}'", TxtIDPublicacion.Text);
                strSQL += "\n" + string.Format(", @cod_usuario			= '{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(", @status_ft_publicacion= '{0}'", ValorFt);
                strSQL += "\n" + string.Format(", @cod_estpro	    	= '{0}'", TxtEP.Text);
                strSQL += "\n" + string.Format(", @cod_version  		= '{0}'", TxtVersion.Text);

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

                        {
                            var withBlock2 = withBlock1.Columns[K_strColCheck];
                            withBlock2.HeaderImage = global::Estilo_Propio_Csharp.Properties.Resources.accept_16X16;
                            withBlock2.AllowDrag = false;
                            withBlock2.Caption = string.Empty;
                            withBlock2.Width = 60;
                            withBlock2.Visible = true;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.HeaderStyle = new GridEXFormatStyle() { FontSize = 1, TextAlignment = TextAlignment.Center, ForeColor = BackColor, ImageHorizontalAlignment = ImageHorizontalAlignment.Center, ImageVerticalAlignment = ImageVerticalAlignment.Center };
                        }

                        {
                            var withBlock2 = withBlock1.Columns["OP"];
                            withBlock2.Width = 70;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["EPV"];
                            withBlock2.Width = 70;
                            withBlock2.Visible = true;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["CLIENTE"];
                            withBlock2.Width = 120;
                            withBlock2.Visible = true;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["TEMPORADA"];
                            withBlock2.Width = 150;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["FechaExFact"];
                            withBlock2.Caption = "FECHA EXFACT";
                            withBlock2.Width = 90;
                            withBlock2.Visible = true;
                            withBlock2.FormatString = "dd/MM/yyyy";
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Pdas Sol."];
                            withBlock2.Width = 80;
                            withBlock2.Visible = true;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["PO"];
                            withBlock2.Width = 90;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Nombre Status Publicacion"];
                            withBlock2.Width = 150;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Descripcion_FT"];
                            withBlock2.Caption = "DESCRIPCION FT";
                            withBlock2.Visible = true;
                            withBlock2.Width = 150;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Observacion_FT"];
                            withBlock2.Caption = "OBSERVACION FT";
                            withBlock2.Visible = true;
                            withBlock2.Width = 150;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec Creacion"];
                            withBlock2.Visible = true;
                            withBlock2.Width = 90;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["SiglaVersion"];
                            withBlock2.Caption = "SIGLE VERSION";
                            withBlock2.Visible = true;
                            withBlock2.Width = 90;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["ComentariosPubl"];
                            withBlock2.Caption = "COMENTARIOS PUBLICACION";
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec 1ra Public"];
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec Ult Public"];
                            withBlock2.Width = 120;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Usuario 1ra Public"];
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Usuario Ult Public"];
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec 1ra Pre-Public"];
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec Ult Pre-Public"];
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Usuario 1ra Pre-Public"];
                            withBlock2.Width = 150;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Usuario ult Pre-Public"];
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Id_Publicacion"];
                            withBlock2.Caption = "ID PUBLICACION";
                            withBlock2.Width = 90;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Ops_Publicacion"];
                            withBlock2.Caption = "OPS PUBLICACION";
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["POR GENERAR PDF"];
                            withBlock2.Caption = "POR GENERAR PDF";
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["COD_MOTIVO"];
                            withBlock2.Caption = "COD MOTIVO";
                            withBlock2.Width = 80;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["NOMBRE_MOTIVO"];
                            withBlock2.Caption = "MOTIVO";
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["COD_MOTIVO_PARCIAL"];
                            withBlock2.Caption = "COD MOTIVO PARCIAL";
                            withBlock2.Width = 90;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["NOMBRE_MOTIVO_PARCIAL"];
                            withBlock2.Caption = "MOTIVO PARCIAL";
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Motivo_Rechazo"];
                            withBlock2.Caption = "COD MOTIVO RECHAZO";
                            withBlock2.Width = 80;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Nombre_Motivo_Rechazo"];
                            withBlock2.Caption = "MOTIVO RECHAZO";
                            withBlock2.Width = 110;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["ID_FichaTecnica"];
                            withBlock2.Caption = "ID FT";
                            withBlock2.Width = 80;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["cod_estcli"];
                            withBlock2.Caption = "ESTILO CLIENTE";
                            withBlock2.Width = 90;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Ult_Modificada_por_PrePublicar"];
                            withBlock2.Caption = "FECHA ULT MODIF. POR PREPUBLICAR";
                            withBlock2.Width = 150;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario_Ult_Modificada_por_PrePublicar"];
                            withBlock2.Caption = "USUARIO ULT MODIF. POR PREPUBLICAR";
                            withBlock2.Width = 150;
                            withBlock2.Visible = true;
                        }


                    }
                }
                gridEX1.FrozenColumns = 5;
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
                    case "CREARFT":
                        if (gridEX1.RecordCount == 0) { return; }
                        oDtEstructuraFTecnica.Clear();
                        ListaOpsSel = "";
                        foreach (GridEXRow oGridEXRow in gridEX1.GetDataRows())
                        {
                            if (Convert.ToBoolean(oGridEXRow.Cells["Flg"].Value) == true)
                            {
                                if(ListaOpsSel == "")
                                {
                                    ListaOpsSel = oGridEXRow.Cells["OP"].Value.ToString();
                                }
                                else
                                {
                                    ListaOpsSel = ListaOpsSel + "," + oGridEXRow.Cells["OP"].Value.ToString();
                                }                                
                                //DataRow oDrNUEVO = oDtEstructuraFTecnica.NewRow();
                                //oDrNUEVO["Cod_Cliente"] = oGridEXRow.Cells["Cod_Cliente"].Value;
                                //oDrNUEVO["Cod_TemCli"] = oGridEXRow.Cells["Cod_TemCli"].Value;
                                //oDrNUEVO["Cod_EstCli"] = oGridEXRow.Cells["Cod_EstCli"].Value;
                                //oDrNUEVO["Cod_EstPro"] = oGridEXRow.Cells["Cod_EstPro"].Value;
                                //oDrNUEVO["Cod_Version"] = oGridEXRow.Cells["Cod_Version"].Value;
                                //oDrNUEVO["AlternativaDeConsumo"] = 0;
                                //oDrNUEVO["Num_Cotizacion"] = 0;
                                //oDrNUEVO["OpcionCotizacion"] =0;
                                //oDrNUEVO["PO"] = oGridEXRow.Cells["PO"].Value;
                                //oDrNUEVO["Cod_Fabrica"] = "001";
                                //oDrNUEVO["OP"] = oGridEXRow.Cells["OP"].Value;
                                //oDrNUEVO["OM"] = "";
                                //oDtEstructuraFTecnica.Rows.Add(oDrNUEVO);
                            }
                        }

                        strSQL = string.Empty;
                        strSQL += "\n" + "EXEC FT_Obtiene_Requerimientos_Por_OP";
                        strSQL += "\n" + string.Format(" @ops           ='{0}'", ListaOpsSel);
                        strSQL += "\n" + string.Format(",@cod_temcli    ='{0}'", FiltroTemporadaSel);
                        oDtEstructuraFTecnica = oHp.EjecutaOperacionRetornaDatos2(strSQL,VariablesGenerales.pConnect);

                        FrmBandejaControlFTPublicaciones_CrearFT oFrm = new FrmBandejaControlFTPublicaciones_CrearFT();
                        oFrm.TxtEstiloPropio.Text = FiltroEPSel;
                        oFrm.TxtVersion.Text = FiltroVersionSel;
                        oFrm.oDtDatosSeleccionadosFT = oDtEstructuraFTecnica;
                        oFrm.ClienteSel = FiltroClienteSel;
                        oFrm.TemporadaSel = FiltroTemporadaSel;
                        oFrm.ShowDialog();
                        if (oFrm.IsCambioOK == true)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "VEROPS":
                        if (gridEX1.RecordCount == 0) { return; }
                        FilaSeleccionado = gridEX1.CurrentRow.Position;
                        FrmBandejaControlFTPublicaciones_VerOPs oAct = new FrmBandejaControlFTPublicaciones_VerOPs();
                        oAct.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oAct.IdFichaTecnicaSel = (int)gridEX1.GetValue(gridEX1.RootTable.Columns["Id_FichaTecnica"].Index);
                        oAct.EPSel = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_EstPro"].Index).ToString();
                        oAct.VersionSel = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Version"].Index).ToString();
                        oAct.CargaGrilla();
                        oAct.ShowDialog();
                        CargaGrilla();
                        break;

                    case "PREPUBLICAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaControlFTPublicaciones_PrePublicar oPrePubl = new FrmBandejaControlFTPublicaciones_PrePublicar();
                        oPrePubl.Text = "Cambio Status a Por PrePublicar";
                        oPrePubl.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oPrePubl.EstiloPropioSel = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_EstPro"].Index).ToString();
                        oPrePubl.Versionsel= gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Version"].Index).ToString();
                        oPrePubl.IdFichaTecnicaSel = (int)gridEX1.GetValue(gridEX1.RootTable.Columns["Id_FichaTecnica"].Index);
                        oPrePubl.CodigoClienteSel = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Cliente"].Index).ToString();
                        oPrePubl.TipoCambioStatus = "PREPUBLICAR";
                        oPrePubl.ShowDialog();
                        if (oPrePubl.IsCambioOK == true)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "MODPREPUBLICAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaControlFTPublicaciones_PrePublicar oMod = new FrmBandejaControlFTPublicaciones_PrePublicar();
                        oMod.Text = "Cambio Status a Modificada por PrePublicar";
                        oMod.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oMod.TipoCambioStatus = "MODPREPUBLICAR";
                        oMod.ShowDialog();
                        if (oMod.IsCambioOK == true)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "PUBLICAR":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaControlFTPublicaciones_PrePublicar oPubl = new FrmBandejaControlFTPublicaciones_PrePublicar();
                        oPubl.Text = "Cambio Status a Publicado";
                        oPubl.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oPubl.TipoCambioStatus = "PUBLICAR";
                        oPubl.ShowDialog();
                        if (oPubl.IsCambioOK == true)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "RECHAZARPREP":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaControlFTPublicaciones_RechazarPrePublicar oRec = new FrmBandejaControlFTPublicaciones_RechazarPrePublicar();
                        oRec.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oRec.ShowDialog();
                        if (oRec.IsCambioOK == true)
                        {
                            CargaGrilla();
                        }
                        break;

                    case "CREADOPREP":
                        if (gridEX1.RecordCount == 0) { return; }

                        break;

                    case "GENERARFT":
                        if (gridEX1.RecordCount == 0) { return; }
                        //if(gridEX1.GetValue(gridEX1.RootTable.Columns["POR GENERAR PDF"].Index) == "SI")
                        //{
                        
                        GeneraFTPDFAsync(gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_EstPro"].Index).ToString(),
                                        gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Version"].Index).ToString(), 
                                        (int)gridEX1.GetValue(gridEX1.RootTable.Columns["Id_FichaTecnica"].Index), 
                                        (int)gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index), 
                                        gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Cliente"].Index).ToString());
                        //}
                        break;

                    case "VERFT":
                        if (gridEX1.RecordCount == 0) { return; }
                        int xfilas;
                        string RutaOriginal;
                        xfilas = gridEX1.Row;
                        if (gridEX1.RowCount == 0)
                            return;
                        string sRutaTemp;
                        sRutaTemp = @"C:\\TEMP\\";

                        if (!System.IO.Directory.Exists(sRutaTemp))
                            System.IO.Directory.CreateDirectory(sRutaTemp);
                        RutaOriginal = gridEX1.GetValue(gridEX1.RootTable.Columns["FILE_ADJUNTO"].Index).ToString();
                        if (RutaOriginal == "")
                        {
                            return;
                        }

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

                                strSQL = string.Empty;
                                strSQL += "\n" + "EXEC FT_Save_Revisiones_por_Usuario";
                                strSQL += "\n" + string.Format(" @id_publicacion    = {0}", gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index));
                                strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                                strSQL += "\n" + string.Format(",@cod_estacion		='{0}'", Environment.MachineName);
                                oHp.EjecutarOperacion(strSQL);
                            }
                            else
                                MessageBox.Show("Archivo ya se encuentra Abierto por Usted. Favor de Revisar", "Mensaje");
                        }
                        else
                            MessageBox.Show("Archivo no existe", "Mensaje");
                        break;

                    case "AUTORIZAPUBLI":
                        if (gridEX1.RecordCount == 0) { return; }
                        rpt = MessageBox.Show("¿Está seguro de actualizar la publicación de la FT seleccionada?", "Pregunta", MessageBoxButtons.YesNo);
                        if (DialogResult.Yes == rpt)
                        {
                            strSQL = string.Empty;
                            strSQL += "\n" + "EXEC FT_Cambia_Status_a_Publicado_Autorizacion";
                            strSQL += "\n" + string.Format(" @id_publicacion    = {0}", gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index));
                            strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                            strSQL += "\n" + string.Format(",@cod_estacion		='{0}'", Environment.MachineName);

                            if (oHp.EjecutarOperacion(strSQL) == true)
                            {
                                CargaGrilla();
                            }
                        }
                        break;

                    case "VERMODIFICACION":
                        if (gridEX1.RecordCount == 0) { return; }
                        FilaSeleccionado = gridEX1.CurrentRow.Position;
                        FrmBandejaControlFTPublicaciones_VerModificaciones oVer = new FrmBandejaControlFTPublicaciones_VerModificaciones();
                        oVer.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oVer.CargaGrilla();
                        oVer.ShowDialog();
                        break;

                    case "VERPUBLICACIONES":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaControlFTPublicaciones_VerPublicaciones oVerPl = new FrmBandejaControlFTPublicaciones_VerPublicaciones();
                        oVerPl.TxtEstiloPropio.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_EstPro"].Index).ToString();
                        oVerPl.TxtVersion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Cod_Version"].Index).ToString();
                        oVerPl.TxtIDFT.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_FichaTecnica"].Index).ToString();
                        oVerPl.IdFichaTecnica = (int)gridEX1.GetValue(gridEX1.RootTable.Columns["Id_FichaTecnica"].Index);
                        oVerPl.CargaGrilla();
                        oVerPl.ShowDialog();
                        break;

                    case "VERDETRECHAZO":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaControlFTPublicaciones_VerDetRechazo oVerRech = new FrmBandejaControlFTPublicaciones_VerDetRechazo();
                        oVerRech.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oVerRech.CargaGrilla();
                        oVerRech.ShowDialog();
                        break;

                    case "USUARIOPUBLI":
                        if (gridEX1.RecordCount == 0) { return; }
                        FrmBandejaControlFTPublicaciones_UsuariosVerPbl oUsuPl = new FrmBandejaControlFTPublicaciones_UsuariosVerPbl();
                        oUsuPl.TxtIdPublicacion.Text = gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index).ToString();
                        oUsuPl.CargaGrilla();
                        oUsuPl.ShowDialog();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int lpdwProcessId);
        private GridEXFormatStyle objGridEXFormatStyle_Check = new GridEXFormatStyle() { FontSize = 1, BackColor = Color.AliceBlue, ForeColor = DefaultBackColor, TextAlignment = TextAlignment.Center, ImageHorizontalAlignment = ImageHorizontalAlignment.Center, ImageVerticalAlignment = ImageVerticalAlignment.Center };
        private GridEXFormatStyle objGridEXFormatStyle_UnCheck = new GridEXFormatStyle() { FontSize = 1, BackColor = Color.AliceBlue, ForeColor = DefaultBackColor, TextAlignment = TextAlignment.Center, ImageHorizontalAlignment = ImageHorizontalAlignment.Center, ImageVerticalAlignment = ImageVerticalAlignment.Center };

        private void gridEX1_FormattingRow(object sender, RowLoadEventArgs e)
        {
            try
            {
                foreach (GridEXColumn oGridEXColumn in gridEX1.RootTable.Columns)
                {
                    if (oGridEXColumn.Key.ToUpper() == K_strColCheck)
                    {
                        switch (e.Row.Cells[oGridEXColumn].Value)
                        {
                            case true:
                                {
                                    e.Row.Cells[oGridEXColumn].Image = global::Estilo_Propio_Csharp.Properties.Resources.accept_16X16;
                                    e.Row.Cells[oGridEXColumn].FormatStyle = objGridEXFormatStyle_Check;
                                    break;
                                }

                            case false:
                                {
                                    e.Row.Cells[oGridEXColumn].Image = null;
                                    e.Row.Cells[oGridEXColumn].FormatStyle = objGridEXFormatStyle_UnCheck;
                                    break;
                                }
                        }
                    }
                }

                if (e.Row.RowType == Janus.Windows.GridEX.RowType.Record)
                {
                    var valor = e.Row.Cells["POR GENERAR PDF"].Value;

                    if (valor != null && valor.ToString().Trim().ToUpper() == "SI")
                    {
                        // Pintamos toda la fila de color amarillo
                        e.Row.RowStyle.BackColor = Color.Yellow;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void gridEX1_EditingCell(object sender, EditingCellEventArgs e)
        {
            try
            {
                if (e.Column.Key != K_strColCheck)
                {
                    e.Cancel = true;
                    Cad1 = "0";
                }
                else
                {
                    e.Cancel = true;
                    Cad1 = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private object Val(object Expression)
        {
            throw new NotImplementedException();
        }

        private void gridEX1_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key.ToUpper() == K_strColCheck.ToUpper())
            {
                bool bolSW = System.Convert.ToBoolean(Val(e.Column.Caption));

                foreach (GridEXRow oGridEXRow in gridEX1.GetDataRows())
                {
                    {
                        var withBlock = oGridEXRow;
                        withBlock.BeginEdit();
                        withBlock.Cells[e.Column.Key].Value = bolSW;
                        withBlock.EndEdit();
                    }
                }
                e.Column.Caption = Val(!bolSW).ToString();
            }
            Cad1 = "1";
        }

        private void gridEX1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow oDr;
                int CountFilasSeleccion = 0;

                int RowIndex;
                int RegistroSeleccionado;

                if (gridEX1.CurrentColumn == null)
                    return;
                if (gridEX1.CurrentColumn.Key == null)
                    return;

                switch (gridEX1.CurrentColumn.Key.ToUpper())
                {
                    case "FLG":
                        {
                            var currentRow = gridEX1.CurrentRow;
                            currentRow.BeginEdit();
                            currentRow.Cells[gridEX1.CurrentColumn.Key].Value =
                                !(bool)gridEX1.CurrentRow.Cells[gridEX1.CurrentColumn.Key].Value;
                            currentRow.EndEdit();

                            RowIndex = gridEX1.Row;
                            foreach (GridEXRow oGridEXRow in gridEX1.GetDataRows())
                            {
                                if (Convert.ToBoolean(oGridEXRow.Cells["Flg"].Value) == true)
                                {
                                    CountFilasSeleccion = CountFilasSeleccion + 1;
                                    if (CountFilasSeleccion > 1)
                                        break;
                                }
                            }

                            if (CountFilasSeleccion == 1)
                            {
                                foreach (GridEXRow oGridEXRow in gridEX1.GetDataRows())
                                {
                                    if (Convert.ToBoolean(oGridEXRow.Cells["Flg"].Value) == true)
                                    {
                                        FiltroClienteSel = oGridEXRow.Cells["COD_CLIENTE"].Value.ToString();
                                        FiltroTemporadaSel = oGridEXRow.Cells["COD_TEMCLI"].Value.ToString();
                                        FiltroEPSel = oGridEXRow.Cells["COD_ESTPRO"].Value.ToString();
                                        FiltroVersionSel = oGridEXRow.Cells["COD_VERSION"].Value.ToString();
                                        FiltroIDFichaTecnicaSel = (int)oGridEXRow.Cells["ID_FICHATECNICA"].Value;
                                        break;
                                    }
                                }

                                oDtFiltraRegistros = oDtDataGlobal.Clone();

                                foreach (GridEXRow oGridEXRow in gridEX1.GetDataRows())
                                {
                                    DataRow dr = ((DataRowView)oGridEXRow.DataRow).Row;
                                    if (oGridEXRow.Cells["COD_CLIENTE"].Value.ToString().TrimEnd() == FiltroClienteSel.ToString().TrimEnd() &&
                                        oGridEXRow.Cells["COD_TEMCLI"].Value.ToString().TrimEnd() == FiltroTemporadaSel.ToString().TrimEnd() &&
                                        oGridEXRow.Cells["COD_ESTPRO"].Value.ToString().TrimEnd() == FiltroEPSel.ToString().TrimEnd() &&
                                        oGridEXRow.Cells["COD_VERSION"].Value.ToString().TrimEnd() == FiltroVersionSel.ToString().TrimEnd())
                                    {
                                        if (dr != null)
                                        {
                                            oDtFiltraRegistros.ImportRow(dr);
                                        }
                                    }
                                }
                                gridEX1.DataSource = oDtFiltraRegistros;
                            }
                            else if (CountFilasSeleccion == 0)
                            {
                                gridEX1.DataSource = oDtDataGlobal;

                                foreach (GridEXRow oGridEXRow in gridEX1.GetDataRows())
                                {
                                    oGridEXRow.BeginEdit();
                                    oGridEXRow.Cells["Flg"].Value = false;
                                    oGridEXRow.EndEdit();
                                }
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public async Task GeneraFTPDFAsync(string EstiloPropioSel,string Versionsel,int IdFichaTecnicaSel, int IDPublicacion, string CodigoClienteSel)
        {
            if (isExecutionGeneracionFT) {
                MessageBox.Show("Ya se esta procesando una FT", "Incormacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isExecutionGeneracionFT = true;

            var cancellationTokenSource = new CancellationTokenSource();
            var formProgreso = new FormularioProgreso();

            // Configurar eventos del formulario de progreso
            formProgreso.ConfigurarCancelacion(cancellationTokenSource);
            formProgreso.ProcesoCompletado += (s, e) => AgregarLog("✓ Proceso completado exitosamente");
            formProgreso.ProcesoCancelado += (s, e) => AgregarLog("⚠ Proceso cancelado por el usuario");
            formProgreso.ProcesoError += (s, ex) => AgregarLog($"✗ Error en proceso: {ex.Message}");

            try
            {
                // Mostrar formulario de progreso
                formProgreso.Show();
                AgregarLog("Iniciando proceso...");

                // Crear progress reporter
                var progress = new Progress<ProgresoInfo>(formProgreso.ActualizarProgreso);

                // Ejecutar tu método asíncrono
                //var resultado = await TuMetodoAsincrono(progress, cancellationTokenSource.Token);
                bool resultado = await GenFT.GenerarPDFAsync(EstiloPropioSel, Versionsel, IdFichaTecnicaSel, IDPublicacion, CodigoClienteSel,
                    progress, cancellationTokenSource.Token);

                if (resultado)
                {
                    // Mostrar completado
                    formProgreso.MostrarCompletado(
                        "Proceso finalizado",
                        $"Resultado: {resultado}"
                    );
                }
                AgregarLog($"Resultado del proceso: {resultado}");
            }
            catch (OperationCanceledException)
            {
                AgregarLog("Proceso cancelado");
                formProgreso.Close();
            }
            catch (Exception ex)
            {
                AgregarLog($"Error: {ex.Message}");
                formProgreso.MostrarError("Error en el proceso", ex.Message);
            }
            isExecutionGeneracionFT = false;

        }

        private void AgregarLog(string mensaje)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AgregarLog), mensaje);
                return;
            }
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {mensaje}\r\n");
            //txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensaje}\r\n");
            //txtLog.SelectionStart = txtLog.Text.Length;
            //txtLog.ScrollToCaret();
        }

    }
}
