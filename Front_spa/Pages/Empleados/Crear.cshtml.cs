using System.Text.RegularExpressions;
using Lib_spa.entidades;
using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Empleados
{
    [SoloAdministrador]
    public class CrearModel : PageModel
    {
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private ICargosPresentacion ICargosPresentacion;

        public CrearModel(IEmpleadosPresentacion IEmpleadosPresentacion,
            IPersonasPresentacion IPersonasPresentacion,
            ICargosPresentacion ICargosPresentacion)
        {
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.ICargosPresentacion = ICargosPresentacion;
        }

        [BindProperty]
        public string Nombre { get; set; } = "";

        [BindProperty]
        public string Telefono { get; set; } = "";

        [BindProperty]
        public string Cedula { get; set; } = "";

        [BindProperty]
        public int IdCargo { get; set; }

        [BindProperty]
        public DateTime FechaContrato { get; set; }

        public List<SelectListItem> Cargos { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            FechaContrato = DateTime.Today;
            CargarCargos();
        }

        public IActionResult OnPost()
        {
            CargarCargos();

            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Telefono) ||
                string.IsNullOrWhiteSpace(Cedula) ||
                IdCargo == 0)
            {
                Mensaje = "Todos los campos son obligatorios.";
                return Page();
            }

            if (!Regex.IsMatch(Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Mensaje = "El nombre solo debe contener letras.";
                return Page();
            }

            if (!Regex.IsMatch(Telefono, @"^\d+$") || !Regex.IsMatch(Cedula, @"^\d+$"))
            {
                Mensaje = "El teléfono y la cédula solo deben contener números.";
                return Page();
            }

            try
            {
                var persona = new Personas();
                persona.Nombre = Nombre;
                persona.Telefono = Telefono;
                persona.Cedula = Cedula;
                persona = this.IPersonasPresentacion.Insertar(persona);

                var empleado = new Lib_spa.entidades.Empleados();
                empleado.Id_Persona = persona.Id_Persona;
                empleado.Id_Cargo = IdCargo;
                empleado.Fecha_Contrato = FechaContrato;
                this.IEmpleadosPresentacion.Insertar(empleado);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo guardar el empleado.";
                return Page();
            }
        }

        private void CargarCargos()
        {
            try
            {
                foreach (var cargo in this.ICargosPresentacion.Consultar())
                    Cargos.Add(new SelectListItem(cargo.Cargo, cargo.Id_Cargo.ToString()));
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los cargos.";
            }
        }
    }
}
