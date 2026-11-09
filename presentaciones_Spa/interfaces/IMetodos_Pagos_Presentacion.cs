using Lib_spa.entidades;
namespace presentaciones_Spa.interfaces
{
    public interface IMetodos_Pagos_Presentacion
    {
        Metodos_Pagos Insertar(Metodos_Pagos entidad);
        Metodos_Pagos Actualizar(Metodos_Pagos entidad);
        List<Metodos_Pagos> Consultar();
        List<Metodos_Pagos> Buscar(Metodos_Pagos entidad);
        Metodos_Pagos Borrar(Metodos_Pagos entidad);
    }
}