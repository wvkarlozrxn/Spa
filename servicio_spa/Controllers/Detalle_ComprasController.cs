using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Detalle_Compras.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Detalle_ComprasController : ControllerBase
    {
        private IDetalle_ComprasAplicacion IDetalle_ComprasAplicacion;

        public Detalle_ComprasController(IDetalle_ComprasAplicacion IDetalle_ComprasAplicacion)
        {
            this.IDetalle_ComprasAplicacion = IDetalle_ComprasAplicacion;
        }

        [HttpGet]
        public List<Detalle_Compras> Consultar()
        {
            return this.IDetalle_ComprasAplicacion.Consultar();
        }

        [HttpPost]
        public Detalle_Compras Insertar(Detalle_Compras entidad)
        {
            return this.IDetalle_ComprasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Detalle_Compras Actualizar(Detalle_Compras entidad)
        {
            return this.IDetalle_ComprasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Detalle_Compras Borrar(Detalle_Compras entidad)
        {
            return this.IDetalle_ComprasAplicacion.Borrar(entidad);
        }
    }
}
