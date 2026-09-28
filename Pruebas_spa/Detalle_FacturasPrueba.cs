using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
<<<<<<< HEAD
    public class Detalle_FacturasPrueba
=======
    public class     Detalle_FacturasPrueba
>>>>>>> aab3627280af00c8caae477c2e25052a932d9001
    {
        private IConexion conexion;
        private Detalle_Facturas? entidad = null;

<<<<<<< HEAD
        public Detalle_FacturasPrueba()
=======
        public      Detalle_FacturasPrueba()
>>>>>>> aab3627280af00c8caae477c2e25052a932d9001
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
            this.entidad = new Detalle_Facturas()
            {
<<<<<<< HEAD
                Cantidad = 20,
                Precio_Unidad = 5000,
                Subtotal = 54210,

=======
                Cantidad = 2103,
                Precio_Unidad = 125.50m,
                Subtotal = 25632.52m
>>>>>>> aab3627280af00c8caae477c2e25052a932d9001
            };
            this.conexion.Detalle_Facturas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Detalle_Facturas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

      /*  private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Detalle_Facturas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      */
        private void Borrar()
        {
            this.conexion.Detalle_Facturas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

