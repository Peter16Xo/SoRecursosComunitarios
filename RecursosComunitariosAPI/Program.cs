using Microsoft.EntityFrameworkCore;
using Data.Contexts; // Asegúrate de importar el espacio de nombres del DbContext

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la aplicación.
builder.Services.AddControllers();

// Agregar DbContext con SQL Server.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("RecursosComunitariosAPI") // Indica el ensamblado para las migraciones
    ));

// Configurar Swagger/OpenAPI para documentar y probar los endpoints.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del middleware HTTP.
// Habilitar Swagger solo en el entorno de desarrollo.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Configuración de Swagger UI
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Recursos Comunitarios v1");
    options.RoutePrefix = string.Empty; // Hace que Swagger esté disponible en la raíz (http://localhost:<puerto>/)
});


app.UseAuthorization();

app.MapControllers();

app.Run();
