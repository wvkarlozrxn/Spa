using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lib_spa.interfaces
{
    public class PersonasAplicacion : IPersonasAplicacion
    {
        private IConexion IConexion;

        public PersonasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Personas Actualizar(Personas entidad)
        {
            if (entidad.Id_Persona == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Cedula))
                throw new Exception("Faltan valores O son inválidos");

            if (!Regex.IsMatch(entidad.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre solo debe contener letras");

            if (!Regex.IsMatch(entidad.Cedula, @"^\d+$") || !Regex.IsMatch(entidad.Telefono, @"^\d+$"))
                throw new Exception("Este campo solo debe contener números");

            var entry = this.IConexion!.Entry<Personas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Personas Borrar(Personas entidad)
        {
            if (entidad.Id_Persona == 0)
                throw new Exception("No se guardo");

            this.IConexion.Personas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Personas> Consultar()
        {
            return this.IConexion.Personas!
                .ToList();
        }

        public Personas Insertar(Personas entidad)
        {
            if (entidad.Id_Persona != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Cedula))
                throw new Exception("Faltan valores O son inválidos");

            if (!Regex.IsMatch(entidad.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre solo debe contener letras");

            if (!Regex.IsMatch(entidad.Cedula, @"^\d+$") || !Regex.IsMatch(entidad.Telefono, @"^\d+$"))
                throw new Exception("Este campo solo debe contener números");

            this.IConexion.Personas!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}