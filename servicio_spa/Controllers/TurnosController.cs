using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Turnos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TurnosController : ControllerBase
    {
        private ITurnosAplicacion ITurnosAplicacion;

        public TurnosController(ITurnosAplicacion ITurnosAplicacion)
        {
            this.ITurnosAplicacion = ITurnosAplicacion;
        }

        [HttpGet]
        public List<Turnos> Consultar()
        {
            return this.ITurnosAplicacion.Consultar();
        }

        [HttpPost]
        public Turnos Insertar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Turnos Actualizar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Turnos Borrar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Borrar(entidad);
        }
    }
}
