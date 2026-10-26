using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Permisos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PermisosController : ControllerBase
    {
        private IPermisosAplicacion IPermisosAplicacion;

        public PermisosController(IPermisosAplicacion IPermisosAplicacion)
        {
            this.IPermisosAplicacion = IPermisosAplicacion;
        }

        [HttpGet]
        public List<Permisos> Consultar()
        {
            return this.IPermisosAplicacion.Consultar();
        }

        [HttpPost]
        public Permisos Insertar(Permisos entidad)
        {
            return this.IPermisosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Permisos Actualizar(Permisos entidad)
        {
            return this.IPermisosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Permisos Borrar(Permisos entidad)
        {
            return this.IPermisosAplicacion.Borrar(entidad);
        }
    }
}
