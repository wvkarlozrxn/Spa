using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class TurnosAplicacion : ITurnosAplicacion
    {
        private IConexion IConexion;

        public TurnosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Turnos Actualizar(Turnos entidad)
        {
            if (entidad.Id_Turno == 0)
                throw new Exception("No se guardo");

            if (entidad.Dia <= 0 ||
                entidad.Hora_Entrada <= 0 ||
                entidad.Hora_Salida <= 0 )
                throw new Exception("Faltan valores O inválidos");

            var entry = this.IConexion!.Entry<Turnos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Turnos Borrar(Turnos entidad)
        {
            if (entidad.Id_Turno == 0)
                throw new Exception("No se guardo");

            this.IConexion.Turnos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Turnos> Consultar()
        {
            return this.IConexion.Turnos!
                .ToList();
        }

        public Turnos Insertar(Turnos entidad)
        {
            if (entidad.Id_Turno != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Dia <= 0 ||
                entidad.Hora_Entrada <= 0 ||
                entidad.Hora_Salida <= 0)
                throw new Exception("Faltan valores O inválidos");

            this.IConexion.Turnos!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}