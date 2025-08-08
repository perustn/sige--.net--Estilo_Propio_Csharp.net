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
        ClsHelper oHp = new ClsHelper();

        enum FormatoDocumento
        {
            Excel,
            Pdf
        }

        //DATATABLE PARA LA INFO FINAL
        DataTable oDT_Datos_Finales = new DataTable();

        string codCliente = "";
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

            FormatoDocumento sFormato = FormatoDocumento.Excel;

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
                        sFormato = FormatoDocumento.Pdf;
                    }
                    else
                    {
                        MessageBox.Show("Formato de archivo no soportado.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                string nombreArchivoSinExtension = Path.GetFileNameWithoutExtension(filePath);

                ExtraerData(sFormato, nombreArchivoSinExtension);

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

        private void ExtraerData(FormatoDocumento sFormato, string nomArchivo)
        {
            try
            {
                var mc = new ClsHelper();

                switch (sFormato)
                {
                    case FormatoDocumento.Pdf:
                        if (codOrganizacion == codOrganizacionSTITCH)
                        {
                            ExtractTableDataSTITCH(txtRuta_Archivo_Excel.Text.Trim());
                        }
                        else if (codOrganizacion == codOrganizacionPETER)
                        {
                            ExtractTableData2(txtRuta_Archivo_Excel.Text.Trim());
                        }
                        break;
                    case FormatoDocumento.Excel:

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

                                if  (workbook.NumberOfSheets > 1) {
                                    if (codOrganizacion == codOrganizacionPETER) { }
                                        if (!sheetName.Contains("ORDERMANAGMENT")) continue;
                                    if (codOrganizacion == codOrganizacionTMW)
                                        if (!sheetName.Contains("SHEET1")) continue;
                                    if (codOrganizacion == codOrganizacionFOOT)
                                        if (!sheetName.Contains("SHEET1")) continue;
                                }
                               

                                // Buscar fila de encabezados
                                for (int rowIndex = sheet.FirstRowNum; rowIndex <= sheet.LastRowNum; rowIndex++)
                                {
                                    IRow row = sheet.GetRow(rowIndex);

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
                                            if (header.Contains("ITEM#")) col_EstiloCliente = cellIndex;
                                            if (header.Contains("NAME")) col_EstiloClienteDes = cellIndex;
                                            if (header.Contains("UPC")) col_UPC = cellIndex;
                                        }
                                    }

                                    if (col_UPC > 0)
                                    {
                                        int emptyRowCounter = 0;
                                        for (int r = rowIndex + 1; r <= sheet.LastRowNum; r++)
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
                        break;
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

        public void ExtractTableData2(string pdfFilePath)
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
                string precio = "";


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
                    
                    foreach (var line in lines)
                    {
                        var lineWords = line.OrderBy(w => w.BoundingBox.Left).ToList();


                        // Detectar línea encabezado tabla
                        if (!tablaIniciada && lineWords.Any(w => w.Text.Equals("varPrice", StringComparison.OrdinalIgnoreCase)))
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

                        ////////////////// Detectar fin tabla
                        ////////////////if (lineWords.Any(w => w.Text.Equals("PO", StringComparison.OrdinalIgnoreCase)))
                        ////////////////    break;

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
                            ItemNumber = fila.ContainsKey("varPONumber") ? fila["varPONumber"] : "";
                            Barcode = fila.ContainsKey("UPC") ? fila["UPC"] : "";
                            Style = fila.ContainsKey("varStyleCode") ? fila["varStyleCode"] : "";
                            Color = fila.ContainsKey("varColorCode") ? fila["varColorCode"] : "";
                            Size = fila.ContainsKey("varSize_Size") ? fila["varSize_Size"] : "";
                            precio = fila.ContainsKey("varPrice") ? fila["varPrice"] : "";

                            if (!string.IsNullOrWhiteSpace(Barcode))
                            {
                                oDT_Datos_Finales.Rows.Add(ItemNumber, Size, Color, precio, Style, Barcode, Color, "", "", "");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error procesando fila: {ex.Message}");
                        }
                    }
                }
            }
        }

        public void ExtractTableDataSTITCH(string pdfFilePath)
        {
            string po = "";
            List<Word> headerLine = null;
            //double xStyle = 0;
            double xColor = 0;
            double xProduct = 0;
            double xDesc = 0;
            double xQty = 0;
            //double xAmount = 0;
            int indexTabla = 0;
            int nroLineasTabla = 5;

            using (PdfDocument document = PdfDocument.Open(pdfFilePath))
            {
                float toleranciaY = 2f;

                for (int pageNum = 1; pageNum <= document.NumberOfPages; pageNum++)
                {
                    var page = document.GetPage(pageNum);
                    var words = page.GetWords().ToList();

                    if (!words.Any())
                        continue;

                    // Agrupar por línea visual (tolerancia en Y)
                    var lines = words
                        .GroupBy(w => w.BoundingBox.Top, new DoubleToleranceComparer(toleranciaY))
                        .Select(g => g.OrderBy(w => w.BoundingBox.Left).ToList()) // Ordenar por X cada línea
                        .OrderByDescending(l => l.First().BoundingBox.Top) // Ordenar líneas de arriba hacia abajo
                        .ToList();
                    
                    //obtenemos ubicacion de las cabeceras de la tabla
                    if (headerLine == null || !headerLine.Any())
                    {
                        for (int i = 0; i < lines.Count; i++)
                        {
                            List<Word> line = lines[i];
                            if (line.Any(w => w.Text.Equals("Style", StringComparison.OrdinalIgnoreCase)) &&
                                line.Any(w => w.Text.Equals("Color", StringComparison.OrdinalIgnoreCase)))
                            {
                                headerLine = line;
                                indexTabla = i;
                                break;
                            }
                        }

                        //xStyle = headerLine.First(w => w.Text.Equals("Style", StringComparison.OrdinalIgnoreCase)).BoundingBox.Left;
                        xColor = headerLine.First(w => w.Text.Equals("Color", StringComparison.OrdinalIgnoreCase)).BoundingBox.Left;
                        xProduct = headerLine.First(w => w.Text.Equals("ProductType", StringComparison.OrdinalIgnoreCase)).BoundingBox.Left;
                        xDesc = headerLine.First(w => w.Text.Equals("Description", StringComparison.OrdinalIgnoreCase)).BoundingBox.Left;
                        xQty = headerLine.First(w => w.Text.Equals("Qty", StringComparison.OrdinalIgnoreCase)).BoundingBox.Left;
                        //xAmount = headerLine.FirstOrDefault(w => w.Text.Equals("Amount", StringComparison.OrdinalIgnoreCase))?.BoundingBox.Left ?? xQty + 50;
                    }

                    for (int i = 0; i < lines.Count; i++)
                    {
                        List<Word> line = lines[i];

                        //obtenemos PO (purcharse order)
                        if (po.Equals(""))
                        {
                            if (words[i].Text.Equals("Purchase", StringComparison.OrdinalIgnoreCase) &&
                            (words[i + 1].Text.Equals("Order", StringComparison.OrdinalIgnoreCase)) &&
                            (words[i + 2].Text.Equals("ID", StringComparison.OrdinalIgnoreCase)))
                            {
                                po = words[i + 3].Text;
                                continue;
                            }
                        }

                        if (i < indexTabla && pageNum == 1) continue;

                        if (line.Count < 3) continue;

                        if (line.Any(w => w.Text.Equals("Shipping", StringComparison.OrdinalIgnoreCase)))
                            return;

                        // Validar que hay suficientes líneas para un bloque
                        if (i + nroLineasTabla >= lines.Count)
                            break;

                        // Detectar la línea de información general por ciertos patrones
                        bool isGeneralInfoLine = line.Any(w => w.Text.Length == 9 && w.Text.All(char.IsLetterOrDigit) ||
                                                                            (w.Text.All(char.IsDigit) && w.Text.Length >= 5));
                        if (!isGeneralInfoLine)
                            continue;

                        List<Word> generalInfo = lines[i];
                        List<Word> sizeLine = lines[i + 1];
                        List<Word> qtyLine = lines[i + 2];
                        List<Word> upcLine = lines[i + 3];
                        List<Word> upcLine_2 = null;
                        List<Word> priceLine = null;

                        if (upcLine.Any(w => w.Text.Length == 12 && w.Text.All(char.IsLetterOrDigit)))
                        {    
                            priceLine = lines[i + 4];
                            nroLineasTabla = 4;
                        }
                        else
                        {
                            upcLine_2 = lines[i + 4];
                            priceLine = lines[i + 5];
                            nroLineasTabla = 5;
                        }

                        double xColorStart = xColor;
                        double xColorEnd = xProduct;

                        var wordsColor = generalInfo
                            .Where(w => w.BoundingBox.Left >= xColorStart && w.BoundingBox.Left < xColorEnd)
                            .ToList();
                        
                        float maxDistanceColor = (float)Math.Abs(xColorEnd - xColorStart);
                        
                        string color = FindNearbyTextJoined(wordsColor, xColorStart, maxDistanceColor);

                        string ec_des="";

                        if (color != null )
                        {

                            double xDescStart = xDesc;
                            double xDescEnd = xQty;

                            var wordsDesc = generalInfo
                                .Where(w => w.BoundingBox.Left >= xDescStart && w.BoundingBox.Left < xDescEnd)
                                .ToList();

                            float maxDistance = (float)Math.Abs(xDescEnd - xDescStart) * 0.7f; // Ajustable
                            ec_des = FindNearbyTextJoined(wordsDesc, xDescStart, maxDistance);
                        }
                       
                        for (int s = 0; s < sizeLine.Count; s++)
                        {
                            var sizeWord = sizeLine[s];
                            double x = sizeWord.BoundingBox.Left;
                            string size = sizeWord.Text;

                            // Calcular distancia con la siguiente talla (si existe)
                            float maxDist = 20f; // Valor por defecto si no hay siguiente
                            if (s + 1 < sizeLine.Count)
                            {
                                var nextSizeWord = sizeLine[s + 1];
                                double nextX = nextSizeWord.BoundingBox.Left;
                                maxDist = (float)Math.Abs(nextX - x) * 0.7f; // o 0.5f según tus pruebas
                            }

                            string qty = FindNearestTextSafe_v3(qtyLine, x, maxDist);
                            string upc = FindNearestTextSafe_v3(upcLine, x, maxDist);
                            string upc_2 = "";
                            if (upcLine_2 != null)
                            {
                                if (upcLine_2.Any())
                                {
                                    upc_2 = FindNearestTextSafe_v3(upcLine_2, x, maxDist);
                                }
                            }
                                
                            string price = FindNearestTextSafe_v3(priceLine, x, maxDist);

                            if (!int.TryParse(qty.Replace(",", ""), out int cantidad))
                                continue;

                            string fullUpc = (!string.IsNullOrWhiteSpace(upc) ? upc : "") +
                                             (!string.IsNullOrWhiteSpace(upc_2) ? upc_2 : "");

                            // Agregar a DataTable
                            oDT_Datos_Finales.Rows.Add(po, size, color, price, GetWordAt(generalInfo, 0), fullUpc, color, ec_des, "", "");

                        }

                        i += nroLineasTabla; // Saltar líneas ya procesadas
                    }
                }
            }
        }

        public string FindNearbyTextJoined(List<Word> line, double x, float maxDist)
        {
            var palabras = line
                .Where(w => Math.Abs(w.BoundingBox.Left - x) <= maxDist)
                .OrderBy(w => w.BoundingBox.Left)
                .Select(w => w.Text.Trim());

            return string.Join(" ", palabras);
        }

        public string FindNearestTextSafe_v3(List<Word> line, double x, float maxDist )
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

                    oTipo.sQuery = string.Format("exec tg_ayuda_cliente_carga_upc @opcion = '{0}', @abr_cliente = '{1}'", "1", txtAbrCliente.Text);
                }
                else
                {
                    oTipo.sQuery = string.Format("exec tg_ayuda_cliente_carga_upc @opcion = '{0}', @Nom_cliente = '{1}'", "2", txtDesCliente.Text);
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

            if (codCliente.Equals(""))
            {
                MessageBox.Show("Cliente no válido o no permitido", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string strSQL = string.Empty;
            foreach (DataRow row in oDT_Datos_Finales.Rows)
            {
                strSQL = string.Empty;
                strSQL += "\n" + "EXEC UP_ACTUALIZA_TG_LOTCOLTAL_BITACORA_UPC";
                strSQL += "\n" + string.Format(" @COD_CLIENTE    ='{0}'", codCliente);
                strSQL += "\n" + string.Format(",@COD_PURORD     ='{0}'", row["PO"].ToString());
                strSQL += "\n" + string.Format(",@COD_LOTPURORD  ='{0}'", "");
                strSQL += "\n" + string.Format(",@COD_ESTCLI     ='{0}'", row["Estilo_Cliente"].ToString());
                strSQL += "\n" + string.Format(",@COD_COLCLI     ='{0}'", row["Cod_Color"].ToString());
                strSQL += "\n" + string.Format(",@COD_TALLA      ='{0}'", row["Talla"].ToString());
                strSQL += "\n" + string.Format(",@UPC            ='{0}'", row["UPC"].ToString());
                strSQL += "\n" + string.Format(",@Cod_Usuario    ='{0}'", VariablesGenerales.pUsuario);
                strSQL += "\n" + string.Format(",@Cod_Estacion   ='{0}'", SystemInformation.ComputerName);

                oHp.EjecutarOperacion(strSQL);
            }

            /*
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
                            cmd.Parameters.Clear();

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

                        MessageBox.Show("Se han guardado los datos CORRECTAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            */
        }
    }
}
