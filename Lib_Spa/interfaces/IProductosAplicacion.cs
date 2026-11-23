using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IProductosAplicacion
    {
        Productos Insertar(Productos entidad);
        Productos Actualizar(Productos entidad);
        List<Productos> Consultar();
        Productos Borrar(Productos entidad);
    }
}