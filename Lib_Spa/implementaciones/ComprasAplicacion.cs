using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class ComprasAplicacion : IComprasAplicacion
    {
        private IConexion IConexion;

        public ComprasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Compras Actualizar(Compras entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se guardo");

            if (entidad.Compra <= 0 ||
                entidad.Total <= 0 ||
                entidad.Id_Proveedor <= 0)
                throw new Exception("Faltan valores o datos erroneos");

                entidad.Fecha = DateTime.Now;

            var entry = this.IConexion!.Entry<Compras>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Compras Borrar(Compras entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se guardo");

            this.IConexion.Compras!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Compras> Consultar()
        {
            return this.IConexion.Compras!
                .ToList();
        }

        public Compras Insertar(Compras entidad)
        {
            if (entidad.Id_Compra != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Compra <= 0 ||
                entidad.Total <= 0 ||
                entidad.Id_Proveedor <= 0)
                throw new Exception("Faltan valores o datos erroneos");

                entidad.Fecha = DateTime.Now;

            this.IConexion.Compras!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}