using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IReservasPresentacion
    {
        Reservas Insertar(Reservas entidad);
        Reservas Actualizar(Reservas entidad);
        List<Reservas> Consultar();
        List<Reservas> Buscar(Reservas entidad);
        Reservas Borrar(Reservas entidad);
    }
}