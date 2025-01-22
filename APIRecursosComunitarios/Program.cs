using Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularAppp",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();
// Lo utilizan cuando crean las migraciones
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    context.Database.Migrate();
//}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularAppp");
app.UseAuthorization();

app.MapControllers();

app.Run();
