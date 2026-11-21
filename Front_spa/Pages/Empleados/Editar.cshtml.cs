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
    public class EditarModel : PageModel
    {
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private ICargosPresentacion ICargosPresentacion;

        public EditarModel(IEmpleadosPresentacion IEmpleadosPresentacion,
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

        public IActionResult OnGet(int id)
        {
            CargarCargos();

            var empleado = BuscarEmpleado(id);
            if (empleado == null)
                return RedirectToPage("Index");

            var persona = BuscarPersona(empleado.Id_Persona);
            Nombre = persona?.Nombre ?? "";
            Telefono = persona?.Telefono ?? "";
            Cedula = persona?.Cedula ?? "";
            IdCargo = empleado.Id_Cargo;
            FechaContrato = empleado.Fecha_Contrato;
            return Page();
        }

        public IActionResult OnPost(int id)
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
                var empleado = BuscarEmpleado(id);
                if (empleado == null)
                    return RedirectToPage("Index");

                var persona = BuscarPersona(empleado.Id_Persona);
                if (persona == null)
                    return RedirectToPage("Index");

                persona.Nombre = Nombre;
                persona.Telefono = Telefono;
                persona.Cedula = Cedula;
                this.IPersonasPresentacion.Actualizar(persona);

                empleado.Id_Cargo = IdCargo;
                empleado.Fecha_Contrato = FechaContrato;
                this.IEmpleadosPresentacion.Actualizar(empleado);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo actualizar el empleado.";
                return Page();
            }
        }

        private Lib_spa.entidades.Empleados? BuscarEmpleado(int id)
        {
            return this.IEmpleadosPresentacion.Consultar()
                .FirstOrDefault(e => e.Id_Empleado == id);
        }

        private Personas? BuscarPersona(int idPersona)
        {
            return this.IPersonasPresentacion.Consultar()
                .FirstOrDefault(p => p.Id_Persona == idPersona);
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
