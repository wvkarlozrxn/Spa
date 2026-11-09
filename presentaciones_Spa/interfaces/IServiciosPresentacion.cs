using Lib_spa.entidades;

namespace presentaciones_Spa.interfaces
{
    public interface IServiciosPresentacion
    {
        Servicios Insertar(Servicios entidad);
        Servicios Actualizar(Servicios entidad);
        List<Servicios> Consultar();
        List<Servicios> Buscar(Servicios entidad);
        Servicios Borrar(Servicios entidad);
    }
}