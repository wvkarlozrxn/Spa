using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class Detalle_ReservasPrueba
    {
        private IConexion conexion;
        private Detalle_Reservas? entidad = null;

        public Detalle_ReservasPrueba()
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
            this.entidad = new Detalle_Reservas()
            {
                Cantidad = 2,
                Precio = 250000,
                Id_Reserva = 1,
                Id_Servicio = 1
            };
            this.conexion.Detalle_Reservas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Detalle_Reservas!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Detalle_Reservas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      
        private void Borrar()
        {
            this.conexion.Detalle_Reservas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

