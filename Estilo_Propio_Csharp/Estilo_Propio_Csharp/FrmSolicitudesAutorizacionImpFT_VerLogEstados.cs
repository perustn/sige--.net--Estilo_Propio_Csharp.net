using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estilo_Propio_Csharp
{
    public partial class FrmSolicitudesAutorizacionImpFT_VerLogEstados : Form
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        private Color colEmpresa;
        private string OpcionFiltro;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        private Boolean bolSW_LoadGUI = true;
        public int FilaSeleccionado;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        #endregion

        public FrmSolicitudesAutorizacionImpFT_VerLogEstados()
        {
            InitializeComponent();
        }

        private void FrmSolicitudesAutorizacionImpFT_VerLogEstados_Load(object sender, EventArgs e)
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
                    Panel1.BackColor = colEmpresa;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CargaGrilla()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC UP_MAN_FT_AI_Solicitud_Estado";
                strSQL += "\n" + string.Format(" @Opcion            ='{0}'", "V");
                strSQL += "\n" + string.Format(",@Id_Solicitud		= {0} ",TxtIdSolicitud.Text);
                strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(",@cod_estacion		='{0}'", Environment.MachineName);

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                GrdLista.RootTable.Columns.Clear();
                GrdLista.DataSource = oDt;
                oHp.CheckLayoutGridEx(GrdLista);

                {
                    var withBlock = GrdLista;
                    withBlock.FilterMode = FilterMode.Automatic;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;

                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 2;
                        withBlock1.RowHeight = 30;
                        withBlock1.PreviewRow = false;
                        withBlock1.PreviewRowLines = 15;
                        withBlock1.PreviewRowMember = "Cambio";

                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }
                        {
                            var withBlock2 = withBlock1.Columns["COD_ESTADO"];
                            withBlock2.Caption = "COD ESTADO";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 50;
                            withBlock2.Visible = false;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["NOM_ESTADO"];
                            withBlock2.Caption = "ESTADO";
                            withBlock2.Width = 150;
                        }
                        
                        {
                            var withBlock2 = withBlock1.Columns["FEC_ESTADO"];
                            withBlock2.Caption = "FECHA ESTADO";
                            withBlock2.Width = 110;
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.FormatString = "dd/MM/yyyy";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 110;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["COD_ESTACION"];
                            withBlock2.Caption = "ESTACION";
                            withBlock2.Width = 110;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonBar1_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            try
            {
                switch (e.Item.Key)
                {
                    case "EXPORTAR":
                        if (GrdLista.RowCount == 0)
                            return;
                        string Ruta_Archivo;

                        Ruta_Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        Random numAleatorio = new Random(System.Convert.ToInt32(DateTime.Now.Ticks & int.MaxValue));
                        string RutaUniArchivo = string.Format(Ruta_Archivo + @"\Export_{0}.xls", System.Convert.ToString(numAleatorio.Next()));
                        System.IO.FileStream fs = new System.IO.FileStream(RutaUniArchivo, System.IO.FileMode.Create);

                        gridEXExporter1.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows;
                        gridEXExporter1.GridEX = GrdLista;
                        gridEXExporter1.Export(fs);
                        fs.Close();
                        Process.Start(RutaUniArchivo);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
