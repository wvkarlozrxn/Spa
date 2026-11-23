using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Front_spa.Seguridad
{
  
    public class SoloAdministradorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext contexto)
        {
            if (contexto.HttpContext.Session.GetString("Rol") != "Administrador")
                contexto.Result = new RedirectToPageResult("/AccesoDenegado");
        }
    }
}
