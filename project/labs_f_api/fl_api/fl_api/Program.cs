using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Repositories;
using fl_api.Services;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configurar OpenAI
builder.Services.Configure<OpenAISettings>(
    builder.Configuration.GetSection("OpenAI"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<OpenAISettings>>().Value);

// Registrar servicios
builder.Services.AddScoped<ILabAnalysisService, LabAnalysisService>();
builder.Services.AddHttpClient<IUflIdService, UflIdService>();
builder.Services.AddSingleton<IPlanningRepository, PlanningRepository>();

builder.Services.AddHttpClient<IGptService, GptService>(client =>
{
    var config = builder.Configuration.GetSection("OpenAI");
    client.BaseAddress = new Uri(config["BaseUrl"]!);
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", config["ApiKey"]);
    client.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
});
builder.Services.AddHttpClient<IStatusService, StatusService>();
builder.Services.AddSingleton<ILabAnalysisRepository, LabAnalysisRepository>();

// Configurar MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

var app = builder.Build();

// Middleware pipeline

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

// HTTPS redirection y autorización
app.UseHttpsRedirection();
app.UseAuthorization();

// Controllers
app.MapControllers();

// Correr la aplicación
app.Run();
