using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Pdf.Annotations;
using System.Linq;
using Newtonsoft.Json;

namespace Estilo_Propio_Csharp
{
    public class PdfIndexProcessor
    {
        public object JsonConvert { get; private set; }

        public void ProcesarPdfConIndice(string pdfOriginal, string archivoMetadatos, string pdfFinal)
        {
            // 1. Cargar metadatos de Excel
            DocumentoMetadata metadata = CargarMetadatos(archivoMetadatos);
            Console.WriteLine($"📊 Cargados metadatos de {metadata.documento.totalHojas} hojas");

            metadata = ActualizarMetaDatos(metadata, pdfOriginal, archivoMetadatos);
           
            // 2. Abrir PDF original
            PdfDocument documento = PdfReader.Open(pdfOriginal, PdfDocumentOpenMode.Import);
            Console.WriteLine($"📄 PDF original cargado: {documento.PageCount} páginas");

            //metadata.documento.totalPaginasPDF = documento.PageCount;

            // Validar que coincida el número de páginas
            if (documento.PageCount != metadata.documento.totalPaginasPDF)
            {
                Console.WriteLine($"⚠️ ADVERTENCIA: PDF tiene {documento.PageCount} páginas, metadatos indican {metadata.documento.totalPaginasPDF}");
            }

            // 3. Crear nuevo documento
            PdfDocument documentoFinal = new PdfDocument();
            documentoFinal.Info.Title = metadata.documento.titulo;
            documentoFinal.Info.Subject = "Documento con índice interactivo generado automáticamente";
            documentoFinal.Info.Creator = "Excel + C# PdfSharp Processor";

            // 4. Crear página de índice
            var paginaIndice = CrearIndiceMultipagina(documentoFinal, metadata);
            Console.WriteLine("📋 Página de índice creada");
            int nroPaginasIndice = paginaIndice.Count();

            // 5. Copiar páginas originales y crear marcadores
            var marcadores = new List<PdfOutline>();

            foreach (var hoja in metadata.documento.hojas)
            {
                Console.WriteLine($"🔄 Procesando hoja: {hoja.nombre}");
                Console.WriteLine($"   📄 Páginas a copiar: {hoja.paginaInicioPDF} a {hoja.paginaFinPDF}");

                PdfPage primeraPaginaCopiada = null;
                int paginasCopiadas = 0;

                // Copiar TODAS las páginas de esta hoja
                for (int indicePagina = hoja.paginaInicioPDF; indicePagina <= hoja.paginaFinPDF; indicePagina++)
                {
                    // Validar que el índice sea válido
                    if (indicePagina >= 0 && indicePagina < documento.PageCount)
                    {
                        // Obtener página original del PDF
                        var paginaOriginal = documento.Pages[indicePagina];

                        // Copiar la página al documento final
                        var nuevaPagina = documentoFinal.AddPage(paginaOriginal);

                        var gfx = XGraphics.FromPdfPage(nuevaPagina);

                        Console.WriteLine($"     ✓ Copiada página {indicePagina} -> Posición {documentoFinal.PageCount - 1} en documento final");

                        // Guardar referencia a la PRIMERA página copiada de esta hoja
                        if (primeraPaginaCopiada == null)
                        {
                            primeraPaginaCopiada = nuevaPagina;
                            Console.WriteLine($"     📍 Primera página de la hoja guardada: posición {documentoFinal.PageCount - 1}");
                        }

                        // Solo agregar título a la primera página de la hoja
                        if (indicePagina == hoja.paginaInicioPDF)
                        {
                            // AgregarTituloAPagina(nuevaPagina, hoja.tituloIndice);
                            Console.WriteLine($"     🏷️ Título agregado a primera página de la sección");
                        }

                        // Número de página del índice
                        gfx.DrawString($"Página {documentoFinal.PageCount } de { metadata.documento.totalPaginasPDF + nroPaginasIndice}",
                                      new XFont("Arial", 8, XFontStyle.Regular),
                                      XBrushes.Gray,
                                      new XRect(0, nuevaPagina.Height - 20, nuevaPagina.Width, 15), 
                                      XStringFormats.TopCenter);

                        gfx.Dispose();
                        paginasCopiadas++;
                    }
                    else
                    {
                        Console.WriteLine($"     ⚠️ ADVERTENCIA: Índice de página inválido: {indicePagina} (PDF tiene {documento.PageCount} páginas)");
                    }
                }

                // Validar que se haya copiado al menos una página
                if (primeraPaginaCopiada != null)
                {
                    // Crear marcador apuntando a la PRIMERA página copiada de esta hoja
                    var marcador = documentoFinal.Outlines.Add(hoja.tituloIndice, primeraPaginaCopiada, true);
                    marcadores.Add(marcador);

                    Console.WriteLine($"   ✅ Hoja procesada exitosamente:");
                    Console.WriteLine($"       - Páginas copiadas: {paginasCopiadas}");
                    Console.WriteLine($"       - Marcador creado: '{hoja.tituloIndice}'");
                    //Console.WriteLine($"       - Apunta a página en posición: {documentoFinal.Pages.IndexOf(primeraPaginaCopiada) + 1}");
                }
                else
                {
                    Console.WriteLine($"   ❌ ERROR: No se pudo copiar ninguna página de la hoja {hoja.nombre}");
                    // Agregar marcador de emergencia (opcional)
                    // var marcadorEmergencia = documentoFinal.Outlines.Add($"[ERROR] {hoja.tituloIndice}", documentoFinal.Pages[0], true);
                    // marcadores.Add(marcadorEmergencia);
                }

                Console.WriteLine(); // Línea en blanco para separar secciones
            }

            Console.WriteLine($"📊 Resumen final:");
            Console.WriteLine($"   - Total de hojas procesadas: {metadata.documento.hojas.Count}");
            Console.WriteLine($"   - Total de marcadores creados: {marcadores.Count}");
            Console.WriteLine($"   - Total de páginas en documento final: {documentoFinal.PageCount}");


            // 6. Actualizar enlaces en el índice
            ActualizarEnlacesIndiceMultipagina(paginaIndice, metadata, marcadores);

            // 7. Guardar documento final
            documentoFinal.Save(pdfFinal);
            documentoFinal.Close();
            documento.Close();

            Console.WriteLine($"💾 Documento guardado con {documentoFinal.PageCount} páginas");
        }

