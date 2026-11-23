using Lib_spa.DTOS;
using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
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

        [HttpPost]
        public List<Usuarios> Consultar()
        {
            return this.IUsuariosAplicacion.Consultar();
        }

        [HttpPost]
        public Usuarios Insertar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Usuarios Actualizar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Usuarios? Borrar(Usuarios entidad)
        {
            try
            {
                return this.IUsuariosAplicacion.Borrar(entidad);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public Usuarios? Validar(UsuariosRequest datos)
        {
            try
            {
                return this.IUsuariosAplicacion.Validar(datos);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
