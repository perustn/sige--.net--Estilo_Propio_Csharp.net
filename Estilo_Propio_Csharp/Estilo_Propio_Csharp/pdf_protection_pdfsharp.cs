using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Security;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Estilo_Propio_Csharp;
using System.Windows.Forms;

namespace PDFProtectionPdfSharp
{
    /// <summary>
    /// Configuración para la protección de PDFs
    /// </summary>
    public class PdfProtectionConfig
    {
        [Required]
        public string InputPath { get; set; } = string.Empty;
        
        [Required]
        public string OutputPath { get; set; } = string.Empty;
        
        public string UserPassword { get; set; } = string.Empty;
        
        [Required]
        [MinLength(8, ErrorMessage = "La contraseña del propietario debe tener al menos 8 caracteres")]
        public string OwnerPassword { get; set; } = string.Empty;
        
        public PdfPermissions Permissions { get; set; } = new();
        
        public bool OverwriteIfExists { get; set; } = false;
        
        public bool CreateBackup { get; set; } = true;
    }

    /// <summary>
    /// Configuración de permisos del PDF
    /// </summary>
    public class PdfPermissions
    {
        public bool AllowPrint { get; set; } = false;
        public bool AllowHighQualityPrint { get; set; } = false;
        public bool AllowExtractContent { get; set; } = true;
        public bool AllowModifyDocument { get; set; } = false;
        public bool AllowAnnotations { get; set; } = true;
        public bool AllowFormsFill { get; set; } = true;
        public bool AllowAccessibilityExtractContent { get; set; } = true;
        public bool AllowAssembleDocument { get; set; } = false;

        /// <summary>
        /// Configuración preestablecida: Solo lectura estricta
        /// </summary>
        public static PdfPermissions StrictReadOnly => new()
        {
            AllowPrint = false,
            AllowHighQualityPrint = false,
            AllowExtractContent = false,
            AllowModifyDocument = false,
            AllowAnnotations = false,
            AllowFormsFill = false,
            AllowAccessibilityExtractContent = true,
            AllowAssembleDocument = false
        };

        /// <summary>
        /// Configuración preestablecida: Sin impresión pero con otras libertades
        /// </summary>
        public static PdfPermissions NoPrintAllowCopy => new()
        {
            AllowPrint = false,
            AllowHighQualityPrint = false,
            AllowExtractContent = true,
            AllowModifyDocument = false,
            AllowAnnotations = true,
            AllowFormsFill = true,
            AllowAccessibilityExtractContent = true,
            AllowAssembleDocument = false
        };
    }

