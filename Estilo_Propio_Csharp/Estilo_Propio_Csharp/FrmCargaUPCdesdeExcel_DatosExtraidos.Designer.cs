
namespace Estilo_Propio_Csharp
{
    partial class FrmCargaUPCdesdeExcel_DatosExtraidos
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
            Janus.Windows.GridEX.GridEXLayout GridEX1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCargaUPCdesdeExcel_DatosExtraidos));
            this.Label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GridEX1 = new Janus.Windows.GridEX.GridEX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNomArchivo = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridEX1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.Gold;
            this.Label3.Location = new System.Drawing.Point(25, 11);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(47, 12);
            this.Label3.TabIndex = 122;
            this.Label3.Text = "CLIENTE";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GridEX1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1063, 373);
            this.panel1.TabIndex = 1;
            // 
            // GridEX1
            // 
            this.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.GridEX1.AlternatingColors = true;
            GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString");
            this.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout;
            this.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridEX1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.GridEX1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
            this.GridEX1.Location = new System.Drawing.Point(0, 30);
            this.GridEX1.Name = "GridEX1";
            this.GridEX1.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.GridEX1.RecordNavigator = true;
            this.GridEX1.SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
            this.GridEX1.Size = new System.Drawing.Size(1063, 343);
            this.GridEX1.TabIndex = 2;
            this.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True;
            this.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtNomArchivo);
            this.panel2.Controls.Add(this.Label3);
            this.panel2.Controls.Add(this.txtCliente);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1063, 30);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(432, 11);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 124;
            this.label1.Text = "ARCHIVO";
            // 
            // txtNomArchivo
            // 
            this.txtNomArchivo.AcceptsReturn = true;
            this.txtNomArchivo.BackColor = System.Drawing.Color.LightCyan;
            this.txtNomArchivo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNomArchivo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNomArchivo.Location = new System.Drawing.Point(489, 5);
            this.txtNomArchivo.MaxLength = 0;
            this.txtNomArchivo.Name = "txtNomArchivo";
            this.txtNomArchivo.ReadOnly = true;
            this.txtNomArchivo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNomArchivo.Size = new System.Drawing.Size(274, 20);
            this.txtNomArchivo.TabIndex = 125;
            // 
            // txtCliente
            // 
            this.txtCliente.AcceptsReturn = true;
            this.txtCliente.BackColor = System.Drawing.Color.LightCyan;
            this.txtCliente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCliente.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCliente.Location = new System.Drawing.Point(111, 5);
            this.txtCliente.MaxLength = 0;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCliente.Size = new System.Drawing.Size(295, 20);
            this.txtCliente.TabIndex = 123;
            // 
            // FrmCargaUPCdesdeExcel_DatosExtraidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 373);
            this.Controls.Add(this.panel1);
            this.Name = "FrmCargaUPCdesdeExcel_DatosExtraidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datos Extraidos";
            this.Load += new System.EventHandler(this.FrmCargaUPCdesdeExcel_DatosExtraidos_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridEX1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label Label3;
        private System.Windows.Forms.Panel panel1;
        protected Janus.Windows.GridEX.GridEX GridEX1;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtNomArchivo;
        public System.Windows.Forms.TextBox txtCliente;
    }
}