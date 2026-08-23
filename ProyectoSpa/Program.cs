Console.WriteLine("SPA APP ");

public class Cargos
{
    public int Id_Cargo { get; set; }
    public string? Cargo { get; set; }

    public List<Empleados> Empleados { get; set; }

}


public class Inventarios
{
    public int Id_Inventario { get; set; }
    public int Cantidad { get; set; }
    public List<Productos> Productos { get; set; }

}


public class Proveedores
{
    public int Id_Proveedor { get; set; }
    public string? Nombre_empresa { get; set; }
    public int Telefono { get; set; } //no seria un int ?
    public string? Correo { get; set; }
    public string? Direccion { get; set; }

    public List<Compras> Compras { get; set; }

}


public class Compras
{
    public int Id_Compra { get; set; }
    public int Id_Proveedor { get; set; }  //revisar
    public DateTime Fecha { get; set; }
    public string? Compra { get; set; }
    public int Total { get; set; }
    public int Estado { get; set; }

    public Proveedores? _Id_Proveedor { get; set; }

    //public List <Compras> Compras {get; set;}
    public List<Detalle_Compras> Detalle_Compras { get; set; }

}


public class Turnos
{
    public int Id_Turno { get; set; }
    public DateTime Dia { get; set; }
    public DateTime Hora_Entrada { get; set; }
    public DateTime Hora_Salida { get; set; }

    public List<Empleados_Turnos> Empleados_Turnos { get; set; }

}


public class Detalle_Facturas
{
    public int Id_Detalle_Factura { get; set; }
    public int Id_Factura { get; set; }
    public int Id_Servicio { get; set; }
    public int Cantidad { get; set; }
    public int Precio_Unidad { get; set; }
    public int Subtotal { get; set; }

    public Facturas? _Id_Factura { get; set; } //revisar
    public Servicios? _Id_Servicio { get; set; } //revisar

}

public class Facturas
{
    public int Id_Factura { get; set; }
    public DateTime Fecha { get; set; }
    public int Subtotal { get; set; }
    public int Descuento { get; set; }
    public int Total { get; set; }
    public int Id_Reserva { get; set; }

    public Reservas? _Id_Reserva { get; set; } //revisar ese _

    public List<Detalle_Facturas> Detalle_Facturas { get; set; }
    public List<Pagos> Pagos { get; set; }
}

public class Servicios
{
    public int Id_Servicio { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public DateTime Duracion { get; set; }
    public int Precio { get; set; }

    public List<Detalle_Facturas> Detalle_Facturas { get; set; }
    public List<Detalle_Reservas> Detalle_Reservas { get; set; }

}


public class Reservas
{
    public int Id_reserva { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime Hora { get; set; }
    public int Estado { get; set; }
    public int Id_Empleado { get; set; }
    public int Id_Cliente { get; set; }

    public Empleados? _Id_Empleados { get; set; }
    public Clientes? _Id_Clientes { get; set; }

    public List<Facturas> Facturas { get; set; }
    public List<Detalle_Reservas> Detalle_reservas { get; set; }
}


public class Detalle_Compras
{
    public int Id_Detalle_Compra { get; set; }
    public int Cantidad { get; set; }
    public int Precio_Unidad { get; set; }
    public int Subtotal { get; set; }
    public int Id_Compra { get; set; }
    public int Id_Producto { get; set; }

    public Compras? _Id_Compras { get; set; }
    public Productos? _Id_Productos { get; set; }

}

public class Productos
{
    public int Id_Producto { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public int Precio_Compra { get; set; }
    public int Precio_Venta { get; set; }
    public int Id_Inventario { get; set; }

    public Inventarios? _Id_Inventarios { get; set; }

    public List<Detalle_Compras> Detalle_Compras { get; set; } //revisar

}


public class Detalle_Reservas
{
    public int Id_Detalle_Reserva { get; set; }
    public int Cantidad { get; set; }
    public int Precio { get; set; }
    public int Id_Reserva { get; set; }
    public int Id_Servico { get; set; }

    public Reservas? _Id_Reservas { get; set; }
    public Servicios? _Id_Servicio { get; set; }

}


public class Pagos
{
    public int Id_Pagos { get; set; }
    public DateTime Fecha_Pago { get; set; }
    public int Id_Factura { get; set; }

    public Facturas _Id_Facturas { get; set; }

    public List<Detalles_Pagos> Descripcion_Detalles_Pagos { get; set; }

}


public class Detalles_Pagos
{
    public int Id_Detalle_Pago { get; set; }
    public string? Descripcion { get; set; }
    public string? Descripcion_Detalles_Pago { get; set; }
    public int Id_Pago { get; set; }

    public Pagos _Id_Pago { get; set; } //revisar

}


public class Clientes
{
    public int Id_Cliente { get; set; }
    public int Id_Persona { get; set; }

    public Personas _Id_Persona { get; set; } //revisar

    //  public List <Clientes> Clientes {get; set;}
}

public class Empleados
{
    public int Id_Empleado { get; set; }
    public int Id_Persona { get; set; }
    public int? Id_Cargo { get; set; }

    public Cargos _Id_Cargo { get; set; } //revisar
    public Personas _Id_Persona { get; set; } //revisar

    public List<Reservas> _Reservas { get; set; }
    public List<Empleados_Turnos> _Empleados_Turnos { get; set; }

}

public class Empleados_Turnos
{
    public int Id_Empleado_Turno { get; set; }
    public int Id_Turno { get; set; }
    public int Id_Empleado { get; set; }

    public Turnos _Id_Turno { get; set; }//revisar
    public Empleados _Id_Empleado { get; set; }//revisar

}


public class Personas
{
    public int Id_Persona { get; set; }
    public string? Nombre { get; set; }
    public int Telefono { get; set; }
    public int Cedula { get; set; }

    public List<Clientes> _Clientes { get; set; }//revisar
    public List<Empleados> _Empleados { get; set; }//revisar
}
























