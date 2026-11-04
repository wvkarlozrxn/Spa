using Lib_spa.implementaciones;
using Lib_spa.interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IConexion, Conexion>();
builder.Services.AddScoped<IAuditoriasAplicacion, AuditoriasAplicacion>();
builder.Services.AddScoped<ICargosAplicacion, CargosAplicacion>();
builder.Services.AddScoped<IClientesAplicacion, ClientesAplicacion>();
builder.Services.AddScoped<IComprasAplicacion, ComprasAplicacion>();
builder.Services.AddScoped<IDetalle_ComprasAplicacion, Detalle_ComprasAplicacion>();
builder.Services.AddScoped<IDetalle_FacturasAplicacion, Detalle_FacturasAplicacion>();
builder.Services.AddScoped<IDetalle_ReservasAplicacion, Detalle_ReservasAplicacion>();
builder.Services.AddScoped<IEmpleados_TurnosAplicacion, Empleados_TurnosAplicacion>();
builder.Services.AddScoped<IEmpleadosAplicacion, EmpleadosAplicacion>();
builder.Services.AddScoped<IFacturasAplicacion, FacturasAplicacion>();
builder.Services.AddScoped<IInventariosAplicacion, InventariosAplicacion>();
builder.Services.AddScoped<IMetodos_PagosAplicacion, Metodos_PagosAplicacion>();
builder.Services.AddScoped<IPagosAplicacion, PagosAplicacion>();
builder.Services.AddScoped<IPermisosAplicacion, PermisosAplicacion>();
builder.Services.AddScoped<IPersonasAplicacion, PersonasAplicacion>();
builder.Services.AddScoped<IProductosAplicacion, ProductosAplicacion>();
builder.Services.AddScoped<IProveedoresAplicacion, ProveedoresAplicacion>();
builder.Services.AddScoped<IReservasAplicacion, ReservasAplicacion>();
builder.Services.AddScoped<IRolesAplicacion, RolesAplicacion>();
builder.Services.AddScoped<IServiciosAplicacion, ServiciosAplicacion>();
builder.Services.AddScoped<ITurnosAplicacion, TurnosAplicacion>();
builder.Services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
