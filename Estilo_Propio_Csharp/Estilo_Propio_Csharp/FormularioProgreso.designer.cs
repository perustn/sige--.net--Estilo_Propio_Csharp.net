
using System.Drawing;
using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    partial class FormularioProgreso
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timerActualizacion?.Stop();
                timerActualizacion?.Dispose();
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelProgreso = new System.Windows.Forms.Panel();
            this.lblPorcentaje = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.chkMostrarLog = new System.Windows.Forms.CheckBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panelProgreso.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelProgreso
            // 
            this.panelProgreso.BackColor = System.Drawing.Color.White;
            this.panelProgreso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProgreso.Controls.Add(this.lblPorcentaje);
            this.panelProgreso.Controls.Add(this.progressBar);
            this.panelProgreso.Controls.Add(this.lblMensaje);
            this.panelProgreso.Controls.Add(this.lblDetalle);
            this.panelProgreso.Controls.Add(this.lblTiempo);
            this.panelProgreso.Location = new System.Drawing.Point(10, 10);
            this.panelProgreso.Name = "panelProgreso";
            this.panelProgreso.Size = new System.Drawing.Size(508, 160);
            this.panelProgreso.TabIndex = 0;
            // 
            // lblPorcentaje
            // 
            this.lblPorcentaje.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPorcentaje.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPorcentaje.Location = new System.Drawing.Point(441, 15);
            this.lblPorcentaje.Name = "lblPorcentaje";
            this.lblPorcentaje.Size = new System.Drawing.Size(80, 30);
            this.lblPorcentaje.TabIndex = 0;
            this.lblPorcentaje.Text = "0%";
            this.lblPorcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(420, 25);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoEllipsis = true;
            this.lblMensaje.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMensaje.ForeColor = System.Drawing.Color.Black;
            this.lblMensaje.Location = new System.Drawing.Point(15, 55);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(420, 25);
            this.lblMensaje.TabIndex = 2;
            this.lblMensaje.Text = "Iniciando proceso...";
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoEllipsis = true;
            this.lblDetalle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDetalle.ForeColor = System.Drawing.Color.Gray;
            this.lblDetalle.Location = new System.Drawing.Point(15, 80);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(420, 40);
            this.lblDetalle.TabIndex = 3;
            this.lblDetalle.Text = "Preparando recursos...";
            // 
            // lblTiempo
            // 
            this.lblTiempo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTiempo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTiempo.Location = new System.Drawing.Point(15, 125);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(200, 20);
            this.lblTiempo.TabIndex = 4;
            this.lblTiempo.Text = "Tiempo: 00:00";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(428, 174);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(90, 30);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Visible = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // chkMostrarLog
            // 
            this.chkMostrarLog.Checked = true;
            this.chkMostrarLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarLog.Location = new System.Drawing.Point(20, 180);
            this.chkMostrarLog.Name = "chkMostrarLog";
            this.chkMostrarLog.Size = new System.Drawing.Size(150, 20);
            this.chkMostrarLog.TabIndex = 0;
            this.chkMostrarLog.Text = "Mostrar log detallado";
            this.chkMostrarLog.UseVisualStyleBackColor = true;
            this.chkMostrarLog.CheckedChanged += new System.EventHandler(this.ChkMostrarLog_CheckedChanged);
            // 
            // lblLog
            // 
            this.lblLog.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLog.Location = new System.Drawing.Point(20, 205);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(100, 20);
            this.lblLog.TabIndex = 1;
            this.lblLog.Text = "Log del proceso:";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Location = new System.Drawing.Point(20, 230);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(498, 197);
            this.txtLog.TabIndex = 2;
            // 
            // FormularioProgreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(528, 439);
            this.ControlBox = false;
            this.Controls.Add(this.chkMostrarLog);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.panelProgreso);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormularioProgreso";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Progreso de la operación";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormularioProgreso_FormClosing);
            this.Load += new System.EventHandler(this.FormularioProgreso_Load);
            this.panelProgreso.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar progressBar;
        private Label lblMensaje;
        private Label lblDetalle;
        private Label lblTiempo;
        private Label lblPorcentaje;
        private Button btnCancelar;
        private Panel panelProgreso;
        private TextBox txtLog;
        private Label lblLog;
        private CheckBox chkMostrarLog;
    }
}