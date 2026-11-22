using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Seguridad
{
    // Se ejecuta antes de abrir cualquier página: si no hay sesión, manda al login.
    public class FiltroSesion : IAuthorizationFilter
    {
        public static readonly string[] Libres = { "/Login", "/AccesoDenegado", "/Error" };

        public void OnAuthorization(AuthorizationFilterContext contexto)
        {
            var pagina = contexto.ActionDescriptor as PageActionDescriptor;
            if (pagina != null && Libres.Contains(pagina.ViewEnginePath))
                return;

            var usuario = contexto.HttpContext.Session.GetString("Usuario");
            if (usuario == null)
            {
                contexto.Result = new RedirectToPageResult("/Login");
                return;
            }

            // las peticiones a la API llevan el nombre del usuario para las auditorías
            var comunicaciones = contexto.HttpContext.RequestServices.GetService<IComunicaciones>();
            if (comunicaciones != null)
                comunicaciones.Usuario = usuario;
        }
    }
}
