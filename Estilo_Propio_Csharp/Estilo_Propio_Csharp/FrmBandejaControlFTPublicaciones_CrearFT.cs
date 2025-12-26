using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Estilo_Propio_Csharp.FormularioProgreso;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBandejaControlFTPublicaciones_CrearFT : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        GeneradorFichaTecnica GenFT = new GeneradorFichaTecnica();
        private Color colEmpresa;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        private const string K_strColCheck = "Flg_Seleccion";
        public Boolean ValidaCopia = false;
        private DataRow oDr;
        clsBtnSeguridadJanus oSeg = new clsBtnSeguridadJanus();
        public Boolean IsCambioOK;
        public string EventoParametrizableGenrarPDF;

        public int IDFichaTecnica;
        public int ID_FT;
        public DataTable oDtDatosSeleccionadosFT;
        public int IDPublicacion;
        public string ClienteSel;
        public string TemporadaSel;

        public DataTable oDtEstructuraFTProcesos;

        private bool isExecutionGeneracionFT = false;

        #endregion

        public FrmBandejaControlFTPublicaciones_CrearFT()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_CrearFT_Load(object sender, EventArgs e)
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
                    Panel3.BackColor = colEmpresa;
                    Panel4.BackColor = colEmpresa;
                }

                PanelCompraTela.Visible = true;
                ID_FT = 0;
                ActiveControl = TxtDescripcion;

                CreateTableFTProcesos();
                CargarRutaDePrenda();
                MarcarTodos(true);
                grxRutaDePrenda.Enabled = false;

                EventoParametrizableGenrarPDF = oHp.DevuelveDato("select DBO.sm_valida_Tg_Eventos_Parametrizables(437)", VariablesGenerales.pConnect).ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateTableFTProcesos()
        {
            oDtEstructuraFTProcesos = new DataTable("EstructuraFTProceso");
            oDtEstructuraFTProcesos.Columns.Add("Cod_EstPro", Type.GetType("System.String"));
            oDtEstructuraFTProcesos.Columns.Add("Cod_Version", Type.GetType("System.String"));
            oDtEstructuraFTProcesos.Columns.Add("Cod_Proceso", Type.GetType("System.String"));
        }

        private void TxtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TxtObservacion.Focus();
            }
        }

        private void TxtCodMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivoFT_Parcial();
            }
        }

        private void TxtDesMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivoFT_Parcial();
            }
        }

        public void BuscarMotivoFT_Parcial()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "EXEC ES_EstProVer_Ficha_Tecnica_Motivo_Tipo_Parcial_Man 'F'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodMotivo.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Motivo_FT_Parcial"]);
                    TxtDesMotivo.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodMotivo.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Motivo_FT_Parcial"].Value);
                        TxtDesMotivo.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }
                TxtObservacion.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpcionFicha(object sender, EventArgs e)
        {
            grxRutaDePrenda.Enabled = rbtParcial.Checked;
            MarcarTodos(rbtCompleta.Checked);
            panDatosFT_parcial.Visible = rbtParcial.Checked;

            if (rbtParcial.Checked)
            {
                TxtCodMotivo.Text = string.Empty;
                TxtDesMotivo.Text = string.Empty;
            }
        }

        private void MarcarTodos(bool bolSW)
        {
            try
            {
                foreach (GridEXRow oGridEXRow in grxRutaDePrenda.GetDataRows())
                {
                    oGridEXRow.BeginEdit();
                    oGridEXRow.Cells[K_strColCheck].Value = bolSW;
                    oGridEXRow.EndEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void BtnMantMotivo_Click(object sender, EventArgs e)
        {
            object oForm1 = oHp.GetFormDesdeOtroProyecto("Estilo_Propio", ".exe", "FrmMantMotivoFT_Parcial");
            ((dynamic)oForm1).ShowDialog();
        }

        private void TxtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BtnAceptar.Focus();
            }
        }

        #region Ruta de la Prenda
        public void CargarRutaDePrenda()
        {
            grxRutaDePrenda.Visible = true;

            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC Es_EstProVer_Ficha_Tecnica_Secciones_Lista_Ruta_Procesos_Cerrada";
                strSQL += "\n" + string.Format("  @cod_estpro       = '{0}'", TxtEstiloPropio.Text.Trim());
                strSQL += "\n" + string.Format(", @cod_version		= '{0}'", TxtVersion.Text.Trim());
                strSQL += "\n" + string.Format(", @ID_FichaTecnica	= '{0}'", ID_FT.ToString());

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                grxRutaDePrenda.DataSource = oDt;
                oHp.CheckLayoutGridEx(grxRutaDePrenda);

                {
                    var withBlock = grxRutaDePrenda;
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
                            var withBlock2 = withBlock1.Columns["Flg_Seleccion"];
                            withBlock2.HeaderImage = global::Estilo_Propio_Csharp.Properties.Resources.accept_16X16;
                            withBlock2.AllowDrag = false;
                            withBlock2.Caption = string.Empty;
                            withBlock2.Width =40;
                            withBlock2.Visible = true;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.WordWrap = true;
                            withBlock2.AllowSort = false;
                            withBlock2.HeaderStyle = new GridEXFormatStyle() { FontSize = 1, TextAlignment = TextAlignment.Center, ForeColor = BackColor, ImageHorizontalAlignment = ImageHorizontalAlignment.Center, ImageVerticalAlignment = ImageVerticalAlignment.Center };
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Proceso"];
                            withBlock2.Width = 60;
                            withBlock2.Caption = "CODIGO";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.CellStyle.FontBold = TriState.True;
                            withBlock2.CellStyle.ForeColor = Color.SteelBlue;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Descripcion"];
                            withBlock2.Caption = "PROCESO";
                            withBlock2.Width = 100;
                            withBlock2.Visible = true;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Orden"];
                            withBlock2.Width = 30;
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.CellStyle.FontBold = TriState.True;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.Caption = String.Empty;
                            withBlock2.HeaderImage = global::Estilo_Propio_Csharp.Properties.Resources.ic_sort_desccending_16x16;
                            withBlock2.Visible = true;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario_Cierre"];
                            withBlock2.Caption = "USUARIO CIERRE";
                            withBlock2.Width = 80;
                            withBlock2.Visible = true;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Cierre"];
                            withBlock2.Caption = "FECHA DE CIERRE";
                            withBlock2.Width = 100;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Flg_ConCapacidadEscasa"];
                            withBlock2.Width = 80;
                            withBlock2.Caption = "¿CAPACIDAD ESCASA?";
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Des_EstadoMaduracion"];
                            withBlock2.Width = 90;
                            withBlock2.Caption = "ESTADO DE MADURACION";
                            withBlock2.Visible = true;
                        }                      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void grxRutaDePrenda_Click(object sender, EventArgs e)
        {
            try
            {
                if (grxRutaDePrenda.CurrentColumn == null)
                    return;

                if (grxRutaDePrenda.CurrentRow == null)
                    return;

                if (grxRutaDePrenda.CurrentColumn.Key.ToUpper() == K_strColCheck.ToUpper())
                {
                    bool currentValue = Convert.ToBoolean(grxRutaDePrenda.CurrentRow.Cells[grxRutaDePrenda.CurrentColumn.Key].Value);

                    grxRutaDePrenda.CurrentRow.BeginEdit();
                    grxRutaDePrenda.CurrentRow.Cells[grxRutaDePrenda.CurrentColumn.Key].Value = !currentValue;
                    grxRutaDePrenda.CurrentRow.EndEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private GridEXFormatStyle objGridEXFormatStyle_Check = new GridEXFormatStyle() { FontSize = 1, BackColor = Color.AliceBlue, ForeColor = DefaultBackColor, TextAlignment = TextAlignment.Center, ImageHorizontalAlignment = ImageHorizontalAlignment.Center, ImageVerticalAlignment = ImageVerticalAlignment.Center };
        private GridEXFormatStyle objGridEXFormatStyle_UnCheck = new GridEXFormatStyle() { FontSize = 1, BackColor = Color.AliceBlue, ForeColor = DefaultBackColor, TextAlignment = TextAlignment.Center, ImageHorizontalAlignment = ImageHorizontalAlignment.Center, ImageVerticalAlignment = ImageVerticalAlignment.Center };

        private void grxRutaDePrenda_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {
            try
            {
                foreach (GridEXColumn oGridEXColumn in grxRutaDePrenda.RootTable.Columns)
                {
                    if (oGridEXColumn.Key == K_strColCheck)
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

                    if (oGridEXColumn.Key == "Flg_ConCapacidadEscasa")
                    {
                        switch (e.Row.Cells[oGridEXColumn].Value)
                        {
                            case true:
                                {
                                    e.Row.Cells[oGridEXColumn].Text = "SI";
                                    break;
                                }
                            case false:
                                {
                                    e.Row.Cells[oGridEXColumn].Text = "NO";
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            AceptarAsync();
            //ActualizaFotoBD();
        }

        private async Task AceptarAsync()
        {
            try
            {
                (int _IDPublicacion, int _IDFichaTecnica) = SavePublicacionBD();

                if (_IDPublicacion == 0)
                {
                    MessageBox.Show("No se pudo publicar la fT", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show($"Se genero el id de publicacion {_IDPublicacion.ToString()}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if(EventoParametrizableGenrarPDF == "S")
                {
                    IsCambioOK = true;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    if (await GeneraFTAsync(TxtEstiloPropio.Text, TxtVersion.Text, _IDFichaTecnica, _IDPublicacion, ClienteSel, true))
                    {
                        IsCambioOK = true;
                        DialogResult = DialogResult.OK;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private (int IDPublicacion, int IDFichaTecnica) SavePublicacionBD()
        {
            if (string.IsNullOrEmpty(TxtDescripcion.Text.TrimEnd()))
            {
                MessageBox.Show("Ingresar Descripcion de Ficha", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (0,0);
            }

            oDtEstructuraFTProcesos.Clear();
            foreach (GridEXRow oGridEXRow in grxRutaDePrenda.GetDataRows())
            {
                if (Convert.ToBoolean(oGridEXRow.Cells["Flg_Seleccion"].Value) == true)
                {
                    DataRow oDrNUEVO = oDtEstructuraFTProcesos.NewRow();
                    oDrNUEVO["Cod_EstPro"] = TxtEstiloPropio.Text.Trim();
                    oDrNUEVO["Cod_Version"] = TxtVersion.Text.Trim();
                    oDrNUEVO["Cod_Proceso"] = oGridEXRow.Cells["cod_proceso"].Value;
                    oDtEstructuraFTProcesos.Rows.Add(oDrNUEVO);
                }
            }

            using (SqlConnection connection = new SqlConnection(VariablesGenerales.pConnect))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction("ValidaTransacción");
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cmd.CommandText = "FT_CREACION_FT_PUBLICACION_DESDE_BANDEJA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@COD_ESTPRO", SqlDbType.Char, 5).Value = TxtEstiloPropio.Text;
                        cmd.Parameters.Add("@COD_VERSION", SqlDbType.Char, 2).Value = TxtVersion.Text;
                        cmd.Parameters.Add("@COD_CLIENTE", SqlDbType.Char, 5).Value = ClienteSel;
                        cmd.Parameters.Add("@COD_TEMCLI", SqlDbType.Char, 3).Value = TemporadaSel;
                        cmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar, 50).Value = TxtDescripcion.Text;
                        cmd.Parameters.Add("@observacion", SqlDbType.NVarChar, 250).Value = TxtObservacion.Text;
                        cmd.Parameters.Add("@Tabla_Procesos", SqlDbType.Structured).Value = oDtEstructuraFTProcesos;
                        cmd.Parameters.Add("@Flg_LaFT_EsCompleta", SqlDbType.Bit).Value = IIf(rbtCompleta.Checked == true, 1, 0);
                        cmd.Parameters.Add("@TablaRequerimientoComercial", SqlDbType.Structured).Value = oDtDatosSeleccionadosFT;
                        cmd.Parameters.Add("@ComentariosPublicar", SqlDbType.VarChar, 4000).Value = "";
                        cmd.Parameters.Add("@Cod_Motivo_FT_Parcial", SqlDbType.Char, 3).Value = TxtCodMotivo.Text;
                        cmd.Parameters.Add("@Fec_Comprometida_FT_Parcial_En_Completo", SqlDbType.DateTime).Value = dtpFecComprometidaFT_Parcial.Value.ToShortDateString();
                        cmd.Parameters.Add("@Flg_ParaCompraDeTela", SqlDbType.Bit).Value = IIf(rbSICompraTela.Checked == true, 1, 0);
                        cmd.Parameters.Add("@PC_CREACION", SqlDbType.VarChar, 100).Value = Environment.MachineName;
                        cmd.Parameters.Add("@COD_USUARIO", SqlDbType.VarChar, 100).Value = VariablesGenerales.pUsuario;
                        cmd.Parameters.Add("@ID_Publicacion", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@ID_FichaTecnica", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        IDPublicacion = (int)cmd.Parameters["@ID_Publicacion"].Value;
                        IDFichaTecnica = (int)cmd.Parameters["@ID_FichaTecnica"].Value;
                        cmd.Parameters.Clear();
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("No es posible generar la Ficha Técnica " + Environment.NewLine + " debido a que " + ex.Message.ToString() + Environment.NewLine + " Por favor, revíselos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return (IDPublicacion, IDFichaTecnica);
        }

        private async Task<bool> GeneraFTAsync (string codEstpro, string codVersion, int IDFichaTecnica, int IdPublicacion, string CodigoClienteSel, Boolean ExportaPDF)
        {
            bool returnOK = false;
            var cancellationTokenSource = new CancellationTokenSource();
            var formProgreso = new FormularioProgreso();

            // Configurar eventos del formulario de progreso
            formProgreso.ConfigurarCancelacion(cancellationTokenSource);
            formProgreso.ProcesoCompletado += (s, e) => AgregarLog("✓ Proceso completado exitosamente");
            formProgreso.ProcesoCancelado += (s, e) => AgregarLog("⚠ Proceso cancelado por el usuario");
            formProgreso.ProcesoError += (s, ex) => AgregarLog($"✗ Error en proceso: {ex.Message}");
            formProgreso.CerrarProgreso += (s, e) => CerrarFormulario();

            try
            {
                // Mostrar formulario de progreso
                formProgreso.Show(this);
                AgregarLog("Iniciando proceso...");

                // Crear progress reporter
                var progress = new Progress<ProgresoInfo>(formProgreso.ActualizarProgreso);

                // Ejecutar tu método asíncrono
                //var resultado = await TuMetodoAsincrono(progress, cancellationTokenSource.Token);
                bool resultado = await GenFT.GenerarPDFAsync(codEstpro, codVersion, IDFichaTecnica, IDPublicacion, CodigoClienteSel,
                    progress, cancellationTokenSource.Token, ExportaPDF);

                if (resultado)
                {
                    // Mostrar completado
                    formProgreso.MostrarCompletado(
                        "Proceso finalizado",
                        $"Resultado: {resultado}"
                    );
                    returnOK = true;
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
            return returnOK;
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
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            IsCambioOK = false;
            DialogResult = DialogResult.Cancel;
        }

        object IIf(bool expression, object truePart, object falsePart) { return expression ? truePart : falsePart; }

        private void CerrarFormulario()
        {
            IsCambioOK = true;
            DialogResult = DialogResult.OK;
        }
    }
}
