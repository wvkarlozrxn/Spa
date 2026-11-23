using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class PermisosAplicacion : IPermisosAplicacion
    {
        private IConexion IConexion;

        public PermisosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Permisos Actualizar(Permisos entidad)
        {
            if (entidad.Id_Permiso == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Permiso))
                throw new Exception("Faltan valores");

            var entry = this.IConexion!.Entry<Permisos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Permisos Borrar(Permisos entidad)
        {
            if (entidad.Id_Permiso == 0)
                throw new Exception("No se guardo");

            this.IConexion.Permisos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Permisos> Consultar()
        {
            return this.IConexion.Permisos!.ToList();
        }

        public Permisos Insertar(Permisos entidad)
        {
            if (entidad.Id_Permiso != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Permiso))
                throw new Exception("Faltan valores");

            this.IConexion.Permisos!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}