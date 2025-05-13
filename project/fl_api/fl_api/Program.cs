using System;
using System.Net.Http.Headers;
using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Repositories.Impl;
using fl_api.Repositories;
using fl_api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using University.Interfaces;
using University.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Controladores y OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Para MapOpenApi()

// 2) CORS – permite todos los orígenes (útil para pruebas y frontend externo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 3) Configuración de archivos
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("purchaseSettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("prompts.json", optional: false, reloadOnChange: true);

// 4) Config binding
builder.Services.Configure<PythonConfigRoutes>(builder.Configuration.GetSection("PythonConfigRoutes"));
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<ApiStudentsSettings>(builder.Configuration.GetSection("ApiStudents"));
builder.Services.Configure<ApiLabsSettings>(builder.Configuration.GetSection("ApiLabs"));
builder.Services.Configure<PromptSettings>(builder.Configuration);
builder.Services.Configure<PurchaseSimulationSettings>(
    builder.Configuration.GetSection("PurchaseSimulationSettings"));

// 5) Servicios de negocio y utilitarios
builder.Services.AddTransient<IPythonAnalyzerService, PythonAnalyzerService>();
builder.Services.AddTransient<IPdfExtractionService, PdfExtractionService>();
builder.Services.AddTransient<IPromptService, PromptService>(); // ✅ Solo uno
builder.Services.AddTransient<IOpenAIAnalysisService, OpenAIAnalysisService>();
builder.Services.AddTransient<IDemandReportService, DemandReportService>();
builder.Services.AddScoped<IPurchaseSimulationService, PurchaseSimulationService>();
builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddTransient<IDocumentService, DocumentService>();
builder.Services.AddSingleton<IMongoDbService, MongoDbService>();
builder.Services.AddTransient<IClassificationService, ClassificationService>();
builder.Services.AddTransient<IPdfProcessingService, PdfProcessingService>();
builder.Services.AddSingleton<IPlanningService, PlanningService>();


builder.Services.AddScoped<IForecastRiesgoRepository, ForecastRiesgoRepository>();
builder.Services.AddScoped<IForecastHistoricoRepository, ForecastHistoricoRepository>();
builder.Services.AddScoped<IForecastPracticaRepository, ForecastPracticaRepository>();

// 6) Clientes HTTP configurados
builder.Services.AddHttpClient<IOpenAIClient, OpenAIClient>(client =>
{
    var openAiCfg = builder.Configuration.GetSection("OpenAI");
    client.BaseAddress = new Uri(openAiCfg["BaseUrl"]!);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAiCfg["ApiKey"]!);
});

builder.Services.AddHttpClient<IStudentsApiClient, StudentsApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiStudents:BaseUrl"]!);
    client.Timeout = TimeSpan.FromSeconds(20);
});

builder.Services.AddHttpClient<ILabsApiClient, LabsApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiLabs:BaseUrl"]!);
    client.Timeout = TimeSpan.FromSeconds(20);
});
builder.Services.AddHttpClient<IUniversityApiClient, UniversityApiClient>(client =>
{
    client.BaseAddress = new Uri("https://universidad-la9h.onrender.com");
});
var app = builder.Build();

// 7) Middleware de manejo de errores global
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"[ERROR] {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { error = "Internal server error" });
    }
});

// 8) Habilitar OpenAPI siempre (producción y desarrollo)
app.MapOpenApi(); // Este middleware sirve la UI moderna para OpenAPI

// 9) Pipeline HTTP
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
