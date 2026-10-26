using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class Detalle_ComprasAplicacion : IDetalle_ComprasAplicacion
    {
        private IConexion IConexion;

        public Detalle_ComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Detalle_Compras Actualizar(Detalle_Compras entidad)
        {
            if (entidad.Id_Detalle_Compra == 0)
                throw new Exception("No se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Precio_Unidad <= 0 ||
                entidad.Subtotal <= 0 ||
                entidad.Id_Compra <= 0 ||
                entidad.Id_Producto <= 0)
                throw new Exception("Faltan valores o datos erroneos");

            var entry = this.IConexion!.Entry<Detalle_Compras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Detalle_Compras Borrar(Detalle_Compras entidad)
        {
            if (entidad.Id_Detalle_Compra == 0)
                throw new Exception("No se guardo");

            this.IConexion.Detalle_Compras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Detalle_Compras> Consultar()
        {
            return this.IConexion.Detalle_Compras!
                .ToList();
        }

        public Detalle_Compras Insertar(Detalle_Compras entidad)
        {
            if (entidad.Id_Detalle_Compra != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Precio_Unidad <= 0 ||
                entidad.Subtotal <= 0 ||
                entidad.Id_Compra <= 0 ||
                entidad.Id_Producto <= 0)
                throw new Exception("Faltan valores o datos erroneos");

            this.IConexion.Detalle_Compras!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}