using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Repositories;
using fl_api.Services;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.Configure<OpenAISettings>(
    builder.Configuration.GetSection("OpenAI"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<OpenAISettings>>().Value);
builder.Services.AddScoped<ILabAnalysisService, LabAnalysisService>();

builder.Services.AddHttpClient<IGptService, GptService>(client =>
{
    var config = builder.Configuration.GetSection("OpenAI");
    client.BaseAddress = new Uri(config["BaseUrl"]!); // debe terminar en /v1/
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", config["ApiKey"]);
});
builder.Services.AddHttpClient<IStatusService, StatusService>();
builder.Services.AddSingleton<ILabAnalysisRepository, LabAnalysisRepository>();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
