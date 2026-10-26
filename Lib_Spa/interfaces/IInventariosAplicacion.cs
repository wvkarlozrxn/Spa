using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IInventariosAplicacion
    {
        Inventarios Insertar(Inventarios entidad);
        Inventarios Actualizar(Inventarios entidad);
        List<Inventarios> Consultar();
        Inventarios Borrar(Inventarios entidad);
    }
}