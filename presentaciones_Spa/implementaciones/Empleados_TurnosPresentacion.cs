using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class Empleados_TurnosPresentacion : IEmpleados_TurnosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public Empleados_TurnosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Empleados_Turnos Actualizar(Empleados_Turnos entidad)
        {
            if (entidad.Id_Empleado_Turno == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Empleados_Turnos>(
                "Empleados_Turnos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Empleados_Turnos Borrar(Empleados_Turnos entidad)
        {
            if (entidad.Id_Empleado_Turno == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Empleados_Turnos>(
                "Empleados_Turnos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Empleados_Turnos> Buscar(Empleados_Turnos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Empleados_Turnos>>(
                "Empleados_Turnos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Empleados_Turnos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Empleados_Turnos>>(
                "Empleados_Turnos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Empleados_Turnos Insertar(Empleados_Turnos entidad)
        {
            if (entidad.Id_Empleado_Turno != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Empleados_Turnos>(
                "Empleados_Turnos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Empleados_Turnos entidad)
        {
            /* return string.IsNullOrEmpty(entidad.) ||
                 string.IsNullOrEmpty(entidad.Nombre) ||
                 string.IsNullOrEmpty(entidad.Lugar) ||
                 entidad.Inicio == null ||
                 entidad.Final == null ||
                 string.IsNullOrEmpty(entidad.Notas);
            */
            return true;
        }
    }
}