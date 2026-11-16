using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
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

        [HttpPost]
        public List<Empleados> Consultar()
        {
            return this.IEmpleadosAplicacion.Consultar();
        }

        [HttpPost]
        public Empleados Insertar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Empleados Actualizar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Empleados Borrar(Empleados entidad)
        {
            return this.IEmpleadosAplicacion.Borrar(entidad);
        }
    }
}
