using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IPagosPresentacion
    {
        Pagos Insertar(Pagos entidad);
        Pagos Actualizar(Pagos entidad);
        List<Pagos> Consultar();
        List<Pagos> Buscar(Pagos entidad);
        Pagos Borrar(Pagos entidad);
    }
}