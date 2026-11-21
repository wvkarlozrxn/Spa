using System.Text.RegularExpressions;
using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Servicios
{
    [SoloAdministrador]
    public class EditarModel : PageModel
    {
        private IServiciosPresentacion IServiciosPresentacion;

        public EditarModel(IServiciosPresentacion IServiciosPresentacion)
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

        public IActionResult OnGet(int id)
        {
            var servicio = Buscar(id);
            if (servicio == null)
                return RedirectToPage("Index");

            Nombre = servicio.Nombre ?? "";
            Descripcion = servicio.Descripcion ?? "";
            Duracion = servicio.Duracion;
            Precio = (int)servicio.Precio;
            return Page();
        }

        public IActionResult OnPost(int id)
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
                var servicio = Buscar(id);
                if (servicio == null)
                    return RedirectToPage("Index");

                servicio.Nombre = Nombre;
                servicio.Descripcion = Descripcion;
                servicio.Duracion = Duracion;
                servicio.Precio = Precio;
                this.IServiciosPresentacion.Actualizar(servicio);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo actualizar el servicio.";
                return Page();
            }
        }

        private Lib_spa.entidades.Servicios? Buscar(int id)
        {
            return this.IServiciosPresentacion.Consultar()
                .FirstOrDefault(s => s.Id_Servicio == id);
        }
    }
}
