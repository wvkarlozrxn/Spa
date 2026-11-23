using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class ClientesAplicacion : IClientesAplicacion
    {
        private IConexion IConexion;

        public ClientesAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Clientes Actualizar(Clientes entidad)
        {
            if (entidad.Id_Cliente == 0)
                throw new Exception("No se guardo");

            if (entidad.Persona == 0)
                throw new Exception("Falta informacion de la persona");

            var entry = this.IConexion!.Entry<Clientes>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Clientes Borrar(Clientes entidad)
        {
            if (entidad.Id_Cliente == 0)
                throw new Exception("No se guardo");

            this.IConexion.Clientes!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Clientes> Consultar()
        {
            return this.IConexion.Clientes!
                .ToList();
        }

        public Clientes Insertar(Clientes entidad)
        {
            if (entidad.Id_Cliente  != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Persona == 0)
                throw new Exception("Falta informacion de la persona");

            this.IConexion.Clientes!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}