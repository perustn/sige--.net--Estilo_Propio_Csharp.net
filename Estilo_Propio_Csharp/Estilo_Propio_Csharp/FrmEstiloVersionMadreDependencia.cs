using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Janus.Windows.GridEX;

namespace Estilo_Propio_Csharp
{
    public partial class FrmEstiloVersionMadreDependencia : ProyectoBase.frmBase
    {
        #region Variables
        ClsHelper oHp = new ClsHelper();
        ModGeneral oMod = new ModGeneral();
        private Color colEmpresa;
        private string OpcionFiltro;
        public string strSQL = string.Empty;
        public new DataTable oDt;
        public string Codigo = ""; string Descripcion = ""; string TipoAdd = ""; string TipoAdd2 = ""; string TipoAdd3 = ""; string TipoAdd4 = "";
        private const string K_strColCheck = "FLG";
        private int FilaSeleccionado;
        clsBtnSeguridadJanus oSeg = new clsBtnSeguridadJanus();
        private string EPMAdre;
        private string VersionMadre;
        #endregion

        public FrmEstiloVersionMadreDependencia()
        {
            InitializeComponent();
        }

        private void FrmEstiloVersionMadreDependencia_Load(object sender, EventArgs e)
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
                }

                //oSeg.GetBotonesJanus(VariablesGenerales.pCodPerfil, VariablesGenerales.pCodEmpresa, this.Name, buttonBar1, "");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void TxtEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if(TxtEP.Text.Length > 0)
                {
                    TxtEP.Text = oHp.RellenaDeCerosEnIzquierda(TxtEP.Text, 5);
                }                
                BuscaEstiloPropio();
            }
        }

        private void TxtEPDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaEstiloPropio();
            }
        }

        public void BuscaEstiloPropio()
        {
            try
            {
                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                oTipo.sQuery = "select cod_estpro as Codigo , des_estpro as Descripcion from es_estpro where cod_estpro LIKE '%" + TxtEP.Text + "%'";

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {
                    TxtEP.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Codigo"]);
                    TxtEPDescripcion.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Descripcion"]);
                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {
                        TxtEP.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Codigo"].Value);
                        TxtEPDescripcion.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Descripcion"].Value);
                    }
                }
                BtnBuscar.Focus();
                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargaGrilla();
        }

        public void CargaGrilla()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC es_Es_EstProVer_Madres";
                strSQL += "\n" + string.Format(" @opcion            ='{0}'", "V");
                strSQL += "\n" + string.Format(",@cod_estpro     	='{0}'", TxtEP.Text);

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                gridEX1.RootTable.Columns.Clear();
                gridEX1.DataSource = oDt;

                oHp.CheckLayoutGridEx(gridEX1);

                {
                    var withBlock = gridEX1;
                    withBlock.FilterMode = FilterMode.Automatic;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;

                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 2;
                        withBlock1.RowHeight = 30;
                        withBlock1.PreviewRow = true;
                        withBlock1.PreviewRowLines = 15;

                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_EstPro_Madre"];
                            withBlock2.Caption = "EP MADRE";
                            withBlock2.Width = 90;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Version_Madre"];
                            withBlock2.Caption = "VERSION MADRE";
                            withBlock2.Width = 90;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Des_Version"];
                            withBlock2.Caption = "DESCRIPCION VERSION";
                            withBlock2.Width = 250;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Creacion"];
                            withBlock2.Caption = "FECHA CREACIÓN";
                            withBlock2.Width = 120;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 90;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonBar1_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            try
            {
                DialogResult rpt;
                switch (e.Item.Key)
                {
                    case "ADDEPVMADRE":
                        {
                            FrmEstiloVersionMadreDependencia_Adicionar oFrm = new FrmEstiloVersionMadreDependencia_Adicionar();
                            if (oFrm.ShowDialog() == DialogResult.OK)
                            {
                                CargaGrilla();
                            }
                            break;
                        }

                    case "ELIMINAREPVMADRE":
                        if (gridEX1.RecordCount == 0) { return; }
                        rpt = MessageBox.Show("¿Está seguro de actualizar la publicación de la FT seleccionada?", "Pregunta", MessageBoxButtons.YesNo);
                        if (DialogResult.Yes == rpt)
                        {
                            strSQL = string.Empty;
                            strSQL += "\n" + "EXEC FT_Cambia_Status_a_Publicado_Autorizacion";
                            strSQL += "\n" + string.Format(" @id_publicacion    = {0}", gridEX1.GetValue(gridEX1.RootTable.Columns["Id_Publicacion"].Index));
                            strSQL += "\n" + string.Format(",@cod_usuario		='{0}'", VariablesGenerales.pUsuario);
                            strSQL += "\n" + string.Format(",@cod_estacion		='{0}'", Environment.MachineName);

                            if (oHp.EjecutarOperacion(strSQL) == true)
                            {
                                CargaGrilla();
                            }
                        }
                        break;

                    case "EXPORTAR":
                        if (gridEX1.RowCount == 0)
                            return;
                        string Ruta_Archivo;

                        Ruta_Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        Random numAleatorio = new Random(System.Convert.ToInt32(DateTime.Now.Ticks & int.MaxValue));
                        string RutaUniArchivo = string.Format(Ruta_Archivo + @"\Export_{0}.xls", System.Convert.ToString(numAleatorio.Next()));
                        System.IO.FileStream fs = new System.IO.FileStream(RutaUniArchivo, System.IO.FileMode.Create);

                        gridEXExporter1.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows;
                        gridEXExporter1.GridEX = gridEX1;
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

        private void gridEX1_SelectionChanged(object sender, EventArgs e)
        {
            if (gridEX1.RowCount > 0)
            {
                DataRow odr;
                odr = oHp.ObtenerDr_DeGridEx(gridEX1);
                if (odr != null)
                {
                    EPMAdre = Convert.ToString(odr["Cod_EstPro_Madre"].ToString());
                    VersionMadre = Convert.ToString(odr["Cod_Version_Madre"].ToString());
                    CargaGrillaDetalle();
                }
            }
        }

        public void CargaGrillaDetalle()
        {
            try
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC es_up_man_Es_EstProVer_Madres_Dependencias";
                strSQL += "\n" + string.Format(" @opcion                ='{0}'", "V");
                strSQL += "\n" + string.Format(",@cod_estpro_madre	    ='{0}'", EPMAdre);
                strSQL += "\n" + string.Format(",@cod_version_madre  	='{0}'", VersionMadre);
                strSQL += "\n" + string.Format(",@cod_usuario			='{0}'", VariablesGenerales.pUsuario);

                oDt = oHp.DevuelveDatos(strSQL, VariablesGenerales.pConnect);
                gridEX2.RootTable.Columns.Clear();
                gridEX2.DataSource = oDt;
                oHp.CheckLayoutGridEx(gridEX2);

                {
                    var withBlock = gridEX2;
                    withBlock.FilterMode = FilterMode.Automatic;
                    withBlock.DefaultFilterRowComparison = FilterConditionOperator.Contains;

                    {
                        var withBlock1 = withBlock.RootTable;
                        withBlock1.HeaderLines = 2;
                        withBlock1.RowHeight = 20;
                        withBlock1.PreviewRow = true;
                        withBlock1.PreviewRowLines = 15;

                        foreach (GridEXColumn oGridEXColumn in withBlock1.Columns)
                        {
                            {
                                var withBlock2 = oGridEXColumn;
                                withBlock2.WordWrap = true;
                                withBlock2.FilterEditType = FilterEditType.Combo;
                            }
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_EstPro_Dependiente"];
                            withBlock2.Caption = "EP DEPENDIENTE";
                            withBlock2.Width = 110;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Version_Dependiente"];
                            withBlock2.Caption = "VERSION DEPENDIENTE";
                            withBlock2.Width = 110;
                            withBlock2.TextAlignment = TextAlignment.Center;
                        }
                        {
                            var withBlock2 = withBlock1.Columns["Des_Version"];
                            withBlock2.Caption = "DESCRIPCION VERSION";
                            withBlock2.Width = 220;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Creacion"];
                            withBlock2.Caption = "FECHA CREACIÓN";
                            withBlock2.Width = 120;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario"];
                            withBlock2.Caption = "USUARIO";
                            withBlock2.Width = 90;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonBar2_ItemClick(object sender, Janus.Windows.ButtonBar.ItemEventArgs e)
        {
            try
            {
                DialogResult rpt;
                switch (e.Item.Key)
                {
                    case "ADICIONARDEPEN":
                        {
                            FrmEstiloVersionMadreDependencia_AddDepend oFrm = new FrmEstiloVersionMadreDependencia_AddDepend();
                            oFrm.TxtEPMadre.Text = EPMAdre;
                            oFrm.TxtVerMadre.Text = VersionMadre;
                            if (oFrm.ShowDialog() == DialogResult.OK)
                            {
                                CargaGrillaDetalle();
                            }
                            break;
                        }

                    case "ELIMINARDEPEN":
                        if (gridEX2.RecordCount == 0) { return; }
                        rpt = MessageBox.Show("¿Está seguro de eliminar el registro seleccionado?", "Pregunta", MessageBoxButtons.YesNo);
                        if (DialogResult.Yes == rpt)
                        {
                            strSQL = string.Empty;
                            strSQL += "\n" + "EXEC es_up_man_Es_EstProVer_Madres_Dependencias";
                            strSQL += "\n" + string.Format(" @opcion                    ='{0}'", "D");
                            strSQL += "\n" + string.Format(",@cod_estpro_madre	        ='{0}'", EPMAdre);
                            strSQL += "\n" + string.Format(",@cod_version_madre  	    ='{0}'", VersionMadre);
                            strSQL += "\n" + string.Format(",@cod_Estpro_dependiente  	='{0}'", gridEX2.GetValue(gridEX2.RootTable.Columns["Cod_EstPro_Dependiente"].Index));
                            strSQL += "\n" + string.Format(",@cod_version_dependiente  	='{0}'", gridEX2.GetValue(gridEX2.RootTable.Columns["Cod_Version_Dependiente"].Index));
                            strSQL += "\n" + string.Format(",@cod_usuario			    ='{0}'", VariablesGenerales.pUsuario);

                            if (oHp.EjecutarOperacion(strSQL) == true)
                            {
                                CargaGrilla();
                            }
                        }
                        break;

                    case "EXPORTAR":
                        if (gridEX2.RowCount == 0)
                            return;
                        string Ruta_Archivo;

                        Ruta_Archivo = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        Random numAleatorio = new Random(System.Convert.ToInt32(DateTime.Now.Ticks & int.MaxValue));
                        string RutaUniArchivo = string.Format(Ruta_Archivo + @"\Export_{0}.xls", System.Convert.ToString(numAleatorio.Next()));
                        System.IO.FileStream fs = new System.IO.FileStream(RutaUniArchivo, System.IO.FileMode.Create);

                        gridEXExporter2.ExportMode = Janus.Windows.GridEX.ExportMode.AllRows;
                        gridEXExporter2.GridEX = gridEX2;
                        gridEXExporter2.Export(fs);
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
