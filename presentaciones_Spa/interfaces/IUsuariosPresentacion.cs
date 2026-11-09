using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IUsuariosPresentacion
    {
        Usuarios Insertar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        List<Usuarios> Consultar();
        List<Usuarios> Buscar(Usuarios entidad);
        Usuarios Borrar(Usuarios entidad);
    }
}