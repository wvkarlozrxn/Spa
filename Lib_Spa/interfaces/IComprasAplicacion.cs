using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IComprasAplicacion
    {
        Compras Insertar(Compras entidad);
        Compras Actualizar(Compras entidad);
        List<Compras> Consultar();
        Compras Borrar(Compras entidad);
    }
}