using Lib_spa.entidades;

namespace Lib_spa.interfaces
{
    public interface IProveedoresAplicacion
    {
        Proveedores Insertar(Proveedores entidad);
        Proveedores Actualizar(Proveedores entidad);
        List<Proveedores> Consultar();
        Proveedores Borrar(Proveedores entidad);
    }
}