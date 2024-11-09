using Microsoft.EntityFrameworkCore;
using TareasAPI_902.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Obtenemos la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");
//Agregando la configuracion para SQLServer
builder.Services.AddDbContext<Bdtareas902Context>(options => options.UseSqlServer(connectionString));

//Definimosla nueva política los CORS para que la API sea accesible despara cualquiera. 
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Activamos la nueva politíca 
app.UseCors("NuevaPolitica");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
