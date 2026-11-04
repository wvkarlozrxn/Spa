using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Metodos_Pagos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Metodos_PagosController : ControllerBase
    {
        private IMetodos_PagosAplicacion IMetodos_PagosAplicacion;

        public Metodos_PagosController(IMetodos_PagosAplicacion IMetodos_PagosAplicacion)
        {
            this.IMetodos_PagosAplicacion = IMetodos_PagosAplicacion;
        }

        [HttpGet]
        public List<Metodos_Pagos> Consultar()
        {
            return this.IMetodos_PagosAplicacion.Consultar();
        }

        [HttpPost]
        public Metodos_Pagos Insertar(Metodos_Pagos entidad)
        {
            return this.IMetodos_PagosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Metodos_Pagos Actualizar(Metodos_Pagos entidad)
        {
            return this.IMetodos_PagosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Metodos_Pagos Borrar(Metodos_Pagos entidad)
        {
            return this.IMetodos_PagosAplicacion.Borrar(entidad);
        }
    }
}