    /// <summary>
    /// Resultado de la operación de protección
    /// </summary>
    public class PdfProtectionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? OutputPath { get; set; }
        public string? BackupPath { get; set; }
        public Exception? Exception { get; set; }
        public TimeSpan ProcessingTime { get; set; }
        public long OriginalFileSize { get; set; }
        public long ProtectedFileSize { get; set; }
    }

    /// <summary>
    /// Información de seguridad de un PDF
    /// </summary>
    public class PdfSecurityInfo
    {
        public bool HasSecurity { get; set; }
        public bool HasUserPassword { get; set; }
        public bool HasOwnerPassword { get; set; }
        public PdfPermissions CurrentPermissions { get; set; } = new();
        public string FilePath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public int PageCount { get; set; }
        public string PdfVersion { get; set; } = string.Empty;
    }

    /// <summary>
    /// Excepción personalizada para operaciones de PDF
    /// </summary>
    public class PdfProtectionException : Exception
    {
        public string? FilePath { get; }
        
        public PdfProtectionException(string message) : base(message) { }
        
        public PdfProtectionException(string message, Exception innerException) : base(message, innerException) { }
        
        public PdfProtectionException(string message, string filePath) : base(message)
        {
            FilePath = filePath;
        }
        
        public PdfProtectionException(string message, string filePath, Exception innerException) : base(message, innerException)
        {
            FilePath = filePath;
        }
    }

    /// <summary>
    /// Servicio robusto para protección de PDFs usando PdfSharp
    /// </summary>
    public class PdfProtectionService : IDisposable
    {
        private readonly ILogger<PdfProtectionService> _logger;
        private bool _disposed = false;

        public PdfProtectionService(ILogger<PdfProtectionService>? logger = null)
        {
            _logger = logger ?? new NullLogger<PdfProtectionService>();
        }

        private static ILogger<PdfProtectionService> CreateDefaultLogger()
        {
            using var loggerFactory = LoggerFactory.Create(builder => 
                builder.AddConsole().SetMinimumLevel(LogLevel.Information));
            return loggerFactory.CreateLogger<PdfProtectionService>();
        }

        /// <summary>
        /// Protege un PDF de forma asíncrona con configuración completa
        /// </summary>
        public async Task<PdfProtectionResult> ProtectPdfAsync(PdfProtectionConfig config)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(PdfProtectionService));

            var startTime = DateTime.UtcNow;
            var result = new PdfProtectionResult();

            try
            {
                // Validar configuración
                ValidateConfiguration(config);

                _logger.LogInformation("Iniciando protección de PDF: {InputPath}", config.InputPath);

                // Verificar archivos
                await ValidateFilesAsync(config);

                // Crear backup si se solicita
                if (config.CreateBackup)
                {
                    result.BackupPath = await CreateBackupAsync(config.InputPath);
                    _logger.LogInformation("Backup creado: {BackupPath}", result.BackupPath);
                }

                // Procesar PDF
                await ProcessPdfProtectionAsync(config, result);

                result.Success = true;
                result.Message = "PDF protegido exitosamente";
                result.OutputPath = config.OutputPath;

                _logger.LogInformation("PDF protegido exitosamente: {OutputPath}", config.OutputPath);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Exception = ex;
                result.Message = ex.Message;
                
                _logger.LogError(ex, "Error al proteger PDF: {InputPath}", config.InputPath);
                
                // Limpiar archivo parcial si existe
                await CleanupOnError(config.OutputPath);
            }
            finally
            {
                result.ProcessingTime = DateTime.UtcNow - startTime;
            }

            return result;
        }

        /// <summary>
        /// Procesa la protección del PDF
        /// </summary>
        private async Task ProcessPdfProtectionAsync(PdfProtectionConfig config, PdfProtectionResult result)
        {
            await Task.Run(() =>
            {
                result.OriginalFileSize = new FileInfo(config.InputPath).Length;

                using var document = PdfReader.Open(config.InputPath, PdfDocumentOpenMode.Modify);
                
                ConfigureSecurity(document, config);
                
                // Asegurar que el directorio de salida existe
                var outputDir = Path.GetDirectoryName(config.OutputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                document.Save(config.OutputPath);
                
                result.ProtectedFileSize = new FileInfo(config.OutputPath).Length;
                
                _logger.LogDebug("Archivo original: {OriginalSize} bytes, Protegido: {ProtectedSize} bytes", 
                    result.OriginalFileSize, result.ProtectedFileSize);
            });
        }

        /// <summary>
        /// Configura la seguridad del documento
        /// </summary>
        private void ConfigureSecurity(PdfDocument document, PdfProtectionConfig config)
        {
            var security = document.SecuritySettings;

            // Configurar contraseñas
            security.UserPassword = config.UserPassword;
            security.OwnerPassword = config.OwnerPassword;

            // Aplicar permisos
            var permissions = config.Permissions;
            security.PermitPrint = permissions.AllowPrint;
            //security.PermitHighQualityPrint = permissions.AllowHighQualityPrint;
            security.PermitExtractContent = permissions.AllowExtractContent;
            security.PermitModifyDocument = permissions.AllowModifyDocument;
            security.PermitAnnotations = permissions.AllowAnnotations;
            security.PermitFormsFill = permissions.AllowFormsFill;
            security.PermitAccessibilityExtractContent = permissions.AllowAccessibilityExtractContent;
            security.PermitAssembleDocument = permissions.AllowAssembleDocument;

            _logger.LogDebug("Configuración de seguridad aplicada: Print={AllowPrint}, Extract={AllowExtract}", 
                permissions.AllowPrint, permissions.AllowExtractContent);
        }

        /// <summary>
        /// Obtiene información de seguridad de un PDF
        /// </summary>
        public async Task<PdfSecurityInfo> GetSecurityInfoAsync(string pdfPath)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(PdfProtectionService));

            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"Archivo no encontrado: {pdfPath}");

            return await Task.Run(() =>
            {
                var info = new PdfSecurityInfo
                {
                    FilePath = pdfPath,
                    FileSize = new FileInfo(pdfPath).Length
                };

                try
                {
                    using var document = PdfReader.Open(pdfPath, PdfDocumentOpenMode.ReadOnly);
                    
                    info.PageCount = document.PageCount;
                    info.PdfVersion = document.Version.ToString();

                    if (document.SecuritySettings != null)
                    {
                        var security = document.SecuritySettings;
                        
                        info.HasSecurity = true;
                        info.HasUserPassword = false; //!string.IsNullOrEmpty(security.UserPassword);
                        info.HasOwnerPassword = false; //!string.IsNullOrEmpty(security.OwnerPassword);

                        info.CurrentPermissions = new PdfPermissions
                        {
                            AllowPrint = security.PermitPrint,
                            //AllowHighQualityPrint = security.PermitHighQualityPrint,
                            AllowHighQualityPrint = security.PermitPrint,
                            AllowExtractContent = security.PermitExtractContent,
                            AllowModifyDocument = security.PermitModifyDocument,
                            AllowAnnotations = security.PermitAnnotations,
                            AllowFormsFill = security.PermitFormsFill,
                            AllowAccessibilityExtractContent = security.PermitAccessibilityExtractContent,
                            AllowAssembleDocument = security.PermitAssembleDocument
                        };
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error al leer información de seguridad del PDF: {Path}", pdfPath);
                }

                return info;
            });
        }

        /// <summary>
        /// Genera una contraseña segura
        /// </summary>
        public static string GenerateSecurePassword(int length = 16)
        {
            if (length < 8)
                throw new ArgumentException("La longitud mínima de la contraseña debe ser 8 caracteres");

            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            var password = new StringBuilder();
            
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            foreach (var b in randomBytes)
            {
                password.Append(validChars[b % validChars.Length]);
            }

            return password.ToString();
        }

        #region Métodos de Validación y Utilidades

        private void ValidateConfiguration(PdfProtectionConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (string.IsNullOrWhiteSpace(config.InputPath))
                throw new ArgumentException("La ruta de entrada no puede estar vacía", nameof(config.InputPath));

            if (string.IsNullOrWhiteSpace(config.OutputPath))
                throw new ArgumentException("La ruta de salida no puede estar vacía", nameof(config.OutputPath));

            if (string.IsNullOrWhiteSpace(config.OwnerPassword) || config.OwnerPassword.Length < 8)
                throw new ArgumentException("La contraseña del propietario debe tener al menos 8 caracteres", nameof(config.OwnerPassword));

            //// Validar que no sean el mismo archivo
            //if (Path.GetFullPath(config.InputPath).Equals(Path.GetFullPath(config.OutputPath), StringComparison.OrdinalIgnoreCase))
            //    throw new ArgumentException("La ruta de entrada y salida no pueden ser el mismo archivo");
        }

        private async Task ValidateFilesAsync(PdfProtectionConfig config)
        {
            if (!File.Exists(config.InputPath))
                throw new FileNotFoundException($"El archivo de entrada no existe: {config.InputPath}");

            // Verificar que es un PDF válido
            await Task.Run(() =>
            {
                try
                {
                    using var testDocument = PdfReader.Open(config.InputPath, PdfDocumentOpenMode.ReadOnly);
                    if (testDocument.PageCount == 0)
                        throw new PdfProtectionException("El PDF no contiene páginas", config.InputPath);
                }
                catch (Exception ex) when (!(ex is PdfProtectionException))
                {
                    throw new PdfProtectionException($"El archivo no es un PDF válido: {ex.Message}", config.InputPath, ex);
                }
            });

            // Verificar permisos de escritura en el directorio de salida
            var outputDir = Path.GetDirectoryName(config.OutputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                try
                {
                    Directory.CreateDirectory(outputDir);
                }
                catch (Exception ex)
                {
                    throw new PdfProtectionException($"No se puede crear el directorio de salida: {ex.Message}", outputDir, ex);
                }
            }

            // Verificar si el archivo de salida ya existe
            if (File.Exists(config.OutputPath) && !config.OverwriteIfExists)
                throw new PdfProtectionException($"El archivo de salida ya existe: {config.OutputPath}", config.OutputPath);
        }

        private async Task<string> CreateBackupAsync(string filePath)
        {
            var backupPath = $"{filePath}.backup_{DateTime.Now:yyyyMMdd_HHmmss}";
            await Task.Run(() => File.Copy(filePath, backupPath));
            return backupPath;
        }

        private async Task CleanupOnError(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    await Task.Run(() => File.Delete(filePath));
                    _logger.LogDebug("Archivo parcial eliminado: {FilePath}", filePath);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "No se pudo eliminar el archivo parcial: {FilePath}", filePath);
                }
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Limpiar recursos manejados si es necesario
                    _logger.LogDebug("PdfProtectionService disposed");
                }
                _disposed = true;
            }
        }

        #endregion

        public async Task<PdfProtectionResult> UnprotectPdfAsync(string inputPath, string outputPath, string ownerPassword, bool overwrite = false)
        {
            var result = new PdfProtectionResult { Success = false };

            try
            {
                await Task.Run(() =>
                {
                    // 1. Abrimos para modificar
                    using (var document = PdfReader.Open(inputPath, ownerPassword, PdfDocumentOpenMode.Modify))
                    {
                        // 2. Limpiamos las contraseñas
                        document.SecuritySettings.UserPassword = "";
                        document.SecuritySettings.OwnerPassword = "";

                        // 3. ESTA ES LA LÍNEA CRUCIAL: 
                        // Al asignar una contraseña vacía y LUEGO poner el nivel en None,
                        // forzamos a PdfSharp a liberar el motor de cifrado.
                        document.SecuritySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.None;

                        // 4. Forzamos los permisos a "Abierto"
                        document.SecuritySettings.PermitPrint = true;
                        document.SecuritySettings.PermitFullQualityPrint = true;
                        document.SecuritySettings.PermitModifyDocument = true;
                        document.SecuritySettings.PermitExtractContent = true;
                        document.SecuritySettings.PermitAccessibilityExtractContent = true;
                        document.SecuritySettings.PermitAnnotations = true;
                        document.SecuritySettings.PermitFormsFill = true;
                        document.SecuritySettings.PermitAssembleDocument = true;

                        // 5. Guardamos (puede ser en la misma ruta si overwrite es true)
                        document.Save(outputPath);
                    }
                });

                result.Success = true;
                result.OutputPath = outputPath;
                result.Message = "Seguridad removida exitosamente.";
            }
            catch (Exception ex)
            {
                result.Message = $"Error: {ex.Message}";
            }

            return result;
        }
    }

    /// <summary>
    /// Programa de ejemplo que demuestra el uso de la clase mejorada
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            // Configurar logging
            using var loggerFactory = LoggerFactory.Create(builder =>
                builder.AddConsole().SetMinimumLevel(LogLevel.Information));
            var logger = loggerFactory.CreateLogger<Program>();

            try
            {
                logger.LogInformation("=== Protección Robusta de PDF con PdfSharp ===");

                using var pdfService = new PdfProtectionService(loggerFactory.CreateLogger<PdfProtectionService>());

                // Ejemplo 1: Proteger contra impresión
                await ProtectAgainstPrinting(pdfService, logger);

                // Ejemplo 2: Crear PDF de solo lectura
                await CreateReadOnlyPdf(pdfService, logger);

                // Ejemplo 3: Analizar seguridad de archivos
                await AnalyzePdfSecurity(pdfService, logger);

                logger.LogInformation("✅ Todas las operaciones completadas exitosamente!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Error en el programa principal");
            }

            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        private static async Task ProtectAgainstPrinting(PdfProtectionService service, ILogger logger)
        {
            logger.LogInformation("\n1. Protegiendo PDF contra impresión...");

            var config = new PdfProtectionConfig
            {
                InputPath = @"C:\temp\documento.pdf",
                OutputPath = @"C:\temp\documento_sin_impresion.pdf",
                OwnerPassword = PdfProtectionService.GenerateSecurePassword(),
                Permissions = PdfPermissions.NoPrintAllowCopy,
                OverwriteIfExists = true,
                CreateBackup = true
            };

            var result = await service.ProtectPdfAsync(config);
            
            if (result.Success)
            {
                logger.LogInformation("✅ PDF protegido en: {OutputPath}", result.OutputPath);
                logger.LogInformation("📁 Backup creado en: {BackupPath}", result.BackupPath);
                logger.LogInformation("⏱️ Tiempo de procesamiento: {ProcessingTime:F2}s", result.ProcessingTime.TotalSeconds);
            }
            else
            {
                logger.LogError("❌ Error: {Message}", result.Message);
            }
        }

        private static async Task CreateReadOnlyPdf(PdfProtectionService service, ILogger logger)
        {
            logger.LogInformation("\n2. Creando PDF de solo lectura...");

            var config = new PdfProtectionConfig
            {
                InputPath = @"C:\temp\documento.pdf",
                OutputPath = @"C:\temp\documento_solo_lectura.pdf",
                OwnerPassword = PdfProtectionService.GenerateSecurePassword(),
                Permissions = PdfPermissions.StrictReadOnly,
                OverwriteIfExists = true
            };

            var result = await service.ProtectPdfAsync(config);
            
            if (result.Success)
            {
                logger.LogInformation("✅ PDF de solo lectura creado: {OutputPath}", result.OutputPath);
            }
            else
            {
                logger.LogError("❌ Error: {Message}", result.Message);
            }
        }

        private static async Task AnalyzePdfSecurity(PdfProtectionService service, ILogger logger)
        {
            logger.LogInformation("\n3. Analizando seguridad de PDFs...");

            var files = new[]
            {
                @"C:\temp\documento.pdf",
                @"C:\temp\documento_sin_impresion.pdf",
                @"C:\temp\documento_solo_lectura.pdf"
            };

            foreach (var file in files)
            {
                try
                {
                    var info = await service.GetSecurityInfoAsync(file);
                    
                    logger.LogInformation("\n--- Análisis: {FileName} ---", Path.GetFileName(file));
                    logger.LogInformation("📄 Páginas: {PageCount} | Tamaño: {FileSize:N0} bytes", info.PageCount, info.FileSize);
                    logger.LogInformation("🔒 Tiene seguridad: {HasSecurity}", info.HasSecurity);
                    
                    if (info.HasSecurity)
                    {
                        var perms = info.CurrentPermissions;
                        logger.LogInformation("🖨️ Puede imprimir: {AllowPrint}", perms.AllowPrint);
                        logger.LogInformation("📋 Puede copiar: {AllowExtract}", perms.AllowExtractContent);
                        logger.LogInformation("✏️ Puede modificar: {AllowModify}", perms.AllowModifyDocument);
                    }
                }
                catch (FileNotFoundException)
                {
                    logger.LogWarning("⚠️ Archivo no encontrado: {File}", file);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "❌ Error analizando {File}", file);
                }
            }
        }      
    }
}