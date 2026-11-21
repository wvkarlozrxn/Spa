using System.Text.RegularExpressions;
using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Usuarios
{
    [SoloAdministrador]
    public class CrearModel : PageModel
    {
        private IUsuariosPresentacion IUsuariosPresentacion;
        private IRolesPresentacion IRolesPresentacion;

        public CrearModel(IUsuariosPresentacion IUsuariosPresentacion,
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

        public void OnGet()
        {
            CargarRoles();
        }

        public IActionResult OnPost()
        {
            CargarRoles();

            if (string.IsNullOrWhiteSpace(Usuario) ||
                string.IsNullOrWhiteSpace(Contrasena) ||
                IdRol == 0)
            {
                Mensaje = "Todos los campos son obligatorios.";
                return Page();
            }

            if (!Regex.IsMatch(Usuario, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Mensaje = "El usuario solo debe contener letras.";
                return Page();
            }

            try
            {
                var existe = this.IUsuariosPresentacion.Consultar()
                    .Any(u => u.Usuario == Usuario);
                if (existe)
                {
                    Mensaje = "Ya existe un usuario con ese nombre.";
                    return Page();
                }

                var nuevo = new Lib_spa.entidades.Usuarios();
                nuevo.Usuario = Usuario;
                nuevo.Contraseña = Contrasena;
                nuevo.Id_Rol = IdRol;
                this.IUsuariosPresentacion.Insertar(nuevo);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo guardar el usuario.";
                return Page();
            }
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
