using Lib_spa.entidades;
using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Usuarios
{
    [SoloAdministrador]
    public class IndexModel : PageModel
    {
        private IUsuariosPresentacion IUsuariosPresentacion;
        private IRolesPresentacion IRolesPresentacion;

        public IndexModel(IUsuariosPresentacion IUsuariosPresentacion,
            IRolesPresentacion IRolesPresentacion)
        {
            this.IUsuariosPresentacion = IUsuariosPresentacion;
            this.IRolesPresentacion = IRolesPresentacion;
        }

        public List<Lib_spa.entidades.Usuarios> Lista { get; set; } = new();
        public List<Roles> Roles { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            try
            {
                Lista = this.IUsuariosPresentacion.Consultar();
                Roles = this.IRolesPresentacion.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los usuarios.";
            }
        }

        public IActionResult OnPostEliminar(int id)
        {
            var idActual = HttpContext.Session.GetString("IdUsuario");
            if (idActual == id.ToString())
            {
                OnGet();
                Mensaje = "No puedes eliminar tu propio usuario.";
                return Page();
            }

            try
            {
                var usuario = this.IUsuariosPresentacion.Consultar()
                    .FirstOrDefault(u => u.Id_Usuario == id);
                if (usuario != null)
                    this.IUsuariosPresentacion.Borrar(usuario);

                return RedirectToPage();
            }
            catch (Exception)
            {
                OnGet();
                Mensaje = "No se pudo eliminar el usuario.";
                return Page();
            }
        }

        public string NombreRol(int idRol)
        {
            return Roles.FirstOrDefault(r => r.Id_Rol == idRol)?.Rol ?? "";
        }
    }
}
