using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Estilo_Propio_Csharp.FormularioProgreso;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBandejaControlFTPublicaciones_ModifPublicar : Form
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
        private const string K_strColCheck = "Flg_Seleccion";
        public string EstiloPropioSel;
        public string Versionsel;
        public int IdFichaTecnicaSel;
        public string CodigoClienteSel;

        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        #endregion

        public FrmBandejaControlFTPublicaciones_ModifPublicar()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_ModifPublicar_Load(object sender, EventArgs e)
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

                this.ActiveControl = TxtComentariosGenerales;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CargarRutaDePrenda()
        {
            grxRutaDePrenda.Visible = true;

            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_UP_MAN_Es_EstProVer_Ficha_Tecnica_Secciones";
                strSQL += "\n" + string.Format(" @opcion            ='{0}'","V");
                strSQL += "\n" + string.Format(",@ID_Publicacion	='{0}'", TxtIdPublicacion.Text);

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
                            withBlock2.Width = 40;
                            withBlock2.Visible = true;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.WordWrap = true;
                            withBlock2.AllowSort = false;
                            withBlock2.EditType = EditType.NoEdit;
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
                            withBlock2.EditType = EditType.NoEdit;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Descripcion"];
                            withBlock2.Caption = "PROCESO";
                            withBlock2.Width = 100;
                            withBlock2.Visible = true;
                            withBlock2.EditType = EditType.NoEdit;
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
                            withBlock2.EditType = EditType.NoEdit;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario_Cierre"];
                            withBlock2.Caption = "USUARIO CIERRE";
                            withBlock2.Width = 80;
                            withBlock2.Visible = true;
                            withBlock2.EditType = EditType.NoEdit;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Cierre"];
                            withBlock2.Caption = "FECHA DE CIERRE";
                            withBlock2.Width = 100;
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.EditType = EditType.NoEdit;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Flg_ConCapacidadEscasa"];
                            withBlock2.Width = 80;
                            withBlock2.Caption = "¿CAPACIDAD ESCASA?";
                            withBlock2.Visible = true;
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.EditType = EditType.NoEdit;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Des_EstadoMaduracion"];
                            withBlock2.Width = 90;
                            withBlock2.Caption = "ESTADO DE MADURACION";
                            withBlock2.Visible = true;
                            withBlock2.EditType = EditType.NoEdit;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Observacion"];
                            withBlock2.Width = 170;
                            withBlock2.Caption = "OBSERVACIONES";
                            withBlock2.Visible = true;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.EditType = EditType.TextBox;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {            
            try
            {
                ModificarPrePublicarAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task ModificarPrePublicarAsync()
        {
            DialogResult rpt;
            rpt = MessageBox.Show("¿Está seguro de cambiar Status a Modificar por Pre Publicación de la FT seleccionada?", "Pregunta", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == rpt)
            {               
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC FT_Cambia_Status_a_Modificada_por_PrePublicar";
                strSQL += "\n" + string.Format(" @Id_Publicacion            = {0} ", TxtIdPublicacion.Text);
                strSQL += "\n" + string.Format(",@cod_usuario               ='{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(",@ComentariosDePublicacion  ='{0}'", TxtObservacion.Text);
                strSQL += "\n" + string.Format(",@cod_estacion              ='{0}'", Environment.MachineName);
                strSQL += "\n" + string.Format(",@observacion               ='{0}'", TxtComentariosGenerales.Text);
                if (oHp.EjecutarOperacion(strSQL) == true)
                {
                    foreach (GridEXRow oGridEXRow in grxRutaDePrenda.GetDataRows())
                    {
                        strSQL = string.Empty;
                        strSQL += "\n" + "EXEC FT_UP_MAN_Es_EstProVer_Ficha_Tecnica_Secciones";
                        strSQL += "\n" + string.Format(" @opcion            ='{0}'", "U");
                        strSQL += "\n" + string.Format(",@ID_Publicacion	='{0}'", TxtIdPublicacion.Text);
                        strSQL += "\n" + string.Format(",@Sec_Seccion	    ='{0}'", oGridEXRow.Cells["SEC_SECCION"].Value);
                        strSQL += "\n" + string.Format(",@Cod_Proceso	    ='{0}'", oGridEXRow.Cells["cod_proceso"].Value);
                        strSQL += "\n" + string.Format(",@Observacion	    ='{0}'", oGridEXRow.Cells["OBSERVACION"].Value);
                        strSQL += "\n" + string.Format(",@cod_usuario	    ='{0}'", VariablesGenerales.pUsuario);
                        strSQL += "\n" + string.Format(",@cod_estacion	    ='{0}'", Environment.MachineName);
                        strSQL += "\n" + string.Format(",@is_seleccionado	= {0} ", IIf((bool)oGridEXRow.Cells["Flg_Seleccion"].Value == true, 1, 0));

                        oHp.EjecutarOperacion(strSQL);
                    }

                    /// Genera Excel y convierte a PDF
                    if (await GeneraFTAsync(EstiloPropioSel, Versionsel, IdFichaTecnicaSel, IDPublicacion, CodigoClienteSel))
                    {
                        IsCambioOK = true;
                        MessageBox.Show("El Proceso Se Ha Generado Correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                }
            }
        }

        object IIf(bool expression, object truePart, object falsePart) { return expression ? truePart : falsePart; }

        private async Task<bool> GeneraFTAsync(string codEstpro, string codVersion, int IDFichaTecnica, int IdPublicacion, string CodigoClienteSel)
        {

            bool returnOK = false;
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
                formProgreso.Show(this);
                AgregarLog("Iniciando proceso...");

                // Crear progress reporter
                var progress = new Progress<ProgresoInfo>(formProgreso.ActualizarProgreso);

                // Ejecutar tu método asíncrono
                //var resultado = await TuMetodoAsincrono(progress, cancellationTokenSource.Token);
                bool resultado = await GenFT.GenerarPDFAsync(codEstpro, codVersion, IDFichaTecnica, IDPublicacion, CodigoClienteSel,
                    progress, cancellationTokenSource.Token);

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
        
        private void grxRutaDePrenda_FormattingRow(object sender, RowLoadEventArgs e)
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
    }
}
