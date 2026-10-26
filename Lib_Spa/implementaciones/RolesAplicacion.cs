using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class RolesAplicacion : IRolesAplicacion
    {
        private IConexion IConexion;

        public RolesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Roles Actualizar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            var entry = this.IConexion!.Entry<Roles>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Roles Borrar(Roles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se guardo");

            this.IConexion.Roles!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Roles> Consultar()
        {
            return this.IConexion.Roles!
                .ToList();
        }

        public Roles Insertar(Roles entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            this.IConexion.Roles!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}