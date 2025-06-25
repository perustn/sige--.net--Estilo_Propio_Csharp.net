
namespace Estilo_Propio_Csharp
{
    partial class FrmCargaUPCdesdeExcel
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
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.txtDesCliente = new System.Windows.Forms.TextBox();
            this.txtAbrCliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRuta_Archivo_Excel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscarArchivo = new Janus.Windows.EditControls.UIButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnVer_Datos_Extraidos = new Janus.Windows.EditControls.UIButton();
            this.btnProcesarArchivo = new Janus.Windows.EditControls.UIButton();
            this.btnCargaExcel = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtDesCliente);
            this.uiGroupBox1.Controls.Add(this.txtAbrCliente);
            this.uiGroupBox1.Controls.Add(this.label2);
            this.uiGroupBox1.Controls.Add(this.txtRuta_Archivo_Excel);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.btnBuscarArchivo);
            this.uiGroupBox1.Location = new System.Drawing.Point(9, 7);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(700, 104);
            this.uiGroupBox1.TabIndex = 0;
            // 
            // txtDesCliente
            // 
            this.txtDesCliente.Location = new System.Drawing.Point(206, 37);
            this.txtDesCliente.Name = "txtDesCliente";
            this.txtDesCliente.Size = new System.Drawing.Size(455, 20);
            this.txtDesCliente.TabIndex = 8;
            this.txtDesCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDesCliente_KeyPress);
            // 
            // txtAbrCliente
            // 
            this.txtAbrCliente.Location = new System.Drawing.Point(139, 37);
            this.txtAbrCliente.Name = "txtAbrCliente";
            this.txtAbrCliente.Size = new System.Drawing.Size(61, 20);
            this.txtAbrCliente.TabIndex = 0;
            this.txtAbrCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAbrCliente_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cliente.........................";
            // 
            // txtRuta_Archivo_Excel
            // 
            this.txtRuta_Archivo_Excel.Location = new System.Drawing.Point(139, 66);
            this.txtRuta_Archivo_Excel.Name = "txtRuta_Archivo_Excel";
            this.txtRuta_Archivo_Excel.ReadOnly = true;
            this.txtRuta_Archivo_Excel.Size = new System.Drawing.Size(522, 20);
            this.txtRuta_Archivo_Excel.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seleccione Archivo.......";
            // 
            // btnBuscarArchivo
            // 
            this.btnBuscarArchivo.Location = new System.Drawing.Point(667, 66);
            this.btnBuscarArchivo.Name = "btnBuscarArchivo";
            this.btnBuscarArchivo.Size = new System.Drawing.Size(21, 20);
            this.btnBuscarArchivo.TabIndex = 5;
            this.btnBuscarArchivo.Text = "...";
            this.btnBuscarArchivo.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.btnBuscarArchivo.Click += new System.EventHandler(this.btnBuscarArchivo_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.uiGroupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(714, 149);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(714, 30);
            this.panel2.TabIndex = 218;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnVer_Datos_Extraidos);
            this.panel3.Controls.Add(this.btnProcesarArchivo);
            this.panel3.Controls.Add(this.btnCargaExcel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(44, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(670, 30);
            this.panel3.TabIndex = 0;
            // 
            // btnVer_Datos_Extraidos
            // 
            this.btnVer_Datos_Extraidos.Enabled = false;
            this.btnVer_Datos_Extraidos.Location = new System.Drawing.Point(323, 3);
            this.btnVer_Datos_Extraidos.Name = "btnVer_Datos_Extraidos";
            this.btnVer_Datos_Extraidos.Size = new System.Drawing.Size(157, 24);
            this.btnVer_Datos_Extraidos.TabIndex = 2;
            this.btnVer_Datos_Extraidos.Text = "Ver Datos Extraidos";
            this.btnVer_Datos_Extraidos.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.btnVer_Datos_Extraidos.Click += new System.EventHandler(this.btnVer_Datos_Extraidos_Click);
            // 
            // btnProcesarArchivo
            // 
            this.btnProcesarArchivo.Enabled = false;
            this.btnProcesarArchivo.Location = new System.Drawing.Point(483, 3);
            this.btnProcesarArchivo.Name = "btnProcesarArchivo";
            this.btnProcesarArchivo.Size = new System.Drawing.Size(185, 24);
            this.btnProcesarArchivo.TabIndex = 1;
            this.btnProcesarArchivo.Text = "Guardar En Base de Datos";
            this.btnProcesarArchivo.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.btnProcesarArchivo.Click += new System.EventHandler(this.btnProcesarArchivo_Click);
            // 
            // btnCargaExcel
            // 
            this.btnCargaExcel.Image = global::Estilo_Propio_Csharp.Properties.Resources.ic_copy2_48x48;
            this.btnCargaExcel.Location = new System.Drawing.Point(171, 3);
            this.btnCargaExcel.Name = "btnCargaExcel";
            this.btnCargaExcel.Size = new System.Drawing.Size(147, 24);
            this.btnCargaExcel.TabIndex = 0;
            this.btnCargaExcel.Text = "Extraer Datos ";
            this.btnCargaExcel.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.btnCargaExcel.Click += new System.EventHandler(this.btnCargaExcel_Click);
            // 
            // FrmCargaUPCdesdeExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 149);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.Name = "FrmCargaUPCdesdeExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carga UPC desde Excel";
            this.Load += new System.EventHandler(this.FrmCargaUPCdesdeExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.TextBox txtDesCliente;
        private System.Windows.Forms.TextBox txtAbrCliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRuta_Archivo_Excel;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIButton btnBuscarArchivo;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel3;
        private Janus.Windows.EditControls.UIButton btnVer_Datos_Extraidos;
        private Janus.Windows.EditControls.UIButton btnProcesarArchivo;
        private Janus.Windows.EditControls.UIButton btnCargaExcel;
    }
}