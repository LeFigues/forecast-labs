using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ufl_id.Data;
using ufl_id.Services;
using Serilog;
using ufl_id.Exceptions;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog para escribir logs en la consola y en un archivo
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/api-log.txt", rollingInterval: RollingInterval.Day) // Logs diarios en archivo
    .CreateLogger();

try
{
    Log.Information("Starting up the application...");

    // Usar Serilog como el logger de la aplicaci�n
    builder.Host.UseSerilog();

    // Configura la cadena de conexi�n a la base de datos
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // Registrar el DbContext con MySQL
    builder.Services.AddDbContext<DataContext>(options =>
    {
        try
        {
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Failed to connect to the database");
            throw;
        }
    });

    // Cargar la clave secreta JWT desde appsettings.json
    var jwtSecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        builder.Configuration["JwtSettings:SecretKey"]));

    // Registrar el TokenService y pasarle la clave secreta
    builder.Services.AddScoped<TokenService>(provider =>
    {
        var context = provider.GetRequiredService<DataContext>();
        return new TokenService(context, jwtSecretKey);
    });

    // Registrar otros servicios
    builder.Services.AddScoped<AuthService>();
    builder.Services.AddScoped<TokenValidationService>();
    builder.Services.AddScoped<ClientService>();
    builder.Services.AddScoped<AuthorizationService>();

    // Configuraci�n de rate limiting
    builder.Services.AddMemoryCache();
    builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
    builder.Services.AddInMemoryRateLimiting();
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

    // A�adir la autenticaci�n JWT
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://api.underflabs.com",
            ValidAudience = "https://api.underflabs.com",
            IssuerSigningKey = jwtSecretKey
        };
    });

    // A�adir servicios de controladores
    builder.Services.AddControllers();

    // Swagger para documentar la API
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configuraci�n del entorno de desarrollo
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();  // Habilitar HSTS en producci�n
    }

    // Middleware para interpretar las cabeceras reenviadas por Nginx
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.UseMiddleware<ExceptionMiddleware>();  // Middleware para manejo de excepciones

    // No se redirige a HTTPS ya que Nginx maneja HTTPS
    // app.UseHttpsRedirection();  // Puedes omitir esta l�nea si ya tienes Nginx manejando HTTPS

    app.UseAuthentication();  // Habilitar autenticaci�n JWT
    app.UseAuthorization();  // Habilitar autorizaci�n

    // Habilitar logging de solicitudes HTTP
    app.UseSerilogRequestLogging();
    app.UseIpRateLimiting();  // Habilitar limitaci�n de tasa
    app.UseMiddleware<CustomRateLimitMiddleware>();

    // Rutas para los controladores
    app.MapControllers();

    // Ejecutar la aplicaci�n en el puerto HTTP 5020
    var urls = builder.Configuration["AppSettings:ApplicationUrl"] ?? "http://*:5021";
    app.Run(urls);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed.");
}
finally
{
    Log.CloseAndFlush();  // Cerrar el logger al final
}
