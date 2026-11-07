using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private IConexion IConexion;

        public ReservasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad.Id_Reserva == 0)
                throw new Exception("No se guardo");

            if (entidad.Estado == 0 ||
                entidad.Id_Empleado == 0 ||
                entidad.Id_Cliente == 0 )
                throw new Exception("Faltan valores O son inválidos");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            var entry = this.IConexion!.Entry<Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Reservas Borrar(Reservas entidad)
        {
            if (entidad.Id_Reserva == 0)
                throw new Exception("No se guardo");

            this.IConexion.Reservas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Reservas> Consultar()
        {
            return this.IConexion.Reservas!
                .ToList();
        }

        public Reservas Insertar(Reservas entidad)
        {
            if (entidad.Id_Reserva != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Estado == 0 ||
                entidad.Id_Empleado == 0 ||
                entidad.Id_Cliente == 0)
                throw new Exception("Faltan valores O son inválidos");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            this.IConexion.Reservas!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}