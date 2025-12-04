
namespace Estilo_Propio_Csharp
{
    partial class FrmMantMotivoRechazo
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
            Janus.Windows.ButtonBar.ButtonBarGroup BarraOpciones_Group_0 = new Janus.Windows.ButtonBar.ButtonBarGroup();
            Janus.Windows.ButtonBar.ButtonBarItem BarraOpciones_Item_0_0 = new Janus.Windows.ButtonBar.ButtonBarItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantMotivoRechazo));
            Janus.Windows.ButtonBar.ButtonBarItem BarraOpciones_Item_0_1 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.ButtonBar.ButtonBarItem BarraOpciones_Item_0_2 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.ButtonBar.ButtonBarItem BarraOpciones_Item_0_3 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.ButtonBar.ButtonBarItem BarraOpciones_Item_0_4 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.GridEX.GridEXLayout gridEX1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.BarraOpciones = new Janus.Windows.ButtonBar.ButtonBar();
            this.fraDatos = new System.Windows.Forms.Panel();
            this.TxtDesCaracteristica = new System.Windows.Forms.TextBox();
            this.TxtCodCaracteristica = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gridEX1 = new Janus.Windows.GridEX.GridEX();
            ((System.ComponentModel.ISupportInitialize)(this.BarraOpciones)).BeginInit();
            this.fraDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEX1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(524, 5);
            this.Panel1.TabIndex = 279;
            // 
            // BarraOpciones
            // 
            this.BarraOpciones.Dock = System.Windows.Forms.DockStyle.Bottom;
            BarraOpciones_Item_0_0.Image = ((System.Drawing.Image)(resources.GetObject("BarraOpciones_Item_0_0.Image")));
            BarraOpciones_Item_0_0.Key = "ADICIONAR";
            BarraOpciones_Item_0_0.SmallImageIndex = 42;
            BarraOpciones_Item_0_0.Text = "&Adicionar";
            BarraOpciones_Item_0_1.Image = ((System.Drawing.Image)(resources.GetObject("BarraOpciones_Item_0_1.Image")));
            BarraOpciones_Item_0_1.Key = "MODIFICAR";
            BarraOpciones_Item_0_1.SmallImageIndex = 40;
            BarraOpciones_Item_0_1.Text = "&Modificar";
            BarraOpciones_Item_0_2.Image = ((System.Drawing.Image)(resources.GetObject("BarraOpciones_Item_0_2.Image")));
            BarraOpciones_Item_0_2.Key = "ELIMINAR";
            BarraOpciones_Item_0_2.LargeImageIndex = 17;
            BarraOpciones_Item_0_2.SmallImageIndex = 39;
            BarraOpciones_Item_0_2.Text = "&Eliminar";
            BarraOpciones_Item_0_3.Image = ((System.Drawing.Image)(resources.GetObject("BarraOpciones_Item_0_3.Image")));
            BarraOpciones_Item_0_3.Key = "GRABAR";
            BarraOpciones_Item_0_3.LargeImageIndex = 22;
            BarraOpciones_Item_0_3.SmallImageIndex = 45;
            BarraOpciones_Item_0_3.Text = "&Grabar";
            BarraOpciones_Item_0_4.Image = ((System.Drawing.Image)(resources.GetObject("BarraOpciones_Item_0_4.Image")));
            BarraOpciones_Item_0_4.Key = "DESHACER";
            BarraOpciones_Item_0_4.LargeImageIndex = 21;
            BarraOpciones_Item_0_4.SmallImageIndex = 44;
            BarraOpciones_Item_0_4.Text = "&Deshacer";
            BarraOpciones_Group_0.Items.AddRange(new Janus.Windows.ButtonBar.ButtonBarItem[] {
            BarraOpciones_Item_0_0,
            BarraOpciones_Item_0_1,
            BarraOpciones_Item_0_2,
            BarraOpciones_Item_0_3,
            BarraOpciones_Item_0_4});
            BarraOpciones_Group_0.Key = "Group1";
            BarraOpciones_Group_0.TextAlignment = Janus.Windows.ButtonBar.Alignment.Near;
            BarraOpciones_Group_0.View = Janus.Windows.ButtonBar.ButtonBarView.SmallIcons;
            this.BarraOpciones.Groups.AddRange(new Janus.Windows.ButtonBar.ButtonBarGroup[] {
            BarraOpciones_Group_0});
            this.BarraOpciones.HeaderGroupVisible = false;
            this.BarraOpciones.ItemAppearance = Janus.Windows.ButtonBar.ItemAppearance.Flat;
            this.BarraOpciones.Location = new System.Drawing.Point(0, 409);
            this.BarraOpciones.Name = "BarraOpciones";
            this.BarraOpciones.OfficeColorScheme = Janus.Windows.ButtonBar.OfficeColorScheme.Blue;
            this.BarraOpciones.Orientation = Janus.Windows.ButtonBar.ButtonBarOrientation.Horizontal;
            this.BarraOpciones.SelectionArea = Janus.Windows.ButtonBar.SelectionArea.FullItem;
            this.BarraOpciones.ShadowOnHover = true;
            this.BarraOpciones.Size = new System.Drawing.Size(524, 25);
            this.BarraOpciones.SmallImageSize = new System.Drawing.Size(16, 16);
            this.BarraOpciones.TabIndex = 282;
            this.BarraOpciones.Text = "ButtonBar2";
            this.BarraOpciones.VisualStyle = Janus.Windows.ButtonBar.VisualStyle.Office2010;
            this.BarraOpciones.ItemClick += new Janus.Windows.ButtonBar.ItemEventHandler(this.BarraOpciones_ItemClick);
            // 
            // fraDatos
            // 
            this.fraDatos.BackColor = System.Drawing.Color.White;
            this.fraDatos.Controls.Add(this.TxtDesCaracteristica);
            this.fraDatos.Controls.Add(this.TxtCodCaracteristica);
            this.fraDatos.Controls.Add(this.label1);
            this.fraDatos.Controls.Add(this.label2);
            this.fraDatos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fraDatos.Location = new System.Drawing.Point(0, 434);
            this.fraDatos.Name = "fraDatos";
            this.fraDatos.Size = new System.Drawing.Size(524, 62);
            this.fraDatos.TabIndex = 281;
            // 
            // TxtDesCaracteristica
            // 
            this.TxtDesCaracteristica.AcceptsReturn = true;
            this.TxtDesCaracteristica.BackColor = System.Drawing.SystemColors.Window;
            this.TxtDesCaracteristica.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtDesCaracteristica.Enabled = false;
            this.TxtDesCaracteristica.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtDesCaracteristica.Location = new System.Drawing.Point(99, 31);
            this.TxtDesCaracteristica.MaxLength = 100;
            this.TxtDesCaracteristica.Name = "TxtDesCaracteristica";
            this.TxtDesCaracteristica.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDesCaracteristica.Size = new System.Drawing.Size(404, 20);
            this.TxtDesCaracteristica.TabIndex = 14;
            // 
            // TxtCodCaracteristica
            // 
            this.TxtCodCaracteristica.AcceptsReturn = true;
            this.TxtCodCaracteristica.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCodCaracteristica.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtCodCaracteristica.Enabled = false;
            this.TxtCodCaracteristica.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtCodCaracteristica.Location = new System.Drawing.Point(99, 7);
            this.TxtCodCaracteristica.MaxLength = 4;
            this.TxtCodCaracteristica.Name = "TxtCodCaracteristica";
            this.TxtCodCaracteristica.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCodCaracteristica.Size = new System.Drawing.Size(82, 20);
            this.TxtCodCaracteristica.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Código .................";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(10, 38);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Descripción .................";
            // 
            // gridEX1
            // 
            this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            gridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>";
            this.gridEX1.DesignTimeLayout = gridEX1_DesignTimeLayout;
            this.gridEX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEX1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridEX1.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridEX1.GroupByBoxVisible = false;
            this.gridEX1.Location = new System.Drawing.Point(0, 5);
            this.gridEX1.Name = "gridEX1";
            this.gridEX1.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.gridEX1.RecordNavigator = true;
            this.gridEX1.Size = new System.Drawing.Size(524, 404);
            this.gridEX1.TabIndex = 283;
            this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            this.gridEX1.SelectionChanged += new System.EventHandler(this.gridEX1_SelectionChanged);
            // 
            // FrmMantMotivoRechazo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 496);
            this.Controls.Add(this.gridEX1);
            this.Controls.Add(this.BarraOpciones);
            this.Controls.Add(this.fraDatos);
            this.Controls.Add(this.Panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMantMotivoRechazo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mantenimiento Motivo Rechazo";
            this.Load += new System.EventHandler(this.FrmMantMotivoRechazo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BarraOpciones)).EndInit();
            this.fraDatos.ResumeLayout(false);
            this.fraDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridEX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal Janus.Windows.ButtonBar.ButtonBar BarraOpciones;
        internal System.Windows.Forms.Panel fraDatos;
        public System.Windows.Forms.TextBox TxtDesCaracteristica;
        public System.Windows.Forms.TextBox TxtCodCaracteristica;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        internal Janus.Windows.GridEX.GridEX gridEX1;
    }
}