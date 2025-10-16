using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Text;
using System.Globalization;

namespace Estilo_Propio_Csharp
{

    public class ConfiguracionLogCSV
    {
        public string RutaArchivoLog { get; set; }
        public string CeldaControl { get; set; } = "A1";
        public string NombreHoja { get; set; } = "Control";
        public bool MonitoreoTiempoReal { get; set; } = true;
        public int IntervaloMonitoreoMs { get; set; } = 100; // Más rápido porque no usa COM
        public bool CrearArchivoSiNoExiste { get; set; } = true;
        public bool AgregarTimestampAlNombre { get; set; } = true;
        public string FormatoTimestamp { get; set; } = "yyyy-MM-dd_HH-mm-ss";
        public bool LimpiarLogAnterior { get; set; } = true;

        // Filtros de log
        public List<LogLevelExcel> NivelesPermitidos { get; set; } = new List<LogLevelExcel>
    {
        LogLevelExcel.Error, LogLevelExcel.Warning, LogLevelExcel.Info
    };

        // Configuración de archivos
        public int MaxTamañoArchivoMB { get; set; } = 10;
        public bool RotarArchivos { get; set; } = true;
    }

    // Entrada de log mejorada
    public class LogEntryCSV
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Modulo { get; set; }
        public string Evento { get; set; }
        public LogLevelExcel Nivel { get; set; }
        public string Detalles { get; set; }
        public long Contador { get; set; }

        public string ToCSVString()
        {
            return $"\"{Timestamp:yyyy-MM-dd HH:mm:ss.fff}\",\"{Modulo}\",\"{Evento}\",\"{Nivel}\",\"{EscapeCSV(Detalles)}\",{Contador}";
        }

