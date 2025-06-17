using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Janus.Windows.GridEX;

namespace Estilo_Propio_Csharp
{
    public partial class FrmBusquedaSensitiva : Form
    {
        public enum ModoVentana
        {
            BusquedaPorPrograma = 1,
            BusquedaPorEstiloCliente = 2,
            BusquedaPorEstiloClienteFlash = 3,
            BusquedaEstCli = 4,
        }

        #region Variables
        ClsHelper oHp = new ClsHelper();
        private Color colEmpresa;
        private string OpcionFiltro;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public oParent oParent;
        public string CodCliente;
        public string CodTemporada;

        public ModoVentana mod;

        #endregion

        public FrmBusquedaSensitiva()
        {
            InitializeComponent();
            oParent = new oParent();
        }

        private void FrmBusquedaSensitiva_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable oDt = oHp.DevuelveDatos(string.Format("SELECT * FROM SEG_Empresas where cod_empresa = '{0}'", VariablesGenerales.pCodEmpresa), VariablesGenerales.pConnectSeguridad);
                if (!(oDt == null))
                {
                    int ColorFondo_R = Convert.ToInt32(oDt.Rows[0]["ColorFondo_R"]);
                    int ColorFondo_G = Convert.ToInt32(oDt.Rows[0]["ColorFondo_G"]);
                    int ColorFondo_B = Convert.ToInt32(oDt.Rows[0]["ColorFondo_B"]);
                    colEmpresa = Color.FromArgb(ColorFondo_R, ColorFondo_G, ColorFondo_B);
                    panel1.BackColor = colEmpresa;
                    Panel2.BackColor = colEmpresa;
                }
                //TxtBusqueda.Focus();
                //ActiveControl = TxtBusqueda;
                TxtBusqueda.Select();
        TxtBusqueda.Focus();
            }
             catch (Exception)
            {

                throw;
            }
        }

        private void GrdLista_DoubleClick(object sender, EventArgs e)
        {
            Aceptar();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Aceptar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            oParent.CODIGO = "";
            this.Close();
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Aceptar();
            }
        }

        private void TxtBusqueda_TextChanged(object sender, EventArgs e)
        {
            CARGA_GRID();
        }

        public void CARGA_GRID()
        {
            try
            {
                switch (mod)
                {
                    case ModoVentana.BusquedaPorPrograma:
                        {
                            strSQL = "Sm_Flash_Cost_Busca_Programa '" + TxtBusqueda.Text +"'";
                            break;
                        }
                    case ModoVentana.BusquedaPorEstiloCliente:
                        {
                            strSQL = "Sm_Flash_Cost_Busca_Estilo_Cliente '" + TxtBusqueda.Text + "'";
                            break;
                        }
                    case ModoVentana.BusquedaPorEstiloClienteFlash:
                        {
                            strSQL = "sm_busqueda_estilo_cliente_flash_cost '" + CodCliente +"','" + CodTemporada + "','" + TxtBusqueda.Text + "'";
                            break;
                        }
                    case ModoVentana.BusquedaEstCli:
                        {
                            strSQL = "select Cod_EstCli Codigo, Des_EstCli Descripcion from  tg_estcli where Cod_EstCli like '%" + TxtBusqueda.Text + "%'";
                            break;
                        }
                }

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                GrdLista.DataSource = oDt;
                oHp.CheckLayoutGridEx(GrdLista);
                {
                    var withBlock = GrdLista;
                    withBlock.FilterMode = FilterMode.None;
                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 2;
                        withBlock1.RowHeight = 30;
                        withBlock1.PreviewRow = false;
                        {
                            switch (mod)
                            {
                                case ModoVentana.BusquedaPorPrograma:
                                    {
                                        var Block = withBlock1.Columns["Programa"];
                                        Block.Width = 120;                                        
                                    }
                                    {
                                        var Block = withBlock1.Columns["Nombre_programa"];
                                        Block.Width = 180;
                                        Block.Caption = "NOMBRE PROGRAMA";     
                                    }
                                    {
                                        var Block = withBlock1.Columns["Cliente"];
                                        Block.Width = 110;
                                        Block.Caption = "CLIENTE";
                                        break;
                                    }
                                case ModoVentana.BusquedaPorEstiloCliente:
                                    {
                                        var Block = withBlock1.Columns["Estilo"];
                                        Block.Width = 150;
                                    }
                                    {
                                        var Block = withBlock1.Columns["Nombre"];
                                        Block.Width = 190;
                                        Block.Caption = "NOMBRE ESTILO CLIENTE";
                                        break;
                                    }
                                case ModoVentana.BusquedaPorEstiloClienteFlash:
                                    {
                                        var Block = withBlock1.Columns["Estilo_Cliente"];
                                        Block.Width = 150;
                                        Block.Caption = "ESTILO CLIENTE";
                                    }
                                    {
                                        var Block = withBlock1.Columns["Nombre"];
                                        Block.Width = 190;
                                        Block.Caption = "NOMBRE ESTILO CLIENTE";
                                        break;
                                    }
                                case ModoVentana.BusquedaEstCli:
                                    {
                                        var Block = withBlock1.Columns["Codigo"];
                                        Block.Width = 150;
                                    }
                                    {
                                        var Block = withBlock1.Columns["Descripcion"];
                                        Block.Width = 190;
                                        break;
                                    }
                            }
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Aceptar()
        {
            if (GrdLista.RowCount > 0)
            {                
                switch (mod)
                {
                    case  ModoVentana.BusquedaPorPrograma:
                        {
                            oParent.CODIGO = GrdLista.GetValue(GrdLista.RootTable.Columns["Programa"].Index).ToString();
                            oParent.DESCRIPCION = GrdLista.GetValue(GrdLista.RootTable.Columns["Nombre_programa"].Index).ToString();
                            break;
                        }
                    case ModoVentana.BusquedaPorEstiloCliente:
                        {
                            oParent.CODIGO = GrdLista.GetValue(GrdLista.RootTable.Columns["Estilo"].Index).ToString();
                            oParent.DESCRIPCION = GrdLista.GetValue(GrdLista.RootTable.Columns["Nombre"].Index).ToString();
                            break;
                        }
                    case ModoVentana.BusquedaPorEstiloClienteFlash:
                        {
                            oParent.CODIGO = GrdLista.GetValue(GrdLista.RootTable.Columns["Estilo_Cliente"].Index).ToString();
                            oParent.DESCRIPCION = GrdLista.GetValue(GrdLista.RootTable.Columns["Nombre"].Index).ToString();
                            break;
                        }
                    case ModoVentana.BusquedaEstCli:
                        {
                            oParent.CODIGO = GrdLista.GetValue(GrdLista.RootTable.Columns["Codigo"].Index).ToString();
                            oParent.DESCRIPCION = GrdLista.GetValue(GrdLista.RootTable.Columns["Descripcion"].Index).ToString();
                            break;
                        }
                }
            }
            else
            {
                oParent.CODIGO = "";
                oParent.DESCRIPCION = "";
            }
            this.Close();
        }
    }
}
