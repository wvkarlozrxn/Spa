using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface ITurnosAplicacion
    {
        Turnos Insertar(Turnos entidad);
        Turnos Actualizar(Turnos entidad);
        List<Turnos> Consultar();
        Turnos Borrar(Turnos entidad);
    }
}