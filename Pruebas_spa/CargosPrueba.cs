using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class CargosPrueba
    {
        private IConexion conexion;
        private Cargos? entidad = null;

        public CargosPrueba()
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
            this.entidad = new Cargos()
            {
                Cargo = "Masajista",
                
            };
            this.conexion.Cargos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Cargos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

       private void Actualizar()
        {
            this.entidad!.Cargo = "masajista";

            var entry = this.conexion!.Entry<Cargos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
        

        private void Borrar()
        {
            this.conexion.Cargos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

