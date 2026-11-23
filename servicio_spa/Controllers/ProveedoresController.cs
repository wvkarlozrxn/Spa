using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProveedoresController : ControllerBase
    {
        private IProveedoresAplicacion IProveedoresAplicacion;

        public ProveedoresController(IProveedoresAplicacion IProveedoresAplicacion)
        {
            this.IProveedoresAplicacion = IProveedoresAplicacion;
        }

        [HttpPost]
        public List<Proveedores> Consultar()
        {
            return this.IProveedoresAplicacion.Consultar();
        }

        [HttpPost]
        public Proveedores Insertar(Proveedores entidad)
        {
            return this.IProveedoresAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Proveedores Actualizar(Proveedores entidad)
        {
            return this.IProveedoresAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Proveedores? Borrar(Proveedores entidad)
        {
            try
            {
                return this.IProveedoresAplicacion.Borrar(entidad);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
