using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IEmpleadosAplicacion
    {
        Empleados Insertar(Empleados entidad);
        Empleados Actualizar(Empleados entidad);
        List<Empleados> Consultar();
        Empleados Borrar(Empleados entidad);
    }
}