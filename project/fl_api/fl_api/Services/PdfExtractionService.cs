using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;
using System.Text.Json;

namespace fl_api.Services
{
    public class PdfExtractionService : IPdfExtractionService
    {
        private readonly PythonConfigRoutes _routes;
        private readonly IMongoDbService _mongo;

        public PdfExtractionService(
            IOptions<PythonConfigRoutes> options,
            IMongoDbService mongo)
        {
            _routes = options.Value;
            _mongo = mongo;
        }

        public async Task<JsonDocument> ExtractJsonAsync(Guid documentId)
        {
            // 1) Obtener metadata del PDF
            var col = _mongo.GetCollection<DocumentRecord>("Documents");
            var doc = await col.Find(d => d.Id == documentId).FirstOrDefaultAsync();
            if (doc == null)
                throw new FileNotFoundException($"Document {documentId} not found in database.");

            // 2) Ruta completa al PDF en PermanentStorage
            var perm = Path.IsPathRooted(_routes.PermanentFolder)
                ? _routes.PermanentFolder
                : Path.Combine(_routes.BasePath, _routes.PermanentFolder);
            var pdfPath = Path.Combine(perm, doc.StoredFileName);
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF not found at {pdfPath}");

            // 3) Ejecutable de Python y script de extracción
            var pyExe = Path.IsPathRooted(_routes.PythonExe)
                ? _routes.PythonExe
                : Path.Combine(_routes.BasePath, _routes.PythonExe);
            var script = Path.IsPathRooted(_routes.ScriptExtractJson)
                ? _routes.ScriptExtractJson
                : Path.Combine(_routes.BasePath, _routes.ScriptExtractJson);
            if (!File.Exists(script))
                throw new FileNotFoundException($"Extract script not found at {script}");

            // 4) Llamar al proceso
            var psi = new ProcessStartInfo
            {
                FileName = pyExe,
                Arguments = $"\"{script}\" \"{pdfPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var proc = Process.Start(psi)!;
            var stdout = await proc.StandardOutput.ReadToEndAsync();
            var stderr = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();
            if (proc.ExitCode != 0)
                throw new Exception($"Extraction script failed (code={proc.ExitCode}): {stderr}");

            // 5) Parsear JSON
            return JsonDocument.Parse(stdout);
        }
    }
}
