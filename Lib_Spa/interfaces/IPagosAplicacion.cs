using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IPagosAplicacion
    {
        Pagos Insertar(Pagos entidad);
        Pagos Actualizar(Pagos entidad);
        List<Pagos> Consultar();
        Pagos Borrar(Pagos entidad);
    }
}