using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Auditorias.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuditoriasController : ControllerBase
    {
        private IAuditoriasAplicacion IAuditoriasAplicacion;

        public AuditoriasController(IAuditoriasAplicacion IAuditoriasAplicacion)
        {
            this.IAuditoriasAplicacion = IAuditoriasAplicacion;
        }

        [HttpGet]
        public List<Auditorias> Consultar()
        {
            return this.IAuditoriasAplicacion.Consultar();
        }

        [HttpPost]
        public Auditorias Insertar(Auditorias entidad)
        {
            return this.IAuditoriasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Auditorias Actualizar(Auditorias entidad)
        {
            return this.IAuditoriasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Auditorias Borrar(Auditorias entidad)
        {
            return this.IAuditoriasAplicacion.Borrar(entidad);
        }
    }
}
