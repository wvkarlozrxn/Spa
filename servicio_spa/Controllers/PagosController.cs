using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Pagos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PagosController : ControllerBase
    {
        private IPagosAplicacion IPagosAplicacion;

        public PagosController(IPagosAplicacion IPagosAplicacion)
        {
            this.IPagosAplicacion = IPagosAplicacion;
        }

        [HttpGet]
        public List<Pagos> Consultar()
        {
            return this.IPagosAplicacion.Consultar();
        }

        [HttpPost]
        public Pagos Insertar(Pagos entidad)
        {
            return this.IPagosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Pagos Actualizar(Pagos entidad)
        {
            return this.IPagosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Pagos Borrar(Pagos entidad)
        {
            return this.IPagosAplicacion.Borrar(entidad);
        }
    }
}
