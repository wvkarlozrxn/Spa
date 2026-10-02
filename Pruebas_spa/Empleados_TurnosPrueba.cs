using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class Empleados_TurnosPrueba
    {
        private IConexion conexion;
        private Empleados_Turnos? entidad = null;

        public Empleados_TurnosPrueba()
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
            this.entidad = new Empleados_Turnos()
            {
                Id_Turno = 1,
                Id_Empleado = 1,

            };
            this.conexion.Empleados_Turnos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Empleados_Turnos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

       private void Actualizar()
        {
            
            this.entidad!.Id_Turno = 2;

            var entry = this.conexion!.Entry<Empleados_Turnos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      
        private void Borrar()
        {
            this.conexion.Empleados_Turnos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

