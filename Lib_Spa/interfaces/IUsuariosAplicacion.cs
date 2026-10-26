using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IUsuariosAplicacion
    {
        Usuarios Insertar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        List<Usuarios> Consultar();
        Usuarios Borrar(Usuarios entidad);
    }
}