        private DocumentoMetadata ActualizarMetaDatos(DocumentoMetadata metadata, string pdfOriginal, string archivoMetadatos)
        {
            Dictionary<string, int> paginas = ContarPaginasPorHoja(pdfOriginal);

            int paginaActual = 0;
            // 1.1 Recorrer la lista de hojas en los metadatos y actualizarlas
            if (metadata?.documento.hojas != null) // Asegurarse de que Hojas no sea null
            {
                foreach (var hojaInfo in metadata.documento.hojas)
                {
                    // Buscar el identificador de la hoja en el diccionario de conteos del PDF
                    if (paginas.TryGetValue("CODHOJAEXCEL:" + hojaInfo.indice.ToString(), out int numPaginas))
                    {
                        hojaInfo.numeroPaginasPDF = numPaginas;
                        hojaInfo.paginaInicioPDF = paginaActual;
                        hojaInfo.paginaFinPDF = (paginaActual + numPaginas - 1);
                    }
                    else
                    {
                        // Si no se encuentra un conteo para esta hoja, podrías poner 0 o dejar un mensaje
                        hojaInfo.numeroPaginasPDF = 0;
                    }
                    paginaActual = paginaActual + numPaginas;
                }
                metadata.documento.totalPaginasPDF = paginaActual;
            }

            string updatedJson = Newtonsoft.Json.JsonConvert.SerializeObject(metadata, Formatting.Indented);
            File.WriteAllText(archivoMetadatos, updatedJson);

            return metadata;
        }

