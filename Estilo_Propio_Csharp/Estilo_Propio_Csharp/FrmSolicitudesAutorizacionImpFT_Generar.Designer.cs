
namespace Estilo_Propio_Csharp
{
    partial class FrmSolicitudesAutorizacionImpFT_Generar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSolicitudesAutorizacionImpFT_Generar));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnCancelar = new Janus.Windows.EditControls.UIButton();
            this.BtnAceptar = new Janus.Windows.EditControls.UIButton();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.panelRechazo = new System.Windows.Forms.Panel();
            this.grxLista = new Janus.Windows.GridEX.GridEX();
            this.label10 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.UiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.grpNPublicadas = new Janus.Windows.EditControls.UIGroupBox();
            this.TxtNPublicadas = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpOP = new Janus.Windows.EditControls.UIGroupBox();
            this.TxtOP = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.grpEstiloVersion = new Janus.Windows.EditControls.UIGroupBox();
            this.TxtVersion = new System.Windows.Forms.TextBox();
            this.TxtEP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnBuscar = new Janus.Windows.EditControls.UIButton();
            this.OptOP = new System.Windows.Forms.RadioButton();
            this.OptEstiloVersion = new System.Windows.Forms.RadioButton();
            this.OptNPublicadas = new System.Windows.Forms.RadioButton();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelRechazo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grxLista)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UiGroupBox1)).BeginInit();
            this.UiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpNPublicadas)).BeginInit();
            this.grpNPublicadas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpOP)).BeginInit();
            this.grpOP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpEstiloVersion)).BeginInit();
            this.grpEstiloVersion.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 420);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 30);
            this.panel2.TabIndex = 283;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnCancelar);
            this.panel3.Controls.Add(this.BtnAceptar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(593, 0);
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
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(800, 7);
            this.Panel1.TabIndex = 282;
            // 
            // panelRechazo
            // 
            this.panelRechazo.Controls.Add(this.grxLista);
            this.panelRechazo.Controls.Add(this.label10);
            this.panelRechazo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRechazo.Location = new System.Drawing.Point(0, 108);
            this.panelRechazo.Name = "panelRechazo";
            this.panelRechazo.Size = new System.Drawing.Size(800, 312);
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
            this.grxLista.Size = new System.Drawing.Size(800, 294);
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
            this.label10.Size = new System.Drawing.Size(800, 18);
            this.label10.TabIndex = 223;
            this.label10.Text = "SELECCIONE";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.AliceBlue;
            this.panel4.Controls.Add(this.UiGroupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 7);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(800, 101);
            this.panel4.TabIndex = 284;
            // 
            // UiGroupBox1
            // 
            this.UiGroupBox1.Controls.Add(this.OptOP);
            this.UiGroupBox1.Controls.Add(this.OptEstiloVersion);
            this.UiGroupBox1.Controls.Add(this.OptNPublicadas);
            this.UiGroupBox1.Controls.Add(this.grpNPublicadas);
            this.UiGroupBox1.Controls.Add(this.grpOP);
            this.UiGroupBox1.Controls.Add(this.grpEstiloVersion);
            this.UiGroupBox1.Controls.Add(this.BtnBuscar);
            this.UiGroupBox1.Location = new System.Drawing.Point(9, 3);
            this.UiGroupBox1.Name = "UiGroupBox1";
            this.UiGroupBox1.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.UiGroupBox1.Size = new System.Drawing.Size(787, 92);
            this.UiGroupBox1.TabIndex = 102;
            this.UiGroupBox1.Text = "Realizar búsqueda";
            this.UiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2010;
            // 
            // grpNPublicadas
            // 
            this.grpNPublicadas.Controls.Add(this.TxtNPublicadas);
            this.grpNPublicadas.Controls.Add(this.label1);
            this.grpNPublicadas.ForeColor = System.Drawing.Color.White;
            this.grpNPublicadas.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.grpNPublicadas.Location = new System.Drawing.Point(220, 16);
            this.grpNPublicadas.Name = "grpNPublicadas";
            this.grpNPublicadas.Size = new System.Drawing.Size(352, 63);
            this.grpNPublicadas.TabIndex = 155;
            this.grpNPublicadas.Tag = "2";
            this.grpNPublicadas.Visible = false;
            this.grpNPublicadas.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2010;
            // 
            // TxtNPublicadas
            // 
            this.TxtNPublicadas.AcceptsReturn = true;
            this.TxtNPublicadas.BackColor = System.Drawing.SystemColors.Window;
            this.TxtNPublicadas.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtNPublicadas.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtNPublicadas.Location = new System.Drawing.Point(112, 13);
            this.TxtNPublicadas.MaxLength = 5;
            this.TxtNPublicadas.Name = "TxtNPublicadas";
            this.TxtNPublicadas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtNPublicadas.Size = new System.Drawing.Size(56, 20);
            this.TxtNPublicadas.TabIndex = 55;
            this.TxtNPublicadas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNPublicadas_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(15, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 56;
            this.label1.Text = "N° Publicadas";
            // 
            // grpOP
            // 
            this.grpOP.Controls.Add(this.TxtOP);
            this.grpOP.Controls.Add(this.label16);
            this.grpOP.ForeColor = System.Drawing.Color.White;
            this.grpOP.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.grpOP.Location = new System.Drawing.Point(220, 16);
            this.grpOP.Name = "grpOP";
            this.grpOP.Size = new System.Drawing.Size(352, 63);
            this.grpOP.TabIndex = 154;
            this.grpOP.Tag = "2";
            this.grpOP.Visible = false;
            this.grpOP.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2010;
            // 
            // TxtOP
            // 
            this.TxtOP.AcceptsReturn = true;
            this.TxtOP.BackColor = System.Drawing.SystemColors.Window;
            this.TxtOP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtOP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtOP.Location = new System.Drawing.Point(54, 13);
            this.TxtOP.MaxLength = 5;
            this.TxtOP.Name = "TxtOP";
            this.TxtOP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtOP.Size = new System.Drawing.Size(105, 20);
            this.TxtOP.TabIndex = 55;
            this.TxtOP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOP_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label16.Location = new System.Drawing.Point(15, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 13);
            this.label16.TabIndex = 56;
            this.label16.Text = "OP";
            // 
            // grpEstiloVersion
            // 
            this.grpEstiloVersion.Controls.Add(this.TxtVersion);
            this.grpEstiloVersion.Controls.Add(this.TxtEP);
            this.grpEstiloVersion.Controls.Add(this.label2);
            this.grpEstiloVersion.Controls.Add(this.label3);
            this.grpEstiloVersion.ForeColor = System.Drawing.Color.White;
            this.grpEstiloVersion.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.grpEstiloVersion.Location = new System.Drawing.Point(220, 16);
            this.grpEstiloVersion.Name = "grpEstiloVersion";
            this.grpEstiloVersion.Size = new System.Drawing.Size(352, 63);
            this.grpEstiloVersion.TabIndex = 153;
            this.grpEstiloVersion.Tag = "2";
            this.grpEstiloVersion.Visible = false;
            this.grpEstiloVersion.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2010;
            // 
            // TxtVersion
            // 
            this.TxtVersion.AcceptsReturn = true;
            this.TxtVersion.BackColor = System.Drawing.SystemColors.Window;
            this.TxtVersion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtVersion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtVersion.Location = new System.Drawing.Point(235, 16);
            this.TxtVersion.MaxLength = 2;
            this.TxtVersion.Name = "TxtVersion";
            this.TxtVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVersion.Size = new System.Drawing.Size(62, 20);
            this.TxtVersion.TabIndex = 57;
            this.TxtVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVersion_KeyPress);
            // 
            // TxtEP
            // 
            this.TxtEP.AcceptsReturn = true;
            this.TxtEP.BackColor = System.Drawing.SystemColors.Window;
            this.TxtEP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtEP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtEP.Location = new System.Drawing.Point(54, 16);
            this.TxtEP.MaxLength = 5;
            this.TxtEP.Name = "TxtEP";
            this.TxtEP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtEP.Size = new System.Drawing.Size(105, 20);
            this.TxtEP.TabIndex = 55;
            this.TxtEP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtEP_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(180, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Versión";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(15, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "EP";
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.BtnBuscar.ImageSize = new System.Drawing.Size(32, 32);
            this.BtnBuscar.Location = new System.Drawing.Point(648, 31);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnBuscar.Size = new System.Drawing.Size(98, 24);
            this.BtnBuscar.TabIndex = 102;
            this.BtnBuscar.Text = "Buscar";
            this.BtnBuscar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // OptOP
            // 
            this.OptOP.AutoSize = true;
            this.OptOP.BackColor = System.Drawing.Color.Transparent;
            this.OptOP.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptOP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptOP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptOP.ForeColor = System.Drawing.Color.Black;
            this.OptOP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptOP.Location = new System.Drawing.Point(34, 44);
            this.OptOP.Name = "OptOP";
            this.OptOP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptOP.Size = new System.Drawing.Size(40, 19);
            this.OptOP.TabIndex = 157;
            this.OptOP.Tag = "2";
            this.OptOP.Text = "OP";
            this.OptOP.UseVisualStyleBackColor = false;
            this.OptOP.Click += new System.EventHandler(this.OpcionFiltroBusqueda);
            // 
            // OptEstiloVersion
            // 
            this.OptEstiloVersion.AutoSize = true;
            this.OptEstiloVersion.BackColor = System.Drawing.Color.Transparent;
            this.OptEstiloVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptEstiloVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptEstiloVersion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptEstiloVersion.ForeColor = System.Drawing.Color.Black;
            this.OptEstiloVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptEstiloVersion.Location = new System.Drawing.Point(34, 67);
            this.OptEstiloVersion.Name = "OptEstiloVersion";
            this.OptEstiloVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptEstiloVersion.Size = new System.Drawing.Size(86, 19);
            this.OptEstiloVersion.TabIndex = 158;
            this.OptEstiloVersion.Tag = "3";
            this.OptEstiloVersion.Text = "EP - Versión";
            this.OptEstiloVersion.UseVisualStyleBackColor = false;
            this.OptEstiloVersion.Click += new System.EventHandler(this.OpcionFiltroBusqueda);
            // 
            // OptNPublicadas
            // 
            this.OptNPublicadas.AutoSize = true;
            this.OptNPublicadas.BackColor = System.Drawing.Color.Transparent;
            this.OptNPublicadas.Checked = true;
            this.OptNPublicadas.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptNPublicadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptNPublicadas.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptNPublicadas.ForeColor = System.Drawing.Color.Black;
            this.OptNPublicadas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptNPublicadas.Location = new System.Drawing.Point(34, 21);
            this.OptNPublicadas.Name = "OptNPublicadas";
            this.OptNPublicadas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptNPublicadas.Size = new System.Drawing.Size(136, 19);
            this.OptNPublicadas.TabIndex = 156;
            this.OptNPublicadas.TabStop = true;
            this.OptNPublicadas.Tag = "1";
            this.OptNPublicadas.Text = "Últimas N Publicadas";
            this.OptNPublicadas.UseVisualStyleBackColor = false;
            this.OptNPublicadas.Click += new System.EventHandler(this.OpcionFiltroBusqueda);
            // 
            // FrmSolicitudesAutorizacionImpFT_Generar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelRechazo);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSolicitudesAutorizacionImpFT_Generar";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generar";
            this.Load += new System.EventHandler(this.FrmSolicitudesAutorizacionImpFT_Generar_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelRechazo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grxLista)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UiGroupBox1)).EndInit();
            this.UiGroupBox1.ResumeLayout(false);
            this.UiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpNPublicadas)).EndInit();
            this.grpNPublicadas.ResumeLayout(false);
            this.grpNPublicadas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpOP)).EndInit();
            this.grpOP.ResumeLayout(false);
            this.grpOP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpEstiloVersion)).EndInit();
            this.grpEstiloVersion.ResumeLayout(false);
            this.grpEstiloVersion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel3;
        internal Janus.Windows.EditControls.UIButton BtnCancelar;
        internal Janus.Windows.EditControls.UIButton BtnAceptar;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Panel panelRechazo;
        internal Janus.Windows.GridEX.GridEX grxLista;
        public System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Panel panel4;
        internal Janus.Windows.EditControls.UIGroupBox UiGroupBox1;
        internal Janus.Windows.EditControls.UIButton BtnBuscar;
        internal Janus.Windows.EditControls.UIGroupBox grpEstiloVersion;
        public System.Windows.Forms.TextBox TxtVersion;
        public System.Windows.Forms.TextBox TxtEP;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal Janus.Windows.EditControls.UIGroupBox grpOP;
        public System.Windows.Forms.TextBox TxtOP;
        internal System.Windows.Forms.Label label16;
        internal Janus.Windows.EditControls.UIGroupBox grpNPublicadas;
        public System.Windows.Forms.TextBox TxtNPublicadas;
        internal System.Windows.Forms.Label label1;
        public System.Windows.Forms.RadioButton OptOP;
        public System.Windows.Forms.RadioButton OptEstiloVersion;
        public System.Windows.Forms.RadioButton OptNPublicadas;
    }
}