using System.Text.RegularExpressions;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Empleado = Lib_spa.entidades.Empleados;

namespace Front_spa.Pages.Empleados
{
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Empleado> Lista { get; set; } = new();
        public List<Personas> ListaPersonas { get; set; } = new();
        public List<Cargos> ListaCargos { get; set; } = new();
        [BindProperty] public string Buscar { get; set; } = "";
        [BindProperty] public Empleado Actual { get; set; } = new();
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
                Lista = Datos.Empleados.Consultar();
                ListaPersonas = Datos.Personas.Consultar();
                ListaCargos = Datos.Cargos.Consultar();

                if (!string.IsNullOrWhiteSpace(Buscar))
                {
                    var texto = Buscar.Trim();
                    Lista = Lista.Where(e =>
                    {
                        var persona = PersonaDe(e.Id_Persona);
                        return persona != null &&
                            ((persona.Nombre ?? "").Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                             (persona.Cedula ?? "").Contains(texto));
                    }).ToList();
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los empleados.";
            }
        }

        public void OnPostBtNuevo()
        {
            if (SinPermiso())
                return;

            Formulario = true;
            Actual = new Empleado();
            Actual.Fecha_Contrato = DateTime.Today;
            Persona = new Personas();
            CargarCargos();
        }

        public void OnPostBtGuardar()
        {
            if (SinPermiso())
                return;

            if (string.IsNullOrWhiteSpace(Persona.Nombre) ||
                string.IsNullOrWhiteSpace(Persona.Telefono) ||
                string.IsNullOrWhiteSpace(Persona.Cedula) ||
                Actual.Id_Cargo == 0)
            {
                Formulario = true;
                CargarCargos();
                Mensaje = "Todos los campos son obligatorios.";
                return;
            }

            if (!Regex.IsMatch(Persona.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Formulario = true;
                CargarCargos();
                Mensaje = "El nombre solo debe contener letras.";
                return;
            }

            if (!Regex.IsMatch(Persona.Telefono, @"^\d+$") || !Regex.IsMatch(Persona.Cedula, @"^\d+$"))
            {
                Formulario = true;
                CargarCargos();
                Mensaje = "El teléfono y la cédula solo deben contener números.";
                return;
            }

            try
            {
                if (Actual.Id_Empleado == 0)
                {
                    Persona = Datos.Personas.Insertar(Persona);
                    Actual.Id_Persona = Persona.Id_Persona;
                    Datos.Empleados.Insertar(Actual);
                }
                else
                {
                    Datos.Personas.Actualizar(Persona);
                    Datos.Empleados.Actualizar(Actual);
                }
            }
            catch (Exception)
            {
                Formulario = true;
                CargarCargos();
                Mensaje = "No se pudo guardar el empleado.";
                return;
            }

            OnPostBtConsultar();
        }

        public void OnPostBtEditar(string data)
        {
            if (SinPermiso())
                return;

            if (!BuscarEmpleado(data))
                return;

            Formulario = true;
            CargarCargos();
        }

        public void OnPostBtPregBorrar(string data)
        {
            if (SinPermiso())
                return;

            if (!BuscarEmpleado(data))
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
                if (Datos.Empleados.Borrar(Actual) == null)
                    Mensaje = "No se pudo eliminar el empleado. Puede que tenga citas registradas.";
                else
                    Datos.Personas.Borrar(Persona);
            }
            catch (Exception)
            {
                Mensaje = "No se pudo eliminar el empleado. Puede que tenga citas registradas.";
            }

            var mensaje = Mensaje;
            OnPostBtConsultar();
            Mensaje = mensaje;
        }

        public Personas? PersonaDe(int idPersona)
        {
            return ListaPersonas.FirstOrDefault(p => p.Id_Persona == idPersona);
        }

        public string CargoDe(int idCargo)
        {
            return ListaCargos.FirstOrDefault(c => c.Id_Cargo == idCargo)?.Cargo ?? "";
        }

        public void CargarCargos()
        {
            try
            {
                ListaCargos = Datos.Cargos.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los cargos.";
            }
        }

        public bool BuscarEmpleado(string data)
        {
            try
            {
                var id = Convert.ToInt32(data);
                var empleado = Datos.Empleados.Consultar()
                    .FirstOrDefault(x => x.Id_Empleado == id);
                var persona = Datos.Personas.Consultar()
                    .FirstOrDefault(x => x.Id_Persona == empleado?.Id_Persona);

                if (empleado != null && persona != null)
                {
                    Actual = empleado;
                    Persona = persona;
                    return true;
                }

                Mensaje = "El empleado no existe.";
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar el empleado.";
            }

            OnPostBtConsultar();
            return false;
        }

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
