using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Estilo_Propio_Csharp
{
    public class MonitorLogBuffer
    {
        private readonly Workbook _workbook;
        private readonly ConfiguracionCeldaControlAvanzada _config;
        private volatile bool _monitoreando;
        private int _ultimoContador = 0;

        public event Action<LogEntry> NuevaEntradaLog;
        public event Action<string> EstadoCambiado;

        public MonitorLogBuffer(Workbook workbook, ConfiguracionCeldaControlAvanzada config)
        {
            _workbook = workbook;
            _config = config;
        }

        public async Task IniciarMonitoreoBuffer(CancellationToken cancellationToken = default)
        {
            _monitoreando = true;

           

            while (_monitoreando && !cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var hoja = _workbook.Sheets[_config.NombreHoja];

                    // Leer contador actual
                    var contadorActual = Convert.ToInt32(hoja.Range["A4"].Value ?? 0);

                    // Si hay nueva entrada
                    if (contadorActual > _ultimoContador)
                    {
                        // Leer desde celda buffer
                        var bufferEntrada = hoja.Range["A5"].Value?.ToString();

                        if (!string.IsNullOrEmpty(bufferEntrada))
                        {
                            var entrada = ParsearEntrada(bufferEntrada);
                            if (entrada != null)
                            {
                                NuevaEntradaLog?.Invoke(entrada);
                            }
                        }

                        _ultimoContador = contadorActual;
                    }

                    // Verificar finalización
                    var estado = hoja.Range[_config.CeldaControl].Value?.ToString() ?? "";
                    if (estado == "SUCCESS" || estado.StartsWith("ERROR"))
                    {
                        // Esperar un poco para logs finales
                        await Task.Delay(300, cancellationToken);

                        var contadorFinal = Convert.ToInt32(hoja.Range["A4"].Value ?? 0);
                        if (contadorFinal > _ultimoContador)
                        {
                            var bufferFinal = hoja.Range["A5"].Value?.ToString();
                            if (!string.IsNullOrEmpty(bufferFinal))
                            {
                                var entradaFinal = ParsearEntrada(bufferFinal);
                                if (entradaFinal != null)
                                {
                                    NuevaEntradaLog?.Invoke(entradaFinal);
                                }
                            }
                        }

                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en monitoreo: {ex.Message}");
                }

                await Task.Delay(200, cancellationToken); // Polling más frecuente
            }

            _monitoreando = false;
        }

        private LogEntry ParsearEntrada(string entrada)
        {
            var partes = entrada.Split('|');
            if (partes.Length >= 4)
            {
                return new LogEntry
                {
                    Timestamp = partes[0],
                    Modulo = partes[1],
                    Evento = partes[2],
                    Tipo = partes[3]
                };
            }
            return null;
        }
        public void Detener()
        {
            _monitoreando = false;
        }
    }
}
