using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class PagosAplicacion : IPagosAplicacion
    {
        private IConexion IConexion;

        public PagosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Pagos Actualizar(Pagos entidad)
        {
            if (entidad.Id_Pago == 0)
                throw new Exception("No se guardo");

            if (entidad.Id_Pago <= 0)
                throw new Exception("Faltan valores O son inválidos");

            if (entidad.Fecha_Pago == null)
                entidad.Fecha_Pago = DateTime.Now;

            var entry = this.IConexion!.Entry<Pagos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Pagos Borrar(Pagos entidad)
        {
            if (entidad.Id_Pago == 0)
                throw new Exception("No se guardo");

            this.IConexion.Pagos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Pagos> Consultar()
        {
            return this.IConexion.Pagos!
                .ToList();
        }

        public Pagos Insertar(Pagos entidad)
        {
            if (entidad.Id_Pago != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Id_Pago <= 0)
                throw new Exception("Faltan valores O son inválidos");

            if (entidad.Fecha_Pago == null)
                entidad.Fecha_Pago = DateTime.Now;

            this.IConexion.Pagos!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}