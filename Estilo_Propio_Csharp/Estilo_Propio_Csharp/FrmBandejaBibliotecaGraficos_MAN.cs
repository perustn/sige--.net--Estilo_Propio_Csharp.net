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
    public partial class FrmBandejaBibliotecaGraficos_MAN : Form
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

        public Boolean LlamadaDesdeGraficosEP = false;
        public string vCodEstpro;
        public string vCodVersion;
        public string vCodigoCliente;
        public string vCodSecuencia;
        public string RutaArchivoDesdeEPV;
        public string NombreArchivoDesdeEPV;
        public string ResultArchivoDesdeEPV;

        public FrmBandejaBibliotecaGraficos_MAN()
        {
            InitializeComponent();
        }

        private void FrmBandejaSolicitudAplicacionesBDESTF_MAN_Load(object sender, EventArgs e)
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

                if (LlamadaDesdeGraficosEP == true)
                {
                    rutaArchivo = RutaArchivoDesdeEPV;
                    nombreArchivo = NombreArchivoDesdeEPV;
                    result = ResultArchivoDesdeEPV;
                    string strSQL = "SELECT RutaGrafico_Proceso_Imagen FROM TG_CONTROL";
                    object resultado = oHp.DevuelveDato(strSQL, VariablesGenerales.pConnect);
                    string rutaBase = resultado != null ? resultado.ToString() : string.Empty;
                    string newRuta = rutaBase + "_" + nombreArchivo + ".jpg";
                    TxtRutaImagen.Text = newRuta;
                }
            }            
        }       

        private void TxtCod_FamItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoRequerimiento(1);
            }
        }

        private void TxtDes_FamItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoRequerimiento(2);
            }
        }

        public void BuscarTipoRequerimiento(short tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();

                if (tipo == 1)
                    oTipo.sQuery = "Exec sm_muestra_procesos_manufactura_OM '1','" + TxtCod_FamItem.Text + "'";
                else
                    oTipo.sQuery = "Exec sm_muestra_procesos_manufactura_OM '2','','" + TxtDes_FamItem.Text + "'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCod_FamItem.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["codigo"]);
                    TxtDes_FamItem.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCod_FamItem.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["codigo"].Value);
                        TxtDes_FamItem.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["descripcion"].Value);
                    }
                }
                //TxtCodSubTipo_Req.Focus();
                TxtCodEtapa.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void TxtCodSubTipo_Req_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarSubTipoReq();
            }            
        }

        private void TxtDesSubTipo_Req_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarSubTipoReq();
            }
        }

        public void BuscarSubTipoReq()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec ap_up_man_Nombre_SubTipo_Requerimiento 'V','" + TxtCod_FamItem.Text + "'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodSubTipo_Req.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["SubTipo"]);
                    TxtDesSubTipo_Req.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Nombre SubTipo"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodSubTipo_Req.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["SubTipo"].Value);
                        TxtDesSubTipo_Req.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Nombre SubTipo"].Value);
                    }
                }
                TxtCodEtapa.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }           

        private void TxtCodEtapa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarEtapá();
            }
        }

        private void TxtDesEtapa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarEtapá();
            }
        }

        public void BuscarEtapá()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec ap_sm_muestra_AP_Etapa_Requerimiento";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodEtapa.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Etapa"]);
                    TxtDesEtapa.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Nombre"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodEtapa.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Etapa"].Value);
                        TxtDesEtapa.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Nombre"].Value);
                    }
                }
                dtpFecReqComercial.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFecReqComercial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtCliente.Focus();
            }
        }     

        private void TxtCodEstCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarEstiloCliente();
            }
        }

        private void TxtNomDiseno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtCod_Combinacion.Focus();
            }
        }
        private void TxtCombinacion_KeyPress(object sender, KeyPressEventArgs e)
        {
                     
                if (e.KeyChar == (char)13)
                {
                    BuscarCombinacion();
                }            
        }

        private void TxtCodTecnica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTecnica();
            }
        }

        private void TxtDesTecnica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTecnica();
            }
        }

        public void BuscarTecnica()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec ap_muestra_Es_Tecnica_Aplicaciones";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTecnica.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_tecnica"]);
                    TxtDesTecnica.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTecnica.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["cod_tecnica"].Value);
                        TxtDesTecnica.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["descripcion"].Value);
                    }
                }
                TxtCod_Ubicacion.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {                
                BuscarUbicacion();                
            }

        }

        private void TxtCodModoProc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarModoProc();
            }
        }

        private void TxtDesModoProc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarModoProc();
            }
        }

        public void BuscarModoProc()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec ap_muestra_es_modoproceso";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodModoProc.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["ModoProceso"]);
                    TxtDesModoProc.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodModoProc.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["ModoProceso"].Value);
                        TxtDesModoProc.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["descripcion"].Value);
                    }
                }
                TxtNumColores.Focus();
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
                TxtCodEstCli.Focus();
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


                oTipo.sQuery = "Exec sm_muestra_tg_estclitem '"+ TxtCliente.Tag +"','"+ TxtTemporada.Tag +"'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodEstCli.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Estilo Cliente"]);
                    TxtDesEstCli.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Nombre Estilo"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodEstCli.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Estilo Cliente"].Value);
                        TxtDesEstCli.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Nombre Estilo"].Value);
                    }
                }
                TxtNomDiseno.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtDesEstCli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarEstiloCliente();
            }
        }


        public void BuscarColorFondo()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec ap_busca_colores_desde_cartas_tela_cliente_temporada '"+ TxtCliente.Tag +"','"+ TxtTemporada.Tag +"'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodColor_FondoTela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_color"]);
                    TxtDesColor_FondoTela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["des_color"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodColor_FondoTela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["cod_color"].Value);
                        TxtDesColor_FondoTela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["des_color"].Value);
                    }
                }
                TxtNomDiseno.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        

        private void TxtDes_Tela_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (e.KeyChar == (char)13)
            {
                BuscarTela();
            }
        }


        public void BuscarTela()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec UP_SEL_tela_COMERCIAL_ATRIBUTOS '" + TxtDes_Tela.Text + "'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCod_Tela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_Tela"]);
                    TxtDes_Tela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["des_Tela_comercial"]);
                    TipoAdd = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_FamTela"]);
                    TipoAdd2 = Convert.ToString(oTipo.dtResultados.Rows[0]["Des_FamTela"]);
                    TipoAdd3 = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_tiptela"]);
                    TipoAdd4 = Convert.ToString(oTipo.dtResultados.Rows[0]["des_tiptela"]);
                    TipoAdd5 = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_SubTipTela"]);
                    TipoAdd6 = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion_subtipela"]);
                    TipoAdd7 = Convert.ToString(oTipo.dtResultados.Rows[0]["ComposicionComercialEnFibras"]);


                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCod_Tela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["cod_Tela"].Value);
                        TxtDes_Tela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["des_Tela_comercial"].Value);
                        TipoAdd = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_FamTela"].Value);
                        TipoAdd2 = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Des_FamTela"].Value);
                        TipoAdd3  = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_tiptela"].Value);
                        TipoAdd4 = Convert.ToString(oTipo.RegistroSeleccionado.Cells["des_tiptela"].Value);
                        TipoAdd5 = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_SubTipTela"].Value);
                        TipoAdd6 = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion_subtipela"].Value);
                        TipoAdd7 = Convert.ToString(oTipo.RegistroSeleccionado.Cells["ComposicionComercialEnFibras"].Value);
                    }
                }


                validaAtibutosTela(0);

                //TxtComposicion_Comercial.Focus();

                validaRutaTela();

                TxtCodRuta.Focus();


                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = ""; oTipo.oParent.TipoAdd2 = ""; oTipo.oParent.TipoAdd3 = ""; oTipo.oParent.TipoAdd4 = ""; oTipo.oParent.TipoAdd5 = ""; oTipo.oParent.TipoAdd6 = ""; oTipo.oParent.TipoAdd7 = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validaAtibutosTela(short flgCargaInicial)
        {
            if (TxtCod_Tela.Text.Trim() != "")
            {
                if (TipoAdd != "") { TxtCodTipo_Tej.Text = TipoAdd; }
                if (TipoAdd2 != "") { TxtDesTipo_Tej.Text = TipoAdd2; }
                if (TipoAdd3 != "") { TxtCodTipo_Tela.Text = TipoAdd3; }
                if (TipoAdd4 != "") { TxtDesTipo_Tela.Text = TipoAdd4; }
                if (TipoAdd5 != "") { TxtCodSubTipo_Tela.Text = TipoAdd5; }
                if (TipoAdd6 != "") { TxtDesSubTipo_Tela.Text = TipoAdd6; }
                if (TipoAdd7 != "") { TxtComposicion_Comercial.Text = TipoAdd7; }
                gbxTela.Enabled = false;
            }
            else
            {
                gbxTela.Enabled = true;

                if (flgCargaInicial !=1)
                { 
                TxtCodTipo_Tej.Text = "";
                TxtDesTipo_Tej.Text = "";
                TxtCodTipo_Tela.Text = "";
                TxtDesTipo_Tela.Text = "";
                TxtCodSubTipo_Tela.Text = "";
                TxtDesSubTipo_Tela.Text = "";
                TxtComposicion_Comercial.Text = "";
                    TxtCodRuta.Text = "";
                    TxtDesRuta.Text = "";
                }
            }

        }


        public void BuscarTipoTej()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec up_sel_famtela_comercial '" + TxtCod_Tela.Text + "'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTipo_Tej.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_famtela"]);
                    TxtDesTipo_Tej.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["des_famtela"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTipo_Tej.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["cod_famtela"].Value);
                        TxtDesTipo_Tej.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["des_famtela"].Value);
                    }
                }
                TxtCodTipo_Tela.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void BuscarTipoTela(short tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec TG_AYUDA_TG_TIPTELA '1'";

                if (tipo == 1)
                    oTipo.sQuery = "Exec TG_AYUDA_TG_TIPTELA '1','" + TxtCodTipo_Tela.Text + "'";
                else
                    oTipo.sQuery = "Exec TG_AYUDA_TG_TIPTELA '2','','" + TxtDesTipo_Tela.Text + "'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTipo_Tela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["codigo"]);
                    TxtDesTipo_Tela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTipo_Tela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["CODIGO"].Value);
                        TxtDesTipo_Tela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["DESCRIPCION"].Value);
                    }
                }
                TxtCodSubTipo_Tela.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void BuscarSubTipoTela()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec tx_muestra_Tg_TipTela_SubTipos '"+ TxtCodTipo_Tela.Text +"'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodSubTipo_Tela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_subtiptela"]);
                    TxtDesSubTipo_Tela.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion_subtipela"]);


                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodSubTipo_Tela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_SubTipTela"].Value);
                        TxtDesSubTipo_Tela.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion_subtipela"].Value);
                    }
                }
                TxtComposicion_Comercial.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCodTipo_Tej_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoTej();
            }
        }

        private void TxtDesTipo_Tej_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoTej();
            }
        }

        private void TxtCodTipo_Tela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoTela(1);
            }
        }

        private void TxtDesTipo_Tela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoTela(2);
            }
        }

        private void TxtCodSubTipo_Tela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarSubTipoTela();
            }
        }

        private void TxtDesSubTipo_Tela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarSubTipoTela();
            }
        }

        private void ChkColorFondoTela_CheckedChanged(object sender, EventArgs e)
        {
            Validacion_ColorFondoTela(0);
        }

        private void Validacion_ColorFondoTela(short flgCargaInicial)
        {
            if (ChkColorFondoTela.Checked)
            {
                gbxColorFondo.Visible = true;
                gbxCodColorFondo.Visible = false;

                if (flgCargaInicial!=1)
                { 
                TxtCodColor_FondoTela.Text = "";
                TxtDesColor_FondoTela.Text = "";
                }

            }
            else
            {
                if (flgCargaInicial != 1)
                { TxtColorFondo.Text = ""; }
                gbxColorFondo.Visible = false;
                gbxCodColorFondo.Visible = true;
            }

        }



        private void ChkCodTela_CheckedChanged(object sender, EventArgs e)
        {
            ValidacodTela(0);
        }

        private void ValidacodTela(short flgCargaInicial)
        {
            if (ChkCodTela.Checked)
            {
                gbxCodTela.Visible = true;
                TxtCodRuta.Visible = true;

            }
            else
            {
                gbxCodTela.Visible = false;
                gbxTela.Visible = true;

                TxtCodRuta.Visible = false;

                if (flgCargaInicial!= 1)
                { 
                TxtCod_Tela.Text = "";
                TxtDes_Tela.Text = "";

                TxtCodTipo_Tej.Text = "";
                TxtDesTipo_Tej.Text = "";
                TxtCodTipo_Tela.Text = "";
                TxtDesTipo_Tela.Text = "";
                TxtCodSubTipo_Tela.Text = "";
                TxtDesSubTipo_Tela.Text = "";
                    TxtComposicion_Comercial.Text = "";
                    TxtCodRuta.Text = "";
                    TxtDesRuta.Text = "";
                }
            }
        }

        private void TxtDes_Tela_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChkFibrasTenido_CheckedChanged(object sender, EventArgs e)
        {
            //validaFibrasTenido(0);

        }

        private void validaFibrasTenido(short flgCargaInicial)
        {
            if (ChkFibrasTenido.Checked)
            {
                if (flgCargaInicial !=1)
                { TxtTela_Estimada.Text = ""; }
                gbxTelaEstimada.Visible = true;
            }
            else
            {
                gbxTelaEstimada.Visible = false;
            }

        }

        private void TxtCodEstCli_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCodTecnica_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtDesColor_FondoTela_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtDesTipo_Tej_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtDesSubTipo_Tela_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCodColor_FondoTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarColorFondo();
            }
        }

        private void TxtDesColor_FondoTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarColorFondo();
            }
        }



        public void BuscarCombinacion()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec ap_sm_muestra_colcli_estilo '" + TxtCliente.Tag + "','"+ TxtTemporada.Tag +"','"+ TxtCodEstCli.Text +"'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCod_Combinacion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Colcli"]);
                    TxtDes_Combinacion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Nom_Colcli"]);


                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCod_Combinacion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Colcli"].Value);
                        TxtDes_Combinacion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Nom_Colcli"].Value);
                    }
                }
                ////////////TxtNomDiseno.Focus();
                TxtCodTecnica.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCod_Combinacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCod_Combinacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarCombinacion();
            }
        }

        private void TxtCod_Ubicacion_TextChanged(object sender, EventArgs e)
        {

        }


        public void BuscarUbicacion()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec AP_SM_MUESTRA_tg_UbicacionesDesarrolloAplicacion '" + TxtDes_Ubicacion.Text + "'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCod_Ubicacion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Ubicacion"]);
                    TxtDes_Ubicacion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Des_Ubicacion"]);                    
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCod_Ubicacion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Ubicacion"].Value);
                        TxtDes_Ubicacion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Des_Ubicacion"].Value);
                    }
                }
                ////////////TxtNomDiseno.Focus();
                TxtCodModoProc.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCod_Ubicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarUbicacion();
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

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            ///////grabar();
          
        }

        private void TxtCod_Tela_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtDes_Combinacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtComposicion_Comercial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtDesRuta.Focus();
            }
        }

        private void TxtTela_Estimada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtComentarios.Focus();
            }
            
        }

        private void TxtComentarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnCargar.Focus();
            }
        }

        private void TxtRequerida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtMetros.Focus();
            }
        }

        private void TxtMetros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnAceptar.Focus();
            }
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {            
            string rutaDestino = string.Empty;
            try
            {
                DialogResult rpt;
                rpt = MessageBox.Show("¿Está seguro de guardar?", "Pregunta", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == rpt)
                {
                    strSQL = string.Empty;
                    strSQL += "\n" + "EXEC UP_MAN_Es_Proceso_Imagen";
                    strSQL += "\n" + string.Format(" @accion                ='{0}'", sOpcion);
                    strSQL += "\n" + string.Format(",@Id_Proceso_Imagen     ='{0}'", TxtIdProceso.Text);
                    strSQL += "\n" + string.Format(",@Cod_proceso           ='{0}'", TxtCodProceso.Text);
                    strSQL += "\n" + string.Format(",@tipo_grafico          ='{0}'", TxtCodTipoGrafico.Text);
                    strSQL += "\n" + string.Format(",@descripcion           ='{0}'", TxtDescripcion.Text);
                    strSQL += "\n" + string.Format(",@descripcion_det       ='{0}'", TxtDescripcionDetallada.Text);
                    if ( nombreArchivo != "")
                    {
                        strSQL += "\n" + string.Format(",@imagen                ='{0}'", nombreArchivo);
                    }                    
                    strSQL += "\n" + string.Format(",@cod_usuario		    ='{0}'", VariablesGenerales.pUsuario);
                    strSQL += "\n" + string.Format(",@cod_estacion          ='{0}'", Environment.MachineName);
                    strSQL += "\n" + string.Format(",@cod_estpro            ='{0}'", vCodEstpro);
                    strSQL += "\n" + string.Format(",@cod_version           ='{0}'", vCodVersion);
                    strSQL += "\n" + string.Format(",@Secuencia             ='{0}'", vCodSecuencia);
                    strSQL += "\n" + string.Format(",@result                ='{0}'", result);

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

        private void uiButton1_Click(object sender, EventArgs e)
        {
            IsCambioOK = false;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void BtnSubTipo_Click(object sender, EventArgs e)
        {
            ////////FrmBandejaSolicitudAplicacionesBDESTF_MAN_SubTipoReq oFrm = new FrmBandejaSolicitudAplicacionesBDESTF_MAN_SubTipoReq();
            ////////oFrm.TxtCod_FamItem.Text = TxtCod_FamItem.Text;
            ////////oFrm.TxtDes_FamItem.Text = TxtDes_FamItem.Text;
            ////////oFrm.CargaGrilla();
            ////////oFrm.ShowDialog();            
        }

        private void TxtCod_FamItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChkFondoTelaDisp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ChkFondoTelaDisp_Click(object sender, EventArgs e)
        {

        }

        private void rbnFondoTelaDispo_CheckedChanged(object sender, EventArgs e)
        {
            Validacion_FondoTela(0);
        }

        private void rbnFondoTelaDispo_Click(object sender, EventArgs e)
        {

        }


        private void Validacion_FondoTela(short flgCargaInicial)
        {
            //if (ChkColorFondoTela.Checked)
           if (rbnColorFondoTela.Checked)
            {
                ///////gbxColorFondo.Visible = true;
                gbxCodColorFondo.Enabled = true;                
            }
            else
            {                                
                gbxCodColorFondo.Enabled = false;
                if (flgCargaInicial != 1)
                {
                    TxtCodColor_FondoTela.Text = "";
                    TxtDesColor_FondoTela.Text = "";
                }
            }

        }

        private void rbnColorFondoTela_CheckedChanged(object sender, EventArgs e)
        {
            Validacion_FondoTela(0);
        }


        public void BuscarRuta()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();


                oTipo.sQuery = "Exec Ap_Muestra_Rutas_Tela '"+ TxtCod_Tela.Text +"'";


                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodRuta.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Ruta"]);
                    TxtDesRuta.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["ruta"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodRuta.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Ruta"].Value);
                        TxtDesRuta.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["ruta"].Value);
                    }
                }
                TxtTela_Estimada.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCodRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (TxtCod_Tela.Text != "")
                { BuscarRuta(); }
                else 
                {
                    MessageBox.Show("Ingrese codigo de Tela", "Validar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtCod_Tela.Focus(); 
                }
            }
        }

        private void TxtDesRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (TxtCod_Tela.Visible == true)
                {
                    MessageBox.Show("Ingrese codigo de Tela", "Validar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtCod_Tela.Focus();
                }

                if (TxtCod_Tela.Text != "")
                { BuscarRuta(); }

                
                    
            }
        }


        private void validaRutaTela()
        {            
           if (TxtCod_Tela.Text.Trim() != "")
            {
                TxtCodRuta.Visible = true;
                TxtDesRuta.Visible = true;
                TxtCodRuta.Focus();
            }
            else
            {
                TxtCodRuta.Text = "";
                TxtCodRuta.Visible = false;                
                TxtDesRuta.Visible = true;
                TxtDesRuta.Focus();
            }
        }


        public void BuscarProceso(short tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();

                if (tipo == 1)
                    oTipo.sQuery = "Exec ES_muestra_Rutas_Proceso '1','" + TxtCodProceso.Text + "'";
                else
                    oTipo.sQuery = "Exec ES_muestra_Rutas_Proceso '2','','" + TxtDesProceso.Text + "'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodProceso.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["codigo"]);
                    TxtDesProceso.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodProceso.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["codigo"].Value);
                        TxtDesProceso.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["descripcion"].Value);
                    }
                }

                TxtCodTipoGrafico.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void BuscarTipoGrafico(short tipo)
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();

                
                if (tipo == 1)
                    oTipo.sQuery = "Exec Es_muestra_TiposImagenes_por_Proceso_Ruta_Manufactura '1','"+ TxtCodProceso.Text +"','" + TxtCodTipoGrafico.Text + "'";
                else
                    oTipo.sQuery = "Exec Es_muestra_TiposImagenes_por_Proceso_Ruta_Manufactura '2','"+ TxtCodProceso.Text +"','','" + TxtDesTipoGrafico.Text + "'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodTipoGrafico.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Tipo_Grafico"]);
                    TxtDesTipoGrafico.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodTipoGrafico.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Tipo_Grafico"].Value);
                        TxtDesTipoGrafico.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["descripcion"].Value);
                    }
                }

                TxtDescripcion.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtCodProceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarProceso(1);
            }
        }

        private void TxtDesProceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarProceso(2);
            }
        }

        private void TxtCodTipoGrafico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoGrafico(1);
            }
        }

        private void TxtDesTipoGrafico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarTipoGrafico(2);
            }
        }

        private void TxtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtDescripcionDetallada.Focus();
            }
        }

        private void TxtDescripcionDetallada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtRutaImagen.Focus();
            }
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

                    string strSQL = "SELECT RutaGrafico_Proceso_Imagen FROM TG_CONTROL";
                    object resultado = oHp.DevuelveDato(strSQL, VariablesGenerales.pConnect);
                    string rutaBase = resultado != null ? resultado.ToString() : string.Empty;
                    string newRuta = rutaBase +  "_" + nombreArchivo + ".jpg";

                    TxtRutaImagen.Text = newRuta;
                    BtnAceptar.Focus();                    
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

        private void BtnImagen_Click(object sender, EventArgs e)
        {
            Foto1();
        }

        private void TxtRutaImagen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnAceptar.Focus();
            }
        }

        private void BtnTipoGrafico_Click(object sender, EventArgs e)
        {
            
            object oForm = oHp.GetFormDesdeOtroProyecto("Estilo_Propio", ".exe", "FrmMantTipoGraficoxProcesos");
            ((dynamic)oForm).TxtProceso.Text = TxtCodProceso.Text + " - "  + TxtDesProceso.Text;
            ((dynamic)oForm).CodProceso = TxtCodProceso.Text;
            ((dynamic)oForm).ShowDialog();
            
        }
    }
}
