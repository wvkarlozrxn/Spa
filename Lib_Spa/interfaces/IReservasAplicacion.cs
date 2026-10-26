using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IReservasAplicacion
    {
        Reservas Insertar(Reservas entidad);
        Reservas Actualizar(Reservas entidad);
        List<Reservas> Consultar();
        Reservas Borrar(Reservas entidad);
    }
}