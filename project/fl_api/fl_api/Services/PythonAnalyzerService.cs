using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;
using System.Text.Json;

namespace fl_api.Services
{
    public class PythonAnalyzerService : IPythonAnalyzerService
    {
        private readonly PythonConfigRoutes _routes;
        private readonly IMongoDbService _mongo;

        public PythonAnalyzerService(IOptions<PythonConfigRoutes> options,
                                     IMongoDbService mongo)
        {
            _routes = options.Value;
            _mongo = mongo;
        }

        public async Task<JsonDocument> AnalyzeWithPythonAsync(Guid documentId)
        {
            // 1) Fetch the document record
            var collection = _mongo.GetCollection<DocumentRecord>("Documents");
            var record = await collection.Find(d => d.Id == documentId)
                                             .FirstOrDefaultAsync();
            if (record == null)
                throw new FileNotFoundException($"Document {documentId} not found.");

            // 2) Build path to PDF in PermanentStorage
            var permFolder = Path.IsPathRooted(_routes.PermanentFolder)
                ? _routes.PermanentFolder
                : Path.Combine(_routes.BasePath, _routes.PermanentFolder);
            var pdfPath = Path.Combine(permFolder, record.StoredFileName);
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF not found at {pdfPath}");

            // 3) Determine Python executable and script paths
            var pythonExe = Path.IsPathRooted(_routes.PythonExe)
                ? _routes.PythonExe
                : Path.Combine(_routes.BasePath, _routes.PythonExe);
            var scriptPath = Path.IsPathRooted(_routes.ScriptPath)
                ? _routes.ScriptPath
                : Path.Combine(_routes.BasePath, _routes.ScriptPath);

            // 4) Launch Python process
            var psi = new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = $"\"{scriptPath}\" \"{pdfPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi)!;
            string output = await proc.StandardOutput.ReadToEndAsync();
            string error = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            if (proc.ExitCode != 0)
                throw new Exception($"Python script failed with code {proc.ExitCode}: {error}");

            // 5) Parse and return JSON
            return JsonDocument.Parse(output);
        }
    }
}
