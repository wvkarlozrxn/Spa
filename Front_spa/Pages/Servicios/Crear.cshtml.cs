using System.Text.RegularExpressions;
using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Servicios
{
    [SoloAdministrador]
    public class CrearModel : PageModel
    {
        private IServiciosPresentacion IServiciosPresentacion;

        public CrearModel(IServiciosPresentacion IServiciosPresentacion)
        {
            this.IServiciosPresentacion = IServiciosPresentacion;
        }

        [BindProperty]
        public string Nombre { get; set; } = "";

        [BindProperty]
        public string Descripcion { get; set; } = "";

        [BindProperty]
        public int Duracion { get; set; }

        [BindProperty]
        public int Precio { get; set; }

        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Nombre) ||
                string.IsNullOrWhiteSpace(Descripcion) ||
                Duracion <= 0 || Precio <= 0)
            {
                Mensaje = "Todos los campos son obligatorios y la duración y el precio deben ser mayores que cero.";
                return Page();
            }

            if (!Regex.IsMatch(Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Mensaje = "El nombre solo debe contener letras.";
                return Page();
            }

            try
            {
                var servicio = new Lib_spa.entidades.Servicios();
                servicio.Nombre = Nombre;
                servicio.Descripcion = Descripcion;
                servicio.Duracion = Duracion;
                servicio.Precio = Precio;
                this.IServiciosPresentacion.Insertar(servicio);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo guardar el servicio.";
                return Page();
            }
        }
    }
}
