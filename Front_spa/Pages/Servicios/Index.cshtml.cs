using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Servicios
{
    public class IndexModel : PageModel
    {
        private IServiciosPresentacion IServiciosPresentacion;

        public IndexModel(IServiciosPresentacion IServiciosPresentacion)
        {
            this.IServiciosPresentacion = IServiciosPresentacion;
        }

        public List<Lib_spa.entidades.Servicios> Lista { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            try
            {
                Lista = this.IServiciosPresentacion.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los servicios.";
            }
        }

        public IActionResult OnPostEliminar(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToPage("/AccesoDenegado");

            try
            {
                var servicio = this.IServiciosPresentacion.Consultar()
                    .FirstOrDefault(s => s.Id_Servicio == id);
                if (servicio != null)
                    this.IServiciosPresentacion.Borrar(servicio);

                return RedirectToPage();
            }
            catch (Exception)
            {
                OnGet();
                Mensaje = "No se pudo eliminar el servicio. Puede que ya tenga citas registradas.";
                return Page();
            }
        }
    }
}
