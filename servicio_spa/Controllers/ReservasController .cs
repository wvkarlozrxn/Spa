using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Reservas.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservasController : ControllerBase
    {
        private IReservasAplicacion IReservasAplicacion;

        public ReservasController(IReservasAplicacion IReservasAplicacion)
        {
            this.IReservasAplicacion = IReservasAplicacion;
        }

        [HttpGet]
        public List<Reservas> Consultar()
        {
            return this.IReservasAplicacion.Consultar();
        }

        [HttpPost]
        public Reservas Insertar(Reservas entidad)
        {
            return this.IReservasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Reservas Actualizar(Reservas entidad)
        {
            return this.IReservasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Reservas Borrar(Reservas entidad)
        {
            return this.IReservasAplicacion.Borrar(entidad);
        }
    }
}
