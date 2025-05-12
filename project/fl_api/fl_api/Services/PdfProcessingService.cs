using fl_api.Configurations;
using fl_api.Interfaces;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace fl_api.Services
{
    public class PdfProcessingService : IPdfProcessingService
    {
        private readonly PythonConfigRoutes _routes;

        public PdfProcessingService(IOptions<PythonConfigRoutes> options)
        {
            _routes = options.Value;
        }

        public async Task<string> ExtractToJsonAsync()
        {
            var pythonPath = Path.Combine(_routes.BasePath, _routes.PythonExe);
            var scriptPath = Path.Combine(_routes.BasePath, _routes.ScriptExtractJson);

            var psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = scriptPath,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            using var process = Process.Start(psi)!;
            using var reader = process.StandardOutput;
            var output = await reader.ReadToEndAsync();
            await process.WaitForExitAsync();
            return output;
        }
    }
}
