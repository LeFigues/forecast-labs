using fl_front;
using fl_front.Models;
using fl_front.Services;
using fl_front.Services.Impl;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Servicio base para Blazor
builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Servicio para Health + Upload (forecast API)
builder.Services.AddHttpClient<IHealthService, HealthService>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});

builder.Services.AddHttpClient<IDocumentService, DocumentService>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});

builder.Services.AddHttpClient<IStudentsService, StudentsService>(client =>
{
    client.BaseAddress = new Uri("https://api.students.underflabs.com/");
});

builder.Services.AddHttpClient<IDemandReportService, DemandReportService>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});

builder.Services.AddHttpClient<IForecastService, ForecastService>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});


builder.Services.AddSingleton<AppState>();

await builder.Build().RunAsync();
