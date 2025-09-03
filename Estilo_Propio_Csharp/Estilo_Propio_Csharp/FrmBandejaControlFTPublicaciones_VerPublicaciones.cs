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
    public partial class FrmBandejaControlFTPublicaciones_VerPublicaciones : Form
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
        public int IdFichaTecnica;
        #endregion

        public FrmBandejaControlFTPublicaciones_VerPublicaciones()
        {
            InitializeComponent();
        }

        private void FrmBandejaControlFTPublicaciones_VerPublicaciones_Load(object sender, EventArgs e)
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
                strSQL += "\n" + "EXEC FT_Muestras_Todas_Publicaciones_EPV_Sec";
                strSQL += "\n" + string.Format(" @cod_estpro        ='{0}'", TxtEstiloPropio.Text);
                strSQL += "\n" + string.Format(",@cod_version       ='{0}'", TxtVersion.Text);
                strSQL += "\n" + string.Format(",@id_fichatecnica   = {0} ", IdFichaTecnica);

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
                            var withBlock2 = withBlock1.Columns["ID_Publicacion"];
                            withBlock2.Caption = "ID PUBLI.";
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.Width = 70;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Fec_Publicacion"];
                            withBlock2.Caption = "FECHA PUBLI.";
                            withBlock2.Width = 90;
                            withBlock2.TextAlignment = TextAlignment.Center;
                            withBlock2.FormatString = "dd/MM/yyyy";
                        }

                        {
                            var withBlock2 = withBlock1.Columns["Cod_Usuario_Publicacion"];
                            withBlock2.Caption = "USUARIO PUBLI.";
                            withBlock2.Width = 110;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["ComentariosdePublicacion"];
                            withBlock2.Caption = "COMENTARIOS DE PUBLICACION";
                            withBlock2.Width = 200;
                        }     
                        
                        {
                            var withBlock2 = withBlock1.Columns["Nombre_Adjunto_Publicacion"];
                            withBlock2.Caption = "ADJUNTO PUBLICACION";
                            withBlock2.ColumnType = ColumnType.Link;
                            withBlock2.CellStyle.BackColor = Color.AliceBlue;
                            withBlock2.CellStyle.ForeColor = Color.Blue;
                            withBlock2.Width = 140;
                        }

                        {
                            var withBlock2 = withBlock1.Columns["FT_Completa"];
                            withBlock2.Caption = "FT COMPLETA";
                            withBlock2.Width = 80;
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

        private void GrdLista_LinkClicked(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (GrdLista.CurrentColumn == null) { return; }
                if (GrdLista.CurrentColumn.Key == null) { return; }
                switch (GrdLista.CurrentColumn.Key.ToUpper())
                {
                    case "NOMBRE_ADJUNTO_PUBLICACION":
                        if (GrdLista.RecordCount == 0) { return; }
                        int xfilas;
                        string RutaOriginal;
                        xfilas = GrdLista.Row;
                        if (GrdLista.RowCount == 0)
                            return;
                        string sRutaTemp;
                        sRutaTemp = @"C:\\TEMP\\";

                        if (!System.IO.Directory.Exists(sRutaTemp))
                            System.IO.Directory.CreateDirectory(sRutaTemp);
                        RutaOriginal = GrdLista.GetValue(GrdLista.RootTable.Columns["NOMBRE_ADJUNTO_PUBLICACION"].Index).ToString();
                        if (RutaOriginal == "")
                        {
                            return;
                        }

                        // Obtener el nombre del Archivo
                            string Namefiles = System.IO.Path.GetFileName(RutaOriginal);

                        // Obtener el Tamaño del Archivo
                        System.IO.FileInfo fi = new System.IO.FileInfo(RutaOriginal);
                        double xTamFile = fi.Length;

                        string sRutaTempNomFile;
                        sRutaTempNomFile = sRutaTemp + Namefiles;

                        if (System.IO.File.Exists(RutaOriginal))
                        {
                            if (System.IO.File.Exists(sRutaTempNomFile))
                                DelFichaTecnicaVista(sRutaTempNomFile);

                            if (!System.IO.File.Exists(sRutaTempNomFile))
                                System.IO.File.Copy(RutaOriginal, sRutaTempNomFile, true);

                            bool rtnvalue = false;

                            // Abrir archivo
                            try
                            {
                                System.IO.FileStream fs = System.IO.File.OpenWrite(sRutaTempNomFile);
                                fs.Close();
                            }
                            catch (Exception exx)
                            {
                                rtnvalue = true;
                            }

                            // CargarDocumentosComercial()
                            GrdLista.Row = xfilas;

                            if (!rtnvalue)
                            {
                                System.Diagnostics.Process Arc;
                                Arc = Process.Start(sRutaTempNomFile);
                            }
                            else
                                MessageBox.Show("Archivo ya se encuentra Abierto por Usted. Favor de Revisar", "Mensaje");
                        }
                        else
                            MessageBox.Show("Archivo no existe", "Mensaje");
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void DelFichaTecnicaVista(string vRutaFT)
        {
            try
            {
                if (!string.IsNullOrEmpty((vRutaFT)))
                    System.IO.File.Delete(vRutaFT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
