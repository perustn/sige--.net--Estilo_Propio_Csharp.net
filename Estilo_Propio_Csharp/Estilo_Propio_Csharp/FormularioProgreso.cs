using System;
using System.Drawing;
using System.Threading;

using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    public partial class FormularioProgreso : Form
    {

        // =====================================================
        // EXCEPCIONES PERSONALIZADAS PARA TU LÓGICA DE NEGOCIO
        // =====================================================
        public class ProcessingException : Exception
        {
            public ProcessingException(string message) : base(message) { }
            public ProcessingException(string message, Exception innerException) : base(message, innerException) { }
        }

        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
            public ValidationException(string message, Exception innerException) : base(message, innerException) { }
        }

        // =====================================================
        // CLASE PARA INFORMACIÓN DE PROGRESO
        // =====================================================
        public class ProgresoInfo
        {
            public int Porcentaje { get; set; }
            public string Mensaje { get; set; }
            public string Detalle { get; set; }
            public bool EsError { get; set; }
            public object DatosAdicionales { get; set; }
        }

        private DateTime tiempoInicio;
        private System.Windows.Forms.Timer timerActualizacion;
        private CancellationTokenSource cancellationTokenSource;
        private bool procesoCancelado = false;
        private bool procesoCompletado = false;

        public event EventHandler ProcesoCompletado;
        public event EventHandler ProcesoCancelado;
        public event EventHandler<Exception> ProcesoError;

        private bool mostrarLog = false;


        public FormularioProgreso()
        {
            InitializeComponent();
            tiempoInicio = DateTime.Now;
            IniciarTimer();
        }

        private void FormularioProgreso_Load(object sender, EventArgs e)
        {
        }

        private void IniciarTimer()
        {
            timerActualizacion = new System.Windows.Forms.Timer();
            timerActualizacion.Interval = 1000; // Actualizar cada segundo
            timerActualizacion.Tick += Timer_Tick;
            timerActualizacion.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            ActualizarTiempoTranscurrido();
        }

        private void ActualizarTiempoTranscurrido()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ActualizarTiempoTranscurrido));
                return;
            }

            var transcurrido = DateTime.Now - tiempoInicio;
            lblTiempo.Text = $"Tiempo: {transcurrido:mm\\:ss}";

            // Calcular tiempo estimado si hay progreso
            if (progressBar.Value > 0 && progressBar.Value < 100)
            {
                //var tiempoEstimadoTotal = TimeSpan.FromMilliseconds(
                //    transcurrido.TotalMilliseconds * 100 / progressBar.Value);
                //var tiempoRestante = tiempoEstimadoTotal - transcurrido;

                //if (tiempoRestante.TotalSeconds > 0)
                //{
                //    lblTiempo.Text += $" | Restante: {tiempoRestante:mm\\:ss}";
                //}

                //ActualizarTiempoRestante(transcurrido);
            }
        }

        private DateTime _inicioProgreso = DateTime.Now;
        private TimeSpan? _ultimoTiempoRestante = null;
        private void ActualizarTiempoRestante(TimeSpan transcurrido)
        {
            if (progressBar.Value <= 0 || progressBar.Value > 100)
                return;

            // Velocidad promedio desde el inicio
            double velocidadPromedio = progressBar.Value / transcurrido.TotalSeconds;

            if (velocidadPromedio > 0)
            {
                double progresoRestante = 100 - progressBar.Value;
                double segundosRestantes = progresoRestante / velocidadPromedio;

                var tiempoRestanteNuevo = TimeSpan.FromSeconds(segundosRestantes);

                // Aplicar suavizado si tenemos estimación previa
                if (_ultimoTiempoRestante.HasValue)
                {
                    // Evitar cambios bruscos (más del 50%)
                    double cambioRelativo = Math.Abs(tiempoRestanteNuevo.TotalSeconds - _ultimoTiempoRestante.Value.TotalSeconds)
                                          / _ultimoTiempoRestante.Value.TotalSeconds;

                    if (cambioRelativo > 0.5) // Si el cambio es mayor al 50%
                    {
                        // Usar promedio ponderado (70% anterior, 30% nuevo)
                        double segundosPromedio = (_ultimoTiempoRestante.Value.TotalSeconds * 0.7) +
                                                (tiempoRestanteNuevo.TotalSeconds * 0.3);
                        tiempoRestanteNuevo = TimeSpan.FromSeconds(segundosPromedio);
                    }
                }

                _ultimoTiempoRestante = tiempoRestanteNuevo;

                // Validar que el tiempo sea razonable
                if (tiempoRestanteNuevo.TotalSeconds > 0 && tiempoRestanteNuevo.TotalHours < 12)
                {
                    string formato = tiempoRestanteNuevo.TotalHours >= 1
                        ? $"{tiempoRestanteNuevo:h\\:mm\\:ss}"
                        : $"{tiempoRestanteNuevo:mm\\:ss}";

                    lblTiempo.Text += $" | Restante: {formato}";
                }
                else if (progressBar.Value > 95)
                {
                    lblTiempo.Text += " | Finalizando...";
                }
            }
        }

        public void ActualizarProgreso(ProgresoInfo progreso)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<ProgresoInfo>(ActualizarProgreso), progreso);
                return;
            }

            // Actualizar barra de progreso
            int porcentaje = Math.Min(Math.Max(progreso.Porcentaje, 0), 100);
            progressBar.Value = porcentaje;
            lblPorcentaje.Text = $"{porcentaje}%";

            // Actualizar mensajes
            if (!string.IsNullOrEmpty(progreso.Mensaje))
            {
                lblMensaje.Text = progreso.Mensaje;
                AgregarLog($"[{porcentaje:000}%] {progreso.Mensaje}");
            }

            if (!string.IsNullOrEmpty(progreso.Detalle))
            {
                lblDetalle.Text = progreso.Detalle;

                // Agregar detalle al log si es diferente del mensaje
                if (progreso.Detalle != progreso.Mensaje)
                {
                    AgregarLog($"      └─ {progreso.Detalle}");
                }
            }

            // Manejar errores
            if (progreso.EsError)
            {
                lblMensaje.ForeColor = Color.Red;
                lblDetalle.ForeColor = Color.Red;
                lblPorcentaje.ForeColor = Color.Red;
                progressBar.ForeColor = Color.Red;
                lblTiempo.ForeColor = Color.Red;

                timerActualizacion?.Stop();
                btnCancelar.Text = "Cerrar";

                btnCancelar.Visible = true;
                procesoCompletado = true;

                AgregarLog($"ERROR: {progreso.Mensaje}", true);
            }
            else
            {
                lblMensaje.ForeColor = Color.Black;
                lblDetalle.ForeColor = Color.Gray;
                lblPorcentaje.ForeColor = Color.DarkBlue;
                lblTiempo.ForeColor = Color.Green;

                //AgregarLog("✓ PROCESO COMPLETADO EXITOSAMENTE");
            }

            // Actualizar título de ventana
            this.Text = $"Progreso: {porcentaje}% - {progreso.Mensaje}";

            // Si llegó al 100%, cambiar botón
            if (porcentaje >= 100 && !progreso.EsError)
            {
                btnCancelar.Text = "Cerrar";
                btnCancelar.Visible = true;
                procesoCompletado = true;
                timerActualizacion?.Stop();
            }

            // Forzar repintado
            this.Refresh();
        }
        public void ConfigurarCancelacion(CancellationTokenSource cts)
        {
            cancellationTokenSource = cts;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (procesoCompletado)
            {
                timerActualizacion?.Stop();
                AgregarLog("Cerrando formulario de progreso");
                this.Close();
                return;
            }

            var resultado = MessageBox.Show(
                "¿Está seguro que desea cancelar el proceso?",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                procesoCancelado = true;
                btnCancelar.Enabled = false;
                btnCancelar.Text = "Cancelando...";

                timerActualizacion?.Stop();
                AgregarLog("⚠ Usuario solicitó cancelación del proceso", true);

                cancellationTokenSource?.Cancel();
                ProcesoCancelado?.Invoke(this, EventArgs.Empty);

                ActualizarProgreso(new ProgresoInfo
                {
                    Porcentaje = progressBar.Value,
                    Mensaje = "Cancelando proceso...",
                    Detalle = "Por favor espere mientras se cancela la operación",
                    EsError = true
                });

                
            }
        }

        private void FormularioProgreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!procesoCompletado && !procesoCancelado)
            {
                e.Cancel = true;
                BtnCancelar_Click(sender, e);
            }
            else
            {

                timerActualizacion?.Stop();
                timerActualizacion?.Dispose();

                AgregarLog("Formulario cerrado");
            }
        }

        // Métodos de conveniencia
        public void MostrarError(string mensaje, string detalle = null)
        {
            timerActualizacion?.Stop();

            ActualizarProgreso(new ProgresoInfo
            {
                Porcentaje = progressBar.Value,
                Mensaje = mensaje,
                Detalle = detalle ?? "Ha ocurrido un error durante el proceso",
                EsError = true
            });

            btnCancelar.Text = "Cerrar";

            AgregarLog($"✗ ERROR CRÍTICO: {mensaje}", true);
            if (!string.IsNullOrEmpty(detalle))
            {
                AgregarLog($"    Detalle: {detalle}", true);
            }

            ProcesoError?.Invoke(this, new Exception(mensaje));
        }

        public void MostrarCompletado(string mensaje = null, string detalle = null)
        {
            timerActualizacion?.Stop();
            ActualizarProgreso(new ProgresoInfo
            {
                Porcentaje = 100,
                Mensaje = mensaje ?? "Proceso completado exitosamente",
                Detalle = detalle ?? "La operación se ha completado sin errores"
            });

            ProcesoCompletado?.Invoke(this, EventArgs.Empty);
        }

        private void ChkMostrarLog_CheckedChanged(object sender, EventArgs e)
        {
            mostrarLog = chkMostrarLog.Checked;

            if (mostrarLog)
            {
                // Mostrar log y expandir formulario
                //this.Size = new Size(480, 450);
                lblLog.Visible = true;
                txtLog.Visible = true;
                //btnCancelar.Location = new Point(370, 360);
            }
            else
            {
                // Ocultar log y contraer formulario
                //this.Size = new Size(480, 250);
                lblLog.Visible = false;
                txtLog.Visible = false;
                //btnCancelar.Location = new Point(370, 175);
            }
        }

        public void AgregarLog(string mensaje, bool esError = false)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, bool>(AgregarLog), mensaje, esError);
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string lineaLog = $"[{timestamp}] {mensaje}";

            txtLog.AppendText(lineaLog + Environment.NewLine);
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();

            // Limitar líneas del log para evitar consumo excesivo de memoria
            if (txtLog.Lines.Length > 500)
            {
                var lineas = txtLog.Lines;
                var nuevasLineas = new string[400];
                Array.Copy(lineas, lineas.Length - 400, nuevasLineas, 0, 400);
                txtLog.Lines = nuevasLineas;
            }
        }

        // Método público para agregar entradas personalizadas al log
        public void EscribirLog(string mensaje)
        {
            AgregarLog($"INFO: {mensaje}");
        }

        // Método para limpiar el log
        public void LimpiarLog()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LimpiarLog));
                return;
            }
            txtLog.Clear();
            AgregarLog("Log limpiado");
        }

    }
}
