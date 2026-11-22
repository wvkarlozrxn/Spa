using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class Metodos_PagosPrueba
    {
        private IConexion conexion;
        private Metodos_Pagos? entidad = null;

        public Metodos_PagosPrueba()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
        }

        [TestMethod]
        public void Execute()
        {
            // necesita al menos un pago registrado
            var pago = this.conexion.Pagos!.FirstOrDefault();
            if (pago == null)
            {
                Assert.Inconclusive("No hay pagos registrados.");
                return;
            }

            Insertar(pago.Id_Pago);
            Consultar();
            Actualizar();
            Borrar();
        }

        public void Insertar(int idPago)
        {
            this.entidad = new Metodos_Pagos()
            {
                Descripcion = "Pago de prueba",
                Metodo_Pago = "Efectivo",
                Id_Pago = idPago
            };
            this.conexion.Metodos_Pagos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Metodos_Pagos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Metodo_Pago = "Tarjeta";

            var entry = this.conexion!.Entry<Metodos_Pagos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Metodos_Pagos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
