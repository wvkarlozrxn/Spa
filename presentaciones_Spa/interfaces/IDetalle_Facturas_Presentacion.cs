using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IDetalle_Facturas_Presentacion
    {
        Detalle_Facturas Insertar(Detalle_Facturas entidad);
        Detalle_Facturas Actualizar(Detalle_Facturas entidad);
        List<Detalle_Facturas> Consultar();
        List<Detalle_Facturas> Buscar(Detalle_Facturas entidad);
        Detalle_Facturas Borrar(Detalle_Facturas entidad);
    }
}