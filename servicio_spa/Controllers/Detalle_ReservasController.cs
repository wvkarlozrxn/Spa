using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Detalle_Reservas.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Detalle_ReservasController : ControllerBase
    {
        private IDetalle_ReservasAplicacion IDetalle_ReservasAplicacion;

        public Detalle_ReservasController(IDetalle_ReservasAplicacion IDetalle_ReservasAplicacion)
        {
            this.IDetalle_ReservasAplicacion = IDetalle_ReservasAplicacion;
        }

        [HttpGet]
        public List<Detalle_Reservas> Consultar()
        {
            return this.IDetalle_ReservasAplicacion.Consultar();
        }

        [HttpPost]
        public Detalle_Reservas Insertar(Detalle_Reservas entidad)
        {
            return this.IDetalle_ReservasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Detalle_Reservas Actualizar(Detalle_Reservas entidad)
        {
            return this.IDetalle_ReservasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Detalle_Reservas Borrar(Detalle_Reservas entidad)
        {
            return this.IDetalle_ReservasAplicacion.Borrar(entidad);
        }
    }
}
