using System.Text.RegularExpressions;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Clientas
{
    public class CrearModel : PageModel
    {
        private IClientesPresentacion IClientesPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;

        public CrearModel(IClientesPresentacion IClientesPresentacion,
            IPersonasPresentacion IPersonasPresentacion)
        {
            this.IClientesPresentacion = IClientesPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
        }

        [BindProperty]
        public string Nombre { get; set; } = "";

        [BindProperty]
        public string Telefono { get; set; } = "";

        [BindProperty]
        public string Cedula { get; set; } = "";

        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Telefono) ||
                string.IsNullOrWhiteSpace(Cedula))
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

                var cliente = new Clientes();
                cliente.Persona = persona.Id_Persona;
                this.IClientesPresentacion.Insertar(cliente);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo guardar la clienta.";
                return Page();
            }
        }
    }
}
