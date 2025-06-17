namespace Estilo_Propio_Csharp
{
    partial class FrmBusquedaGeneral
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
            Janus.Windows.GridEX.GridEXLayout DGridLista_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.BtnCancelar = new Janus.Windows.EditControls.UIButton();
            this.BtnAceptar = new Janus.Windows.EditControls.UIButton();
            this.DGridLista = new Janus.Windows.GridEX.GridEX();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGridLista)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.Teal;
            this.Panel1.Controls.Add(this.Panel2);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 366);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(508, 36);
            this.Panel1.TabIndex = 8;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.BtnCancelar);
            this.Panel2.Controls.Add(this.BtnAceptar);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel2.Location = new System.Drawing.Point(357, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(151, 36);
            this.Panel2.TabIndex = 7;
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.ImageKey = "48px-Crystal_Clear_action_button_cancel.png";
            this.BtnCancelar.ImageSize = new System.Drawing.Size(25, 25);
            this.BtnCancelar.Location = new System.Drawing.Point(76, 6);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(66, 23);
            this.BtnCancelar.TabIndex = 6;
            this.BtnCancelar.Text = "&cancelar";
            this.BtnCancelar.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Empty;
            this.BtnCancelar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // BtnAceptar
            // 
            this.BtnAceptar.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.BtnAceptar.ImageSize = new System.Drawing.Size(25, 25);
            this.BtnAceptar.Location = new System.Drawing.Point(8, 6);
            this.BtnAceptar.Name = "BtnAceptar";
            this.BtnAceptar.Size = new System.Drawing.Size(66, 23);
            this.BtnAceptar.TabIndex = 5;
            this.BtnAceptar.Text = "&aceptar";
            this.BtnAceptar.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Empty;
            this.BtnAceptar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
            // 
            // DGridLista
            // 
            this.DGridLista.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.DGridLista.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            DGridLista_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>";
            this.DGridLista.DesignTimeLayout = DGridLista_DesignTimeLayout;
            this.DGridLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGridLista.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.DGridLista.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.DGridLista.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.DGridLista.GroupByBoxVisible = false;
            this.DGridLista.Location = new System.Drawing.Point(0, 0);
            this.DGridLista.Name = "DGridLista";
            this.DGridLista.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.DGridLista.RecordNavigator = true;
            this.DGridLista.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.DGridLista.Size = new System.Drawing.Size(508, 366);
            this.DGridLista.TabIndex = 9;
            this.DGridLista.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            this.DGridLista.DoubleClick += new System.EventHandler(this.DGridLista_DoubleClick);
            this.DGridLista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DGridLista_KeyPress);
            // 
            // FrmBusquedaGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 402);
            this.ControlBox = false;
            this.Controls.Add(this.DGridLista);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmBusquedaGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Busqueda General";
            this.Load += new System.EventHandler(this.FrmBusquedaGeneral_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmBusquedaGeneral_KeyUp);
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGridLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel Panel2;
        internal Janus.Windows.EditControls.UIButton BtnCancelar;
        internal Janus.Windows.EditControls.UIButton BtnAceptar;
        internal Janus.Windows.GridEX.GridEX DGridLista;
    }
}