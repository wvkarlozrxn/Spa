using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IPermisosAplicacion
    {
        Permisos Insertar(Permisos entidad);
        Permisos Actualizar(Permisos entidad);
        List<Permisos> Consultar();
        Permisos Borrar(Permisos entidad);
    }
}