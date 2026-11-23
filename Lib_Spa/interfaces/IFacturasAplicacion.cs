using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IFacturasAplicacion
    {
        Facturas Insertar(Facturas entidad);
        Facturas Actualizar(Facturas entidad);
        List<Facturas> Consultar();
        Facturas Borrar(Facturas entidad);
    }
}