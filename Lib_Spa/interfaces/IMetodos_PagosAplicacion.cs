using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IMetodos_PagosAplicacion
    {
        Metodos_Pagos Insertar(Metodos_Pagos entidad);
        Metodos_Pagos Actualizar(Metodos_Pagos entidad);
        List<Metodos_Pagos> Consultar();
        Metodos_Pagos Borrar(Metodos_Pagos entidad);
    }
}