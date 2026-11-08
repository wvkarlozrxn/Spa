
using presentaciones_Spa.implementaciones;
using presentaciones_Spa.interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddScoped<IComunicaciones, Comunicaciones>();
builder.Services.AddScoped<IAuditoriasPresentacion, AuditoriasPresentacion>();
builder.Services.AddScoped<ICargosPresentacion, CargosPresentacion>();
builder.Services.AddScoped<IClientesPresentacion, ClientesPresentacion>();
builder.Services.AddScoped<IComprasPresentacion, ComprasPresentacion>();
builder.Services.AddScoped<IDetalle_Compras_Presentacion, Detalle_Compras_Presentacion>();
builder.Services.AddScoped<IDetalle_Facturas_Presentacion, Detalle_Facturas_Presentacion>();
builder.Services.AddScoped<IDetalle_Reservas_Presentacion, Detalle_Reservas_Presentacion>();
builder.Services.AddScoped<IEmpleados_TurnosPresentacion, Empleados_TurnosPresentacion>();
builder.Services.AddScoped<IEmpleadosPresentacion, EmpleadosPresentacion>();
builder.Services.AddScoped<IFacturasPresentacion, FacturasPresentacion>();
builder.Services.AddScoped<IInventariosPresentacion, InventariosPresentacion>();
builder.Services.AddScoped<IMetodos_Pagos_Presentacion, Metodos_Pagos_Presentacion>();
builder.Services.AddScoped<IPagosPresentacion, PagosPresentacion>();
builder.Services.AddScoped<IPermisosPresentacion, PermisosPresentacion>();
builder.Services.AddScoped<IPersonasPresentacion, PersonasPresentacion>();
builder.Services.AddScoped<IProductosPresentacion, ProductosPresentacion>();
builder.Services.AddScoped<IProveedoresPresentacion, ProveedoresPresentacion>();
builder.Services.AddScoped<IReservasPresentacion, ReservasPresentacion>();
builder.Services.AddScoped<IRolesPresentacion, RolesPresentacion>();
builder.Services.AddScoped<IServiciosPresentacion, ServiciosPresentacion>();
builder.Services.AddScoped<ITurnosPresentacion, TurnosPresentacion>();
builder.Services.AddScoped<IUsuariosPresentacion, UsuariosPresentacion>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
 
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
