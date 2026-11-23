using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class EmpleadosAplicacion : IEmpleadosAplicacion
    {
        private IConexion IConexion;

        public EmpleadosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Empleados Actualizar(Empleados entidad)
        {
            if (entidad.Id_Empleado == 0)
                throw new Exception("No se guardo");

            if (entidad.Id_Persona <= 0 ||
                entidad.Id_Cargo <= 0 )
                throw new Exception("Faltan valores");

            if (entidad.Fecha_Contrato == null)
                entidad.Fecha_Contrato = DateTime.Now;

            var entry = this.IConexion!.Entry<Empleados>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Empleados Borrar(Empleados entidad)
        {
            if (entidad.Id_Empleado == 0)
                throw new Exception("No se guardo");

            this.IConexion.Empleados!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Empleados> Consultar()
        {
            return this.IConexion.Empleados!
                .ToList();
        }

        public Empleados Insertar(Empleados entidad)
        {
            if (entidad.Id_Empleado != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Id_Persona <= 0 ||
                entidad.Id_Cargo <= 0  )
                throw new Exception("Faltan valores");

            if (entidad.Fecha_Contrato == null)
                entidad.Fecha_Contrato = DateTime.Now;

            this.IConexion.Empleados!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}