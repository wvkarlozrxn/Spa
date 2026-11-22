using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    // Comprueba que cada cambio en cualquier entidad deja una auditoría.
    [TestClass]
    public class AuditoriaAutomaticaPrueba
    {
        private IConexion conexion;
        private Cargos? entidad = null;

        public AuditoriaAutomaticaPrueba()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
        }

        [TestMethod]
        public void Execute()
        {
            Insertar();
            Actualizar();
            Borrar();
            Limpiar();
        }

        public void Insertar()
        {
            this.entidad = new Cargos() { Cargo = "Cargo de prueba" };
            this.conexion.Cargos!.Add(this.entidad);
            this.conexion.SaveChanges();

            Verificar("Insertar");
        }

        private void Actualizar()
        {
            this.entidad!.Cargo = "Cargo modificado";

            var entry = this.conexion!.Entry<Cargos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            Verificar("Actualizar");
        }

        private void Borrar()
        {
            this.conexion.Cargos!.Remove(this.entidad!);
            this.conexion.SaveChanges();

            Verificar("Borrar");
        }

        // Busca la auditoría de esa acción sobre el cargo de prueba.
        private void Verificar(string accion)
        {
            var existe = this.conexion.Auditorias!.Any(a =>
                a.Entidad == "Cargos" &&
                a.Accion == accion &&
                a.Id_Registro == this.entidad!.Id_Cargo);

            if (!existe)
                throw new Exception("No se registró la auditoría de " + accion);
        }

        // Borra las auditorías que dejó la prueba.
        private void Limpiar()
        {
            var otra = new Conexion();
            otra.StringConexion = Datosgenerales.ObtenerStringConexion();

            var lista = otra.Auditorias!
                .Where(a => a.Entidad == "Cargos" && a.Id_Registro == this.entidad!.Id_Cargo)
                .ToList();

            otra.Auditorias!.RemoveRange(lista);
            otra.SaveChanges();
        }
    }
}
