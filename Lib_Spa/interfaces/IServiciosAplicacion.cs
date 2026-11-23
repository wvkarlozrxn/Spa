using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IServiciosAplicacion
    {
        Servicios Insertar(Servicios entidad);
        Servicios Actualizar(Servicios entidad);
        List<Servicios> Consultar();
        Servicios Borrar(Servicios entidad);
    }
}