        private DocumentoMetadata CargarMetadatos(string archivo)
        {
            string json = File.ReadAllText(archivo);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentoMetadata>(json);
        }

        // Método actualizado para crear el índice con información de páginas múltiples
        private PdfPage CrearPaginaIndice(PdfDocument documento, DocumentoMetadata metadata)
        {
            var pagina = documento.AddPage();
            var gfx = XGraphics.FromPdfPage(pagina);

            // Configurar fuentes (igual que antes)
            var fuenteTitulo = new XFont("Arial", 20, XFontStyle.Bold);
            var fuenteSubtitulo = new XFont("Arial", 12, XFontStyle.Regular);
            var fuenteItem = new XFont("Arial", 11, XFontStyle.Regular);
            var fuenteEnlace = new XFont("Arial", 11, XFontStyle.Underline);

            double y = 80;
            double margenIzquierdo = 60;
            double anchoUtil = pagina.Width - 120;

            // Título principal
            gfx.DrawString("ÍNDICE DEL DOCUMENTO", fuenteTitulo, XBrushes.DarkBlue,
                          new XRect(0, y, pagina.Width, 30), XStringFormats.TopCenter);
            y += 50;

            // Información del documento
            gfx.DrawString($"Documento: {metadata.documento.titulo}", fuenteSubtitulo,
                          XBrushes.Black, margenIzquierdo, y);
            y += 20;
            gfx.DrawString($"Fecha: {metadata.documento.fechaCreacion}", fuenteSubtitulo,
                          XBrushes.Black, margenIzquierdo, y);
            y += 20;
            gfx.DrawString($"Total páginas: {metadata.documento.totalPaginasPDF}", fuenteSubtitulo,
                          XBrushes.Black, margenIzquierdo, y);
            y += 40;

            // Línea separadora
            gfx.DrawLine(XPens.Gray, margenIzquierdo, y, pagina.Width - margenIzquierdo, y);
            y += 30;

            // Encabezados de tabla
            gfx.DrawString("SECCIÓN", fuenteItem, XBrushes.White,
                          new XRect(margenIzquierdo, y, 180, 20), XStringFormats.CenterLeft);
            gfx.DrawString("DESCRIPCIÓN", fuenteItem, XBrushes.White,
                          new XRect(margenIzquierdo + 200, y, 180, 20), XStringFormats.CenterLeft);
            gfx.DrawString("PÁGINAS", fuenteItem, XBrushes.White,
                          new XRect(pagina.Width - 120, y, 80, 20), XStringFormats.Center);

            // Fondo para encabezados
            gfx.DrawRectangle(XBrushes.DarkBlue, margenIzquierdo - 5, y - 2,
                             anchoUtil + 10, 24);

            y += 35;

            // Items del índice con información de páginas múltiples
            int numeroPaginaIndice = 2; // Empezar en página 2 (la 1 es el índice)
            foreach (var hoja in metadata.documento.hojas)
            {
                if (y > pagina.Height - 100) break; // Evitar overflow de página

                // Fondo alternado para filas
                if ((numeroPaginaIndice % 2) == 0)
                {
                    gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(240, 240, 240)),
                                     margenIzquierdo - 5, y - 2, anchoUtil + 10, 22);
                }

                // Nombre de la sección (será enlace)
                gfx.DrawString(hoja.tituloIndice, fuenteEnlace, XBrushes.Blue,
                              new XRect(margenIzquierdo, y, 180, 20), XStringFormats.CenterLeft);

                // Descripción
                gfx.DrawString(hoja.descripcion, fuenteItem, XBrushes.Black,
                              new XRect(margenIzquierdo + 200, y, 180, 20), XStringFormats.CenterLeft);

