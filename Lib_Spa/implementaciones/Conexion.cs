
using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.implementaciones
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
        public DbSet<Auditorias>? Auditorias { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<Permisos>? Permisos { get; set; }

        public DbSet<Turnos>? Turnos { get; set; }

        // Usuario que hace los cambios; llega desde el front y se guarda en las auditorías.
        public string? Usuario { get; set; }

        // Guarda los cambios pedidos y después registra una auditoría por cada uno.
        public override int SaveChanges()
        {
            var cambios = ChangeTracker.Entries()
                .Where(e => e.Entity.GetType() != typeof(Auditorias) &&
                            (e.State == EntityState.Added ||
                             e.State == EntityState.Modified ||
                             e.State == EntityState.Deleted))
                .ToList();
            var acciones = cambios.Select(e => e.State).ToList();

            var resultado = base.SaveChanges();

            for (var i = 0; i < cambios.Count; i++)
            {
                var clave = cambios[i].Metadata.FindPrimaryKey()!.Properties[0].Name;

                var auditoria = new Auditorias();
                auditoria.Fecha = DateTime.Now;
                auditoria.Usuario = Usuario ?? "";
                auditoria.Entidad = cambios[i].Entity.GetType().Name;
                auditoria.Id_Registro = Convert.ToInt32(cambios[i].Property(clave).CurrentValue);

                if (acciones[i] == EntityState.Added)
                    auditoria.Accion = "Insertar";
                else if (acciones[i] == EntityState.Modified)
                    auditoria.Accion = "Actualizar";
                else
                    auditoria.Accion = "Borrar";

                Auditorias!.Add(auditoria);
            }

            if (cambios.Count > 0)
                base.SaveChanges();

            return resultado;
        }
    }
}