        public static LogEntryCSV FromCSVString(string csvLine)
        {
            try
            {
                var campos = ParseCSVLine(csvLine);
                if (campos.Length >= 6)
                {
                    return new LogEntryCSV
                    {
                        Timestamp = DateTime.ParseExact(campos[0], "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        Modulo = campos[1],
                        Evento = campos[2],
                        Nivel = Enum.TryParse<LogLevelExcel>(campos[3], out var nivel) ? nivel : LogLevelExcel.Info,
                        Detalles = campos[4],
                        Contador = long.TryParse(campos[5], out var contador) ? contador : 0
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parseando línea CSV: {ex.Message}");
            }
            return null;
        }

        private static string EscapeCSV(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("\"", "\"\"").Replace("\r", "\\r").Replace("\n", "\\n");
        }

        private static string[] ParseCSVLine(string line)
        {
            var result = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++; // Skip next quote
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            result.Add(current.ToString());
            return result.ToArray();
        }

        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss.fff}] {Nivel}: {Modulo} - {Evento}";
        }
    }

    // Writer CSV thread-safe
    public class CSVLogWriter : IDisposable
    {
        private readonly string _rutaArchivo;
        private readonly object _lockObject = new object();
        private readonly ConfiguracionLogCSV _config;
        private long _contadorEntradas = 0;
        private FileStream _fileStream;
        private StreamWriter _writer;
        private bool _disposed = false;

        public string RutaArchivoActual { get; private set; }

        public CSVLogWriter(ConfiguracionLogCSV config)
        {
            _config = config;
            InicializarArchivo();
        }

        private void InicializarArchivo()
        {
            try
            {
                // Generar nombre de archivo con timestamp si está configurado
                var rutaBase = _config.RutaArchivoLog;
                if (_config.AgregarTimestampAlNombre)
                {
                    var directorio = Path.GetDirectoryName(rutaBase);
                    var nombreSinExt = Path.GetFileNameWithoutExtension(rutaBase);
                    var extension = Path.GetExtension(rutaBase);
                    var timestamp = DateTime.Now.ToString(_config.FormatoTimestamp);

                    RutaArchivoActual = Path.Combine(directorio ?? "", $"{nombreSinExt}_{timestamp}{extension}");
                }
                else
                {
                    RutaArchivoActual = rutaBase;
                }

                // Crear directorio si no existe
                var dir = Path.GetDirectoryName(RutaArchivoActual);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // Limpiar archivo anterior si está configurado
                if (_config.LimpiarLogAnterior && File.Exists(RutaArchivoActual))
                {
                    File.Delete(RutaArchivoActual);
                }


                // Abrir archivo para escritura
                _fileStream = new FileStream(RutaArchivoActual, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                //_writer = new StreamWriter(_fileStream, Encoding.UTF8);


                // Forma clásica
                if (_fileStream.Length == 0) {

                    using (StreamWriter writer = new StreamWriter(_fileStream, Encoding.UTF8))
                    {
                        writer.WriteLine("\"Timestamp\",\"Modulo\",\"Evento\",\"Nivel\",\"Detalles\",\"Contador\"");
                        writer.Flush();
                    }
                }
               


                //// Escribir header si es un archivo nuevo
                //if (_fileStream.Length == 0)
                //{
                //    _writer.WriteLine("\"Timestamp\",\"Modulo\",\"Evento\",\"Nivel\",\"Detalles\",\"Contador\"");
                //    _writer.Flush();
                //}

                Console.WriteLine($"📄 Log CSV inicializado: {RutaArchivoActual}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"No se pudo inicializar el archivo de log CSV: {ex.Message}", ex);
            }
        }

        public void EscribirEntrada(LogEntryCSV entrada)
        {
            if (_disposed) return;

            lock (_lockObject)
            {
                try
                {
                    // Verificar si necesita filtrar por nivel
                    if (!_config.NivelesPermitidos.Contains(entrada.Nivel))
                    {
                        return;
                    }

                    // Asignar contador secuencial
                    entrada.Contador = Interlocked.Increment(ref _contadorEntradas);

                    // Escribir al archivo
                    _writer.WriteLine(entrada.ToCSVString());
                    _writer.Flush(); // Asegurar que se escriba inmediatamente

                    // Verificar rotación de archivo si está habilitada
                    if (_config.RotarArchivos && _fileStream.Length > _config.MaxTamañoArchivoMB * 1024 * 1024)
                    {
                        RotarArchivo();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error escribiendo al log CSV: {ex.Message}");
                }
            }
        }

        private void RotarArchivo()
        {
            try
            {
                _writer?.Close();
                _fileStream?.Close();

                // Renombrar archivo actual
                var archivoRotado = RutaArchivoActual.Replace(".csv", $"_rotado_{DateTime.Now:HHmmss}.csv");
                File.Move(RutaArchivoActual, archivoRotado);

                // Crear nuevo archivo
                InicializarArchivo();

                Console.WriteLine($"🔄 Archivo de log rotado: {archivoRotado}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error rotando archivo de log: {ex.Message}");
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                lock (_lockObject)
                {
                    _writer?.Close();
                    _fileStream?.Close();
                    _disposed = true;
                }
            }
        }
    }

    // Monitor CSV que lee el archivo en tiempo real
    public class MonitorLogCSV : IDisposable
    {
        private readonly string _rutaArchivoLog;
        private readonly ConfiguracionLogCSV _config;
        private volatile bool _monitoreando;
        private long _ultimaLinea = 0;
        private readonly Workbook _workbook;

        public event Action<LogEntryCSV> NuevaEntradaLog;
        public event Action<string> EstadoCambiado;

        public MonitorLogCSV(Workbook workbook, ConfiguracionLogCSV config)
        {
            _workbook = workbook;
            _config = config;
            _rutaArchivoLog = config.RutaArchivoLog;
        }

        public async Task IniciarMonitoreo(CancellationToken cancellationToken = default)
        {
            _monitoreando = true;

            // Determinar archivo actual si tiene timestamp
            var archivoAMonitorear = DeterminarArchivoLog();

            try
            {
                while (_monitoreando && !cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        // Leer nuevas entradas del CSV
                        await LeerNuevasEntradasCSV(archivoAMonitorear);

                        // Verificar estado en Excel (solo la celda de control)
                        //await VerificarEstadoExcel();

                        await Task.Delay(_config.IntervaloMonitoreoMs, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Error en monitoreo CSV: {ex.Message}");
                        await Task.Delay(_config.IntervaloMonitoreoMs * 2, cancellationToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("🛑 Monitoreo CSV cancelado");
            }
            finally
            {
                _monitoreando = false;
            }
        }

        private string DeterminarArchivoLog()
        {
            if (!_config.AgregarTimestampAlNombre)
            {
                return _config.RutaArchivoLog;
            }

            // Buscar el archivo más reciente con el patrón
            var directorio = Path.GetDirectoryName(_config.RutaArchivoLog);
            var nombreBase = Path.GetFileNameWithoutExtension(_config.RutaArchivoLog);
            var extension = Path.GetExtension(_config.RutaArchivoLog);

            if (string.IsNullOrEmpty(directorio)) directorio = ".";

            var archivos = Directory.GetFiles(directorio, $"{nombreBase}_*{extension}")
                                   .OrderByDescending(File.GetCreationTime)
                                   .FirstOrDefault();

            return archivos ?? _config.RutaArchivoLog;
        }

        private async Task LeerNuevasEntradasCSV(string rutaArchivo)
        {
            if (!File.Exists(rutaArchivo)) return;

            try
            {
                using (var fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(fileStream,Encoding.UTF8))
                {
                    var lineas = new List<string>();
                    string linea;
                    long numeroLinea = 0;

                    // Leer todas las líneas
                    while ((linea = reader.ReadLine()) != null)
                    {
                        numeroLinea++;
                        if (numeroLinea > _ultimaLinea && numeroLinea > 1) // Saltar header
                        {
                            lineas.Add(linea);
                        }
                    }

                    // Procesar nuevas líneas
                    foreach (var nuevaLinea in lineas)
                    {
                        var entrada = LogEntryCSV.FromCSVString(nuevaLinea);
                        if (entrada != null)
                        {
                            NuevaEntradaLog?.Invoke(entrada);
                        }
                    }

                    _ultimaLinea = numeroLinea;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error leyendo archivo CSV: {ex.Message}");
            }
        }

        private async Task VerificarEstadoExcel()
        {
            try
            {
                // Solo leer la celda de control, sin bloquear Excel
                var hoja = _workbook.Sheets[_config.NombreHoja];
                var estado = hoja.Range[_config.CeldaControl].Value?.ToString() ?? "";

                EstadoCambiado?.Invoke(estado);

                // Detener si terminó
                if (estado == "SUCCESS" || estado.StartsWith("ERROR"))
                {
                    await Task.Delay(1000); // Dar tiempo para logs finales
                    _monitoreando = false;
                }

                Marshal.ReleaseComObject(hoja);
            }
            catch (Exception ex)
            {
                // No mostrar errores COM aquí para no saturar la consola
                if (!(ex is COMException comEx && comEx.HResult == unchecked((int)0x8001010A)))
                {
                    Console.WriteLine($"⚠️ Error verificando estado: {ex.Message}");
                }
            }
        }

        public void Detener()
        {
            _monitoreando = false;
        }

        public void Dispose()
        {
            Detener();
        }
    }
}
