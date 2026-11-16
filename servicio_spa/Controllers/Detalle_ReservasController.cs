using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
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

        [HttpPost]
        public List<Detalle_Reservas> Consultar()
        {
            return this.IDetalle_ReservasAplicacion.Consultar();
        }

        [HttpPost]
        public Detalle_Reservas Insertar(Detalle_Reservas entidad)
        {
            return this.IDetalle_ReservasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Detalle_Reservas Actualizar(Detalle_Reservas entidad)
        {
            return this.IDetalle_ReservasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Detalle_Reservas Borrar(Detalle_Reservas entidad)
        {
            return this.IDetalle_ReservasAplicacion.Borrar(entidad);
        }
    }
}
