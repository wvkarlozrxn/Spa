using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class Empleados_TurnosAplicacion : IEmpleados_TurnosAplicacion
    {
        private IConexion IConexion;

        public Empleados_TurnosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Empleados_Turnos Actualizar(Empleados_Turnos entidad)
        {
            if (entidad.Id_Empleado_Turno == 0)
                throw new Exception("No se guardo");

            if (entidad.Id_Empleado <= 0 ||
                entidad.Id_Turno <= 0 )
                throw new Exception("Faltan valores o datos erroneos");

            var entry = this.IConexion!.Entry<Empleados_Turnos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Empleados_Turnos Borrar(Empleados_Turnos entidad)
        {
            if (entidad.Id_Empleado_Turno == 0)
                throw new Exception("No se guardo");

            this.IConexion.Empleados_Turnos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Empleados_Turnos> Consultar()
        {
            return this.IConexion.Empleados_Turnos!
                .ToList();
        }

        public Empleados_Turnos Insertar(Empleados_Turnos entidad)
        {
            if (entidad.Id_Empleado_Turno != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Id_Empleado <= 0 ||
                entidad.Id_Turno <= 0)
                throw new Exception("Faltan valores o datos erroneos    ");

            this.IConexion.Empleados_Turnos!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}