using fl_students_lib.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    try
    {
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // Asegura que la URL sea /swagger
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
