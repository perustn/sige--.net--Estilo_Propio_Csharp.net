
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaBibliotecaGraficos_AddImagen
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.pctIMG_CAB1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctIMG_CAB1)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 635);
            this.Panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(688, 12);
            this.Panel1.TabIndex = 137;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(688, 12);
            this.panel2.TabIndex = 138;
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.White;
            this.Panel5.Controls.Add(this.pctIMG_CAB1);
            this.Panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel5.Location = new System.Drawing.Point(0, 12);
            this.Panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(688, 623);
            this.Panel5.TabIndex = 139;
            // 
            // pctIMG_CAB1
            // 
            this.pctIMG_CAB1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pctIMG_CAB1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctIMG_CAB1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pctIMG_CAB1.Location = new System.Drawing.Point(7, 7);
            this.pctIMG_CAB1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pctIMG_CAB1.Name = "pctIMG_CAB1";
            this.pctIMG_CAB1.Size = new System.Drawing.Size(674, 608);
            this.pctIMG_CAB1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctIMG_CAB1.TabIndex = 121;
            this.pctIMG_CAB1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FrmBandejaBibliotecaGraficos_AddImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 647);
            this.Controls.Add(this.Panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBandejaBibliotecaGraficos_AddImagen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Imagen";
            this.Load += new System.EventHandler(this.FrmBandejaCotizacionFlashCost_AddImagen_Load);
            this.Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pctIMG_CAB1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel Panel5;
        internal System.Windows.Forms.PictureBox pctIMG_CAB1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}