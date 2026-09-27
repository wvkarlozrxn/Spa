using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class ProductosPrueba
    {
        private IConexion conexion;
        private Productos? entidad = null;

        public ProductosPrueba()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
        }

        [TestMethod]
        public void Execute()
        {
            Insertar();
            Consultar();
            //Actualizar();
            Borrar();
        }

        public void Insertar()
        {
            this.entidad = new Productos()
            {
                Nombre = "ifoerbes",
                Descripcion = "crema exfoleante",
                Precio_Compra =20000,
                Precio_Venta =25000,

            };
            this.conexion.Productos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Productos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

      /*  private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Productos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      */
        private void Borrar()
        {
            this.conexion.Productos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

