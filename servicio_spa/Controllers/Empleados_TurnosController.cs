using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_spa.Controllers
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

        [HttpPost]
        public List<Empleados_Turnos> Consultar()
        {
            return this.IEmpleados_TurnosAplicacion.Consultar();
        }

        [HttpPost]
        public Empleados_Turnos Insertar(Empleados_Turnos entidad)
        {
            return this.IEmpleados_TurnosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Empleados_Turnos Actualizar(Empleados_Turnos entidad)
        {
            return this.IEmpleados_TurnosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Empleados_Turnos Borrar(Empleados_Turnos entidad)
        {
            return this.IEmpleados_TurnosAplicacion.Borrar(entidad);
        }
    }
}
