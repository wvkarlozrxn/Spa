using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class Detalle_ReservasAplicacion : IDetalle_ReservasAplicacion
    {
        private IConexion IConexion;

        public Detalle_ReservasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Detalle_Reservas Actualizar(Detalle_Reservas entidad)
        {
            if (entidad.Id_Detalle_Reserva == 0)
                throw new Exception("No se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Precio <= 0 ||
                entidad.Id_Reserva <= 0 ||
                entidad.Id_Servicio <= 0)
                throw new Exception("Faltan valores o datos erroneos");


            var entry = this.IConexion!.Entry<Detalle_Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Detalle_Reservas Borrar(Detalle_Reservas entidad)
        {
            if (entidad.Id_Detalle_Reserva == 0)
                throw new Exception("No se guardo");

            this.IConexion.Detalle_Reservas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalle_Reservas> Consultar()
        {
            return this.IConexion.Detalle_Reservas!
                .ToList();
        }

        public Detalle_Reservas Insertar(Detalle_Reservas entidad)
        {
            if (entidad.Id_Detalle_Reserva != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Precio <= 0 ||
                entidad.Id_Reserva <= 0 ||
                entidad.Id_Servicio <= 0)
                throw new Exception("Faltan valores o datos erroneos");

            this.IConexion.Detalle_Reservas!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}