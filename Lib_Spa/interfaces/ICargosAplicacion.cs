using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface ICargosAplicacion
    {
        Cargos Insertar(Cargos entidad);
        Cargos Actualizar(Cargos entidad);
        List<Cargos> Consultar();
        Cargos Borrar(Cargos entidad);
    }
}