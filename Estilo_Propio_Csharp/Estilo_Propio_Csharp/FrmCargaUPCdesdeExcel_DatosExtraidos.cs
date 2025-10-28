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
using System.Diagnostics;

namespace Estilo_Propio_Csharp
{
    public partial class FrmCargaUPCdesdeExcel_DatosExtraidos : Form
    {

        ClsHelper oHP = new ClsHelper();
        Color colEmpresa = new Color();
        public DataTable oDT_Datos = new DataTable();

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
            GridEX1.RootTable.RowHeight = 20;
            GridEX1.RootTable.PreviewRow = true;
            foreach (GridEXColumn oGridEXColumn in GridEX1.RootTable.Columns)
            {
                oGridEXColumn.WordWrap = true;
                oGridEXColumn.FilterEditType = FilterEditType.Combo;
                oGridEXColumn.TextAlignment = TextAlignment.Center;
                oGridEXColumn.Visible = true;
            }

            GridEX1.RootTable.Columns["PO"].Position = 0;
            GridEX1.RootTable.Columns["ESTILO_CLIENTE"].Position =1;
            GridEX1.RootTable.Columns["ESTILO_CLIENTE_DES"].Position = 2;
            GridEX1.RootTable.Columns["COD_COLOR"].Position = 3;
            GridEX1.RootTable.Columns["DES_COLOR"].Position = 4;
            GridEX1.RootTable.Columns["TALLA"].Position = 5;
            GridEX1.RootTable.Columns["PRECIO"].Position = 6;
            GridEX1.RootTable.Columns["UPC"].Position = 7;
            GridEX1.RootTable.Columns["PO_LINE_ITEM"].Position = 8;
            GridEX1.RootTable.Columns["Material_Key"].Position = 9;
        }

        private void chkExpandir_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonBar1_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            switch (e.Item.Key)
            {
                case "EXPORTAR":
                    if (GridEX1.RowCount == 0)
                        return;
                    string Ruta_Archivo;

                    Ruta_Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    Random numAleatorio = new Random(System.Convert.ToInt32(DateTime.Now.Ticks & int.MaxValue));
                    string RutaUniArchivo = string.Format(Ruta_Archivo + @"\Export_{0}.xls", System.Convert.ToString(numAleatorio.Next()));
                    System.IO.FileStream fs = new System.IO.FileStream(RutaUniArchivo, System.IO.FileMode.Create);

                    gridEXExporter1.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows;
                    gridEXExporter1.GridEX = GridEX1;
                    gridEXExporter1.Export(fs);
                    fs.Close();
                    Process.Start(RutaUniArchivo);
                    break;
            }
        }
    }
}
