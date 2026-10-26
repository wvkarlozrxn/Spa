using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Usuarios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosAplicacion IUsuariosAplicacion;

        public UsuariosController(IUsuariosAplicacion IUsuariosAplicacion)
        {
            this.IUsuariosAplicacion = IUsuariosAplicacion;
        }

        [HttpGet]
        public List<Usuarios> Consultar()
        {
            return this.IUsuariosAplicacion.Consultar();
        }

        [HttpPost]
        public Usuarios Insertar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Usuarios Actualizar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Usuarios Borrar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Borrar(entidad);
        }
    }
}
