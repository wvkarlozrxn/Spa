using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class UsuariosAplicacion : IUsuariosAplicacion
    {
        private IConexion IConexion;

        public UsuariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se guardo");

            this.IConexion.Usuarios!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Usuarios> Consultar()
        {
            return this.IConexion.Usuarios!
                .ToList();
        }

        public Usuarios Insertar(Usuarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            this.IConexion.Usuarios!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}