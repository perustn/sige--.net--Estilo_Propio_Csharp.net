
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaControlFTPublicaciones_UsuariosVerPbl
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
            Janus.Windows.GridEX.GridEXLayout GrdLista_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.GrdLista = new Janus.Windows.GridEX.GridEX();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtIdPublicacion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrdLista
            // 
            this.GrdLista.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.GrdLista.AlternatingColors = true;
            GrdLista_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>";
            this.GrdLista.DesignTimeLayout = GrdLista_DesignTimeLayout;
            this.GrdLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdLista.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.GrdLista.GroupByBoxVisible = false;
            this.GrdLista.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
            this.GrdLista.Location = new System.Drawing.Point(0, 27);
            this.GrdLista.Name = "GrdLista";
            this.GrdLista.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.GrdLista.RecordNavigator = true;
            this.GrdLista.Size = new System.Drawing.Size(471, 359);
            this.GrdLista.TabIndex = 285;
            this.GrdLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.label29);
            this.Panel1.Controls.Add(this.TxtIdPublicacion);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(471, 27);
            this.Panel1.TabIndex = 284;
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
            // FrmBandejaControlFTPublicaciones_UsuariosVerPbl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 386);
            this.Controls.Add(this.GrdLista);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBandejaControlFTPublicaciones_UsuariosVerPbl";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Usuarios que vieron Publicación";
            this.Load += new System.EventHandler(this.FrmBandejaControlFTPublicaciones_UsuariosVerPbl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Janus.Windows.GridEX.GridEX GrdLista;
        internal System.Windows.Forms.Panel Panel1;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox TxtIdPublicacion;
    }
}