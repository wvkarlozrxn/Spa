using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class Detalle_ComprasPrueba
    {
        private IConexion conexion;
        private Detalle_Compras? entidad = null;

        public Detalle_ComprasPrueba()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
        }

        [TestMethod]
        public void Execute()
        {
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
        }

        public void Insertar()
        {
            this.entidad = new Detalle_Compras()
            {
                Cantidad = 25,
                Precio_Unidad = 125.50m,
                Subtotal=25632.52m,
                Id_Compra = 1,
                Id_Producto = 1

            };
            this.conexion.Detalle_Compras!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Detalle_Compras!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

       private void Actualizar()
        {
            this.entidad!.Id_Compra = 1;

            var entry = this.conexion!.Entry<Detalle_Compras>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      
        private void Borrar()
        {
            this.conexion.Detalle_Compras!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

