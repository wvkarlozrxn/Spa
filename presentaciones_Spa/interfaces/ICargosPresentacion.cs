using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface ICargosPresentacion
    {
        Cargos Insertar(Cargos entidad);
        Cargos Actualizar(Cargos entidad);
        List<Cargos> Consultar();
        List<Cargos> Buscar(Cargos entidad);
        Cargos Borrar(Cargos entidad);
    }
}