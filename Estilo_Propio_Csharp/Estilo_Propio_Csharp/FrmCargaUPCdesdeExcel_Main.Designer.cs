
namespace Estilo_Propio_Csharp
{
    partial class FrmCargaUPCdesdeExcel_Main
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
            this.btnVer_Datos_Extraidos = new Janus.Windows.EditControls.UIButton();
            this.SuspendLayout();
            // 
            // btnVer_Datos_Extraidos
            // 
            this.btnVer_Datos_Extraidos.Location = new System.Drawing.Point(46, 24);
            this.btnVer_Datos_Extraidos.Name = "btnVer_Datos_Extraidos";
            this.btnVer_Datos_Extraidos.Size = new System.Drawing.Size(182, 24);
            this.btnVer_Datos_Extraidos.TabIndex = 3;
            this.btnVer_Datos_Extraidos.Text = "Abrir opcion de carga de archivo";
            this.btnVer_Datos_Extraidos.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.btnVer_Datos_Extraidos.Click += new System.EventHandler(this.btnVer_Datos_Extraidos_Click);
            // 
            // FrmCargaUPCdesdeExcel_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 70);
            this.Controls.Add(this.btnVer_Datos_Extraidos);
            this.Name = "FrmCargaUPCdesdeExcel_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.FrmCargaUPCdesdeExcel_Main_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIButton btnVer_Datos_Extraidos;
    }
}