using Lib_spa.entidades;
using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_mst
{
    [TestClass]
    public class UsuariosPrueba
    {
        private IConexion conexion;
        private Usuarios? entidad = null;

        public UsuariosPrueba()
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
            this.entidad = new Usuarios()
            {
                Usuario = "PruebaUnitaria",
                Contraseña = "1234",
                Id_Rol = 1
            };
            this.conexion.Usuarios!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista = this.conexion.Usuarios!.ToList();
            if (lista.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Contraseña = "5678";

            var entry = this.conexion!.Entry<Usuarios>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Usuarios!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
