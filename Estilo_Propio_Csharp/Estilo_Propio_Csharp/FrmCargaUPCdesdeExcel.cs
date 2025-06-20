using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data.SqlClient;
using System.IO;

using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Estilo_Propio_Csharp
{
    public partial class FrmCargaUPCdesdeExcel : ProyectoBase.frmBase
    {
        public FrmCargaUPCdesdeExcel()
        {
            InitializeComponent();
        }


        //VARIABLES        
        string filePath = "";
        string sNom_Archivo = "";
        string sFormato = "";
        bool IsArchivoProcesado = false;
        IWorkbook workbook;

        ClsHelper oHP = new ClsHelper();
        //DATATABLE PARA LA INFO FINAL
        DataTable oDT_Datos_Finales = new DataTable();



        string codCliente = "";
        string poPETER = "";
        string codOrganizacion = "";
        string codOrganizacionPETER = "PM";
        string codOrganizacionTMW = "TM";
        string codOrganizacionFOOT = "FJ";
        string codOrganizacionSTITCH = "SGH";


        private void FrmCargaUPCdesdeExcel_Load(object sender, EventArgs e)
        {


            //DATATABLE PARA ALMACENAR LA INFO FINAL
            oDT_Datos_Finales.Columns.Add("PO", typeof(string));
            oDT_Datos_Finales.Columns.Add("Talla", typeof(string));
            oDT_Datos_Finales.Columns.Add("Cod_Color", typeof(string));
            oDT_Datos_Finales.Columns.Add("Precio", typeof(string));
            oDT_Datos_Finales.Columns.Add("Estilo_Cliente", typeof(string));
            oDT_Datos_Finales.Columns.Add("UPC", typeof(string));

            oDT_Datos_Finales.Columns.Add("Des_Color", typeof(string));
            oDT_Datos_Finales.Columns.Add("Estilo_Cliente_Des", typeof(string));
            oDT_Datos_Finales.Columns.Add("PO_Line_Item", typeof(string));
            oDT_Datos_Finales.Columns.Add("Material_Key", typeof(string));
        }


        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            BuscarArchivo();
        }

        private void BuscarArchivo()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Archivos (*.xlsx, *.xlsm , *.pdf)|*.xlsx;*.xlsm;*.pdf";
                openFileDialog.Title = "Seleccionar un archivo de Excel ó PDF";

                // Mostrar el diálogo al usuario
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtRuta_Archivo_Excel.Text = openFileDialog.FileName;
                    filePath = openFileDialog.FileName;
                    sNom_Archivo = Path.GetFileName(txtRuta_Archivo_Excel.Text);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }
        }

        private void btnCargaExcel_Click(object sender, EventArgs e)
        {

            if (txtAbrCliente.Text.Trim() == "")
            {
                MessageBox.Show("El cliente no puede estar Vacio", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtRuta_Archivo_Excel.Text.Trim() == "")
            {
                MessageBox.Show("No Se Ha Cargado Ningún Archivo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Esta seguro de extraer los datos del archivo seleccionado?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //LIMPIAMOS LOS DATATABLES
            oDT_Datos_Finales.Clear();

            sFormato = "1";

            if (string.IsNullOrWhiteSpace(codOrganizacion) ||
                        (codOrganizacion != codOrganizacionPETER &&
                         codOrganizacion != codOrganizacionTMW &&
                         codOrganizacion != codOrganizacionFOOT &&
                         codOrganizacion != codOrganizacionSTITCH))
            {

                MessageBox.Show("Cliente No puede usar esta opción ", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("El Archivo No Existe", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileStream is null)
                    {
                        MessageBox.Show("es nulo");
                        return;
                    }

                    fileStream.Seek(0, SeekOrigin.Begin);

                    if (Path.GetExtension(filePath).ToLower() == ".xlsx" || Path.GetExtension(filePath).ToLower() == ".xlsm")
                    {
                        workbook = new XSSFWorkbook(fileStream); // Para XLSX
                    }
                    else if (Path.GetExtension(filePath).ToLower() == ".xls")
                    {
                        workbook = new HSSFWorkbook(fileStream); // Para XLS
                    }
                    else if (Path.GetExtension(filePath).ToLower() == ".pdf")
                    {
                        sFormato = "2";
                    }
                    else
                    {
                        MessageBox.Show("Formato de archivo no soportado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string nombreSinExtension = Path.GetFileNameWithoutExtension(filePath);

                CargaExcel(sFormato, nombreSinExtension);

            }
            catch (IOException ioEx)
            {
                MessageBox.Show("Error de E/S:" + ioEx.Message);
            }
            catch (InvalidDataException dataEx)
            {
                MessageBox.Show("Error de datos inválidos (posiblemente archivo corrupto):" + dataEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void CargaExcel(string sFormato, string nomArchivo)
        {
            try
            {
                var mc = new ClsHelper();

                if (sFormato == "2")
                {
                    if (codOrganizacion == codOrganizacionSTITCH)
                    {
                        ExtractTableDataSTITCH_v2(txtRuta_Archivo_Excel.Text.Trim());
                    }

                    if (codOrganizacion == codOrganizacionPETER)
                    {
                        ExtractTableData(txtRuta_Archivo_Excel.Text.Trim());
                    }

                }

                if (sFormato == "1")
                {

                    int col_PO = -1;
                    int col_Talla = -1;
                    int col_CodColor = -1;
                    int col_DesColor = -1;
                    int col_Precio = -1;
                    int col_EstiloCliente = -1;
                    int col_EstiloClienteDes = -1;
                    int col_UPC = -1;

                    int col_PO_Line_Item = -1;
                    int col_Material_Key = -1;

                    for (int i = 0; i < workbook.NumberOfSheets; i++)
                    {
                        ISheet sheet = workbook.GetSheetAt(i);
                        string sheetName = sheet.SheetName.ToUpper();

                        if (codOrganizacion == codOrganizacionPETER)         //peter                   
                            if (!sheetName.Contains("ORDERMANAGMENT")) continue;
                        if (codOrganizacion == codOrganizacionTMW)   //tmw
                            if (!sheetName.Contains("SHEET1")) continue;
                        //filaEncabezado = 2;

                        if (codOrganizacion == codOrganizacionFOOT)  //foot
                            if (!sheetName.Contains("SHEET1")) continue;

                        // Buscar fila de encabezados
                        for (int rowIndex = sheet.FirstRowNum; rowIndex <= sheet.LastRowNum; rowIndex++)
                        {
                            IRow row = sheet.GetRow(rowIndex);
                            ///////IRow row = sheet.GetRow(filaEncabezado);
                            if (row == null) continue;

                            col_PO = col_Talla = col_CodColor = col_DesColor = col_Precio = col_EstiloCliente = col_EstiloClienteDes = col_UPC = col_PO_Line_Item = col_Material_Key = -1;


                            for (int cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
                            {
                                string header = mc.GetCellValue(row.GetCell(cellIndex)).ToString().ToUpper();

                                if (codOrganizacion == codOrganizacionPETER)
                                {
                                    if (header.Contains("VARPONUMBER")) col_PO = cellIndex;
                                    if (header.Contains("VARSIZE_SIZE")) col_Talla = cellIndex;
                                    if (header.Contains("VARCOLORCODE")) col_CodColor = cellIndex;
                                    if (header.Contains("VARPRICE")) col_Precio = cellIndex;
                                    if (header.Contains("VARSTYLECODE")) col_EstiloCliente = cellIndex;
                                    if (header.Contains("UPC")) col_UPC = cellIndex;
                                }
                                else if (codOrganizacion == codOrganizacionTMW)
                                {
                                    if (header.Contains("PO #")) col_PO = cellIndex;
                                    if (header.Contains("SIZE (ATTRIBUTE)")) col_Talla = cellIndex;
                                    if (header.Contains("COLOR DESC") && col_DesColor == -1) col_DesColor = cellIndex;
                                    if (header.Contains("COLOR") && col_CodColor == -1) col_CodColor = cellIndex;
                                    if (header.Contains("STYLE #")) col_EstiloCliente = cellIndex;
                                    if (header.Contains("STYLE DESC")) col_EstiloClienteDes = cellIndex;
                                    if (header.Contains("UPC")) col_UPC = cellIndex;
                                    if (header.Contains("PO LINE ITEM")) col_PO_Line_Item = cellIndex;
                                    if (header.Contains("MATERIAL - KEY")) col_Material_Key = cellIndex;
                                }
                                else if (codOrganizacion == codOrganizacionFOOT)
                                {

                                    if (header.Contains("ITEM#")) col_Talla = cellIndex;
                                    ///if (header.Contains("Name")) col_DesColor = cellIndex;
                                    if (header.Contains("ITEM#")) col_EstiloCliente = cellIndex;
                                    if (header.Contains("NAME")) col_EstiloClienteDes = cellIndex;
                                    if (header.Contains("UPC")) col_UPC = cellIndex;
                                }
                            }

                            //if (col_PO >= 0 && col_Talla >= 0 && col_CodColor>0 && col_Precio> 0 && col_EstiloCliente > 0 && col_UPC> 0)
                            if (col_UPC > 0)
                            {
                                int emptyRowCounter = 0;
                                for (int r = rowIndex + 1; r <= sheet.LastRowNum; r++)
                                ///for (int r = filaEncabezado + 1; r <= sheet.LastRowNum; r++)
                                {

                                    IRow dataRow = sheet.GetRow(r);
                                    if (dataRow == null)
                                    {
                                        emptyRowCounter++;
                                        if (emptyRowCounter > 50) break;
                                        continue;
                                    }

                                    string po = col_PO >= 0 ? mc.GetCellValue(dataRow.GetCell(col_PO))?.ToString() ?? "" : "";
                                    string talla = col_Talla >= 0 ? mc.GetCellValue(dataRow.GetCell(col_Talla))?.ToString() ?? "" : "";

                                    string codColor = col_CodColor >= 0 ? mc.GetCellValue(dataRow.GetCell(col_CodColor))?.ToString() ?? "" : "";
                                    string desColor = col_DesColor >= 0 ? mc.GetCellValue(dataRow.GetCell(col_DesColor))?.ToString() ?? "" : "";
                                    string precio = col_Precio >= 0 ? mc.GetCellValue(dataRow.GetCell(col_Precio))?.ToString() ?? "" : "";
                                    string estilo = col_EstiloCliente >= 0 ? mc.GetCellValue(dataRow.GetCell(col_EstiloCliente))?.ToString() ?? "" : "";
                                    string estiloDes = col_EstiloClienteDes >= 0 ? mc.GetCellValue(dataRow.GetCell(col_EstiloClienteDes))?.ToString() ?? "" : "";
                                    string upc = col_UPC >= 0 ? mc.GetCellValue(dataRow.GetCell(col_UPC))?.ToString() ?? "" : "";

                                    string poLineItem = col_PO_Line_Item >= 0 ? mc.GetCellValue(dataRow.GetCell(col_PO_Line_Item))?.ToString() ?? "" : "";
                                    string materialKey = col_Material_Key >= 0 ? mc.GetCellValue(dataRow.GetCell(col_Material_Key))?.ToString() ?? "" : "";

                                    if (codOrganizacion == codOrganizacionFOOT)
                                    {
                                        string[] partesNomArchivo = nomArchivo.Split('-');
                                        po = partesNomArchivo[1].Replace(" ", "");

                                        if (!string.IsNullOrWhiteSpace(talla) && talla.Contains("-"))
                                        {
                                            string[] partes = talla.Split('-');
                                            estilo = partes[0];
                                            talla = partes[1].Replace(" ", "");
                                        }
                                    }

                                    // Si está vacío, contamos; si no, reiniciamos contador
                                    if (string.IsNullOrWhiteSpace(po) && string.IsNullOrWhiteSpace(talla))
                                    {
                                        emptyRowCounter++;
                                        if (emptyRowCounter > 50) break;
                                        continue;
                                    }
                                    emptyRowCounter = 0;

                                    // Agregar a DataTable
                                    oDT_Datos_Finales.Rows.Add(po, talla, codColor, precio, estilo, upc, desColor, estiloDes, poLineItem, materialKey);
                                }

                                break; // Ya procesamos la hoja
                            }
                        }
                    }
                }

                if (oDT_Datos_Finales.Rows.Count > 0)
                {
                    IsArchivoProcesado = true;
                    MessageBox.Show("Datos Han Sido Extraidos Correctamente...!!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnVer_Datos_Extraidos.Enabled = true;
                    btnProcesarArchivo.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //throw;
            }
        }






        /////////////////////////////////////////////////////
        public void ExtractTableData(string pdfFilePath)
        {
            using (PdfDocument document = PdfDocument.Open(pdfFilePath))
            {
                bool tablaIniciada = false;
                float toleranciaY = 2f;
                float toleranciaX = 30f; // tolerancia horizontal para asignar columnas
                string ItemNumber = "";
                string Barcode = "";
                string Style = "";
                string Color = "";
                string Size = "";


                Dictionary<string, float> posicionesColumnas = null; // se llena al detectar encabezado

                for (int pageNum = 1; pageNum <= document.NumberOfPages; pageNum++)
                {
                    var page = document.GetPage(pageNum);
                    var words = page.GetWords().ToList();

                    if (!words.Any())
                        continue;

                    var sortedWords = words
                        .OrderByDescending(w => w.BoundingBox.Top)
                        .ThenBy(w => w.BoundingBox.Left)
                        .ToList();

                    var lines = sortedWords
                        .GroupBy(w => w.BoundingBox.Top, new DoubleToleranceComparer(toleranciaY))
                        .ToList();


                    for (int i = 0; i < sortedWords.Count - 3; i++)
                    {
                        string w0 = sortedWords[i].Text.Trim();
                        string w1 = sortedWords[i + 1].Text.Trim();
                        string w2 = sortedWords[i + 2].Text.Trim();
                        string w3 = sortedWords[i + 3].Text.Trim();

                        if (w0.Equals("PO", StringComparison.OrdinalIgnoreCase) &&
                            w1.Equals("Details", StringComparison.OrdinalIgnoreCase) &&
                            w2.Equals("As", StringComparison.OrdinalIgnoreCase) &&
                            w3.Equals("Below:", StringComparison.OrdinalIgnoreCase))
                        {
                            return;
                        }
                    }


                    if (sortedWords.Any(w => w.Text.Trim().Equals("Customer", StringComparison.OrdinalIgnoreCase)))
                    {
                        int index = sortedWords.FindIndex(w => w.Text.Trim().Equals("Customer", StringComparison.OrdinalIgnoreCase));
                        if (index > 0)
                        {
                            poPETER = sortedWords[index - 1].Text.Trim();
                        }
                    }


                    foreach (var line in lines)
                    {
                        var lineWords = line.OrderBy(w => w.BoundingBox.Left).ToList();


                        // Detectar línea encabezado tabla
                        if (!tablaIniciada && lineWords.Any(w => w.Text.Equals("MSRP", StringComparison.OrdinalIgnoreCase)))
                        {
                            tablaIniciada = true;

                            // Crear diccionario con posiciones de columnas basadas en encabezado
                            posicionesColumnas = new Dictionary<string, float>();

                            foreach (var w in lineWords)
                            {
                                // Usar el texto como clave, y posición Left como valor                                
                                posicionesColumnas[w.Text] = (float)w.BoundingBox.Left;
                            }

                            // Por ejemplo, si quieres renombrar claves para que coincidan con tus propiedades
                            // Puedes hacer un mapeo aquí, si el encabezado es diferente

                            continue; // saltar línea encabezado
                        }

                        if (!tablaIniciada)
                            continue;

                        // Detectar fin tabla
                        if (lineWords.Any(w => w.Text.Equals("PO", StringComparison.OrdinalIgnoreCase)))
                            break;

                        if (posicionesColumnas == null)
                            continue; // seguridad

                        // Ahora asignar las palabras de la fila a cada columna según proximidad en X
                        Dictionary<string, string> fila = new Dictionary<string, string>();

                        foreach (var col in posicionesColumnas)
                        {
                            // Buscar la palabra más cercana a la posición X de la columna
                            var palabraColumna = lineWords
                                .Where(w => Math.Abs(w.BoundingBox.Left - col.Value) < toleranciaX)
                                .OrderBy(w => Math.Abs(w.BoundingBox.Left - col.Value))
                                .FirstOrDefault();

                            fila[col.Key] = palabraColumna?.Text ?? "";
                        }

                        try
                        {
                          
                            ItemNumber = fila.ContainsKey("Item#") ? fila["Item#"] : "";
                            Barcode = fila.ContainsKey("Barcode") ? fila["Barcode"] : "";
                            Style = fila.ContainsKey("STYLE") ? fila["STYLE"] : "";
                            Color = fila.ContainsKey("COLOR") ? fila["COLOR"] : "";
                            Size = fila.ContainsKey("SIZE") ? fila["SIZE"] : "";

                            oDT_Datos_Finales.Rows.Add(poPETER, Size, Color, "", Style, Barcode, Color, "", "", "");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error procesando fila: {ex.Message}");
                        }
                    }
                }
            }
        }

        public void ExtractTableDataSTITCH_v2(string pdfFilePath)
        {
            using (PdfDocument document = PdfDocument.Open(pdfFilePath))
            {
                float toleranciaY = 2f;

                for (int pageNum = 1; pageNum <= document.NumberOfPages; pageNum++)
                {
                    var page = document.GetPage(pageNum);
                    var words = page.GetWords().ToList();
                    string po = "";

                    if (!words.Any())
                        continue;

                    // Agrupar por línea visual (tolerancia en Y)
                    var lines = words
                        .GroupBy(w => w.BoundingBox.Top, new DoubleToleranceComparer(toleranciaY))
                        .Select(g => g.OrderBy(w => w.BoundingBox.Left).ToList()) // Ordenar por X cada línea
                        .OrderByDescending(l => l.First().BoundingBox.Top) // Ordenar líneas de arriba hacia abajo
                        .ToList();

                    for (int i = 0; i < lines.Count; i++)
                    {
                        var line = lines[i];
                        if (line.Count < 3) continue;

                        if (line.Any(w => w.Text.Equals("Shipping", StringComparison.OrdinalIgnoreCase)))
                            return;

                        if (line.Any(w => w.Text.Equals("PurchaseOrder", StringComparison.OrdinalIgnoreCase)))
                            po = GetWordAt(lines[i], i + 1);


                        // Detectar la línea de información general por ciertos patrones
                        bool isGeneralInfoLine = line.Any(w => w.Text.Length == 9 && w.Text.All(char.IsLetterOrDigit) ||
                                                                            (w.Text.All(char.IsDigit) && w.Text.Length >= 5));
                        //////////bool isGeneralInfoLine = line.Any(w => w.Text.StartsWith("000SA") ||
                        //////////                                       (w.Text.All(char.IsDigit) && w.Text.Length >= 5));

                        if (!isGeneralInfoLine)
                            continue;

                        // Validar que hay suficientes líneas para un bloque
                        if (i + 5 >= lines.Count)
                            break;

                        var generalInfo = lines[i];
                        var sizeLine = lines[i + 1];
                        var qtyLine = lines[i + 2];
                        var upcLine = lines[i + 3];
                        var styleLine = lines[i + 4];
                        var priceLine = lines[i + 5];


                        ////////////////////////////////////
                        // --- Calcular color con control de proximidad ---
                        var colorWord1 = generalInfo.ElementAtOrDefault(1);
                        var colorWord2 = generalInfo.ElementAtOrDefault(2);
                        string color;

                        if (colorWord1 != null && colorWord2 != null)
                        {
                            float espacioMaximo = 10f; // Ajusta según pruebas
                            double distancia = colorWord2.BoundingBox.Left - colorWord1.BoundingBox.Right;

                            if (distancia <= espacioMaximo)
                                color = $"{colorWord1.Text} {colorWord2.Text}";
                            else
                                color = colorWord1.Text;
                        }
                        else
                        {
                            color = colorWord1?.Text ?? "";
                        }

                        ///////////////////////////////////

                        foreach (var sizeWord in sizeLine)
                        {
                            double x = sizeWord.BoundingBox.Left;
                            string size = sizeWord.Text;

                            string qty = FindNearestTextSafe(qtyLine, x);
                            string upc = FindNearestTextSafe(upcLine, x);
                            string style = FindNearestTextSafe(styleLine, x);
                            string price = FindNearestTextSafe(priceLine, x);

                            if (!int.TryParse(qty.Replace(",", ""), out int cantidad))
                                continue;

                            string fullUpc = (!string.IsNullOrWhiteSpace(upc) ? upc : "") +
                                             (!string.IsNullOrWhiteSpace(style) ? style : "");

                            // Agregar a DataTable
                            oDT_Datos_Finales.Rows.Add(po, size, color, price, GetWordAt(generalInfo, 0), fullUpc, color, GetWordAt(generalInfo, 2), "", "");

                        }

                        i += 5; // Saltar líneas ya procesadas
                    }
                }
            }
        }

        public string FindNearestTextSafe(List<Word> line, double x, float maxDist = 20f)
        {
            var nearest = line
                .OrderBy(w => Math.Abs(w.BoundingBox.Left - x))
                .FirstOrDefault();

            if (nearest != null && Math.Abs(nearest.BoundingBox.Left - x) <= maxDist)
                return nearest.Text.Trim();

            return ""; // vacío si no hay valor cercano
        }

        public string GetWordAt(List<Word> words, int index)
        {
            if (index >= 0 && index < words.Count)
                return words[index].Text.Trim();
            return "";
        }

        public string GetWordsBetween(List<Word> words, int startIndex, int endIndex)
        {
            var sb = new StringBuilder();
            for (int i = startIndex; i <= endIndex && i < words.Count; i++)
            {
                sb.Append(words[i].Text.Trim());
                if (i < endIndex) sb.Append(" ");
            }
            return sb.ToString();
        }

        private string FindNearestText(List<Word> line, float x)
        {
            return line
                .OrderBy(w => Math.Abs(w.BoundingBox.Left - x))
                .FirstOrDefault()?.Text ?? "";
        }
        string GetValueOrEmpty(Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key] : "";
        }

        // Comparador con tolerancia para agrupar líneas con Y similar
        public class DoubleToleranceComparer : IEqualityComparer<double>
        {
            private readonly double _tolerance;

            public DoubleToleranceComparer(double tolerance)
            {
                _tolerance = tolerance;
            }

            public bool Equals(double x, double y)
            {
                return Math.Abs(x - y) <= _tolerance;
            }

            public int GetHashCode(double obj)
            {
                return 0; // fuerza uso de Equals
            }
        }

        private void btnVer_Datos_Extraidos_Click(object sender, EventArgs e)
        {
            using (FrmCargaUPCdesdeExcel_DatosExtraidos Frm = new FrmCargaUPCdesdeExcel_DatosExtraidos())
            {
                Frm.oDT_Datos = oDT_Datos_Finales.Copy();
                Frm.txtCliente.Text = txtAbrCliente.Text + "-" + txtDesCliente.Text;
                Frm.txtNomArchivo.Text = sNom_Archivo.ToString();
                DialogResult dr = Frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    return;
                }
            }
        }

        public void BuscaCliente(int opcion)
        {
            try
            {

                txtRuta_Archivo_Excel.Text = "";
                filePath = "";
                sNom_Archivo = "";
                codOrganizacion = "";

                FrmBusquedaGeneral oTipo = new FrmBusquedaGeneral();
                if (opcion == 1)
                {
                    oTipo.sQuery = "select Abr_cliente, Nom_cliente, Cod_cliente,cod_organizacion from tg_cliente where cod_organizacion in ('TM','PM','FJ','SGH') and abr_cliente like '%" + txtAbrCliente.Text + "%'";
                }
                else
                {
                    oTipo.sQuery = "select Abr_cliente, Nom_cliente, Cod_cliente,cod_organizacion from tg_cliente where cod_organizacion in ('TM','PM','FJ','SGH') and Nom_cliente like '%" + txtDesCliente.Text + "%'";
                }

                oTipo.Cargar_Datos();
                if (oTipo.dtResultados.Rows.Count == 1)
                {

                    codCliente = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_cliente"]);
                    txtAbrCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["abr_cliente"]);
                    txtDesCliente.Text = Convert.ToString(oTipo.dtResultados.Rows[0]["Nom_cliente"]);
                    codOrganizacion = Convert.ToString(oTipo.dtResultados.Rows[0]["cod_organizacion"]).Trim().ToUpper();

                }
                else
                {
                    oTipo.ShowDialog();
                    if (oTipo.oParent.CODIGO != "")
                    {

                        codCliente = Convert.ToString(oTipo.RegistroSeleccionado.Cells["cod_cliente"].Value);
                        txtAbrCliente.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["abr_Cliente"].Value);
                        txtDesCliente.Text = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Nom_cliente"].Value);
                        codOrganizacion = Convert.ToString(oTipo.RegistroSeleccionado.Cells["Cod_Organizacion"].Value).Trim().ToUpper();
                    }
                }


                btnBuscarArchivo.Focus();

                oTipo.oParent.CODIGO = ""; oTipo.oParent.DESCRIPCION = ""; oTipo.oParent.TipoAdd = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAbrCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaCliente(1);
            }
        }

        private void txtDesCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                BuscaCliente(2);
            }
        }

        private void btnProcesarArchivo_Click(object sender, EventArgs e)
        {
            if (IsArchivoProcesado == false)
            {
                MessageBox.Show("Primero Debe Cargar El Archivo Excel.", "Sin Selección", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Esta seguro de procesar el archivo cargado?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }



            DataTable DtClientes = oHP.DevuelveDatos("select cod_cliente from tg_cliente where cod_organizacion in ('TM','PM','FJ','SGH') and cod_cliente='" + codCliente + "'", VariablesGenerales.pConnect);

            if (DtClientes == null || DtClientes.Rows.Count == 0)
            {
                MessageBox.Show("Cliente no válido o no permitido", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(VariablesGenerales.pConnect))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    SqlTransaction transaction = connection.BeginTransaction("CargaUPC_DesdeExcel");
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        string NumMovimiento = string.Empty;
                        foreach (DataRow row in oDT_Datos_Finales.Rows)
                        {

                            //PROCESAMOS LA INFO FINAL
                            cmd.CommandText = "UP_ACTUALIZA_TG_LOTCOLTAL_BITACORA_UPC";
                            cmd.Parameters.Add(new SqlParameter("@COD_CLIENTE", SqlDbType.Char, 5)).Value = codCliente;
                            cmd.Parameters.Add(new SqlParameter("@COD_PURORD", SqlDbType.Char, 20)).Value = row["PO"].ToString();
                            cmd.Parameters.Add(new SqlParameter("@COD_LOTPURORD", SqlDbType.Char, 3)).Value = "";
                            cmd.Parameters.Add(new SqlParameter("@COD_ESTCLI", SqlDbType.Char, 20)).Value = row["Estilo_Cliente"].ToString();
                            cmd.Parameters.Add(new SqlParameter("@COD_COLCLI", SqlDbType.Char, 20)).Value = row["Cod_Color"].ToString();
                            cmd.Parameters.Add(new SqlParameter("@COD_TALLA", SqlDbType.Char, 10)).Value = row["Talla"].ToString();
                            cmd.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 35)).Value = row["UPC"].ToString();
                            cmd.Parameters.Add(new SqlParameter("@Cod_Usuario", SqlDbType.VarChar, 50)).Value = VariablesGenerales.pUsuario;
                            cmd.Parameters.Add(new SqlParameter("@Cod_Estacion", SqlDbType.VarChar, 50)).Value = SystemInformation.ComputerName;

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();

                        MessageBox.Show("Se Ha Generado CORRECTAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show(ex2.Message, "Error al revertir transacción", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
