using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Clientas
{
    public class IndexModel : PageModel
    {
        private IClientesPresentacion IClientesPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;

        public IndexModel(IClientesPresentacion IClientesPresentacion,
            IPersonasPresentacion IPersonasPresentacion)
        {
            this.IClientesPresentacion = IClientesPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
        }

        public List<Clientes> Lista { get; set; } = new();
        public List<Personas> Personas { get; set; } = new();
        public string Mensaje { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public string Buscar { get; set; } = "";

        public void OnGet()
        {
            try
            {
                Lista = this.IClientesPresentacion.Consultar();
                Personas = this.IPersonasPresentacion.Consultar();

                if (!string.IsNullOrWhiteSpace(Buscar))
                {
                    var texto = Buscar.Trim();
                    Lista = Lista.Where(c =>
                    {
                        var persona = PersonaDe(c.Persona);
                        if (persona == null)
                            return false;
                        return (persona.Nombre ?? "").Contains(texto, StringComparison.OrdinalIgnoreCase)
                            || (persona.Cedula ?? "").Contains(texto);
                    }).ToList();
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las clientas.";
            }
        }

        public IActionResult OnPostEliminar(int id)
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador")
                return RedirectToPage("/AccesoDenegado");

            try
            {
                var cliente = this.IClientesPresentacion.Consultar()
                    .FirstOrDefault(c => c.Id_Cliente == id);

                if (cliente != null)
                {
                    this.IClientesPresentacion.Borrar(cliente);

                    try
                    {
                        var persona = this.IPersonasPresentacion.Consultar()
                            .FirstOrDefault(p => p.Id_Persona == cliente.Persona);
                        if (persona != null)
                            this.IPersonasPresentacion.Borrar(persona);
                    }
                    catch (Exception)
                    {
                        // la persona sigue en uso (por ejemplo, también es empleada)
                    }
                }

                return RedirectToPage();
            }
            catch (Exception)
            {
                OnGet();
                Mensaje = "No se pudo eliminar la clienta. Puede que tenga reservas registradas.";
                return Page();
            }
        }

        public Personas? PersonaDe(int idPersona)
        {
            return Personas.FirstOrDefault(p => p.Id_Persona == idPersona);
        }
    }
}
