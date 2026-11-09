using Lib_spa.entidades;
using Lib_spa.DTOS;

namespace Lib_spa.interfaces
{
    public interface IReservasAplicacion
    {
        Reservas Insertar(Reservas entidad);
        Reservas Actualizar(Reservas entidad);
        List<Reservas> Consultar();
        Reservas Borrar(Reservas entidad);
        ReservaRespuesta Reservar(ReservaRequest datos);
    }
}