using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_spa.Seguridad
{
    // Se ejecuta antes de abrir cualquier página: si no hay sesión, manda al login.
    public class FiltroSesion : IAuthorizationFilter
    {
        private static readonly string[] Libres = { "/Login", "/AccesoDenegado", "/Error" };

        public void OnAuthorization(AuthorizationFilterContext contexto)
        {
            var pagina = contexto.ActionDescriptor as PageActionDescriptor;
            if (pagina != null && Libres.Contains(pagina.ViewEnginePath))
                return;

            if (contexto.HttpContext.Session.GetString("Usuario") == null)
                contexto.Result = new RedirectToPageResult("/Login");
        }
    }
}
