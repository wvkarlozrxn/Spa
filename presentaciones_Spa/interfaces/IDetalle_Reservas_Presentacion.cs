using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IDetalle_Reservas_Presentacion
    {
        Detalle_Reservas Insertar(Detalle_Reservas entidad);
        Detalle_Reservas Actualizar(Detalle_Reservas entidad);
        List<Detalle_Reservas> Consultar();
        List<Detalle_Reservas> Buscar(Detalle_Reservas entidad);
        Detalle_Reservas Borrar(Detalle_Reservas entidad);
    }
}