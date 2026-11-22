using System.Text.RegularExpressions;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front_spa.Pages.Clientas
{
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Clientes> Lista { get; set; } = new();
        public List<Personas> ListaPersonas { get; set; } = new();
        [BindProperty] public string Buscar { get; set; } = "";
        [BindProperty] public Clientes Actual { get; set; } = new();
        [BindProperty] public Personas Persona { get; set; } = new();
        public bool Formulario { get; set; } = false;
        public bool Borrar { get; set; } = false;
        public string Mensaje { get; set; } = "";

        public bool EsAdmin
        {
            get { return HttpContext.Session.GetString("Rol") == "Administrador"; }
        }

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Lista = Datos.Clientes.Consultar();
                ListaPersonas = Datos.Personas.Consultar();

                if (!string.IsNullOrWhiteSpace(Buscar))
                {
                    var texto = Buscar.Trim();
                    Lista = Lista.Where(c =>
                    {
                        var persona = PersonaDe(c.Persona);
                        return persona != null &&
                            ((persona.Nombre ?? "").Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                             (persona.Cedula ?? "").Contains(texto));
                    }).ToList();
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las clientas.";
            }
        }

        public void OnPostBtNuevo()
        {
            Formulario = true;
            Actual = new Clientes();
            Persona = new Personas();
        }

        public void OnPostBtGuardar()
        {
            if (string.IsNullOrWhiteSpace(Persona.Nombre) ||
                string.IsNullOrWhiteSpace(Persona.Telefono) ||
                string.IsNullOrWhiteSpace(Persona.Cedula))
            {
                Formulario = true;
                Mensaje = "Todos los campos son obligatorios.";
                return;
            }

            if (!Regex.IsMatch(Persona.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Formulario = true;
                Mensaje = "El nombre solo debe contener letras.";
                return;
            }

            if (!Regex.IsMatch(Persona.Telefono, @"^\d+$") || !Regex.IsMatch(Persona.Cedula, @"^\d+$"))
            {
                Formulario = true;
                Mensaje = "El teléfono y la cédula solo deben contener números.";
                return;
            }

            try
            {
                if (Actual.Id_Cliente == 0)
                {
                    Persona = Datos.Personas.Insertar(Persona);
                    Actual.Persona = Persona.Id_Persona;
                    Datos.Clientes.Insertar(Actual);
                }
                else
                {
                    Datos.Personas.Actualizar(Persona);
                }
            }
            catch (Exception)
            {
                Formulario = true;
                Mensaje = "No se pudo guardar la clienta.";
                return;
            }

            OnPostBtConsultar();
        }

        public void OnPostBtEditar(string data)
        {
            if (!BuscarClienta(data))
                return;

            Formulario = true;
        }

        public void OnPostBtPregBorrar(string data)
        {
            if (SinPermiso())
                return;

            if (!BuscarClienta(data))
                return;

            OnPostBtConsultar();
            Borrar = true;
        }

        public void OnPostBtBorrar()
        {
            if (SinPermiso())
                return;

            try
            {
                Datos.Clientes.Borrar(Actual);

                try
                {
                    Datos.Personas.Borrar(Persona);
                }
                catch (Exception)
                {
                    // la persona sigue en uso (por ejemplo, también es empleada)
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudo eliminar la clienta. Puede que tenga reservas registradas.";
            }

            var mensaje = Mensaje;
            OnPostBtConsultar();
            Mensaje = mensaje;
        }

        public Personas? PersonaDe(int idPersona)
        {
            return ListaPersonas.FirstOrDefault(p => p.Id_Persona == idPersona);
        }

        // Busca la clienta y su persona y las deja en Actual y Persona.
        public bool BuscarClienta(string data)
        {
            try
            {
                var id = Convert.ToInt32(data);
                var cliente = Datos.Clientes.Consultar()
                    .FirstOrDefault(x => x.Id_Cliente == id);
                var persona = Datos.Personas.Consultar()
                    .FirstOrDefault(x => x.Id_Persona == cliente?.Persona);

                if (cliente != null && persona != null)
                {
                    Actual = cliente;
                    Persona = persona;
                    return true;
                }

                Mensaje = "La clienta no existe.";
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar la clienta.";
            }

            OnPostBtConsultar();
            return false;
        }

        // Solo el administrador puede borrar.
        public bool SinPermiso()
        {
            if (EsAdmin)
                return false;

            Mensaje = "Solo el administrador puede hacer esta acción.";
            OnPostBtConsultar();
            return true;
        }
    }
}
