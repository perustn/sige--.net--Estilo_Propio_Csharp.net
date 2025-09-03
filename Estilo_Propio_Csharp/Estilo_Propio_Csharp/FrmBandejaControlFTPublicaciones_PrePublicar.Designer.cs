
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaControlFTPublicaciones_PrePublicar
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnCancelar = new Janus.Windows.EditControls.UIButton();
            this.BtnAceptar = new Janus.Windows.EditControls.UIButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtIdPublicacion = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TxtDesMotivo = new System.Windows.Forms.TextBox();
            this.TxtCodMotivo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtObservacion = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.ChkEsEstampado = new Janus.Windows.EditControls.UICheckBox();
            this.TxtDesMotivoParcial = new System.Windows.Forms.TextBox();
            this.TxtCodMotivoParcial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpMotivoParcial = new Janus.Windows.EditControls.UIGroupBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMotivoParcial)).BeginInit();
            this.grpMotivoParcial.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 205);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(508, 30);
            this.panel2.TabIndex = 277;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnCancelar);
            this.panel3.Controls.Add(this.BtnAceptar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(301, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(207, 30);
            this.panel3.TabIndex = 0;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.ImageKey = "48px-Crystal_Clear_action_button_cancel.png";
            this.BtnCancelar.ImageSize = new System.Drawing.Size(32, 32);
            this.BtnCancelar.Location = new System.Drawing.Point(105, 3);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnCancelar.Size = new System.Drawing.Size(98, 24);
            this.BtnCancelar.TabIndex = 6;
            this.BtnCancelar.Text = "&Cancelar";
            this.BtnCancelar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.BtnAceptar.ImageSize = new System.Drawing.Size(32, 32);
            this.BtnAceptar.Location = new System.Drawing.Point(3, 3);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnAceptar.Size = new System.Drawing.Size(98, 24);
            this.BtnAceptar.TabIndex = 5;
            this.BtnAceptar.Text = "&Aceptar";
            this.BtnAceptar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.label29);
            this.Panel1.Controls.Add(this.TxtIdPublicacion);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(508, 27);
            this.Panel1.TabIndex = 278;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Cursor = System.Windows.Forms.Cursors.Default;
            this.label29.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            this.label29.ForeColor = System.Drawing.Color.Gold;
            this.label29.Location = new System.Drawing.Point(9, 8);
            this.label29.Name = "label29";
            this.label29.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label29.Size = new System.Drawing.Size(92, 12);
            this.label29.TabIndex = 125;
            this.label29.Text = "ID PUBLICACION";
            // 
            // TxtIdPublicacion
            // 
            this.TxtIdPublicacion.AcceptsReturn = true;
            this.TxtIdPublicacion.BackColor = System.Drawing.Color.LightCyan;
            this.TxtIdPublicacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtIdPublicacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtIdPublicacion.Location = new System.Drawing.Point(116, 4);
            this.TxtIdPublicacion.MaxLength = 0;
            this.TxtIdPublicacion.Name = "TxtIdPublicacion";
            this.TxtIdPublicacion.ReadOnly = true;
            this.TxtIdPublicacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtIdPublicacion.Size = new System.Drawing.Size(44, 20);
            this.TxtIdPublicacion.TabIndex = 126;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.ChkEsEstampado);
            this.panel4.Controls.Add(this.grpMotivoParcial);
            this.panel4.Controls.Add(this.TxtDesMotivo);
            this.panel4.Controls.Add(this.TxtCodMotivo);
            this.panel4.Controls.Add(this.TxtObservacion);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label32);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 27);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(508, 178);
            this.panel4.TabIndex = 279;
            // 
            // TxtDesMotivo
            // 
            this.TxtDesMotivo.AcceptsReturn = true;
            this.TxtDesMotivo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtDesMotivo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDesMotivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtDesMotivo.Location = new System.Drawing.Point(183, 97);
            this.TxtDesMotivo.MaxLength = 0;
            this.TxtDesMotivo.Name = "TxtDesMotivo";
            this.TxtDesMotivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDesMotivo.Size = new System.Drawing.Size(315, 20);
            this.TxtDesMotivo.TabIndex = 134;
            this.TxtDesMotivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDesMotivo_KeyPress);
            // 
            // TxtCodMotivo
            // 
            this.TxtCodMotivo.AcceptsReturn = true;
            this.TxtCodMotivo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCodMotivo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtCodMotivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtCodMotivo.Location = new System.Drawing.Point(139, 97);
            this.TxtCodMotivo.MaxLength = 0;
            this.TxtCodMotivo.Name = "TxtCodMotivo";
            this.TxtCodMotivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCodMotivo.Size = new System.Drawing.Size(43, 20);
            this.TxtCodMotivo.TabIndex = 132;
            this.TxtCodMotivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodMotivo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 104);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 133;
            this.label1.Text = "Motivo .................................";
            // 
            // TxtObservacion
            // 
            this.TxtObservacion.AcceptsReturn = true;
            this.TxtObservacion.BackColor = System.Drawing.SystemColors.Window;
            this.TxtObservacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtObservacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtObservacion.Location = new System.Drawing.Point(139, 7);
            this.TxtObservacion.MaxLength = 0;
            this.TxtObservacion.Multiline = true;
            this.TxtObservacion.Name = "TxtObservacion";
            this.TxtObservacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtObservacion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtObservacion.Size = new System.Drawing.Size(359, 84);
            this.TxtObservacion.TabIndex = 74;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(7, 15);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(148, 13);
            this.label32.TabIndex = 72;
            this.label32.Text = "Observacion ..........................";
            // 
            // ChkEsEstampado
            // 
            this.ChkEsEstampado.AutoSize = true;
            this.ChkEsEstampado.BackColor = System.Drawing.Color.Transparent;
            this.ChkEsEstampado.Checked = true;
            this.ChkEsEstampado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkEsEstampado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkEsEstampado.Location = new System.Drawing.Point(7, 124);
            this.ChkEsEstampado.Name = "ChkEsEstampado";
            this.ChkEsEstampado.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.ChkEsEstampado.Size = new System.Drawing.Size(121, 17);
            this.ChkEsEstampado.TabIndex = 241;
            this.ChkEsEstampado.Text = "¿Es Ficha Completa?";
            this.ChkEsEstampado.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.ChkEsEstampado.Click += new System.EventHandler(this.ChkEsEstampado_Click);
            // 
            // TxtDesMotivoParcial
            // 
            this.TxtDesMotivoParcial.AcceptsReturn = true;
            this.TxtDesMotivoParcial.BackColor = System.Drawing.SystemColors.Window;
            this.TxtDesMotivoParcial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDesMotivoParcial.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtDesMotivoParcial.Location = new System.Drawing.Point(180, 14);
            this.TxtDesMotivoParcial.MaxLength = 0;
            this.TxtDesMotivoParcial.Name = "TxtDesMotivoParcial";
            this.TxtDesMotivoParcial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDesMotivoParcial.Size = new System.Drawing.Size(315, 20);
            this.TxtDesMotivoParcial.TabIndex = 244;
            this.TxtDesMotivoParcial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDesMotivoParcial_KeyPress);
            // 
            // TxtCodMotivoParcial
            // 
            this.TxtCodMotivoParcial.AcceptsReturn = true;
            this.TxtCodMotivoParcial.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCodMotivoParcial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtCodMotivoParcial.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtCodMotivoParcial.Location = new System.Drawing.Point(136, 14);
            this.TxtCodMotivoParcial.MaxLength = 0;
            this.TxtCodMotivoParcial.Name = "TxtCodMotivoParcial";
            this.TxtCodMotivoParcial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCodMotivoParcial.Size = new System.Drawing.Size(43, 20);
            this.TxtCodMotivoParcial.TabIndex = 242;
            this.TxtCodMotivoParcial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCodMotivoParcial_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 243;
            this.label2.Text = "Motivo Ficha Parcial.................";
            // 
            // grpMotivoParcial
            // 
            this.grpMotivoParcial.Controls.Add(this.TxtCodMotivoParcial);
            this.grpMotivoParcial.Controls.Add(this.TxtDesMotivoParcial);
            this.grpMotivoParcial.Controls.Add(this.label2);
            this.grpMotivoParcial.Enabled = false;
            this.grpMotivoParcial.Location = new System.Drawing.Point(3, 131);
            this.grpMotivoParcial.Name = "grpMotivoParcial";
            this.grpMotivoParcial.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.grpMotivoParcial.Size = new System.Drawing.Size(501, 41);
            this.grpMotivoParcial.TabIndex = 245;
            this.grpMotivoParcial.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2010;
            // 
            // FrmBandejaControlFTPublicaciones_PrePublicar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 235);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBandejaControlFTPublicaciones_PrePublicar";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar Status";
            this.Load += new System.EventHandler(this.FrmBandejaControlFTPublicaciones_PrePublicar_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpMotivoParcial)).EndInit();
            this.grpMotivoParcial.ResumeLayout(false);
            this.grpMotivoParcial.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel3;
        internal Janus.Windows.EditControls.UIButton BtnCancelar;
        internal Janus.Windows.EditControls.UIButton BtnAceptar;
        internal System.Windows.Forms.Panel Panel1;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox TxtIdPublicacion;
        private System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.TextBox TxtObservacion;
        private System.Windows.Forms.Label label32;
        public System.Windows.Forms.TextBox TxtDesMotivo;
        public System.Windows.Forms.TextBox TxtCodMotivo;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UICheckBox ChkEsEstampado;
        private Janus.Windows.EditControls.UIGroupBox grpMotivoParcial;
        public System.Windows.Forms.TextBox TxtCodMotivoParcial;
        public System.Windows.Forms.TextBox TxtDesMotivoParcial;
        private System.Windows.Forms.Label label2;
    }
}