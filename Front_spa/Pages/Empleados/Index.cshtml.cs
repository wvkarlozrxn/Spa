using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Empleados
{
    public class IndexModel : PageModel
    {
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private ICargosPresentacion ICargosPresentacion;

        public IndexModel(IEmpleadosPresentacion IEmpleadosPresentacion,
            IPersonasPresentacion IPersonasPresentacion,
            ICargosPresentacion ICargosPresentacion)
        {
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.ICargosPresentacion = ICargosPresentacion;
        }

        public List<Lib_spa.entidades.Empleados> Lista { get; set; } = new();
        public List<Personas> Personas { get; set; } = new();
        public List<Cargos> Cargos { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            try
            {
                Lista = this.IEmpleadosPresentacion.Consultar();
                Personas = this.IPersonasPresentacion.Consultar();
                Cargos = this.ICargosPresentacion.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los empleados.";
            }
        }

        public IActionResult OnPostEliminar(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToPage("/AccesoDenegado");

            try
            {
                var empleado = this.IEmpleadosPresentacion.Consultar()
                    .FirstOrDefault(e => e.Id_Empleado == id);

                if (empleado != null)
                {
                    this.IEmpleadosPresentacion.Borrar(empleado);

                    try
                    {
                        var persona = this.IPersonasPresentacion.Consultar()
                            .FirstOrDefault(p => p.Id_Persona == empleado.Id_Persona);
                        if (persona != null)
                            this.IPersonasPresentacion.Borrar(persona);
                    }
                    catch (Exception)
                    {
                        // la persona sigue en uso (por ejemplo, también es clienta)
                    }
                }

                return RedirectToPage();
            }
            catch (Exception)
            {
                OnGet();
                Mensaje = "No se pudo eliminar el empleado. Puede que tenga citas registradas.";
                return Page();
            }
        }

        public string NombreDe(int idPersona)
        {
            var persona = Personas.FirstOrDefault(p => p.Id_Persona == idPersona);
            return persona?.Nombre ?? "";
        }

        public string TelefonoDe(int idPersona)
        {
            var persona = Personas.FirstOrDefault(p => p.Id_Persona == idPersona);
            return persona?.Telefono ?? "";
        }

        public string CargoDe(int idCargo)
        {
            var cargo = Cargos.FirstOrDefault(c => c.Id_Cargo == idCargo);
            return cargo?.Cargo ?? "";
        }
    }
}
