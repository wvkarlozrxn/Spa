using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class Metodos_Pagos_Pruebas
    {
        private IConexion conexion;
        private Metodos_Pagos? entidad = null;

        public Metodos_Pagos_Pruebas()
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
            this.entidad = new Metodos_Pagos()
            {
                Descripcion = "paga el servicio",
                Metodo_Pago = "tarjeta"
                
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

      /*  private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Metodos_Pagos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      */
        private void Borrar()
        {
            this.conexion.Metodos_Pagos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

