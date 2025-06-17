
namespace Estilo_Propio_Csharp
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BtnBuscar = new Janus.Windows.EditControls.UIButton();
            this.uiButton1 = new Janus.Windows.EditControls.UIButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "INGRESO";
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscar.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.BtnBuscar.Location = new System.Drawing.Point(12, 107);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BtnBuscar.Size = new System.Drawing.Size(209, 47);
            this.BtnBuscar.TabIndex = 148;
            this.BtnBuscar.Text = "Bandeja Seguimiento de Cotizaciones de Precios de Tela ";
            this.BtnBuscar.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Empty;
            this.BtnBuscar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // uiButton1
            // 
            this.uiButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiButton1.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.uiButton1.Location = new System.Drawing.Point(8, 160);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.uiButton1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.uiButton1.Size = new System.Drawing.Size(209, 47);
            this.uiButton1.TabIndex = 149;
            this.uiButton1.Text = "Bandeja Seguimiento de Cotizaciones de Precios de Tela ";
            this.uiButton1.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Empty;
            this.uiButton1.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.uiButton1.Click += new System.EventHandler(this.uiButton1_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 450);
            this.Controls.Add(this.uiButton1);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.label1);
            this.Name = "MenuPrincipal";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        internal Janus.Windows.EditControls.UIButton BtnBuscar;
        internal Janus.Windows.EditControls.UIButton uiButton1;
    }
}

