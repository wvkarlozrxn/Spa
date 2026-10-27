using Lib_spa.implementaciones;
using Lib_spa.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IConexion, Conexion>();
//builder.Services.AddScoped<ICentrosAplicacion, CentrosAplicacion>();
//builder.Services.AddScoped<IEspaciosAplicacion, EspaciosAplicacion>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
