using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Auditoria = Lib_spa.entidades.Auditorias;

namespace Front_spa.Pages.Auditorias
{
    [SoloAdministrador]
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Auditoria> Lista { get; set; } = new();
        public List<string> ListaEntidades { get; set; } = new();
        [BindProperty] public string FiltroEntidad { get; set; } = "";
        [BindProperty] public string FiltroAccion { get; set; } = "";
        [BindProperty] public string FiltroUsuario { get; set; } = "";
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                var todas = Datos.Auditorias.Consultar();
                ListaEntidades = todas.Select(x => x.Entidad ?? "").Distinct().OrderBy(x => x).ToList();

                Lista = todas.OrderByDescending(x => x.Fecha).ToList();

                if (!string.IsNullOrEmpty(FiltroEntidad))
                    Lista = Lista.Where(x => x.Entidad == FiltroEntidad).ToList();

                if (!string.IsNullOrEmpty(FiltroAccion))
                    Lista = Lista.Where(x => x.Accion == FiltroAccion).ToList();

                if (!string.IsNullOrWhiteSpace(FiltroUsuario))
                    Lista = Lista.Where(x => (x.Usuario ?? "").Contains(FiltroUsuario.Trim(),
                        StringComparison.OrdinalIgnoreCase)).ToList();

                
                Lista = Lista.Take(200).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las auditorías.";
            }
        }
    }
}
