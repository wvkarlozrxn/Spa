using aplicaciones_centros.implementaciones;
using aplicaciones_centros.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IConexion, Conexion>();
builder.Services.AddScoped<ICentrosAplicacion, CentrosAplicacion>();
builder.Services.AddScoped<IEspaciosAplicacion, EspaciosAplicacion>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
