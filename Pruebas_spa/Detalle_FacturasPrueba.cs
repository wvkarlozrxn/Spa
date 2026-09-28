using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class     Detalle_FacturasPrueba
    {
        private IConexion conexion;
        private Detalle_Facturas? entidad = null;

        public      Detalle_FacturasPrueba()
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
                Cantidad = 2103,
                Precio_Unidad = 125.50m,
                Subtotal = 25632.52m
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

