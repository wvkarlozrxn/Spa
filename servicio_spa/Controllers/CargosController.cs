using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CargosController : ControllerBase
    {
        private ICargosAplicacion ICargosAplicacion;

        public CargosController(ICargosAplicacion ICargosAplicacion)
        {
            this.ICargosAplicacion = ICargosAplicacion;
        }

        [HttpPost]
        public List<Cargos> Consultar()
        {
           return this.ICargosAplicacion.Consultar();
        }

        [HttpPost]
        public Cargos Insertar(Cargos entidad)
        {
            return this.ICargosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Cargos Actualizar(Cargos entidad)
        {
            return this.ICargosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Cargos Borrar(Cargos entidad)
        {
            return this.ICargosAplicacion.Borrar(entidad);
        }
    }
}
