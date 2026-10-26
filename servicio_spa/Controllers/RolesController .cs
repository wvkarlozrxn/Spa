using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Roles.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        private IRolesAplicacion IRolesAplicacion;

        public RolesController(IRolesAplicacion IRolesAplicacion)
        {
            this.IRolesAplicacion = IRolesAplicacion;
        }

        [HttpGet]
        public List<Roles> Consultar()
        {
            return this.IRolesAplicacion.Consultar();
        }

        [HttpPost]
        public Roles Insertar(Roles entidad)
        {
            return this.IRolesAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Roles Actualizar(Roles entidad)
        {
            return this.IRolesAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Roles Borrar(Roles entidad)
        {
            return this.IRolesAplicacion.Borrar(entidad);
        }
    }
}
