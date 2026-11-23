using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class InventariosAplicacion : IInventariosAplicacion
    {
        private IConexion IConexion;

        public InventariosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Inventarios Actualizar(Inventarios entidad)
        {
            if (entidad.Id_Inventario == 0)
                throw new Exception("No se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Stock_min <= 0)
                throw new Exception("Faltan valores o son inválidos");

            var entry = this.IConexion!.Entry<Inventarios>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Inventarios Borrar(Inventarios entidad)
        {
            if (entidad.Id_Inventario == 0)
                throw new Exception("No se guardo");

            this.IConexion.Inventarios!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Inventarios> Consultar()
        {
            return this.IConexion.Inventarios!
                .ToList();
        }

        public Inventarios Insertar(Inventarios entidad)
        {
            if (entidad.Id_Inventario != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Cantidad <= 0 ||
                entidad.Stock_min <= 0)
                throw new Exception("Faltan valores o son inválidos");

            this.IConexion.Inventarios!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}