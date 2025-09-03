
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaControlFTPublicaciones_RechazarPrePublicar
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
            Janus.Windows.GridEX.GridEXLayout grxLista_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBandejaControlFTPublicaciones_RechazarPrePublicar));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtIdPublicacion = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnCancelar = new Janus.Windows.EditControls.UIButton();
            this.BtnAceptar = new Janus.Windows.EditControls.UIButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.BtnMantMotivoRechazo = new Janus.Windows.EditControls.UIButton();
            this.panelRechazo = new System.Windows.Forms.Panel();
            this.grxLista = new Janus.Windows.GridEX.GridEX();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtDesMotivo = new System.Windows.Forms.TextBox();
            this.TxtCodMotivo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtObservacion = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panelRechazo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grxLista)).BeginInit();
            this.SuspendLayout();
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
            this.Panel1.TabIndex = 279;
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 376);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(508, 30);
            this.panel2.TabIndex = 280;
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.BtnMantMotivoRechazo);
            this.panel4.Controls.Add(this.panelRechazo);
            this.panel4.Controls.Add(this.TxtDesMotivo);
            this.panel4.Controls.Add(this.TxtCodMotivo);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.TxtObservacion);
            this.panel4.Controls.Add(this.label32);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 27);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(508, 349);
            this.panel4.TabIndex = 281;
            // 
            // BtnMantMotivoRechazo
            // 
            this.BtnMantMotivoRechazo.Image = global::Estilo_Propio_Csharp.Properties.Resources.ic_table_header_edit_48x48;
            this.BtnMantMotivoRechazo.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.BtnMantMotivoRechazo.ImageSize = new System.Drawing.Size(18, 18);
            this.BtnMantMotivoRechazo.Location = new System.Drawing.Point(475, 94);
            this.BtnMantMotivoRechazo.Name = "BtnMantMotivoRechazo";
            this.BtnMantMotivoRechazo.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnMantMotivoRechazo.Size = new System.Drawing.Size(23, 22);
            this.BtnMantMotivoRechazo.TabIndex = 136;
            this.BtnMantMotivoRechazo.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnMantMotivoRechazo.Click += new System.EventHandler(this.BtnMantMotivoRechazo_Click);
            // 
            // panelRechazo
            // 
            this.panelRechazo.Controls.Add(this.grxLista);
            this.panelRechazo.Controls.Add(this.label10);
            this.panelRechazo.Location = new System.Drawing.Point(10, 121);
            this.panelRechazo.Name = "panelRechazo";
            this.panelRechazo.Size = new System.Drawing.Size(488, 222);
            this.panelRechazo.TabIndex = 135;
            // 
            // grxLista
            // 
            this.grxLista.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grxLista_DesignTimeLayout.LayoutString = resources.GetString("grxLista_DesignTimeLayout.LayoutString");
            this.grxLista.DesignTimeLayout = grxLista_DesignTimeLayout;
            this.grxLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grxLista.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.grxLista.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grxLista.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grxLista.GroupByBoxVisible = false;
            this.grxLista.Location = new System.Drawing.Point(0, 18);
            this.grxLista.Name = "grxLista";
            this.grxLista.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.grxLista.RecordNavigator = true;
            this.grxLista.Size = new System.Drawing.Size(488, 204);
            this.grxLista.TabIndex = 224;
            this.grxLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Gold;
            this.label10.Cursor = System.Windows.Forms.Cursors.Default;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(488, 18);
            this.label10.TabIndex = 223;
            this.label10.Text = "INGRESAR EL NUMERO DE OCURRENCIAS PARA LAS SECCIONES";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtDesMotivo
            // 
            this.TxtDesMotivo.AcceptsReturn = true;
            this.TxtDesMotivo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtDesMotivo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDesMotivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtDesMotivo.Location = new System.Drawing.Point(183, 96);
            this.TxtDesMotivo.MaxLength = 0;
            this.TxtDesMotivo.Name = "TxtDesMotivo";
            this.TxtDesMotivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDesMotivo.Size = new System.Drawing.Size(290, 20);
            this.TxtDesMotivo.TabIndex = 134;
            this.TxtDesMotivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDesMotivo_KeyPress);
            // 
            // TxtCodMotivo
            // 
            this.TxtCodMotivo.AcceptsReturn = true;
            this.TxtCodMotivo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCodMotivo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtCodMotivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtCodMotivo.Location = new System.Drawing.Point(139, 96);
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
            this.label1.Location = new System.Drawing.Point(7, 103);
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
            // FrmBandejaControlFTPublicaciones_RechazarPrePublicar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 406);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBandejaControlFTPublicaciones_RechazarPrePublicar";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambio Status a PrePublicada Rechazada";
            this.Load += new System.EventHandler(this.FrmBandejaControlFTPublicaciones_RechazarPrePublicar_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panelRechazo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grxLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox TxtIdPublicacion;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel3;
        internal Janus.Windows.EditControls.UIButton BtnCancelar;
        internal Janus.Windows.EditControls.UIButton BtnAceptar;
        private System.Windows.Forms.Panel panel4;
        internal Janus.Windows.EditControls.UIButton BtnMantMotivoRechazo;
        private System.Windows.Forms.Panel panelRechazo;
        internal Janus.Windows.GridEX.GridEX grxLista;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox TxtDesMotivo;
        public System.Windows.Forms.TextBox TxtCodMotivo;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox TxtObservacion;
        private System.Windows.Forms.Label label32;
    }
}