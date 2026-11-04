using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Productos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductosController : ControllerBase
    {
        private IProductosAplicacion IProductosAplicacion;

        public ProductosController(IProductosAplicacion IProductosAplicacion)
        {
            this.IProductosAplicacion = IProductosAplicacion;
        }

        [HttpGet]
        public List<Productos> Consultar()
        {
            return this.IProductosAplicacion.Consultar();
        }

        [HttpPost]
        public Productos Insertar(Productos entidad)
        {
            return this.IProductosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Productos Actualizar(Productos entidad)
        {
            return this.IProductosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Productos Borrar(Productos entidad)
        {
            return this.IProductosAplicacion.Borrar(entidad);
        }
    }
}
