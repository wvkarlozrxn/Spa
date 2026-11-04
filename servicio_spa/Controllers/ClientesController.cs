using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Clientes.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClientesController : ControllerBase
    {
        private IClientesAplicacion IClientesAplicacion;

        public ClientesController(IClientesAplicacion IClientesAplicacion)
        {
            this.IClientesAplicacion = IClientesAplicacion;
        }

        [HttpGet]
        public List<Clientes> Consultar()
        {
            return this.IClientesAplicacion.Consultar();
        }

        [HttpPost]
        public Clientes Insertar(Clientes entidad)
        {
            return this.IClientesAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Clientes Actualizar(Clientes entidad)
        {
            return this.IClientesAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Clientes Borrar(Clientes entidad)
        {
            return this.IClientesAplicacion.Borrar(entidad);
        }
    }
}