                // Rango de páginas o página única
                string textoPaginas;
                if (hoja.numeroPaginasPDF == 1)
                {
                    textoPaginas = numeroPaginaIndice.ToString();
                }
                else
                {
                    textoPaginas = $"{numeroPaginaIndice}-{numeroPaginaIndice + hoja.numeroPaginasPDF - 1}";
                }

                gfx.DrawString(textoPaginas, fuenteItem, XBrushes.Black,
                              new XRect(pagina.Width - 120, y, 80, 20), XStringFormats.Center);

                y += 25;
                numeroPaginaIndice += hoja.numeroPaginasPDF;
            }

            // Instrucciones de uso actualizadas
            y += 30;
            gfx.DrawString("💡 Instrucciones:", new XFont("Arial", 10, XFontStyle.Bold),
                          XBrushes.DarkGreen, margenIzquierdo, y);
            y += 20;
            gfx.DrawString("• Haz clic en cualquier sección para navegar a su primera página",
                          new XFont("Arial", 9, XFontStyle.Regular), XBrushes.Black,
                          margenIzquierdo + 10, y);
            y += 15;
            gfx.DrawString("• Las secciones pueden ocupar múltiples páginas consecutivas",
                          new XFont("Arial", 9, XFontStyle.Regular), XBrushes.Black,
                          margenIzquierdo + 10, y);
            y += 15;
            gfx.DrawString("• Usa el panel de marcadores para navegación rápida",
                          new XFont("Arial", 9, XFontStyle.Regular), XBrushes.Black,
                          margenIzquierdo + 10, y);

