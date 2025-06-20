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
using NPOI.SS.Formula.Functions;
using System.Xml;

namespace Estilo_Propio_Csharp
{
    public partial class FrmCargaUPCdesdeExcel_DatosExtraidos : Form
    {

        ClsHelper oHP = new ClsHelper();
        Color colEmpresa = new Color();
        public DataTable oDT_Datos = new DataTable();
        string strSQL;


        public FrmCargaUPCdesdeExcel_DatosExtraidos()
        {
            InitializeComponent();
        }

        private void FrmCargaUPCdesdeExcel_DatosExtraidos_Load(object sender, EventArgs e)
        {
            DataTable oDtColor = oHP.DevuelveDatos(string.Format("SELECT * FROM SEG_Empresas where cod_empresa = '{0}'", VariablesGenerales.pCodEmpresa), VariablesGenerales.pConnectSeguridad);
            colEmpresa = Color.FromArgb(Convert.ToInt32(oDtColor.Rows[0]["ColorFondo_R"]), Convert.ToInt32(oDtColor.Rows[0]["ColorFondo_G"]), Convert.ToInt32(oDtColor.Rows[0]["ColorFondo_B"]));
            panel2.BackColor = colEmpresa;

            CargaDatos();
        }

        private void CargaDatos()
        {            
            GridEX1.RootTable.Columns.Clear();
            GridEX1.DataSource = oDT_Datos;
            oHP.CheckLayoutGridEx(GridEX1);


            GridEX1.FilterMode = FilterMode.Automatic;
            GridEX1.DefaultFilterRowComparison = FilterConditionOperator.Contains;

            GridEX1.RootTable.HeaderLines = 2;
            GridEX1.RootTable.RowHeight = 30;
            GridEX1.RootTable.PreviewRow = true;
            //GridEX1.RootTable.Groups.Add("SHIPMENT");

            foreach (GridEXColumn oGridEXColumn in GridEX1.RootTable.Columns)
            {
                oGridEXColumn.WordWrap = true;
                oGridEXColumn.FilterEditType = FilterEditType.Combo;
                oGridEXColumn.TextAlignment = TextAlignment.Center;
                oGridEXColumn.Visible = true;
            }

            //////////////GridEXColumn SHIPMENT = GridEX1.RootTable.Columns["SHIPMENT"];
            //////////////SHIPMENT.Caption = "SHIPMENT";
            //////////////SHIPMENT.Width = 70;

            //////////////GridEXColumn COD_DESTINO = GridEX1.RootTable.Columns["COD_DESTINO"];
            //////////////COD_DESTINO.Caption = "CODIGO DESTINO";
            //////////////COD_DESTINO.Width = 80;

            //////////////GridEXColumn FINAL_NDC = GridEX1.RootTable.Columns["FINAL_INDC"];
            //////////////FINAL_NDC.Caption = "FINAL INDC";
            //////////////FINAL_NDC.Width = 90;

            //////////////GridEXColumn DES_RATIO_TALLA = GridEX1.RootTable.Columns["DES_RATIO_TALLA"];
            //////////////DES_RATIO_TALLA.Caption = "RATIO TALLAS";
            //////////////DES_RATIO_TALLA.Width = 80;

            //////////////GridEXColumn QTY_CASE_PACK = GridEX1.RootTable.Columns["QTY_CASE_PACK"];
            //////////////QTY_CASE_PACK.Caption = "QTY CASE PACK";
            //////////////QTY_CASE_PACK.Width = 90;

            //////////////GridEXColumn COD_ESTILO = GridEX1.RootTable.Columns["COD_ESTILO"];
            //////////////COD_ESTILO.Caption = "CODIGO ESTILO";
            //////////////COD_ESTILO.Width = 90;

            //////////////GridEXColumn DES_ESTILO = GridEX1.RootTable.Columns["DES_ESTILO"];
            //////////////DES_ESTILO.Caption = "ESTILO";
            //////////////DES_ESTILO.Width = 100;

            //////////////GridEXColumn COD_COLOR = GridEX1.RootTable.Columns["COD_COLOR"];
            //////////////COD_COLOR.Caption = "CODIGO COLOR";
            //////////////COD_COLOR.Width = 90;

            //////////////GridEXColumn COD_TALLA = GridEX1.RootTable.Columns["COD_TALLA"];
            //////////////COD_TALLA.Caption = "CODIGO TALLA";
            //////////////COD_TALLA.Width = 80;

            //////////////GridEXColumn CANTIDAD = GridEX1.RootTable.Columns["CANTIDAD"];
            //////////////CANTIDAD.Caption = "CANTIDAD";
            //////////////CANTIDAD.Width = 90;
            //////////////CANTIDAD.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum;
            //////////////CANTIDAD.TotalFormatString = "#,##0.00";
        }

        private void chkExpandir_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
