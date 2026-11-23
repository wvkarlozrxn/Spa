using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
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

        [HttpPost]
        public List<Inventarios> Consultar()
        {
            return this.IInventariosAplicacion.Consultar();
        }

        [HttpPost]
        public Inventarios Insertar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Inventarios Actualizar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Inventarios? Borrar(Inventarios entidad)
        {
            try
            {
                return this.IInventariosAplicacion.Borrar(entidad);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
