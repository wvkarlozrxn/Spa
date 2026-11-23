using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IEmpleados_TurnosPresentacion
    {
        Empleados_Turnos Insertar(Empleados_Turnos entidad);
        Empleados_Turnos Actualizar(Empleados_Turnos entidad);
        List<Empleados_Turnos> Consultar();
        List<Empleados_Turnos> Buscar(Empleados_Turnos entidad);
        Empleados_Turnos Borrar(Empleados_Turnos entidad);
    }
}