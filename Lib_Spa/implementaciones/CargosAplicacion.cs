using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class CargosAplicacion : ICargosAplicacion
    {
        private IConexion IConexion;

        public CargosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Cargos Actualizar(Cargos entidad)
        {
            if (entidad.Id_Cargo == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Cargo))
                throw new Exception("Faltan valores");

            var entry = this.IConexion!.Entry<Cargos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Cargos Borrar(Cargos entidad)
        {
            if (entidad.Id_Cargo == 0)
                throw new Exception("No se guardo");

            this.IConexion.Cargos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Cargos> Consultar(Cargos entidad)
        {
            var cargoExistente = this.IConexion.Cargos!
                .FirstOrDefault(x => x.Cargo == entidad.Cargo);

            if (cargoExistente == null)
                throw new Exception("No existe ese cargo");

            return this.IConexion.Cargos!
            .Where(x => x.Cargo == entidad.Cargo)
            .ToList();
        }

        public Cargos Insertar(Cargos entidad)
        {
            if (entidad.Id_Cargo!= 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Cargo))
                throw new Exception("Faltan valores");

            this.IConexion.Cargos!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}