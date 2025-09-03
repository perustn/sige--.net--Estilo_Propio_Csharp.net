
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaControlFTPublicaciones_VerPublicaciones
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
            this.TxtEstiloPropio = new System.Windows.Forms.TextBox();
            this.GrdLista = new Janus.Windows.GridEX.GridEX();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtIDFT = new System.Windows.Forms.TextBox();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.TxtIDFT);
            this.Panel1.Controls.Add(this.label1);
            this.Panel1.Controls.Add(this.TxtVersion);
            this.Panel1.Controls.Add(this.label29);
            this.Panel1.Controls.Add(this.TxtEstiloPropio);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(740, 27);
            this.Panel1.TabIndex = 283;
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
            this.label29.Size = new System.Drawing.Size(87, 12);
            this.label29.TabIndex = 125;
            this.label29.Text = "ESTILO PROPIO";
            // 
            // TxtEstiloPropio
            // 
            this.TxtEstiloPropio.AcceptsReturn = true;
            this.TxtEstiloPropio.BackColor = System.Drawing.Color.LightCyan;
            this.TxtEstiloPropio.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtEstiloPropio.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtEstiloPropio.Location = new System.Drawing.Point(102, 4);
            this.TxtEstiloPropio.MaxLength = 0;
            this.TxtEstiloPropio.Name = "TxtEstiloPropio";
            this.TxtEstiloPropio.ReadOnly = true;
            this.TxtEstiloPropio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtEstiloPropio.Size = new System.Drawing.Size(65, 20);
            this.TxtEstiloPropio.TabIndex = 126;
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
            this.GrdLista.Size = new System.Drawing.Size(740, 374);
            this.GrdLista.TabIndex = 284;
            this.GrdLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            this.GrdLista.LinkClicked += new Janus.Windows.GridEX.ColumnActionEventHandler(this.GrdLista_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(192, 7);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 127;
            this.label1.Text = "VERSION";
            // 
            // TxtVersion
            // 
            this.TxtVersion.AcceptsReturn = true;
            this.TxtVersion.BackColor = System.Drawing.Color.LightCyan;
            this.TxtVersion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtVersion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtVersion.Location = new System.Drawing.Point(251, 3);
            this.TxtVersion.MaxLength = 0;
            this.TxtVersion.Name = "TxtVersion";
            this.TxtVersion.ReadOnly = true;
            this.TxtVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVersion.Size = new System.Drawing.Size(49, 20);
            this.TxtVersion.TabIndex = 128;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(330, 7);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(99, 12);
            this.label2.TabIndex = 129;
            this.label2.Text = "ID FICHA TECNICA";
            // 
            // TxtIDFT
            // 
            this.TxtIDFT.AcceptsReturn = true;
            this.TxtIDFT.BackColor = System.Drawing.Color.LightCyan;
            this.TxtIDFT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtIDFT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtIDFT.Location = new System.Drawing.Point(436, 3);
            this.TxtIDFT.MaxLength = 0;
            this.TxtIDFT.Name = "TxtIDFT";
            this.TxtIDFT.ReadOnly = true;
            this.TxtIDFT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtIDFT.Size = new System.Drawing.Size(49, 20);
            this.TxtIDFT.TabIndex = 130;
            // 
            // FrmBandejaControlFTPublicaciones_VerPublicaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 401);
            this.Controls.Add(this.GrdLista);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBandejaControlFTPublicaciones_VerPublicaciones";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visualiza todas las Publicaciones";
            this.Load += new System.EventHandler(this.FrmBandejaControlFTPublicaciones_VerPublicaciones_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox TxtEstiloPropio;
        internal Janus.Windows.GridEX.GridEX GrdLista;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox TxtIDFT;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox TxtVersion;
    }
}