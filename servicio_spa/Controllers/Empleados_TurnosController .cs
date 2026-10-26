using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_Empleados_Turnos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Empleados_TurnosController : ControllerBase
    {
        private IEmpleados_TurnosAplicacion IEmpleados_TurnosAplicacion;

        public Empleados_TurnosController(IEmpleados_TurnosAplicacion IEmpleados_TurnosAplicacion)
        {
            this.IEmpleados_TurnosAplicacion = IEmpleados_TurnosAplicacion;
        }

        [HttpGet]
        public List<Empleados_Turnos> Consultar()
        {
            return this.IEmpleados_TurnosAplicacion.Consultar();
        }

        [HttpPost]
        public Empleados_Turnos Insertar(Empleados_Turnos entidad)
        {
            return this.IEmpleados_TurnosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Empleados_Turnos Actualizar(Empleados_Turnos entidad)
        {
            return this.IEmpleados_TurnosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Empleados_Turnos Borrar(Empleados_Turnos entidad)
        {
            return this.IEmpleados_TurnosAplicacion.Borrar(entidad);
        }
    }
}
