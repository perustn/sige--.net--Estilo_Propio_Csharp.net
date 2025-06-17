
namespace Estilo_Propio_Csharp
{
    partial class FrmBandejaBibliotecaFormaMedir
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBandejaBibliotecaFormaMedir));
            Janus.Windows.ButtonBar.ButtonBarGroup buttonBar1_Group_0 = new Janus.Windows.ButtonBar.ButtonBarGroup();
            Janus.Windows.ButtonBar.ButtonBarItem buttonBar1_Item_0_0 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.ButtonBar.ButtonBarItem buttonBar1_Item_0_1 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.ButtonBar.ButtonBarItem buttonBar1_Item_0_2 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.ButtonBar.ButtonBarItem buttonBar1_Item_0_3 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.ButtonBar.ButtonBarItem buttonBar1_Item_0_4 = new Janus.Windows.ButtonBar.ButtonBarItem();
            Janus.Windows.GridEX.GridEXLayout gridEX1_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.BtnBuscar = new Janus.Windows.EditControls.UIButton();
            this.OptSMT = new System.Windows.Forms.RadioButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.TxtCliente = new System.Windows.Forms.TextBox();
            this.TxtTemporada = new System.Windows.Forms.TextBox();
            this.TxtTipoPrenda = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.OptTipoTejido = new System.Windows.Forms.RadioButton();
            this.OptCodigoTela = new System.Windows.Forms.RadioButton();
            this.OptTipoSolicitudPrecio = new System.Windows.Forms.RadioButton();
            this.OptEstadoSolicitudPrecio = new System.Windows.Forms.RadioButton();
            this.OptEstadoAprobacionPrecio = new System.Windows.Forms.RadioButton();
            this.OptEstatusAsignacionTela = new System.Windows.Forms.RadioButton();
            this.OptRangoFecha = new System.Windows.Forms.RadioButton();
            this.buttonBar1 = new Janus.Windows.ButtonBar.ButtonBar();
            this.gridEX1 = new Janus.Windows.GridEX.GridEX();
            this.gridEXExporter1 = new Janus.Windows.GridEX.Export.GridEXExporter(this.components);
            this.panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEX1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.Panel2);
            this.panel1.Controls.Add(this.OptSMT);
            this.panel1.Controls.Add(this.uiGroupBox1);
            this.panel1.Controls.Add(this.OptTipoTejido);
            this.panel1.Controls.Add(this.OptCodigoTela);
            this.panel1.Controls.Add(this.OptTipoSolicitudPrecio);
            this.panel1.Controls.Add(this.OptEstadoSolicitudPrecio);
            this.panel1.Controls.Add(this.OptEstadoAprobacionPrecio);
            this.panel1.Controls.Add(this.OptEstatusAsignacionTela);
            this.panel1.Controls.Add(this.OptRangoFecha);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1065, 91);
            this.panel1.TabIndex = 2;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.Controls.Add(this.BtnBuscar);
            this.Panel2.Location = new System.Drawing.Point(877, 0);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(188, 574);
            this.Panel2.TabIndex = 1;
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscar.ImageKey = "48px-Crystal_Clear_action_apply.png";
            this.BtnBuscar.Location = new System.Drawing.Point(13, 28);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.BtnBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BtnBuscar.Size = new System.Drawing.Size(162, 31);
            this.BtnBuscar.TabIndex = 147;
            this.BtnBuscar.Text = "Realizar Búsqueda";
            this.BtnBuscar.TextHorizontalAlignment = Janus.Windows.EditControls.TextAlignment.Empty;
            this.BtnBuscar.VisualStyle = Janus.Windows.UI.VisualStyle.Office2010;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // OptSMT
            // 
            this.OptSMT.AutoSize = true;
            this.OptSMT.BackColor = System.Drawing.Color.Transparent;
            this.OptSMT.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptSMT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptSMT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptSMT.ForeColor = System.Drawing.Color.Black;
            this.OptSMT.Image = ((System.Drawing.Image)(resources.GetObject("OptSMT.Image")));
            this.OptSMT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptSMT.Location = new System.Drawing.Point(-8, 294);
            this.OptSMT.Name = "OptSMT";
            this.OptSMT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptSMT.Size = new System.Drawing.Size(98, 24);
            this.OptSMT.TabIndex = 152;
            this.OptSMT.Tag = "9";
            this.OptSMT.Text = "         Por SMT";
            this.OptSMT.UseVisualStyleBackColor = false;
            this.OptSMT.Visible = false;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiGroupBox1.Controls.Add(this.TxtCliente);
            this.uiGroupBox1.Controls.Add(this.TxtTemporada);
            this.uiGroupBox1.Controls.Add(this.TxtTipoPrenda);
            this.uiGroupBox1.Controls.Add(this.Label9);
            this.uiGroupBox1.Controls.Add(this.label16);
            this.uiGroupBox1.Controls.Add(this.Label4);
            this.uiGroupBox1.FrameStyle = Janus.Windows.EditControls.FrameStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(6, 6);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.OfficeColorScheme = Janus.Windows.UI.OfficeColorScheme.Blue;
            this.uiGroupBox1.Size = new System.Drawing.Size(865, 79);
            this.uiGroupBox1.TabIndex = 0;
            this.uiGroupBox1.Text = "Filtros de Búsqueda";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2010;
            // 
            // TxtCliente
            // 
            this.TxtCliente.AcceptsReturn = true;
            this.TxtCliente.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCliente.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtCliente.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtCliente.Location = new System.Drawing.Point(26, 42);
            this.TxtCliente.MaxLength = 0;
            this.TxtCliente.Name = "TxtCliente";
            this.TxtCliente.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCliente.Size = new System.Drawing.Size(252, 20);
            this.TxtCliente.TabIndex = 55;
            this.TxtCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCliente_KeyPress);
            // 
            // TxtTemporada
            // 
            this.TxtTemporada.AcceptsReturn = true;
            this.TxtTemporada.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTemporada.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtTemporada.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTemporada.Location = new System.Drawing.Point(312, 43);
            this.TxtTemporada.MaxLength = 0;
            this.TxtTemporada.Name = "TxtTemporada";
            this.TxtTemporada.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtTemporada.Size = new System.Drawing.Size(252, 20);
            this.TxtTemporada.TabIndex = 51;
            this.TxtTemporada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTemporada_KeyPress);
            // 
            // TxtTipoPrenda
            // 
            this.TxtTipoPrenda.AcceptsReturn = true;
            this.TxtTipoPrenda.BackColor = System.Drawing.SystemColors.Window;
            this.TxtTipoPrenda.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtTipoPrenda.ForeColor = System.Drawing.SystemColors.WindowText;
            this.TxtTipoPrenda.Location = new System.Drawing.Point(596, 42);
            this.TxtTipoPrenda.MaxLength = 0;
            this.TxtTipoPrenda.Name = "TxtTipoPrenda";
            this.TxtTipoPrenda.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtTipoPrenda.Size = new System.Drawing.Size(252, 20);
            this.TxtTipoPrenda.TabIndex = 55;
            this.TxtTipoPrenda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTipoPrenda_KeyPress);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label9.Location = new System.Drawing.Point(22, 26);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(47, 13);
            this.Label9.TabIndex = 56;
            this.Label9.Text = "Cliente";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label16.Location = new System.Drawing.Point(592, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 13);
            this.label16.TabIndex = 56;
            this.label16.Text = "Tipo Prenda";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Label4.Location = new System.Drawing.Point(308, 26);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(71, 13);
            this.Label4.TabIndex = 52;
            this.Label4.Text = "Temporada";
            // 
            // OptTipoTejido
            // 
            this.OptTipoTejido.AutoSize = true;
            this.OptTipoTejido.BackColor = System.Drawing.Color.Transparent;
            this.OptTipoTejido.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptTipoTejido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptTipoTejido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptTipoTejido.ForeColor = System.Drawing.Color.Black;
            this.OptTipoTejido.Image = ((System.Drawing.Image)(resources.GetObject("OptTipoTejido.Image")));
            this.OptTipoTejido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptTipoTejido.Location = new System.Drawing.Point(-32, 277);
            this.OptTipoTejido.Name = "OptTipoTejido";
            this.OptTipoTejido.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptTipoTejido.Size = new System.Drawing.Size(218, 24);
            this.OptTipoTejido.TabIndex = 151;
            this.OptTipoTejido.Tag = "8";
            this.OptTipoTejido.Text = "         Por Tipo Tejido/Familia de Tela";
            this.OptTipoTejido.UseVisualStyleBackColor = false;
            this.OptTipoTejido.Visible = false;
            // 
            // OptCodigoTela
            // 
            this.OptCodigoTela.AutoSize = true;
            this.OptCodigoTela.BackColor = System.Drawing.Color.Transparent;
            this.OptCodigoTela.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptCodigoTela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptCodigoTela.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptCodigoTela.ForeColor = System.Drawing.Color.Black;
            this.OptCodigoTela.Image = ((System.Drawing.Image)(resources.GetObject("OptCodigoTela.Image")));
            this.OptCodigoTela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptCodigoTela.Location = new System.Drawing.Point(144, 273);
            this.OptCodigoTela.Name = "OptCodigoTela";
            this.OptCodigoTela.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptCodigoTela.Size = new System.Drawing.Size(153, 24);
            this.OptCodigoTela.TabIndex = 150;
            this.OptCodigoTela.Tag = "7";
            this.OptCodigoTela.Text = "         Por Código de Tela";
            this.OptCodigoTela.UseVisualStyleBackColor = false;
            this.OptCodigoTela.Visible = false;
            // 
            // OptTipoSolicitudPrecio
            // 
            this.OptTipoSolicitudPrecio.AutoSize = true;
            this.OptTipoSolicitudPrecio.BackColor = System.Drawing.Color.Transparent;
            this.OptTipoSolicitudPrecio.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptTipoSolicitudPrecio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptTipoSolicitudPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptTipoSolicitudPrecio.ForeColor = System.Drawing.Color.Black;
            this.OptTipoSolicitudPrecio.Image = ((System.Drawing.Image)(resources.GetObject("OptTipoSolicitudPrecio.Image")));
            this.OptTipoSolicitudPrecio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptTipoSolicitudPrecio.Location = new System.Drawing.Point(112, 246);
            this.OptTipoSolicitudPrecio.Name = "OptTipoSolicitudPrecio";
            this.OptTipoSolicitudPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptTipoSolicitudPrecio.Size = new System.Drawing.Size(159, 24);
            this.OptTipoSolicitudPrecio.TabIndex = 143;
            this.OptTipoSolicitudPrecio.Tag = "2";
            this.OptTipoSolicitudPrecio.Text = "         Por tipo de proceso";
            this.OptTipoSolicitudPrecio.UseVisualStyleBackColor = false;
            // 
            // OptEstadoSolicitudPrecio
            // 
            this.OptEstadoSolicitudPrecio.AutoSize = true;
            this.OptEstadoSolicitudPrecio.BackColor = System.Drawing.Color.Transparent;
            this.OptEstadoSolicitudPrecio.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptEstadoSolicitudPrecio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptEstadoSolicitudPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptEstadoSolicitudPrecio.ForeColor = System.Drawing.Color.Black;
            this.OptEstadoSolicitudPrecio.Image = ((System.Drawing.Image)(resources.GetObject("OptEstadoSolicitudPrecio.Image")));
            this.OptEstadoSolicitudPrecio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptEstadoSolicitudPrecio.Location = new System.Drawing.Point(92, 294);
            this.OptEstadoSolicitudPrecio.Name = "OptEstadoSolicitudPrecio";
            this.OptEstadoSolicitudPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptEstadoSolicitudPrecio.Size = new System.Drawing.Size(198, 24);
            this.OptEstadoSolicitudPrecio.TabIndex = 146;
            this.OptEstadoSolicitudPrecio.Tag = "4";
            this.OptEstadoSolicitudPrecio.Text = "         Por Estado Solicitud Precio";
            this.OptEstadoSolicitudPrecio.UseVisualStyleBackColor = false;
            this.OptEstadoSolicitudPrecio.Visible = false;
            // 
            // OptEstadoAprobacionPrecio
            // 
            this.OptEstadoAprobacionPrecio.AutoSize = true;
            this.OptEstadoAprobacionPrecio.BackColor = System.Drawing.Color.Transparent;
            this.OptEstadoAprobacionPrecio.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptEstadoAprobacionPrecio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptEstadoAprobacionPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptEstadoAprobacionPrecio.ForeColor = System.Drawing.Color.Black;
            this.OptEstadoAprobacionPrecio.Image = ((System.Drawing.Image)(resources.GetObject("OptEstadoAprobacionPrecio.Image")));
            this.OptEstadoAprobacionPrecio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptEstadoAprobacionPrecio.Location = new System.Drawing.Point(20, 295);
            this.OptEstadoAprobacionPrecio.Name = "OptEstadoAprobacionPrecio";
            this.OptEstadoAprobacionPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptEstadoAprobacionPrecio.Size = new System.Drawing.Size(213, 24);
            this.OptEstadoAprobacionPrecio.TabIndex = 149;
            this.OptEstadoAprobacionPrecio.Tag = "6";
            this.OptEstadoAprobacionPrecio.Text = "         Por Estado Aprobación Precio";
            this.OptEstadoAprobacionPrecio.UseVisualStyleBackColor = false;
            this.OptEstadoAprobacionPrecio.Visible = false;
            // 
            // OptEstatusAsignacionTela
            // 
            this.OptEstatusAsignacionTela.AutoSize = true;
            this.OptEstatusAsignacionTela.BackColor = System.Drawing.Color.Transparent;
            this.OptEstatusAsignacionTela.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptEstatusAsignacionTela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptEstatusAsignacionTela.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptEstatusAsignacionTela.ForeColor = System.Drawing.Color.Black;
            this.OptEstatusAsignacionTela.Image = ((System.Drawing.Image)(resources.GetObject("OptEstatusAsignacionTela.Image")));
            this.OptEstatusAsignacionTela.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptEstatusAsignacionTela.Location = new System.Drawing.Point(56, 268);
            this.OptEstatusAsignacionTela.Name = "OptEstatusAsignacionTela";
            this.OptEstatusAsignacionTela.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptEstatusAsignacionTela.Size = new System.Drawing.Size(141, 24);
            this.OptEstatusAsignacionTela.TabIndex = 145;
            this.OptEstatusAsignacionTela.Tag = "5";
            this.OptEstatusAsignacionTela.Text = "         Por Estilo Propio";
            this.OptEstatusAsignacionTela.UseVisualStyleBackColor = false;
            // 
            // OptRangoFecha
            // 
            this.OptRangoFecha.AutoSize = true;
            this.OptRangoFecha.BackColor = System.Drawing.Color.Transparent;
            this.OptRangoFecha.Cursor = System.Windows.Forms.Cursors.Default;
            this.OptRangoFecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptRangoFecha.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptRangoFecha.ForeColor = System.Drawing.Color.Black;
            this.OptRangoFecha.Image = ((System.Drawing.Image)(resources.GetObject("OptRangoFecha.Image")));
            this.OptRangoFecha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.OptRangoFecha.Location = new System.Drawing.Point(134, 306);
            this.OptRangoFecha.Name = "OptRangoFecha";
            this.OptRangoFecha.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OptRangoFecha.Size = new System.Drawing.Size(165, 24);
            this.OptRangoFecha.TabIndex = 148;
            this.OptRangoFecha.Tag = "3";
            this.OptRangoFecha.Text = "         Por Rango de Fechas";
            this.OptRangoFecha.UseVisualStyleBackColor = false;
            // 
            // buttonBar1
            // 
            this.buttonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonBar1.DragStyle = Janus.Windows.ButtonBar.DragStyle.InvertedRect;
            buttonBar1_Item_0_0.Image = ((System.Drawing.Image)(resources.GetObject("buttonBar1_Item_0_0.Image")));
            buttonBar1_Item_0_0.Key = "ADICIONAR";
            buttonBar1_Item_0_0.Text = "Adicionar";
            buttonBar1_Item_0_1.Image = ((System.Drawing.Image)(resources.GetObject("buttonBar1_Item_0_1.Image")));
            buttonBar1_Item_0_1.Key = "MODIFICAR";
            buttonBar1_Item_0_1.Text = "Modificar";
            buttonBar1_Item_0_2.Image = ((System.Drawing.Image)(resources.GetObject("buttonBar1_Item_0_2.Image")));
            buttonBar1_Item_0_2.Key = "ELIMINAR";
            buttonBar1_Item_0_2.Text = "Eliminar";
            buttonBar1_Item_0_3.Image = ((System.Drawing.Image)(resources.GetObject("buttonBar1_Item_0_3.Image")));
            buttonBar1_Item_0_3.Key = "EXPORTAR";
            buttonBar1_Item_0_3.Text = "Exportar información a excel";
            buttonBar1_Item_0_4.Image = ((System.Drawing.Image)(resources.GetObject("buttonBar1_Item_0_4.Image")));
            buttonBar1_Item_0_4.Key = "TRASLADAREPV";
            buttonBar1_Item_0_4.Text = "Trasladar a EPV";
            buttonBar1_Group_0.Items.AddRange(new Janus.Windows.ButtonBar.ButtonBarItem[] {
            buttonBar1_Item_0_0,
            buttonBar1_Item_0_1,
            buttonBar1_Item_0_2,
            buttonBar1_Item_0_3,
            buttonBar1_Item_0_4});
            buttonBar1_Group_0.Key = "Group1";
            buttonBar1_Group_0.Text = "New Group";
            buttonBar1_Group_0.TextAlignment = Janus.Windows.ButtonBar.Alignment.Near;
            buttonBar1_Group_0.View = Janus.Windows.ButtonBar.ButtonBarView.SmallIcons;
            this.buttonBar1.Groups.AddRange(new Janus.Windows.ButtonBar.ButtonBarGroup[] {
            buttonBar1_Group_0});
            this.buttonBar1.HeaderGroupVisible = false;
            this.buttonBar1.Location = new System.Drawing.Point(0, 91);
            this.buttonBar1.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBar1.Name = "buttonBar1";
            this.buttonBar1.OfficeColorScheme = Janus.Windows.ButtonBar.OfficeColorScheme.Blue;
            this.buttonBar1.Orientation = Janus.Windows.ButtonBar.ButtonBarOrientation.Horizontal;
            this.buttonBar1.SelectionArea = Janus.Windows.ButtonBar.SelectionArea.FullItem;
            this.buttonBar1.Size = new System.Drawing.Size(1065, 25);
            this.buttonBar1.SmallImageSize = new System.Drawing.Size(18, 18);
            this.buttonBar1.TabIndex = 29;
            this.buttonBar1.Text = "buttonBar1";
            this.buttonBar1.VisualStyle = Janus.Windows.ButtonBar.VisualStyle.Office2010;
            this.buttonBar1.ItemClick += new Janus.Windows.ButtonBar.ItemEventHandler(this.buttonBar1_ItemClick);
            // 
            // gridEX1
            // 
            this.gridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.gridEX1.AlternatingColors = true;
            this.gridEX1.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            gridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /><HeaderLines>2</HeaderLines></Root" +
    "Table></GridEXLayoutData>";
            this.gridEX1.DesignTimeLayout = gridEX1_DesignTimeLayout;
            this.gridEX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEX1.EnterKeyBehavior = Janus.Windows.GridEX.EnterKeyBehavior.None;
            this.gridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.gridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.gridEX1.GroupByBoxVisible = false;
            this.gridEX1.Location = new System.Drawing.Point(0, 116);
            this.gridEX1.Margin = new System.Windows.Forms.Padding(2);
            this.gridEX1.Name = "gridEX1";
            this.gridEX1.OfficeColorScheme = Janus.Windows.GridEX.OfficeColorScheme.Blue;
            this.gridEX1.RecordNavigator = true;
            this.gridEX1.Size = new System.Drawing.Size(1065, 334);
            this.gridEX1.TabIndex = 30;
            this.gridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            this.gridEX1.LinkClicked += new Janus.Windows.GridEX.ColumnActionEventHandler(this.gridEX1_LinkClicked);
            // 
            // gridEXExporter1
            // 
            this.gridEXExporter1.GridEX = this.gridEX1;
            // 
            // FrmBandejaBibliotecaFormaMedir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 450);
            this.Controls.Add(this.gridEX1);
            this.Controls.Add(this.buttonBar1);
            this.Controls.Add(this.panel1);
            this.Name = "FrmBandejaBibliotecaFormaMedir";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bandeja de Biblioteca de Forma de Medir";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBandejaBibliotecaFormaMedir_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel Panel2;
        public System.Windows.Forms.TextBox TxtTipoPrenda;
        internal System.Windows.Forms.Label label16;
        public System.Windows.Forms.TextBox TxtCliente;
        public System.Windows.Forms.TextBox TxtTemporada;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label4;
        internal Janus.Windows.EditControls.UIButton BtnBuscar;
        public System.Windows.Forms.RadioButton OptSMT;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        public System.Windows.Forms.RadioButton OptTipoTejido;
        public System.Windows.Forms.RadioButton OptCodigoTela;
        public System.Windows.Forms.RadioButton OptTipoSolicitudPrecio;
        public System.Windows.Forms.RadioButton OptEstadoSolicitudPrecio;
        public System.Windows.Forms.RadioButton OptEstadoAprobacionPrecio;
        public System.Windows.Forms.RadioButton OptEstatusAsignacionTela;
        public System.Windows.Forms.RadioButton OptRangoFecha;
        private Janus.Windows.ButtonBar.ButtonBar buttonBar1;
        internal Janus.Windows.GridEX.GridEX gridEX1;
        private Janus.Windows.GridEX.Export.GridEXExporter gridEXExporter1;
    }
}