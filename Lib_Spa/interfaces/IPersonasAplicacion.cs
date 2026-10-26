using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IPersonasAplicacion
    {
        Personas Insertar(Personas entidad);
        Personas Actualizar(Personas entidad);
        List<Personas> Consultar();
        Personas Borrar(Personas entidad);
    }
}