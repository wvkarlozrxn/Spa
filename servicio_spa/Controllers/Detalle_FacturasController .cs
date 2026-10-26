using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Detalle_Facturas.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Detalle_FacturasController : ControllerBase
    {
        private IDetalle_FacturasAplicacion IDetalle_FacturasAplicacion;

        public Detalle_FacturasController(IDetalle_FacturasAplicacion IDetalle_FacturasAplicacion)
        {
            this.IDetalle_FacturasAplicacion = IDetalle_FacturasAplicacion;
        }

        [HttpGet]
        public List<Detalle_Facturas> Consultar()
        {
            return this.IDetalle_FacturasAplicacion.Consultar();
        }

        [HttpPost]
        public Detalle_Facturas Insertar(Detalle_Facturas entidad)
        {
            return this.IDetalle_FacturasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Detalle_Facturas Actualizar(Detalle_Facturas entidad)
        {
            return this.IDetalle_FacturasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Detalle_Facturas Borrar(Detalle_Facturas entidad)
        {
            return this.IDetalle_FacturasAplicacion.Borrar(entidad);
        }
    }
}
