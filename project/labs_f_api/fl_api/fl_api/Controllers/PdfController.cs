using fl_api.DTOs;
using fl_api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace fl_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfController : ControllerBase
{
    private readonly ILabAnalysisService _labAnalysisService;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly ILabForecastRepository _repository;
    public PdfController(ILabAnalysisService labAnalysisService, IConfiguration configuration, IWebHostEnvironment env, ILabForecastRepository repository)
    {
        _labAnalysisService = labAnalysisService;
        _configuration = configuration;
        _env = env;
        _repository = repository;
    }

    /// <summary>
    /// Analyzes a laboratory PDF using OpenAI Assistant and stores the result in MongoDB.
    /// </summary>
    [HttpPost("analyze-and-save")]
    public async Task<IActionResult> AnalyzeAndSave(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Invalid PDF file.");

        try
        {
            var result = await _labAnalysisService.AnalyzeAndSaveAsync(file);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to analyze and save lab data: {ex.Message}");
        }
    }

    /// <summary>
    /// Simple ping to confirm controller is working.
    /// </summary>
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("PdfController is alive ✅");

    [HttpPost("analyze")]
    public async Task<IActionResult> AnalyzePdf(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo no válido.");

        // Rutas de configuración
        var inputDir = _configuration["PdfProcessing:InputFolder"];
        var outputDir = _configuration["PdfProcessing:OutputFolder"];
        var pythonPath = _configuration["PdfProcessing:PythonExe"];
        var scriptPath = _configuration["PdfProcessing:ScriptPath"];
        var permanentDir = _configuration["PdfProcessing:PermanentFolder"];

        var fileName = Path.GetFileName(file.FileName);
        var inputPath = Path.Combine(inputDir, fileName);
        var outputPath = Path.Combine(outputDir, "result.json");
        var permanentPath = Path.Combine(permanentDir, $"{DateTime.Now:yyyyMMdd_HHmmss}_{fileName}");

        // Guardar archivo temporal en Input
        using (var stream = new FileStream(inputPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Guardar copia permanente
        try
        {
            Directory.CreateDirectory(permanentDir); // Asegura que exista
            using (var src = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (var dst = new FileStream(permanentPath, FileMode.Create))
            {
                await src.CopyToAsync(dst);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("⚠️ Error al guardar copia permanente: " + ex.Message);
        }

        // Ejecutar script Python
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(scriptPath)
                }
            };

            process.Start();
            string stdout = await process.StandardOutput.ReadToEndAsync();
            string stderr = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            if (process.ExitCode != 0)
                return StatusCode(500, $"Error en Python:\n{stderr}\n{stdout}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al ejecutar el script: {ex.Message}");
        }

        if (!System.IO.File.Exists(outputPath))
            return StatusCode(500, "No se encontró el archivo de salida JSON.");

        var json = await System.IO.File.ReadAllTextAsync(outputPath);
        try { System.IO.File.Delete(outputPath); } catch { }

        // Deserializar
        var data = JsonSerializer.Deserialize<LabAnalysisResult>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (data == null ||
            string.IsNullOrWhiteSpace(data.Laboratorio) ||
            string.IsNullOrWhiteSpace(data.Titulo) ||
            (data.Materiales?.Equipos == null || data.Materiales.Equipos.Count == 0) &&
            (data.Materiales?.Insumos == null || data.Materiales.Insumos.Count == 0))
        {
            return BadRequest("<!> El archivo no contiene información relevante para análisis. Verifica que sea un PDF de laboratorio válido.");
        }

        // Guardar en MongoDB
        if (data != null)
            await _repository.SaveAsync(data);

        return Ok(data);
    }

    [HttpPost("analyze-preview")]
    public async Task<IActionResult> AnalyzePdfPreview(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo no válido.");

        var inputDir = _configuration["PdfProcessing:InputFolder"];
        var outputDir = _configuration["PdfProcessing:OutputFolder"];
        var pythonPath = _configuration["PdfProcessing:PythonExe"];
        var scriptPath = _configuration["PdfProcessing:ScriptPath"];

        var fileName = Path.GetFileName(file.FileName);
        var inputPath = Path.Combine(inputDir, fileName);
        var outputPath = Path.Combine(outputDir, "result.json");

        // Guardar PDF temporalmente
        using (var stream = new FileStream(inputPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Ejecutar script Python
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = pythonPath,
                    Arguments = $"\"{scriptPath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(scriptPath)
                }
            };

            process.Start();
            string stdout = await process.StandardOutput.ReadToEndAsync();
            string stderr = await process.StandardError.ReadToEndAsync();
            process.WaitForExit();

            if (process.ExitCode != 0)
                return StatusCode(500, $"Error en Python:\n{stderr}\n{stdout}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al ejecutar el script: {ex.Message}");
        }

        if (!System.IO.File.Exists(outputPath))
            return StatusCode(500, "No se encontró el archivo de salida JSON.");

        var json = await System.IO.File.ReadAllTextAsync(outputPath);

        // Limpieza de archivos temporales
        try { System.IO.File.Delete(outputPath); } catch { }
        try { System.IO.File.Delete(inputPath); } catch { }

        var data = JsonSerializer.Deserialize<LabAnalysisResult>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (data == null ||
            string.IsNullOrWhiteSpace(data.Laboratorio) ||
            string.IsNullOrWhiteSpace(data.Titulo) ||
            (data.Materiales?.Equipos == null || data.Materiales.Equipos.Count == 0) &&
            (data.Materiales?.Insumos == null || data.Materiales.Insumos.Count == 0))
        {
            return BadRequest("⚠️ El archivo no contiene información relevante para análisis. Verifica que sea un PDF de laboratorio válido.");
        }

        return Ok(data);
    }

}
