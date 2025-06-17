using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBusquedaGeneral : Form
    {

        #region Variables

        public GridEXRow RegistroSeleccionado;
        ClsHelper oHp = new ClsHelper();
        public DataTable dtResultados = new DataTable();
        public DataRow oDr;
        public string sQuery;
        public oParent oParent;

        #endregion

        public FrmBusquedaGeneral()
        {
            InitializeComponent();
            oParent = new oParent();
        }

        private void FrmBusquedaGeneral_Load(object sender, EventArgs e)
        {
            {
                try
                {
                    //if (DGridLista.RootTable.Columns.Count > 2 & DGridLista.RootTable.Columns.Count < 4)
                    //{
                    //    DGridLista.RootTable.Columns[0].Width = 100;
                    //    DGridLista.RootTable.Columns[0].Caption = "CÓDIGO";
                    //    DGridLista.RootTable.Columns[1].Width = 300;
                    //    DGridLista.RootTable.Columns[1].Caption = "DESCRIPCIÓN";
                    //    DGridLista.RootTable.Columns[2].Caption = "TIPO";
                    //    //DGridLista.RootTable.Columns[2].Visible = false;
                    //}
                    //else 
                    //if (DGridLista.RootTable.Columns.Count == 2)
                    //{
                        DGridLista.RootTable.Columns[0].Width = 100;
                        DGridLista.RootTable.Columns[0].Caption = "CÓDIGO";
                        DGridLista.RootTable.Columns[1].Width = 300;
                        DGridLista.RootTable.Columns[1].Caption = "DESCRIPCIÓN";
                    //}

                    DGridLista.Col = 1;
                    DGridLista.Row = DGridLista.FilterRow.Position;
                    DGridLista.Select();
                    DGridLista.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void Cargar_Datos()
        {
            try
            {
                dtResultados = oHp.DevuelveDatos(sQuery, VariablesGenerales.pConnect);
                DGridLista.DataSource = dtResultados;
                oHp.CheckLayoutGridEx(DGridLista);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Carga Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGridLista_DoubleClick(object sender, EventArgs e)
        {
            ClsHelper met = new ClsHelper();
            try
            {
                if (DGridLista.RowCount == 0)
                {
                    oParent.CODIGO = "";
                    return;
                }                    
                DataRow oDr = met.ObtenerDr_DeGridEx(DGridLista);
                if (oDr == null)   { 
                    oParent.CODIGO = "";
                    MessageBox.Show("No se ha seleccionado ningún registro", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                oParent.CODIGO = oDr[0].ToString();
                switch (DGridLista.RootTable.Columns.Count)
                {
                    case 2:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        break;
                    case 3:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        oParent.TipoAdd = oDr[2].ToString();
                        break;
                    case 4:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        oParent.TipoAdd = oDr[2].ToString();
                        oParent.TipoAdd2 = oDr[3].ToString();
                        break;
                    case 5:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        oParent.TipoAdd = oDr[2].ToString();
                        oParent.TipoAdd2 = oDr[3].ToString();
                        oParent.TipoAdd3 = oDr[4].ToString();
                        break;
                    case 6:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        oParent.TipoAdd = oDr[2].ToString();
                        oParent.TipoAdd2 = oDr[3].ToString();
                        oParent.TipoAdd3 = oDr[4].ToString();
                        oParent.TipoAdd4 = oDr[5].ToString();
                        break;
                    case 7:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        oParent.TipoAdd = oDr[2].ToString();
                        oParent.TipoAdd2 = oDr[3].ToString();
                        oParent.TipoAdd3 = oDr[4].ToString();
                        oParent.TipoAdd4 = oDr[5].ToString();
                        oParent.TipoAdd5 = oDr[6].ToString();
                        break;
                    case 8:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        oParent.TipoAdd = oDr[2].ToString();
                        oParent.TipoAdd2 = oDr[3].ToString();
                        oParent.TipoAdd3 = oDr[4].ToString();
                        oParent.TipoAdd4 = oDr[5].ToString();
                        oParent.TipoAdd5 = oDr[6].ToString();
                        oParent.TipoAdd6 = oDr[7].ToString();
                        break;
                    case 9:
                        oParent.DESCRIPCION = oDr[1].ToString();
                        oParent.TipoAdd = oDr[2].ToString();
                        oParent.TipoAdd2 = oDr[3].ToString();
                        oParent.TipoAdd3 = oDr[4].ToString();
                        oParent.TipoAdd4 = oDr[5].ToString();
                        oParent.TipoAdd5 = oDr[6].ToString();
                        oParent.TipoAdd6 = oDr[7].ToString();
                        oParent.TipoAdd7 = oDr[8].ToString();
                        break;
                    default:
                        break;
                }
                RegistroSeleccionado = DGridLista.CurrentRow;
                if (oParent.CODIGO is null) { oParent.CODIGO = ""; }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                RegistroSeleccionado = null;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGridLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                DGridLista_DoubleClick(sender, e);
            }
        }

        private void FrmBusquedaGeneral_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            DGridLista_DoubleClick(sender, e);
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            oParent.CODIGO = "";
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
