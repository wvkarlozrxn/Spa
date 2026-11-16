using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

            if (string.IsNullOrEmpty(entidad.Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
                throw new Exception("Faltan valores");

            if (!Regex.IsMatch(entidad.Usuario, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El usuario solo debe contener letras");

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

            if (string.IsNullOrEmpty(entidad.Usuario) ||
                string.IsNullOrEmpty(entidad.Contraseña))
                throw new Exception("Faltan valores");

            this.IConexion.Usuarios!.Add(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Usuarios Validar(UsuariosRequest datos)
        {
            if (string.IsNullOrWhiteSpace(datos.Usuario) ||
                string.IsNullOrWhiteSpace(datos.Contraseña))
                throw new Exception("Faltan valores o son inválidos");

            var usuario = this.IConexion.Usuarios!
                .FirstOrDefault(u => u.Usuario == datos.Usuario &&
                                     u.Contraseña == datos.Contraseña);

            if (usuario == null)
                throw new Exception("Credenciales inválidas");

            return usuario;
        }
    }
}