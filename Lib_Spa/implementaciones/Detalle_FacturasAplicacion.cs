using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lib_spa.interfaces
{
    public class Detalle_FacturasAplicacion : IDetalle_FacturasAplicacion
    {
        private IConexion IConexion;

        public Detalle_FacturasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Detalle_Facturas Actualizar(Detalle_Facturas entidad)
        {
            if (entidad.Id_Detalle_Factura == 0)
                throw new Exception("No se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Precio_Unidad <= 0 ||
                entidad.Subtotal <= 0 ||
                entidad.Id_Factura <= 0 ||
                entidad.Id_Servicio <= 0)
                throw new Exception("Faltan valores o datos erroneos");

            var entry = this.IConexion!.Entry<Detalle_Facturas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Detalle_Facturas Borrar(Detalle_Facturas entidad)
        {
            if (entidad.Id_Detalle_Factura == 0)
                throw new Exception("No se guardo");

            this.IConexion.Detalle_Facturas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalle_Facturas> Consultar()
        {
            return this.IConexion.Detalle_Facturas!
                .ToList();
        }

        public Detalle_Facturas Insertar(Detalle_Facturas entidad)
        {
            if (entidad.Id_Detalle_Factura != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Precio_Unidad <= 0 ||
                entidad.Subtotal <= 0 ||
                entidad.Id_Factura <= 0 ||
                entidad.Id_Servicio <= 0)
                throw new Exception("Faltan valores o datos erroneos");

            this.IConexion.Detalle_Facturas!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}