using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace presentacion_mst
{
    [TestClass]
    public class TurnosPrueba
    {
        private IConexion conexion;
        private Turnos? entidad = null;

        public TurnosPrueba()
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
            this.entidad = new Turnos()
            {
                Dia = 1,          
                Hora_Entrada = 8,  
                Hora_Salida = 17,

            };
            this.conexion.Turnos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Turnos!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

      private void Actualizar()
        {
            this.entidad!.Dia = 2;

            var entry = this.conexion!.Entry<Turnos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }
      
        private void Borrar()
        {
            this.conexion.Turnos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}

