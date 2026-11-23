using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IAuditoriasAplicacion
    {
        Auditorias Insertar(Auditorias entidad);
        Auditorias Actualizar(Auditorias entidad);
        List<Auditorias> Consultar();
        Auditorias Borrar(Auditorias entidad);
    }
}