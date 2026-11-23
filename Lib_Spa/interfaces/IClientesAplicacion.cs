using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IClientesAplicacion
    {
        Clientes Insertar(Clientes entidad);
        Clientes Actualizar(Clientes entidad);
        List<Clientes> Consultar();
        Clientes Borrar(Clientes entidad);
    }
}