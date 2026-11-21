using System.Text.RegularExpressions;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Clientas
{
    public class EditarModel : PageModel
    {
        private IClientesPresentacion IClientesPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;

        public EditarModel(IClientesPresentacion IClientesPresentacion,
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

        public IActionResult OnGet(int id)
        {
            var persona = BuscarPersona(id);
            if (persona == null)
                return RedirectToPage("Index");

            Nombre = persona.Nombre ?? "";
            Telefono = persona.Telefono ?? "";
            Cedula = persona.Cedula ?? "";
            return Page();
        }

        public IActionResult OnPost(int id)
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
                var persona = BuscarPersona(id);
                if (persona == null)
                    return RedirectToPage("Index");

                persona.Nombre = Nombre;
                persona.Telefono = Telefono;
                persona.Cedula = Cedula;
                this.IPersonasPresentacion.Actualizar(persona);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo actualizar la clienta.";
                return Page();
            }
        }

        private Personas? BuscarPersona(int idCliente)
        {
            var cliente = this.IClientesPresentacion.Consultar()
                .FirstOrDefault(c => c.Id_Cliente == idCliente);
            if (cliente == null)
                return null;

            return this.IPersonasPresentacion.Consultar()
                .FirstOrDefault(p => p.Id_Persona == cliente.Persona);
        }
    }
}
