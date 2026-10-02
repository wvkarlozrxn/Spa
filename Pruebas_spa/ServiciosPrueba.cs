using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace presentacion_mst
{
    [TestClass]
    public class ServiciosPrueba
    {
        private IConexion conexion;
        private Servicios? entidad = null;

        public ServiciosPrueba()
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
            this.entidad = new Servicios()
            {
                Nombre ="masaje turco",
                Descripcion = "manaje con tecnicas de turca y aroma terapia",
                Duracion = 2,
                Precio = 120000,
            };
            this.conexion.Servicios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Servicios!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

       private void Actualizar()
        {
            this.entidad!.Duracion = 2;

            var entry = this.conexion!.Entry<Servicios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      
        private void Borrar()
        {
            this.conexion.Servicios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

