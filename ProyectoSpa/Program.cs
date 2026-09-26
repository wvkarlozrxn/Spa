Console.WriteLine("SPA APP ");

public class Cargos
{
    public int Id_Cargo { get; set; }
    public string? Cargo { get; set; }

    public List<Empleados>? Empleados { get; set; }
}

public class Inventarios
{
    public int Id_Inventario { get; set; }
    public int Cantidad { get; set; }
    public int Stock_min { get; set; }

    public List<Productos>?  Productos { get; set; }
}

public class Proveedores
{
    public int Id_Proveedor { get; set; }
    public string? Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
    public string? Direccion { get; set; }

    public List<Compras>? Compras { get; set; }
}

public class Compras
{
    public int Id_Compra { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Compra { get; set; }
    public decimal Total { get; set; }
    public int Estado { get; set; }
    public int Id_Proveedor { get; set; }

    public Proveedores? _Proveedor { get; set; }

    public List<Detalle_Compras>? Detalle_Compras { get; set; }
}

public class Turnos
{
    public int Id_Turno { get; set; }
    public DateTime Dia { get; set; }
    public DateTime Hora_Entrada { get; set; }
    public DateTime Hora_Salida { get; set; }

    public List<Empleados_Turnos>? Empleados_Turnos { get; set; }
}

public class Detalle_Facturas
{
    public int Id_Detalle_Factura { get; set; }
    public int Id_Factura { get; set; }
    public int Id_Servicio { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio_Unidad { get; set; }
    public decimal Subtotal { get; set; }

    public Facturas? _Factura { get; set; }
    public Servicios? _Servicio { get; set; }
}

public class Facturas
{
    public int Id_Factura { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Descuento { get; set; }
    public decimal Total { get; set; }
    public int Id_Reserva { get; set; }

    public Reservas? _Reserva { get; set; }

    public List<Detalle_Facturas>? Detalle_Facturas { get; set; }
    public List<Pagos>? Pagos { get; set; }
}

public class Servicios
{
    public int Id_Servicio { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public DateTime Duracion { get; set; }
    public decimal Precio { get; set; }

    public List<Detalle_Reservas>? Detalle_Reservas { get; set; }
    public List<Detalle_Facturas>? Detalle_Facturas { get; set; }
}

public class Reservas
{
    public int Id_Reserva { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime Hora { get; set; }
    public int Estado { get; set; }
    public int Id_Empleado { get; set; }
    public int Id_Cliente { get; set; }

    public Empleados? _Empleado { get; set; }
    public Clientes? _Cliente { get; set; }

    public List<Facturas>? Facturas { get; set; }
    public List<Detalle_Reservas>? Detalle_Reservas { get; set; }
}

public class Detalle_Compras
{
    public int Id_Detalle_Compra { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio_Unidad { get; set; }
    public decimal Subtotal { get; set; }
    public int Id_Compra { get; set; }
    public int Id_Producto { get; set; }

    public Compras? _Compra { get; set; }
    public Productos? _Producto { get; set; }
}

public class Productos
{
    public int Id_Producto { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio_Compra { get; set; }
    public decimal Precio_Venta { get; set; }
    public int Id_Inventario { get; set; }

    public Inventarios? _Inventario { get; set; }

    public List<Detalle_Compras>? Detalle_Compras { get; set; }
}

public class Detalle_Reservas
{
    public int Id_Detalle_Reserva { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public int Id_Reserva { get; set; }
    public int Id_Servicio { get; set; }

    public Reservas? _Reserva { get; set; }
    public Servicios? _Servicio { get; set; }
}

public class Pagos
{
    public int Id_Pago { get; set; }
    public DateTime Fecha_Pago { get; set; }
    public int Id_Factura { get; set; }

    public Facturas? _Factura { get; set; }

    public List<Metodos_Pagos>? Metodos_Pagos { get; set; }
}

public class Metodos_Pagos
{
    public int Id_Detalle_Pago { get; set; }
    public string? Descripcion { get; set; }
    public string? Metodo_Pago { get; set; }
    public int Id_Pago { get; set; }

    public Pagos? _Pago { get; set; }
}

public class Clientes
{
    public int Id_Cliente { get; set; }
    public int Id_Persona { get; set; }

    public Personas? _Persona { get; set; }

    public List<Reservas>? Reservas { get; set; }
}

public class Empleados
{
    public int Id_Empleado { get; set; }
    public int Id_Persona { get; set; }
    public int Id_Cargo { get; set; }
    public DateTime Fecha_Contrato { get; set; }

    public Cargos? _Cargo { get; set; }
    public Personas? _Persona { get; set; }

    public List<Reservas>? Reservas { get; set; }
    public List<Empleados_Turnos>? Empleados_Turnos { get; set; }
}

public class Empleados_Turnos
{
    public int Id_Empleado_Turno { get; set; }
    public int Id_Turno { get; set; }
    public int Id_Empleado { get; set; }

    public Turnos? _Turno { get; set; }
    public Empleados? _Empleado { get; set; }
}

public class Personas
{
    public int Id_Persona { get; set; }
    public string? Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Cedula { get; set; }

    public List<Clientes>? Clientes { get; set; }
    public List<Empleados>? Empleados { get; set; }
}






















