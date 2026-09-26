using aplicaciones_libreria.entidades;
using aplicaciones_libreria.interfaces;
using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace aplicaciones_libreria.implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Cargos>? Cargos { get; set; }

        public DbSet<Clientes>? Clientes { get; set; }

        public DbSet<Compras>? Compras { get; set; }

        public DbSet<Detalle_Compras>? Detalle_Compras { get; set; }

        public DbSet<Detalle_Facturas>? Detalle_Facturas { get; set; }

        public DbSet<Detalle_Reservas>? Detalle_Reservas { get; set; }

        public DbSet<Empleados>? Empleados { get; set; }

        public DbSet<Empleados_Turnos>? Empleados_Turnos { get; set; }

        public DbSet<Facturas>? Facturas { get; set; }

        public DbSet<Inventarios>? Inventarios { get; set; }

        public DbSet<Metodos_Pagos>? Metodos_Pagos { get; set; }

        public DbSet<Pagos>? Pagos { get; set; }

        public DbSet<Personas>? Personas { get; set; }

        public DbSet<Productos>? Productos { get; set; }

        public DbSet<Proveedores>? Proveedores { get; set; }

        public DbSet<Reservas>? Reservas { get; set; }

        public DbSet<Servicios>? Servicios { get; set; }

        public DbSet<Turnos>? Turnos { get; set; }
    }
}
