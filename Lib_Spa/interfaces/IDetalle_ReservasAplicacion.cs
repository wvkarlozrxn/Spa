using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IDetalle_ReservasAplicacion
    {
        Detalle_Reservas Insertar(Detalle_Reservas entidad);
        Detalle_Reservas Actualizar(Detalle_Reservas entidad);
        List<Detalle_Reservas> Consultar();
        Detalle_Reservas Borrar(Detalle_Reservas entidad);
    }
}