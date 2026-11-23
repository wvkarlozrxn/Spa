using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IEmpleados_TurnosAplicacion
    {
        Empleados_Turnos Insertar(Empleados_Turnos entidad);
        Empleados_Turnos Actualizar(Empleados_Turnos entidad);
        List<Empleados_Turnos> Consultar();
        Empleados_Turnos Borrar(Empleados_Turnos entidad);
    }
}