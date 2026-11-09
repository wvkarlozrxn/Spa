using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IDetalle_Detalle_Compras_Presentacion
    {
        Detalle_Compras Insertar(Detalle_Compras entidad);
        Detalle_Compras Actualizar(Detalle_Compras entidad);
        List<Detalle_Compras> Consultar();
        List<Detalle_Compras> Buscar(Detalle_Compras entidad);
        Detalle_Compras Borrar(Detalle_Compras entidad);
    }
}