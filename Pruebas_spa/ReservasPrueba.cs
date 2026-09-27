using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class ReservasPrueba
    {
        private IConexion conexion;
        private Reservas? entidad = null;

        public ReservasPrueba()
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
            this.entidad = new Reservas()
            {
               Fecha = DateTime.Now,
               //Hora = DateTime.Now,
               //Estado =
            };
            this.conexion.Reservas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Reservas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

      /*  private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Reservas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      */
        private void Borrar()
        {
            this.conexion.Reservas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

