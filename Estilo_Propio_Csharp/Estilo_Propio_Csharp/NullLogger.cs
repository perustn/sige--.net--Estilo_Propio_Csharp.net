using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estilo_Propio_Csharp
{
    public class NullLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => null!;
        public bool IsEnabled(LogLevel logLevel) => false;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) { }
    }
}
