using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Proveedores.Controllers
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

        [HttpGet]
        public List<Proveedores> Consultar()
        {
            return this.IProveedoresAplicacion.Consultar();
        }

        [HttpPost]
        public Proveedores Insertar(Proveedores entidad)
        {
            return this.IProveedoresAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Proveedores Actualizar(Proveedores entidad)
        {
            return this.IProveedoresAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Proveedores Borrar(Proveedores entidad)
        {
            return this.IProveedoresAplicacion.Borrar(entidad);
        }
    }
}
