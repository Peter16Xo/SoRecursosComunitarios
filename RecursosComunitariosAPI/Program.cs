using Microsoft.EntityFrameworkCore;
using Data.Contexts; // Aseg�rate de importar el espacio de nombres del DbContext

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios a la aplicaci�n.
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

// Configuraci�n del middleware HTTP.
// Habilitar Swagger solo en el entorno de desarrollo.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Configuraci�n de Swagger UI
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Recursos Comunitarios v1");
    options.RoutePrefix = string.Empty; // Hace que Swagger est� disponible en la ra�z (http://localhost:<puerto>/)
});


app.UseAuthorization();

app.MapControllers();

app.Run();
