using Lib_spa.entidades;
using Lib_spa.interfaces;
using presentacion_mst.Nucleo;

namespace presentacion_mst
{
    // Comprueba las reglas de cada aplicación: con datos inválidos debe lanzar error.
    // Estas pruebas fallan antes de llegar a la base, así que no guardan nada.
    [TestClass]
    public class ValidacionesPrueba
    {
        private IConexion conexion;

        public ValidacionesPrueba()
        {
            this.conexion = Ayuda.NuevaConexion();
        }

        [TestMethod]
        public void Servicios()
        {
            var app = new ServiciosAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Servicios
            { Nombre = "", Descripcion = "d", Duracion = 60, Precio = 1000 }), "nombre vacío");
            Ayuda.DebeFallar(() => app.Insertar(new Servicios
            { Nombre = "Masaje", Descripcion = "d", Duracion = 0, Precio = 1000 }), "duración en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Servicios
            { Nombre = "Masaje", Descripcion = "d", Duracion = 60, Precio = 0 }), "precio en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Servicios
            { Id_Servicio = 5, Nombre = "Masaje", Descripcion = "d", Duracion = 60, Precio = 1000 }), "ya tiene id");
        }

        [TestMethod]
        public void Personas()
        {
            var app = new PersonasAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Personas
            { Nombre = "", Telefono = "300", Cedula = "100" }), "nombre vacío");
            Ayuda.DebeFallar(() => app.Insertar(new Personas
            { Nombre = "Ana123", Telefono = "300", Cedula = "100" }), "nombre con números");
            Ayuda.DebeFallar(() => app.Insertar(new Personas
            { Nombre = "Ana", Telefono = "abc", Cedula = "100" }), "teléfono con letras");
            Ayuda.DebeFallar(() => app.Insertar(new Personas
            { Nombre = "Ana", Telefono = "300", Cedula = "12a" }), "cédula con letras");
        }

        [TestMethod]
        public void Clientes()
        {
            var app = new ClientesAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Clientes { Persona = 0 }), "sin persona");
            Ayuda.DebeFallar(() => app.Actualizar(new Clientes { Id_Cliente = 0, Persona = 1 }), "actualizar sin id");
        }

        [TestMethod]
        public void Empleados()
        {
            var app = new EmpleadosAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Empleados
            { Id_Persona = 0, Id_Cargo = 1 }), "sin persona");
            Ayuda.DebeFallar(() => app.Insertar(new Empleados
            { Id_Persona = 1, Id_Cargo = 0 }), "sin cargo");
        }

        [TestMethod]
        public void Proveedores()
        {
            var app = new ProveedoresAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Proveedores
            { Nombre = "", Telefono = "300", Correo = "a@a.com", Direccion = "calle" }), "nombre vacío");
            Ayuda.DebeFallar(() => app.Insertar(new Proveedores
            { Nombre = "Aromas", Telefono = "abc", Correo = "a@a.com", Direccion = "calle" }), "teléfono con letras");
            Ayuda.DebeFallar(() => app.Insertar(new Proveedores
            { Nombre = "Aromas 2", Telefono = "300", Correo = "a@a.com", Direccion = "calle" }), "nombre con números");
            Ayuda.DebeFallar(() => app.Insertar(new Proveedores
            { Nombre = "Aromas", Telefono = "300", Correo = "", Direccion = "calle" }), "correo vacío");
        }

        [TestMethod]
        public void Productos()
        {
            var app = new ProductosAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Productos
            { Nombre = "", Descripcion = "d", Precio_Compra = 1, Precio_Venta = 2, Id_Inventario = 1 }), "nombre vacío");
            Ayuda.DebeFallar(() => app.Insertar(new Productos
            { Nombre = "Aceite", Descripcion = "d", Precio_Compra = 0, Precio_Venta = 2, Id_Inventario = 1 }), "precio de compra en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Productos
            { Nombre = "Aceite", Descripcion = "d", Precio_Compra = 1, Precio_Venta = 0, Id_Inventario = 1 }), "precio de venta en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Productos
            { Nombre = "Aceite", Descripcion = "d", Precio_Compra = 1, Precio_Venta = 2, Id_Inventario = 0 }), "sin inventario");
        }

        [TestMethod]
        public void Inventarios()
        {
            var app = new InventariosAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Inventarios
            { Cantidad = 0, Stock_min = 5 }), "cantidad en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Inventarios
            { Cantidad = 10, Stock_min = 0 }), "stock mínimo en cero");
        }

        [TestMethod]
        public void Compras()
        {
            var app = new ComprasAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Compras
            { Compra = 0, Total = 100, Id_Proveedor = 1 }), "compra en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Compras
            { Compra = 100, Total = 0, Id_Proveedor = 1 }), "total en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Compras
            { Compra = 100, Total = 100, Id_Proveedor = 0 }), "sin proveedor");
        }

        [TestMethod]
        public void Detalle_Compras()
        {
            var app = new Detalle_ComprasAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Detalle_Compras
            { Cantidad = 0, Precio_Unidad = 1, Subtotal = 1, Id_Compra = 1, Id_Producto = 1 }), "cantidad en cero");
            Ayuda.DebeFallar(() => app.Insertar(new Detalle_Compras
            { Cantidad = 1, Precio_Unidad = 1, Subtotal = 1, Id_Compra = 0, Id_Producto = 1 }), "sin compra");
            Ayuda.DebeFallar(() => app.Insertar(new Detalle_Compras
            { Cantidad = 1, Precio_Unidad = 1, Subtotal = 1, Id_Compra = 1, Id_Producto = 0 }), "sin producto");
        }

        [TestMethod]
        public void Pagos()
        {
            var app = new PagosAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Pagos { Id_Factura = 0 }), "insertar sin factura");
            // este caso era el error que se corrigió: antes validaba Id_Pago en lugar de Id_Factura
            Ayuda.DebeFallar(() => app.Actualizar(new Pagos { Id_Pago = 1, Id_Factura = 0 }), "actualizar sin factura");
        }

        [TestMethod]
        public void Metodos_Pagos()
        {
            var app = new Metodos_PagosAplicacion(this.conexion);

            Ayuda.DebeFallar(() => app.Insertar(new Metodos_Pagos
            { Descripcion = "", Metodo_Pago = "Efectivo", Id_Pago = 1 }), "descripción vacía");
            Ayuda.DebeFallar(() => app.Insertar(new Metodos_Pagos
            { Descripcion = "Pago", Metodo_Pago = "", Id_Pago = 1 }), "método vacío");
            Ayuda.DebeFallar(() => app.Insertar(new Metodos_Pagos
            { Descripcion = "Pago", Metodo_Pago = "Efectivo", Id_Pago = 0 }), "sin pago");
        }

        [TestMethod]
        public void Cargos_Roles_Permisos()
        {
            Ayuda.DebeFallar(() => new CargosAplicacion(this.conexion)
                .Insertar(new Cargos { Cargo = "" }), "cargo vacío");
            Ayuda.DebeFallar(() => new RolesAplicacion(this.conexion)
                .Insertar(new Roles { Rol = "" }), "rol vacío");
            Ayuda.DebeFallar(() => new PermisosAplicacion(this.conexion)
                .Insertar(new Permisos { Permiso = "" }), "permiso vacío");
        }
    }
}
