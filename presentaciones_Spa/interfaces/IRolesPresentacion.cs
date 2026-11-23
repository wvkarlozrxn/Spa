using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IRolesPresentacion
    {
        Roles Insertar(Roles entidad);
        Roles Actualizar(Roles entidad);
        List<Roles> Consultar();
        List<Roles> Buscar(Roles entidad);
        Roles Borrar(Roles entidad);
    }
}