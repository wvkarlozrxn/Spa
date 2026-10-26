using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IRolesAplicacion
    {
        Roles Insertar(Roles entidad);
        Roles Actualizar(Roles entidad);
        List<Roles> Consultar();
        Roles Borrar(Roles entidad);
    }
}