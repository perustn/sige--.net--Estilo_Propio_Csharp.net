using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Estilo_Propio_Csharp.FormularioProgreso;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBandejaControlFTPublicaciones_PrePublicar : Form
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

        public FrmBandejaControlFTPublicaciones_PrePublicar()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_PrePublicar_Load(object sender, EventArgs e)
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
                switch (TipoCambioStatus)
                {
                    case "PREPUBLICAR":
                        this.Size = new System.Drawing.Size(524, 274);
                        break;

                    case "PUBLICAR":
                        this.Size = new System.Drawing.Size(524, 192);
                        break;

                    case "MODPREPUBLICAR":
                        this.Size = new System.Drawing.Size(524, 192);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TxtCodMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivo();
            }
        }

        private void TxtDesMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivo();
            }
        }

        public void BuscarMotivo()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "Exec FT_AYUDA_ES_EstProVer_Ficha_Tecnica_Motivo";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodMotivo.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Motivo"]);
                    TxtDesMotivo.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodMotivo.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Motivo"].Value);
                        TxtDesMotivo.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }
                BtnAceptar.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkEsEstampado_Click(object sender, EventArgs e)
        {
            if (ChkEsEstampado.Checked == true)
            {
                grpMotivoParcial.Enabled = false;
            }
            else
            {
                grpMotivoParcial.Enabled = true;
            }
        }

        private void TxtCodMotivoParcial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivoParcial();
            }
        }

        private void TxtDesMotivoParcial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscarMotivoParcial();
            }
        }

        public void BuscarMotivoParcial()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "Exec FT_AYUDA_ES_EstProVer_Ficha_Tecnica_Motivo_Tipo_Parcial";
                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtCodMotivoParcial.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Cod_Motivo"]);
                    TxtDesMotivoParcial.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtCodMotivoParcial.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Motivo"].Value);
                        TxtDesMotivoParcial.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }
                BtnAceptar.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task PrePublicarAsync()
        {
            DesRptstatus = "Pre Publicar";

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
                        cmd.CommandText = "FT_Cambia_Status_a_PrePublicar";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID_Publicacion", SqlDbType.Int).Value = TxtIdPublicacion.Text;
                        cmd.Parameters.Add("@COD_USUARIO", SqlDbType.VarChar, 100).Value = VariablesGenerales.pUsuario;
                        cmd.Parameters.Add("@ComentariosDePublicacion", SqlDbType.VarChar, 4000).Value = TxtObservacion.Text;
                        cmd.Parameters.Add("@MAIL", SqlDbType.VarChar, 200).Value = "";
                        cmd.Parameters.Add("@cod_estacion", SqlDbType.VarChar, 500).Value = Environment.MachineName;
                        cmd.Parameters.Add("@ID_Publicacion_Ult", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Cod_Motivo_publicacion", SqlDbType.Char, 3).Value = TxtCodMotivo.Text;
                        cmd.Parameters.Add("@Flg_LaFT_Escomplete_publicacion", SqlDbType.Bit).Value = IIf(ChkEsEstampado.Checked == true, 1, 0);
                        cmd.Parameters.Add("@cod_motivo_ft_parcial_publicacion", SqlDbType.Char, 3).Value = TxtCodMotivoParcial.Text;

                        cmd.ExecuteNonQuery();
                        IDPublicacion = (int)cmd.Parameters["@ID_Publicacion_Ult"].Value;
                        cmd.Parameters.Clear();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("No se ha podido realizar la operación solicitada, por favor vuélvalo a intentar: " + ex.Message.ToString(), "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }


            //Genera Excel y convierte a PDF
            if (await GeneraFTAsync(EstiloPropioSel, Versionsel, IdFichaTecnicaSel, IDPublicacion, CodigoClienteSel))
            {
                IsCambioOK = true;
                MessageBox.Show("El Proceso Se Ha Generado Correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }

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
                formProgreso.Show();
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

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rpt;
                switch (TipoCambioStatus)
                {
                    case "PREPUBLICAR":
                        PrePublicarAsync();

                        break;

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

        object IIf(bool expression, object truePart, object falsePart) { return expression ? truePart : falsePart; }
    }
}
