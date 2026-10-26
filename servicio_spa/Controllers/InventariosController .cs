using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventariosController : ControllerBase
    {
        private IInventariosAplicacion IInventariosAplicacion;

        public InventariosController(IInventariosAplicacion IInventariosAplicacion)
        {
            this.IInventariosAplicacion = IInventariosAplicacion;
        }

        [HttpGet]
        public List<Inventarios> Consultar()
        {
            return this.IInventariosAplicacion.Consultar();
        }

        [HttpPost]
        public Inventarios Insertar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Inventarios Actualizar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Inventarios Borrar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Borrar(entidad);
        }
    }
}