            gfx.Dispose();
            return pagina;
        }

        private List<PdfPage> CrearIndiceMultipagina(PdfDocument documento, DocumentoMetadata metadata)
        {
            var paginasIndice = new List<PdfPage>();

            // Configurar fuentes
            var fuenteTitulo = new XFont("Arial", 18, XFontStyle.Bold);
            var fuenteSubtitulo = new XFont("Arial", 11, XFontStyle.Regular);
            var fuenteItem = new XFont("Arial", 10, XFontStyle.Regular);
            var fuenteEnlace = new XFont("Arial", 10, XFontStyle.Underline);

            double margenIzquierdo = 50;
            double margenSuperior = 60;
            double margenInferior = 60;
            double altoPagina = 0;
            double anchoUtil = 0;

            // Variables para controlar el contenido
            int itemsProcessados = 0;
            int paginasIndiceEstimadas = (int)Math.Ceiling((double)metadata.documento.hojas.Count / 26); // ~20 items por página

            int numeroPaginaIndice = 1;

            while (itemsProcessados < metadata.documento.hojas.Count)
            {
                // Crear nueva página de índice
                var pagina = documento.AddPage();
                paginasIndice.Add(pagina);
                var gfx = XGraphics.FromPdfPage(pagina);

                altoPagina = pagina.Height;
                anchoUtil = pagina.Width - (margenIzquierdo * 2);
                double y = margenSuperior;

                // Título (solo en la primera página)
                if (paginasIndice.Count == 1)
                {
                    gfx.DrawString("ÍNDICE DEL DOCUMENTO", fuenteTitulo, XBrushes.DarkBlue,
                                  new XRect(0, y, pagina.Width, 30), XStringFormats.TopCenter);
                    y += 40;

                    // Información del documento
                    gfx.DrawString($"Documento: {metadata.documento.titulo}", fuenteSubtitulo,
                                  XBrushes.Black, margenIzquierdo, y);
                    y += 18;
                    gfx.DrawString($"Fecha: {metadata.documento.fechaCreacion}", fuenteSubtitulo,
                                  XBrushes.Black, margenIzquierdo, y);
                    y += 18;
                    gfx.DrawString($"Total páginas: {metadata.documento.totalPaginasPDF + paginasIndiceEstimadas } | Secciones: {metadata.documento.hojas.Count}",
                                  fuenteSubtitulo, XBrushes.Black, margenIzquierdo, y);
                    y += 30;
                }
                else
                {
                    // Título para páginas siguientes
                    gfx.DrawString($"ÍNDICE (Página {numeroPaginaIndice})", fuenteTitulo, XBrushes.DarkBlue,
                                  new XRect(0, y, pagina.Width, 25), XStringFormats.TopCenter);
                    y += 35;
                }

                // Línea separadora
                gfx.DrawLine(XPens.Gray, margenIzquierdo, y, pagina.Width - margenIzquierdo, y);
                y += 20;

                // Encabezados de tabla
                double altoEncabezado = 22;
                gfx.DrawRectangle(XBrushes.DarkBlue, margenIzquierdo - 5, y - 2, anchoUtil + 10, altoEncabezado);

                gfx.DrawString("SECCIÓN", fuenteItem, XBrushes.White,
                              new XRect(margenIzquierdo, y + 2, 160, 18), XStringFormats.CenterLeft);
                //gfx.DrawString("DESCRIPCIÓN", fuenteItem, XBrushes.White,
                //              new XRect(margenIzquierdo + 180, y + 2, 200, 18), XStringFormats.CenterLeft);
                gfx.DrawString("PÁGINAS", fuenteItem, XBrushes.White,
                              new XRect(pagina.Width - 100, y + 2, 80, 18), XStringFormats.Center);

                y += altoEncabezado + 8;

                // Calcular cuántos items caben en esta página
                double espacioDisponible = altoPagina - y - margenInferior - 40; // 40 para instrucciones
                double altoItem = 20; // Altura de cada item
                int itemsQueCaben = (int)(espacioDisponible / altoItem);

                Console.WriteLine($"📄 Página {numeroPaginaIndice} del índice: {itemsQueCaben} items disponibles, {metadata.documento.hojas.Count - itemsProcessados} items restantes");

                // Procesar items para esta página
                int itemsEnEstaPagina = 0;

                for (int i = itemsProcessados; i < metadata.documento.hojas.Count && itemsEnEstaPagina < itemsQueCaben; i++)
                {
                    var hoja = metadata.documento.hojas[i];

                    // Fondo alternado para filas
                    if ((itemsEnEstaPagina % 2) == 1)
                    {
                        gfx.DrawRectangle(new XSolidBrush(XColor.FromArgb(245, 245, 245)),
                                         margenIzquierdo - 5, y - 2, anchoUtil + 10, altoItem);
                    }

                    // Nombre de la sección (enlace)
                    gfx.DrawString(hoja.tituloIndice, fuenteEnlace, XBrushes.Blue,
                                  new XRect(margenIzquierdo, y, 160, 18), XStringFormats.CenterLeft);

                    //// Descripción (truncar si es muy larga)
                    //string descripcionTruncada = hoja.descripcion;
                    //if (descripcionTruncada.Length > 35)
                    //{
                    //    descripcionTruncada = descripcionTruncada.Substring(0, 32) + "...";
                    //}

                    //gfx.DrawString(descripcionTruncada, fuenteItem, XBrushes.Black,
                    //              new XRect(margenIzquierdo + 180, y, 200, 18), XStringFormats.CenterLeft);

                    // Rango de páginas
                    string textoPaginas;
                    if (hoja.numeroPaginasPDF == 1)
                    {
                        textoPaginas = (hoja.paginaInicioPDF + paginasIndiceEstimadas + 1).ToString();
                    }
                    else
                    {
                        //textoPaginas = $"{numeroPagina}-{numeroPagina + hoja.numeroPaginasPDF - 1}";
                        // Mostrar rango real + offset del índice
                        int paginaInicioMostrar = hoja.paginaInicioPDF + paginasIndiceEstimadas + 1;
                        int paginaFinMostrar = hoja.paginaFinPDF + paginasIndiceEstimadas + 1;
                        textoPaginas = $"{paginaInicioMostrar}-{paginaFinMostrar}";
                    }

                    gfx.DrawString(textoPaginas, fuenteItem, XBrushes.Black,
                                  new XRect(pagina.Width - 100, y, 80, 18), XStringFormats.Center);

                    y += altoItem;
                    //numeroPagina += hoja.numeroPaginasPDF;
                    itemsEnEstaPagina++;
                    itemsProcessados++;
                }

                // Agregar instrucciones solo en la última página
                if (itemsProcessados >= metadata.documento.hojas.Count)
                {
                    y += 20;
                    gfx.DrawString("💡 Instrucciones:", new XFont("Arial", 9, XFontStyle.Bold),
                                  XBrushes.DarkGreen, margenIzquierdo, y);
                    y += 15;
                    gfx.DrawString("• Haz clic en cualquier sección para navegar a su primera página",
                                  new XFont("Arial", 8, XFontStyle.Regular), XBrushes.Black,
                                  margenIzquierdo + 10, y);
                    y += 12;
                    gfx.DrawString("• Las secciones pueden ocupar múltiples páginas consecutivas",
                                  new XFont("Arial", 8, XFontStyle.Regular), XBrushes.Black,
                                  margenIzquierdo + 10, y);
                    y += 12;
                    gfx.DrawString("• Usa el panel de marcadores para navegación rápida",
                                  new XFont("Arial", 8, XFontStyle.Regular), XBrushes.Black,
                                  margenIzquierdo + 10, y);
                }
                else
                {
                    // Indicador de continuación
                    gfx.DrawString($"... continúa en página siguiente ({metadata.documento.hojas.Count - itemsProcessados} secciones restantes)",
                                  new XFont("Arial", 9, XFontStyle.Italic), XBrushes.Gray,
                                  new XRect(0, altoPagina - 40, pagina.Width, 20), XStringFormats.TopCenter);
                }

                // Número de página del índice
                gfx.DrawString($"Página {numeroPaginaIndice}",
                              new XFont("Arial", 8, XFontStyle.Regular), XBrushes.Gray,
                              new XRect(0, altoPagina - 20, pagina.Width, 15), XStringFormats.TopCenter);

                gfx.Dispose();
                numeroPaginaIndice++;

            }

            Console.WriteLine($"📋 Índice creado con {paginasIndice.Count} páginas");
            Console.WriteLine($"📄 El contenido empezará en la página {paginasIndice.Count + 1 }");
            return paginasIndice;
        }

        private void ActualizarEnlacesIndice(PdfPage paginaIndice, DocumentoMetadata metadata,
                            List<PdfOutline> marcadores)
        {
            double y = 275;
            double margenIzquierdo = 60;
            int indice = 0;

            foreach (var hoja in metadata.documento.hojas)
            {
                if (y > paginaIndice.Height - 100 || indice >= marcadores.Count) break;

                try
                {
                    // Versión compatible con PdfSharp 1.50.x
                    var annotationDict = new PdfDictionary(paginaIndice.Owner);

                    // Agregar el diccionario al documento ANTES de configurarlo
                    paginaIndice.Owner.Internals.AddObject(annotationDict);

                    // Configurar como enlace
                    annotationDict.Elements["/Type"] = new PdfName("/Annot");
                    annotationDict.Elements["/Subtype"] = new PdfName("/Link");

                    // Área clicable
                    double left = margenIzquierdo;
                    double bottom = paginaIndice.Height.Point - y - 22;
                    double right = paginaIndice.Width.Point - 60;
                    double top = paginaIndice.Height.Point - y + 3;

                    var rectArray = new PdfArray(paginaIndice.Owner);
                    rectArray.Elements.Add(new PdfReal(left));
                    rectArray.Elements.Add(new PdfReal(bottom));
                    rectArray.Elements.Add(new PdfReal(right));
                    rectArray.Elements.Add(new PdfReal(top));
                    annotationDict.Elements["/Rect"] = rectArray;

                    // Acción de navegación
                    var actionDict = new PdfDictionary(paginaIndice.Owner);
                    paginaIndice.Owner.Internals.AddObject(actionDict);

                    actionDict.Elements["/S"] = new PdfName("/GoTo");

                    var destArray = new PdfArray(paginaIndice.Owner);
                    destArray.Elements.Add(marcadores[indice].DestinationPage.Reference);
                    destArray.Elements.Add(new PdfName("/Fit"));
                    actionDict.Elements["/D"] = destArray;

                    annotationDict.Elements["/A"] = actionDict;

                    // Inicializar array de anotaciones si no existe
                    if (paginaIndice.Elements["/Annots"] == null)
                    {
                        var annotsArray = new PdfArray(paginaIndice.Owner);
                        paginaIndice.Elements["/Annots"] = annotsArray;
                    }

                    // Agregar la anotación al array
                    var existingAnnots = (PdfArray)paginaIndice.Elements["/Annots"];
                    existingAnnots.Elements.Add(annotationDict.Reference);

                    Console.WriteLine($"✓ Enlace creado para: {hoja.tituloIndice}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error creando enlace para {hoja.tituloIndice}: {ex.Message}");
                    // Continuar con el siguiente elemento sin fallar
                }

                y += 25;
                indice++;
            }

            Console.WriteLine($"🔗 Total de enlaces clicables creados: {indice}");
        }


        // ALTERNATIVA MÁS SIMPLE Y VISUAL PARA DEBUGGING:
        private void ActualizarEnlacesIndiceMultipagina(List<PdfPage> paginasIndice, DocumentoMetadata metadata,
                                                 List<PdfOutline> marcadores)
        {
            // Esta versión añade rectángulos visibles para debug
            double margenIzquierdo = 50;
            double altoItem = 20;
            int itemsProcessados = 0;
            int indiceMarcador = 0;

            foreach (var paginaIndice in paginasIndice)
            {
                double y = (paginasIndice.IndexOf(paginaIndice) == 0) ? 215 : 143; // Posiciones calculadas
                double espacioDisponible = paginaIndice.Height - y - 100;
                int itemsQueCaben = (int)(espacioDisponible / altoItem);

                //Para debugging: dibujar rectángulos visibles
               //var gfx = XGraphics.FromPdfPage(paginaIndice);
                var penDebug = new XPen(XColors.Red, 1) { DashStyle = XDashStyle.Dash };

                for (int i = 0; i < itemsQueCaben && itemsProcessados < metadata.documento.hojas.Count; i++)
                {
                    var hoja = metadata.documento.hojas[itemsProcessados];

                    // Área clicable corregida
                    double left = margenIzquierdo - 5;
                    double right = paginaIndice.Width.Point - 50;
                    double top = y;
                    double bottom = y + altoItem;

                    // 🐛 DEBUGGING: Dibujar rectángulo visible (quitar después)
                    //gfx.DrawRectangle(penDebug, left, top, right - left, bottom - top);

                    // Crear enlace con coordenadas corregidas
                    try
                    {
                        var annotationDict = new PdfDictionary(paginaIndice.Owner);
                        paginaIndice.Owner.Internals.AddObject(annotationDict);

                        annotationDict.Elements["/Type"] = new PdfName("/Annot");
                        annotationDict.Elements["/Subtype"] = new PdfName("/Link");

                        // Convertir a coordenadas PDF
                        var rectArray = new PdfArray(paginaIndice.Owner);
                        rectArray.Elements.Add(new PdfReal(left));
                        rectArray.Elements.Add(new PdfReal(paginaIndice.Height.Point - bottom)); // bottom en PDF
                        rectArray.Elements.Add(new PdfReal(right));
                        rectArray.Elements.Add(new PdfReal(paginaIndice.Height.Point - top));   // top en PDF
                        annotationDict.Elements["/Rect"] = rectArray;

                        // Acción de navegación
                        var actionDict = new PdfDictionary(paginaIndice.Owner);
                        paginaIndice.Owner.Internals.AddObject(actionDict);

                        actionDict.Elements["/S"] = new PdfName("/GoTo");
                        var destArray = new PdfArray(paginaIndice.Owner);
                        destArray.Elements.Add(marcadores[indiceMarcador].DestinationPage.Reference);
                        destArray.Elements.Add(new PdfName("/Fit"));
                        actionDict.Elements["/D"] = destArray;
                        annotationDict.Elements["/A"] = actionDict;

                        if (paginaIndice.Elements["/Annots"] == null)
                        {
                            paginaIndice.Elements["/Annots"] = new PdfArray(paginaIndice.Owner);
                        }
                        ((PdfArray)paginaIndice.Elements["/Annots"]).Elements.Add(annotationDict.Reference);

                        Console.WriteLine($"✓ Enlace creado para {hoja.tituloIndice} en Y={y:F1}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Error: {ex.Message}");
                    }

                    y += altoItem;
                    itemsProcessados++;
                    indiceMarcador++;
                }

                //gfx.Dispose();
            }
        }


        public Dictionary<string, int> ContarPaginasPorHoja(string filePath)
        {
            
            // Un diccionario para almacenar el conteo de páginas por cada hoja de Excel
            var pageCounts = new Dictionary<string, int>();

            // Usamos un bloque 'using' para asegurar que el documento se cierre correctamente
            using (var document = UglyToad.PdfPig.PdfDocument.Open(filePath))
            {
                // Itera a través de cada página del PDF
                foreach (var page in document.GetPages())
                {
                    var hojaCodeWord = page.GetWords()
                                       .FirstOrDefault(word => word.Text.StartsWith("CODHOJAEXCEL", StringComparison.OrdinalIgnoreCase));
                    if (hojaCodeWord != null)
                    {
                        // Extraemos el texto completo de esa palabra, que debería ser nuestro identificador único
                        string sheetIdentifier = hojaCodeWord.Text;

                        // Incrementamos el contador para este identificador de hoja
                        if (pageCounts.ContainsKey(sheetIdentifier))
                        {
                            pageCounts[sheetIdentifier]++;
                        }
                        else
                        {
                            pageCounts[sheetIdentifier] = 1;
                        }
                    }
                }
            }
            return pageCounts;
        }
    }

    // Clase de extensión para facilitar el uso
    public static class PdfExtensions
    {
        public static void ProcesarDesdeExcel(string rutaArchivoBase, string rutaMetadatos = null)
        {
            if (string.IsNullOrEmpty(rutaMetadatos))
            {
                rutaMetadatos = rutaArchivoBase.Replace("_Base.pdf", "_Metadatos.json");
            }

            string rutaFinal = rutaArchivoBase.Replace("_Base.pdf", "_ConIndiceInteractivo.pdf");

            var procesador = new PdfIndexProcessor();
            procesador.ProcesarPdfConIndice(rutaArchivoBase, rutaMetadatos, rutaFinal);
        }
    }

    // Modelo para deserializar el JSON de Excel
    public class DocumentoMetadata
    {
        public DocumentoInfo documento { get; set; }
    }

    public class DocumentoInfo
    {
        public string titulo { get; set; }
        public string fechaCreacion { get; set; }
        public int totalHojas { get; set; }

        public int totalPaginasPDF { get; set; }  // NUEVO CAMPO
        public List<HojaInfo> hojas { get; set; }
    }

    public class HojaInfo
    {
        public int indice { get; set; }
        public string nombre { get; set; }
        public string tituloIndice { get; set; }
        public string descripcion { get; set; }
        public bool tieneContenido { get; set; }
        public int numeroFilas { get; set; }
        public int numeroColumnas { get; set; }

        // NUEVOS CAMPOS para páginas múltiples
        public int paginaInicioPDF { get; set; }
        public int paginaFinPDF { get; set; }
        public int numeroPaginasPDF { get; set; }
    }
}
