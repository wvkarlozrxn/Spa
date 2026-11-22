using presentaciones_Spa.implementaciones;
using presentaciones_Spa.interfaces;

namespace Front_spa
{
   
    public class Datos
    {
        public IAuditoriasPresentacion Auditorias;
        public ICargosPresentacion Cargos;
        public IClientesPresentacion Clientes;
        public IComprasPresentacion Compras;
        public IDetalle_Compras_Presentacion Detalle_Compras;
        public IDetalle_Facturas_Presentacion Detalle_Facturas;
        public IDetalle_Reservas_Presentacion Detalle_Reservas;
        public IEmpleados_TurnosPresentacion Empleados_Turnos;
        public IEmpleadosPresentacion Empleados;
        public IFacturasPresentacion Facturas;
        public IInventariosPresentacion Inventarios;
        public IMetodos_Pagos_Presentacion Metodos_Pagos;
        public IPagosPresentacion Pagos;
        public IPermisosPresentacion Permisos;
        public IPersonasPresentacion Personas;
        public IProductosPresentacion Productos;
        public IProveedoresPresentacion Proveedores;
        public IReservasPresentacion Reservas;
        public IRolesPresentacion Roles;
        public IServiciosPresentacion Servicios;
        public ITurnosPresentacion Turnos;
        public IUsuariosPresentacion Usuarios;

        public Datos(IAuditoriasPresentacion Auditorias,
            ICargosPresentacion Cargos,
            IClientesPresentacion Clientes,
            IComprasPresentacion Compras,
            IDetalle_Compras_Presentacion Detalle_Compras,
            IDetalle_Facturas_Presentacion Detalle_Facturas,
            IDetalle_Reservas_Presentacion Detalle_Reservas,
            IEmpleados_TurnosPresentacion Empleados_Turnos,
            IEmpleadosPresentacion Empleados,
            IFacturasPresentacion Facturas,
            IInventariosPresentacion Inventarios,
            IMetodos_Pagos_Presentacion Metodos_Pagos,
            IPagosPresentacion Pagos,
            IPermisosPresentacion Permisos,
            IPersonasPresentacion Personas,
            IProductosPresentacion Productos,
            IProveedoresPresentacion Proveedores,
            IReservasPresentacion Reservas,
            IRolesPresentacion Roles,
            IServiciosPresentacion Servicios,
            ITurnosPresentacion Turnos,
            IUsuariosPresentacion Usuarios)
        {
            this.Auditorias = Auditorias;
            this.Cargos = Cargos;
            this.Clientes = Clientes;
            this.Compras = Compras;
            this.Detalle_Compras = Detalle_Compras;
            this.Detalle_Facturas = Detalle_Facturas;
            this.Detalle_Reservas = Detalle_Reservas;
            this.Empleados_Turnos = Empleados_Turnos;
            this.Empleados = Empleados;
            this.Facturas = Facturas;
            this.Inventarios = Inventarios;
            this.Metodos_Pagos = Metodos_Pagos;
            this.Pagos = Pagos;
            this.Permisos = Permisos;
            this.Personas = Personas;
            this.Productos = Productos;
            this.Proveedores = Proveedores;
            this.Reservas = Reservas;
            this.Roles = Roles;
            this.Servicios = Servicios;
            this.Turnos = Turnos;
            this.Usuarios = Usuarios;
        }
    }
}
