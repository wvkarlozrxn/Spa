using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
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

        [HttpPost]
        public List<Turnos> Consultar()
        {
            return this.ITurnosAplicacion.Consultar();
        }

        [HttpPost]
        public Turnos Insertar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Turnos Actualizar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Turnos Borrar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Borrar(entidad);
        }
    }
}
