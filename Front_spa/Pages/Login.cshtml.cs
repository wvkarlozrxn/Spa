using Lib_spa.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_spa.Pages
{
    public class LoginModel : PageModel
    {
        public Datos Datos;

        public LoginModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        [BindProperty]
        public string Usuario { get; set; } = "";

        [BindProperty]
        public string Contrasena { get; set; } = "";

        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Contrasena))
            {
                Mensaje = "Escribe el usuario y la contraseña.";
                return Page();
            }

            try
            {
                var datos = new UsuariosRequest();
                datos.Usuario = Usuario;
                datos.Contraseña = Contrasena;

                var usuario = Datos.Usuarios.Validar(datos);

                var roles = Datos.Roles.Consultar();
                var rol = roles.FirstOrDefault(r => r.Id_Rol == usuario.Id_Rol);

                HttpContext.Session.SetString("IdUsuario", usuario.Id_Usuario.ToString());
                HttpContext.Session.SetString("Usuario", usuario.Usuario!);
                HttpContext.Session.SetString("Rol", rol?.Rol ?? "");

                return RedirectToPage("/Index");
            }
            catch (Exception)
            {
                Mensaje = "Usuario o contraseña incorrectos.";
                return Page();
            }
        }
    }
}
