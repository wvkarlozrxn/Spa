using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class AuditoriasPrueba
    {
        private IConexion conexion;
        private Auditorias? entidad = null;

        public AuditoriasPrueba()
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
            this.entidad = new Auditorias()
            {
                Fecha = DateTime.Now,
                Usuario = "Prueba",
                Entidad = "Cargos",
                Accion = "Insertar",
                Id_Registro = 1
            };
            this.conexion.Auditorias!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Auditorias!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Accion = "Actualizar";

            var entry = this.conexion!.Entry<Auditorias>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Auditorias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
