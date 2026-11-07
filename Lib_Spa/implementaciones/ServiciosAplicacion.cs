using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lib_spa.interfaces
{
    public class ServiciosAplicacion : IServiciosAplicacion
    {
        private IConexion IConexion;

        public ServiciosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Servicios Actualizar(Servicios entidad)
        {
            if (entidad.Id_Servicio == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Duracion <= 0 ||
                entidad.Precio <= 0 )
                throw new Exception("Faltan valores O inválidos");

            if (!Regex.IsMatch(entidad.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre solo debe contener letras");

            var entry = this.IConexion!.Entry<Servicios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Servicios Borrar(Servicios entidad)
        {
            if (entidad.Id_Servicio == 0)
                throw new Exception("No se guardo");

            this.IConexion.Servicios!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Servicios> Consultar()
        {
            return this.IConexion.Servicios!
                .ToList();
        }

        public Servicios Insertar(Servicios entidad)
        {
            if (entidad.Id_Servicio != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Duracion <= 0 ||
                entidad.Precio <= 0)
                throw new Exception("Faltan valores O inválidos");

            this.IConexion.Servicios!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}