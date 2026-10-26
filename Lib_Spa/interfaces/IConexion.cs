using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lib_spa.interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Cargos>? Cargos { get; set; }

        DbSet<Clientes>? Clientes { get; set; }

        DbSet<Compras>? Compras { get; set; }

        DbSet<Detalle_Compras>? Detalle_Compras { get; set; }

        DbSet<Detalle_Facturas>? Detalle_Facturas { get; set; }

        DbSet<Detalle_Reservas>? Detalle_Reservas { get; set; }

        DbSet<Empleados>? Empleados { get; set; }

        DbSet<Empleados_Turnos>? Empleados_Turnos { get; set; }

        DbSet<Facturas>? Facturas { get; set; }

        DbSet<Inventarios>? Inventarios { get; set; }

        DbSet<Metodos_Pagos>? Metodos_Pagos { get; set; }

        DbSet<Pagos>? Pagos { get; set; }

        DbSet<Personas>? Personas { get; set; }

        DbSet<Productos>? Productos { get; set; }

        DbSet<Proveedores>? Proveedores { get; set; }

        DbSet<Reservas>? Reservas { get; set; }

        DbSet<Servicios>? Servicios { get; set; }

<<<<<<< HEAD
        DbSet<Turnos>? Turnos { get; set; }
=======
        DbSet<Turnos>? Turnos { get; set; } 

        DbSet<Turnos>? Auditorias { get; set; }

        DbSet<Turnos>? Usuarios { get; set; }

        DbSet<Turnos>? Roles { get; set; }

        DbSet<Turnos>? Permisos { get; set; }
>>>>>>> 4d33c06 (integracion con la parte de juan, agrega servicio_spa y front_spa)

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}