using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class ProductosAplicacion : IProductosAplicacion
    {
        private IConexion IConexion;

        public ProductosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Productos Actualizar(Productos entidad)
        {
            if (entidad.Id_Producto == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Precio_Compra <= 0 ||
                entidad.Precio_Venta <= 0 ||
                entidad.Id_Inventario <= 0)
                throw new Exception("Faltan valores O son inválidos");

            var entry = this.IConexion!.Entry<Productos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Productos Borrar(Productos entidad)
        {
            if (entidad.Id_Producto == 0)
                throw new Exception("No se guardo");

            this.IConexion.Productos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Productos> Consultar()
        {
            return this.IConexion.Productos!
                .ToList();
        }

        public Productos Insertar(Productos entidad)
        {
            if (entidad.Id_Producto != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Precio_Compra <= 0 ||
                entidad.Precio_Venta <= 0 ||
                entidad.Id_Inventario <= 0)
                throw new Exception("Faltan valores O son inválidos");

            this.IConexion.Productos!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}