
namespace Estilo_Propio_Csharp
{
    partial class FrmEstiloVersionMadreDependencia_Adicionar
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BtnCancelar = new Janus.Windows.EditControls.UIButton();
            this.BtnAceptar = new Janus.Windows.EditControls.UIButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TxtVersionDescripcion = new System.Windows.Forms.TextBox();
            this.TxtVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtEPDescripcion = new System.Windows.Forms.TextBox();
            this.TxtEP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(494, 7);
            this.panel2.TabIndex = 139;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 30);
            this.panel1.TabIndex = 281;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.BtnCancelar);
            this.panel3.Controls.Add(this.BtnAceptar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(287, 0);
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
            this.panel4.Controls.Add(this.TxtVersionDescripcion);
            this.panel4.Controls.Add(this.TxtVersion);
            this.panel4.Controls.Add(this.TxtEPDescripcion);
            this.panel4.Controls.Add(this.TxtEP);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 7);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(494, 65);
            this.panel4.TabIndex = 282;
            // 
            // TxtVersionDescripcion
            // 
            this.TxtVersionDescripcion.AcceptsReturn = true;
            this.TxtVersionDescripcion.BackColor = System.Drawing.SystemColors.Window;
            this.TxtVersionDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtVersionDescripcion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtVersionDescripcion.Location = new System.Drawing.Point(188, 36);
            this.TxtVersionDescripcion.MaxLength = 5;
            this.TxtVersionDescripcion.Name = "TxtVersionDescripcion";
            this.TxtVersionDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVersionDescripcion.Size = new System.Drawing.Size(294, 20);
            this.TxtVersionDescripcion.TabIndex = 138;
            this.TxtVersionDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVersionDescripcion_KeyPress);
            // 
            // TxtVersion
            // 
            this.TxtVersion.AcceptsReturn = true;
            this.TxtVersion.BackColor = System.Drawing.SystemColors.Window;
            this.TxtVersion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtVersion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtVersion.Location = new System.Drawing.Point(124, 36);
            this.TxtVersion.MaxLength = 5;
            this.TxtVersion.Name = "TxtVersion";
            this.TxtVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtVersion.Size = new System.Drawing.Size(62, 20);
            this.TxtVersion.TabIndex = 137;
            this.TxtVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVersion_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 136;
            this.label2.Text = "Versión ..............................";
            // 
            // TxtEPDescripcion
            // 
            this.TxtEPDescripcion.AcceptsReturn = true;
            this.TxtEPDescripcion.BackColor = System.Drawing.SystemColors.Window;
            this.TxtEPDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtEPDescripcion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtEPDescripcion.Location = new System.Drawing.Point(188, 9);
            this.TxtEPDescripcion.MaxLength = 5;
            this.TxtEPDescripcion.Name = "TxtEPDescripcion";
            this.TxtEPDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtEPDescripcion.Size = new System.Drawing.Size(294, 20);
            this.TxtEPDescripcion.TabIndex = 135;
            this.TxtEPDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtEPDescripcion_KeyPress);
            // 
            // TxtEP
            // 
            this.TxtEP.AcceptsReturn = true;
            this.TxtEP.BackColor = System.Drawing.SystemColors.Window;
            this.TxtEP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtEP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtEP.Location = new System.Drawing.Point(124, 9);
            this.TxtEP.MaxLength = 5;
            this.TxtEP.Name = "TxtEP";
            this.TxtEP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtEP.Size = new System.Drawing.Size(62, 20);
            this.TxtEP.TabIndex = 134;
            this.TxtEP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtEP_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 133;
            this.label1.Text = "Estilo Propio .................";
            // 
            // FrmEstiloVersionMadreDependencia_Adicionar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 102);
            this.ControlBox = false;
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmEstiloVersionMadreDependencia_Adicionar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adicionar EPV Madre";
            this.Load += new System.EventHandler(this.FrmEstiloVersionMadreDependencia_Adicionar_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel panel3;
        internal Janus.Windows.EditControls.UIButton BtnCancelar;
        internal Janus.Windows.EditControls.UIButton BtnAceptar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox TxtVersion;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox TxtEPDescripcion;
        public System.Windows.Forms.TextBox TxtEP;
        public System.Windows.Forms.TextBox TxtVersionDescripcion;
    }
}