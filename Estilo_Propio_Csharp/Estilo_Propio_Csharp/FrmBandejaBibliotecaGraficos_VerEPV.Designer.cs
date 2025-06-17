
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaBibliotecaGraficos_VerEPV
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtIdProceso = new System.Windows.Forms.TextBox();
            this.GrdLista = new Janus.Windows.GridEX.GridEX();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.label29);
            this.Panel1.Controls.Add(this.TxtIdProceso);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(483, 27);
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
            this.label29.Size = new System.Drawing.Size(112, 12);
            this.label29.TabIndex = 125;
            this.label29.Text = "ID PROCESO IMAGEN";
            // 
            // TxtIdProceso
            // 
            this.TxtIdProceso.AcceptsReturn = true;
            this.TxtIdProceso.BackColor = System.Drawing.Color.LightCyan;
            this.TxtIdProceso.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtIdProceso.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtIdProceso.Location = new System.Drawing.Point(130, 4);
            this.TxtIdProceso.MaxLength = 0;
            this.TxtIdProceso.Name = "TxtIdProceso";
            this.TxtIdProceso.ReadOnly = true;
            this.TxtIdProceso.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtIdProceso.Size = new System.Drawing.Size(44, 20);
            this.TxtIdProceso.TabIndex = 126;
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
            this.GrdLista.Size = new System.Drawing.Size(483, 384);
            this.GrdLista.TabIndex = 279;
            this.GrdLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            // 
            // FrmBandejaBibliotecaGraficos_VerEPV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 411);
            this.Controls.Add(this.GrdLista);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBandejaBibliotecaGraficos_VerEPV";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ver EVP";
            this.Load += new System.EventHandler(this.FrmBandejaBibliotecaGraficos_VerEPV_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox TxtIdProceso;
        internal Janus.Windows.GridEX.GridEX GrdLista;
    }
}