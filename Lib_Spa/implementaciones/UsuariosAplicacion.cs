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
            if (entidad.Id_Usuario == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
                throw new Exception("Faltan valores");

            var entry = this.IConexion!.Entry<Usuarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id_Usuario == 0)
                throw new Exception("No se guardo");

            this.IConexion.Usuarios!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Usuarios> Consultar()
        {
            return this.IConexion.Usuarios!.ToList();
        }

        public Usuarios Insertar(Usuarios entidad)
        {
            if (entidad.Id_Usuario != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
                throw new Exception("Faltan valores");

            this.IConexion.Usuarios!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}