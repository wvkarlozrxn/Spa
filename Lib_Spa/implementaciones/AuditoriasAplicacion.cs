using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class AuditoriasAplicacion : IAuditoriasAplicacion
    {
        private IConexion IConexion;

        public AuditoriasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Auditorias Actualizar(Auditorias entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            var entry = this.IConexion!.Entry<Auditorias>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Auditorias Borrar(Auditorias entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se guardo");

            this.IConexion.Auditorias!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Auditorias> Consultar()
        {
            return this.IConexion.Auditorias!
                .ToList();
        }

        public Auditorias Insertar(Auditorias entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            this.IConexion.Auditorias!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}