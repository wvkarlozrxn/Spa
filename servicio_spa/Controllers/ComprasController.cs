using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Compras.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ComprasController : ControllerBase
    {
        private IComprasAplicacion IComprasAplicacion;

        public ComprasController(IComprasAplicacion IComprasAplicacion)
        {
            this.IComprasAplicacion = IComprasAplicacion;
        }

        [HttpGet]
        public List<Compras> Consultar()
        {
            return this.IComprasAplicacion.Consultar();
        }

        [HttpPost]
        public Compras Insertar(Compras entidad)
        {
            return this.IComprasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Compras Actualizar(Compras entidad)
        {
            return this.IComprasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Compras Borrar(Compras entidad)
        {
            return this.IComprasAplicacion.Borrar(entidad);
        }
    }
}
