using System.Text.RegularExpressions;
using Front_spa.Seguridad;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Usuario = Lib_spa.entidades.Usuarios;

namespace Front_spa.Pages.Usuarios
{
    [SoloAdministrador]
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Usuario> Lista { get; set; } = new();
        public List<Roles> ListaRoles { get; set; } = new();
        [BindProperty] public string Buscar { get; set; } = "";
        [BindProperty] public Usuario Actual { get; set; } = new();
        public bool Formulario { get; set; } = false;
        public bool Borrar { get; set; } = false;
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Lista = Datos.Usuarios.Consultar();
                ListaRoles = Datos.Roles.Consultar();

                if (!string.IsNullOrWhiteSpace(Buscar))
                    Lista = Lista.Where(x => (x.Usuario ?? "").Contains(Buscar.Trim(),
                        StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los usuarios.";
            }
        }

        public void OnPostBtNuevo()
        {
            Formulario = true;
            Actual = new Usuario();
            CargarRoles();
        }

        public void OnPostBtGuardar()
        {
            if (string.IsNullOrWhiteSpace(Actual.Usuario) || Actual.Id_Rol == 0)
            {
                ErrorFormulario("El usuario y el rol son obligatorios.");
                return;
            }

            if (Actual.Id_Usuario == 0 && string.IsNullOrWhiteSpace(Actual.Contraseña))
            {
                ErrorFormulario("La contraseña es obligatoria.");
                return;
            }

            if (!Regex.IsMatch(Actual.Usuario, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                ErrorFormulario("El usuario solo debe contener letras.");
                return;
            }

            try
            {
                var todos = Datos.Usuarios.Consultar();

                if (todos.Any(x => x.Usuario == Actual.Usuario && x.Id_Usuario != Actual.Id_Usuario))
                {
                    ErrorFormulario("Ya existe un usuario con ese nombre.");
                    return;
                }

                if (Actual.Id_Usuario == 0)
                {
                    Datos.Usuarios.Insertar(Actual);
                }
                else
                {
                    var original = todos.First(x => x.Id_Usuario == Actual.Id_Usuario);

                    if (EsElMismo(Actual.Id_Usuario) && Actual.Id_Rol != original.Id_Rol)
                    {
                        ErrorFormulario("No puedes cambiar tu propio rol.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(Actual.Contraseña))
                        Actual.Contraseña = original.Contraseña;

                    Datos.Usuarios.Actualizar(Actual);
                }
            }
            catch (Exception)
            {
                ErrorFormulario("No se pudo guardar el usuario.");
                return;
            }

            OnPostBtConsultar();
        }

        public void OnPostBtEditar(string data)
        {
            if (!BuscarUsuario(data))
                return;

            Actual.Contraseña = "";
            Formulario = true;
            CargarRoles();
        }

        public void OnPostBtPregBorrar(string data)
        {
            if (!BuscarUsuario(data))
                return;

            OnPostBtConsultar();
            Borrar = true;
        }

        public void OnPostBtBorrar()
        {
            if (EsElMismo(Actual.Id_Usuario))
            {
                Mensaje = "No puedes eliminar tu propio usuario.";
            }
            else
            {
                try
                {
                    if (Datos.Usuarios.Borrar(Actual) == null)
                        Mensaje = "No se pudo eliminar el usuario.";
                }
                catch (Exception)
                {
                    Mensaje = "No se pudo eliminar el usuario.";
                }
            }

            var mensaje = Mensaje;
            OnPostBtConsultar();
            Mensaje = mensaje;
        }

        public string NombreRol(int idRol)
        {
            return ListaRoles.FirstOrDefault(r => r.Id_Rol == idRol)?.Rol ?? "";
        }

        public bool EsElMismo(int idUsuario)
        {
            return idUsuario.ToString() == HttpContext.Session.GetString("IdUsuario");
        }

        public void CargarRoles()
        {
            try
            {
                ListaRoles = Datos.Roles.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los roles.";
            }
        }

        public void ErrorFormulario(string texto)
        {
            Formulario = true;
            CargarRoles();
            Mensaje = texto;
        }

        public bool BuscarUsuario(string data)
        {
            try
            {
                var id = Convert.ToInt32(data);
                var encontrado = Datos.Usuarios.Consultar()
                    .FirstOrDefault(x => x.Id_Usuario == id);

                if (encontrado != null)
                {
                    Actual = encontrado;
                    return true;
                }

                Mensaje = "El usuario no existe.";
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar el usuario.";
            }

            OnPostBtConsultar();
            return false;
        }
    }
}
