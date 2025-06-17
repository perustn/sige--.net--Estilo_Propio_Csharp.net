
namespace Estilo_Propio_Csharp
{
    partial class FrmBusquedaSensitiva
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
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtBusqueda = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnCancelar = new Janus.Windows.EditControls.UIButton();
            this.BtnAceptar = new Janus.Windows.EditControls.UIButton();
            this.GrdLista = new Janus.Windows.GridEX.GridEX();
            this.Panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Controls.Add(this.TxtBusqueda);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel2.Location = new System.Drawing.Point(0, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(445, 33);
            this.Panel2.TabIndex = 8;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.ForeColor = System.Drawing.Color.Gold;
            this.Label1.Location = new System.Drawing.Point(15, 12);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(145, 13);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "INGRESE DATO A BUSCAR";
            // 
            // TxtBusqueda
            // 
            this.TxtBusqueda.AcceptsReturn = true;
            this.TxtBusqueda.BackColor = System.Drawing.SystemColors.Window;
            this.TxtBusqueda.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtBusqueda.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtBusqueda.Location = new System.Drawing.Point(179, 7);
            this.TxtBusqueda.MaxLength = 0;
            this.TxtBusqueda.Name = "TxtBusqueda";
            this.TxtBusqueda.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtBusqueda.Size = new System.Drawing.Size(239, 20);
            this.TxtBusqueda.TabIndex = 0;
            this.TxtBusqueda.TextChanged += new System.EventHandler(this.TxtBusqueda_TextChanged);
            this.TxtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBusqueda_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 489);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 29);
            this.panel1.TabIndex = 170;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnCancelar);
            this.panel3.Controls.Add(this.BtnAceptar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(234, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(211, 29);
            this.panel3.TabIndex = 34;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.ImageKey = "48px-Crystal_Clear_action_button_cancel.png";
            this.BtnCancelar.ImageSize = new System.Drawing.Size(25, 25);
            this.BtnCancelar.Location = new System.Drawing.Point(110, 1);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnCancelar.Size = new System.Drawing.Size(95, 27);
            this.BtnCancelar.TabIndex = 33;
            this.BtnCancelar.Text = "&cancelar";
            this.BtnCancelar.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Empty;
            this.BtnCancelar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAceptar.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.BtnAceptar.ImageSize = new System.Drawing.Size(25, 25);
            this.BtnAceptar.Location = new System.Drawing.Point(6, 1);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnAceptar.Size = new System.Drawing.Size(95, 27);
            this.BtnAceptar.TabIndex = 32;
            this.BtnAceptar.Text = "&aceptar";
            this.BtnAceptar.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Empty;
            this.BtnAceptar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
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
            this.GrdLista.Location = new System.Drawing.Point(0, 33);
            this.GrdLista.Name = "GrdLista";
            this.GrdLista.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.GrdLista.RecordNavigator = true;
            this.GrdLista.Size = new System.Drawing.Size(445, 456);
            this.GrdLista.TabIndex = 171;
            this.GrdLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            this.GrdLista.DoubleClick += new System.EventHandler(this.GrdLista_DoubleClick);
            // 
            // FrmBusquedaSensitiva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 518);
            this.ControlBox = false;
            this.Controls.Add(this.GrdLista);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmBusquedaSensitiva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Búsqueda de dato";
            this.Load += new System.EventHandler(this.FrmBusquedaSensitiva_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrdLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel2;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.TextBox TxtBusqueda;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel panel3;
        internal Janus.Windows.EditControls.UIButton BtnCancelar;
        internal Janus.Windows.EditControls.UIButton BtnAceptar;
        internal Janus.Windows.GridEX.GridEX GrdLista;
    }
}