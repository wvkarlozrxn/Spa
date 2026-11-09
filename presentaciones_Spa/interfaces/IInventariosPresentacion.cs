using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IInventariosPresentacion
    {
        Inventarios Insertar(Inventarios entidad);
        Inventarios Actualizar(Inventarios entidad);
        List<Inventarios> Consultar();
        List<Inventarios> Buscar(Inventarios entidad);
        Inventarios Borrar(Inventarios entidad);
    }
}