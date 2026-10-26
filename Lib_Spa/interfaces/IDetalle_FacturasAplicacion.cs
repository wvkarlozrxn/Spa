using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IDetalle_FacturasAplicacion
    {
        Detalle_Facturas Insertar(Detalle_Facturas entidad);
        Detalle_Facturas Actualizar(Detalle_Facturas entidad);
        List<Detalle_Facturas> Consultar();
        Detalle_Facturas Borrar(Detalle_Facturas entidad);

    }
}