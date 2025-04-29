// Program.cs
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FL_Front_Web_SIN;
using FL_Front_Web_SIN.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ForecastAPI", client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/api/");
});

builder.Services.AddHttpClient("StudentsAPI", client =>
{
    client.BaseAddress = new Uri("https://api.students.underflabs.com/api/");
});

builder.Services.AddScoped<Services_ForecastService>();
builder.Services.AddScoped<PlanningService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7261/")
});

await builder.Build().RunAsync();
