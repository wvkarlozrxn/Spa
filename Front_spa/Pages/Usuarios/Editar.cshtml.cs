using System.Text.RegularExpressions;
using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Usuarios
{
    [SoloAdministrador]
    public class EditarModel : PageModel
    {
        private IUsuariosPresentacion IUsuariosPresentacion;
        private IRolesPresentacion IRolesPresentacion;

        public EditarModel(IUsuariosPresentacion IUsuariosPresentacion,
            IRolesPresentacion IRolesPresentacion)
        {
            this.IUsuariosPresentacion = IUsuariosPresentacion;
            this.IRolesPresentacion = IRolesPresentacion;
        }

        [BindProperty]
        public string Usuario { get; set; } = "";

        [BindProperty]
        public string Contrasena { get; set; } = "";

        [BindProperty]
        public int IdRol { get; set; }

        public List<SelectListItem> Roles { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public IActionResult OnGet(int id)
        {
            CargarRoles();

            var usuario = Buscar(id);
            if (usuario == null)
                return RedirectToPage("Index");

            Usuario = usuario.Usuario ?? "";
            IdRol = usuario.Id_Rol;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            CargarRoles();

            if (string.IsNullOrWhiteSpace(Usuario) || IdRol == 0)
            {
                Mensaje = "El usuario y el rol son obligatorios.";
                return Page();
            }

            if (!Regex.IsMatch(Usuario, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Mensaje = "El usuario solo debe contener letras.";
                return Page();
            }

            try
            {
                var usuario = Buscar(id);
                if (usuario == null)
                    return RedirectToPage("Index");

                var idActual = HttpContext.Session.GetString("IdUsuario");
                if (idActual == id.ToString() && IdRol != usuario.Id_Rol)
                {
                    Mensaje = "No puedes cambiar tu propio rol.";
                    return Page();
                }

                var repetido = this.IUsuariosPresentacion.Consultar()
                    .Any(u => u.Usuario == Usuario && u.Id_Usuario != id);
                if (repetido)
                {
                    Mensaje = "Ya existe un usuario con ese nombre.";
                    return Page();
                }

                usuario.Usuario = Usuario;
                usuario.Id_Rol = IdRol;
                if (!string.IsNullOrWhiteSpace(Contrasena))
                    usuario.Contraseña = Contrasena;
                this.IUsuariosPresentacion.Actualizar(usuario);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo actualizar el usuario.";
                return Page();
            }
        }

        private Lib_spa.entidades.Usuarios? Buscar(int id)
        {
            return this.IUsuariosPresentacion.Consultar()
                .FirstOrDefault(u => u.Id_Usuario == id);
        }

        private void CargarRoles()
        {
            try
            {
                foreach (var rol in this.IRolesPresentacion.Consultar())
                    Roles.Add(new SelectListItem(rol.Rol, rol.Id_Rol.ToString()));
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los roles.";
            }
        }
    }
}
