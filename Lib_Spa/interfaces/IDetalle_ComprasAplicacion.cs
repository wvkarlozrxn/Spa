using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IDetalle_ComprasAplicacion
    {
        Detalle_Compras Insertar(Detalle_Compras entidad);
        Detalle_Compras Actualizar(Detalle_Compras entidad);
        List<Detalle_Compras> Consultar();
        Detalle_Compras Borrar(Detalle_Compras entidad);
    }
}