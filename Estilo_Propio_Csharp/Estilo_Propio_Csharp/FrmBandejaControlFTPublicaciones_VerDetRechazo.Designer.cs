
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaControlFTPublicaciones_VerDetRechazo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBandejaControlFTPublicaciones_VerDetRechazo));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtIdPublicacion = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelRechazo = new System.Windows.Forms.Panel();
            this.grxLista = new Janus.Windows.GridEX.GridEX();
            this.TxtDesMotivo = new System.Windows.Forms.TextBox();
            this.TxtCodMotivo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
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
            this.Panel1.TabIndex = 285;
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
            this.TxtIdPublicacion.Location = new System.Drawing.Point(114, 4);
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
            this.panel4.Controls.Add(this.panelRechazo);
            this.panel4.Controls.Add(this.TxtDesMotivo);
            this.panel4.Controls.Add(this.TxtCodMotivo);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 27);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(508, 307);
            this.panel4.TabIndex = 286;
            // 
            // panelRechazo
            // 
            this.panelRechazo.Controls.Add(this.grxLista);
            this.panelRechazo.Location = new System.Drawing.Point(9, 35);
            this.panelRechazo.Name = "panelRechazo";
            this.panelRechazo.Size = new System.Drawing.Size(488, 264);
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
            this.grxLista.Location = new System.Drawing.Point(0, 0);
            this.grxLista.Name = "grxLista";
            this.grxLista.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.grxLista.RecordNavigator = true;
            this.grxLista.Size = new System.Drawing.Size(488, 264);
            this.grxLista.TabIndex = 224;
            this.grxLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            // 
            // TxtDesMotivo
            // 
            this.TxtDesMotivo.AcceptsReturn = true;
            this.TxtDesMotivo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtDesMotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDesMotivo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDesMotivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtDesMotivo.Location = new System.Drawing.Point(182, 8);
            this.TxtDesMotivo.MaxLength = 0;
            this.TxtDesMotivo.Name = "TxtDesMotivo";
            this.TxtDesMotivo.ReadOnly = true;
            this.TxtDesMotivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDesMotivo.Size = new System.Drawing.Size(315, 20);
            this.TxtDesMotivo.TabIndex = 134;
            // 
            // TxtCodMotivo
            // 
            this.TxtCodMotivo.AcceptsReturn = true;
            this.TxtCodMotivo.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCodMotivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCodMotivo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtCodMotivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtCodMotivo.Location = new System.Drawing.Point(138, 8);
            this.TxtCodMotivo.MaxLength = 0;
            this.TxtCodMotivo.Name = "TxtCodMotivo";
            this.TxtCodMotivo.ReadOnly = true;
            this.TxtCodMotivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCodMotivo.Size = new System.Drawing.Size(43, 20);
            this.TxtCodMotivo.TabIndex = 132;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 133;
            this.label1.Text = "Motivo .................................";
            // 
            // FrmBandejaControlFTPublicaciones_VerDetRechazo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 334);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.Panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBandejaControlFTPublicaciones_VerDetRechazo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ver Detalle Rechazo";
            this.Load += new System.EventHandler(this.FrmBandejaControlFTPublicaciones_VerDetRechazo_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelRechazo;
        internal Janus.Windows.GridEX.GridEX grxLista;
        public System.Windows.Forms.TextBox TxtDesMotivo;
        public System.Windows.Forms.TextBox TxtCodMotivo;
        private System.Windows.Forms.Label label1;
    }
}