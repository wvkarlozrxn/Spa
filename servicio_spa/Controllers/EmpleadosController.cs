using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Empleados.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpleadosController : ControllerBase
    {
        private IEmpleadosAplicacion IEmpleadosAplicacion;

        public EmpleadosController(IEmpleadosAplicacion IEmpleadosAplicacion)
        {
            this.IEmpleadosAplicacion = IEmpleadosAplicacion;
        }

        [HttpGet]
        public List<Empleados> Consultar()
        {
            return this.IEmpleadosAplicacion.Consultar();
        }

        [HttpPost]
        public Empleados Insertar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Empleados Actualizar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Empleados Borrar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Borrar(entidad);
        }
    }
}
