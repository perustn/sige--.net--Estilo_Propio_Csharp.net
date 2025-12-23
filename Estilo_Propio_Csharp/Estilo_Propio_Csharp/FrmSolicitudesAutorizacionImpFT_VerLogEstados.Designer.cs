
namespace Estilo_Propio_Csharp
{
    partial class FrmSolicitudesAutorizacionImpFT_VerLogEstados
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
            this.components = new System.ComponentModel.Container();
            Janus.Windows.GridEX.GridEXLayout GrdLista_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.ButtonBar.ButtonBarGroup ButtonBar1_Group_0 = new Janus.Windows.ButtonBar.ButtonBarGroup();
            Janus.Windows.ButtonBar.ButtonBarItem ButtonBar1_Item_0_0 = new Janus.Windows.ButtonBar.ButtonBarItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSolicitudesAutorizacionImpFT_VerLogEstados));
            this.GrdLista = new Janus.Windows.GridEX.GridEX();
            this.ButtonBar1 = new Janus.Windows.ButtonBar.ButtonBar();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.TxtIdSolicitud = new System.Windows.Forms.TextBox();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonBar1)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GrdLista
            // 
            this.GrdLista.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.GrdLista.AlternatingColors = true;
            this.GrdLista.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            GrdLista_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>";
            this.GrdLista.DesignTimeLayout = GrdLista_DesignTimeLayout;
            this.GrdLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdLista.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.GrdLista.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.GrdLista.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.GrdLista.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.GrdLista.GroupByBoxVisible = false;
            this.GrdLista.HideSelection = Janus.Windows.GridEX.HideSelection.HighlightInactive;
            this.GrdLista.Location = new System.Drawing.Point(0, 49);
            this.GrdLista.Name = "GrdLista";
            this.GrdLista.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.GrdLista.RecordNavigator = true;
            this.GrdLista.Size = new System.Drawing.Size(511, 382);
            this.GrdLista.TabIndex = 287;
            this.GrdLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            // 
            // ButtonBar1
            // 
            this.ButtonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            ButtonBar1_Group_0.ImageKey = "Opciones.jpg";
            ButtonBar1_Item_0_0.Image = ((System.Drawing.Image)(resources.GetObject("ButtonBar1_Item_0_0.Image")));
            ButtonBar1_Item_0_0.Key = "EXPORTAR";
            ButtonBar1_Item_0_0.LargeImageKey = "new_16x16.png";
            ButtonBar1_Item_0_0.SmallImageIndex = 10;
            ButtonBar1_Item_0_0.Text = "Exportar información a excel";
            ButtonBar1_Group_0.Items.AddRange(new Janus.Windows.ButtonBar.ButtonBarItem[] {
            ButtonBar1_Item_0_0});
            ButtonBar1_Group_0.Key = "Group1";
            ButtonBar1_Group_0.Text = "Opciones";
            ButtonBar1_Group_0.TextAlignment = Janus.Windows.ButtonBar.Alignment.Near;
            ButtonBar1_Group_0.View = Janus.Windows.ButtonBar.ButtonBarView.SmallIcons;
            this.ButtonBar1.Groups.AddRange(new Janus.Windows.ButtonBar.ButtonBarGroup[] {
            ButtonBar1_Group_0});
            this.ButtonBar1.HeaderGroupVisible = false;
            this.ButtonBar1.ItemAppearance = Janus.Windows.ButtonBar.ItemAppearance.Flat;
            this.ButtonBar1.Location = new System.Drawing.Point(0, 27);
            this.ButtonBar1.Name = "ButtonBar1";
            this.ButtonBar1.OfficeColorScheme = Janus.Windows.ButtonBar.OfficeColorScheme.Blue;
            this.ButtonBar1.Orientation = Janus.Windows.ButtonBar.ButtonBarOrientation.Horizontal;
            this.ButtonBar1.SelectionArea = Janus.Windows.ButtonBar.SelectionArea.FullItem;
            this.ButtonBar1.ShadowOnHover = true;
            this.ButtonBar1.Size = new System.Drawing.Size(511, 22);
            this.ButtonBar1.SmallImageSize = new System.Drawing.Size(16, 16);
            this.ButtonBar1.TabIndex = 286;
            this.ButtonBar1.Text = "Opciones";
            this.ButtonBar1.VisualStyle = Janus.Windows.ButtonBar.VisualStyle.Office2010;
            this.ButtonBar1.ItemClick += new Janus.Windows.ButtonBar.ItemEventHandler(this.ButtonBar1_ItemClick);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Controls.Add(this.label29);
            this.Panel1.Controls.Add(this.TxtIdSolicitud);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(511, 27);
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
            this.label29.Size = new System.Drawing.Size(76, 12);
            this.label29.TabIndex = 125;
            this.label29.Text = "ID SOLICITUD";
            // 
            // TxtIdSolicitud
            // 
            this.TxtIdSolicitud.AcceptsReturn = true;
            this.TxtIdSolicitud.BackColor = System.Drawing.Color.LightCyan;
            this.TxtIdSolicitud.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtIdSolicitud.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtIdSolicitud.Location = new System.Drawing.Point(94, 4);
            this.TxtIdSolicitud.MaxLength = 0;
            this.TxtIdSolicitud.Name = "TxtIdSolicitud";
            this.TxtIdSolicitud.ReadOnly = true;
            this.TxtIdSolicitud.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtIdSolicitud.Size = new System.Drawing.Size(44, 20);
            this.TxtIdSolicitud.TabIndex = 126;
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.GrdLista;
            // 
            // FrmSolicitudesAutorizacionImpFT_VerLogEstados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 431);
            this.Controls.Add(this.GrdLista);
            this.Controls.Add(this.ButtonBar1);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSolicitudesAutorizacionImpFT_VerLogEstados";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ver Log Estados";
            this.Load += new System.EventHandler(this.FrmSolicitudesAutorizacionImpFT_VerLogEstados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonBar1)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Janus.Windows.GridEX.GridEX GrdLista;
        internal Janus.Windows.ButtonBar.ButtonBar ButtonBar1;
        internal System.Windows.Forms.Panel Panel1;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.TextBox TxtIdSolicitud;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
    }
}