using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class FacturasPrueba
    {
        private IConexion conexion;
        private Facturas? entidad = null;

        public FacturasPrueba()
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
            this.entidad = new Facturas()
            {
                Fecha = DateTime.Now,
                Subtotal = 2000,
                Descuento =500,
                Total = 1500,
                //Activo = true,
            };
            this.conexion.Facturas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Facturas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

      /*  private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Facturas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      */
        private void Borrar()
        {
            this.conexion.Facturas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

