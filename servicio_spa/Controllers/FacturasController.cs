using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Facturas.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturasController : ControllerBase
    {
        private IFacturasAplicacion IFacturasAplicacion;

        public FacturasController(IFacturasAplicacion IFacturasAplicacion)
        {
            this.IFacturasAplicacion = IFacturasAplicacion;
        }

        [HttpGet]
        public List<Facturas> Consultar()
        {
            return this.IFacturasAplicacion.Consultar();
        }

        [HttpPost]
        public Facturas Insertar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Facturas Actualizar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Facturas Borrar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Borrar(entidad);
        }
    }
}
