using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class FacturasAplicacion : IFacturasAplicacion
    {
        private IConexion IConexion;

        public FacturasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Facturas Actualizar(Facturas entidad)
        {
            if (entidad.Id_Factura == 0)
                throw new Exception("No se guardo");

            if (entidad.Subtotal <= 0 ||
                entidad.Descuento <= 0 ||
                entidad.Total <= 0 ||
                entidad.Id_Reserva <= 0 )
                throw new Exception("Faltan valores o son inválidos");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            var entry = this.IConexion!.Entry<Facturas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Facturas Borrar(Facturas entidad)
        {
            if (entidad.Id_Factura == 0)
                throw new Exception("No se guardo");

            this.IConexion.Facturas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Facturas> Consultar()
        {
            return this.IConexion.Facturas!
                .ToList();
        }

        public Facturas Insertar(Facturas entidad)
        {
            if (entidad.Id_Factura != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Subtotal <= 0 ||
                entidad.Descuento <= 0 ||
                entidad.Total <= 0 ||
                entidad.Id_Reserva <= 0)
                throw new Exception("Faltan valores o son inválidos");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            this.IConexion.Facturas!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}