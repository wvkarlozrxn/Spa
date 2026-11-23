using Lib_spa.entidades;

namespace presentaciones_Spa.implementaciones
{
    public interface IAuditoriasPresentacion
    {
        Auditorias Insertar(Auditorias entidad);
        Auditorias Actualizar(Auditorias entidad);
        List<Auditorias> Consultar();
        List<Auditorias> Buscar(Auditorias entidad);
        Auditorias Borrar(Auditorias entidad);
    }
}