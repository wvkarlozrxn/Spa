using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;
using Servicio = Lib_spa.entidades.Servicios;

namespace Front_spa.Pages.Servicios
{
    public class IndexModel : PageModel
    {
        public IServiciosPresentacion IServiciosPresentacion;

        public IndexModel(IServiciosPresentacion IServiciosPresentacion)
        {
            this.IServiciosPresentacion = IServiciosPresentacion;
        }

        public List<Servicio> Lista { get; set; } = new();
        [BindProperty] public Servicio Filtro { get; set; } = new();
        [BindProperty] public Servicio Actual { get; set; } = new();
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
                Lista = this.IServiciosPresentacion.Consultar();

                if (!string.IsNullOrWhiteSpace(Filtro.Nombre))
                    Lista = Lista.Where(x => x.Nombre!.Contains(Filtro.Nombre,
                        StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los servicios.";
            }
        }

        public void OnPostBtNuevo()
        {
            if (SinPermiso())
                return;

            Formulario = true;
            Actual = new Servicio();
        }

        public void OnPostBtGuardar()
        {
            if (SinPermiso())
                return;

            if (string.IsNullOrWhiteSpace(Actual.Nombre) ||
                string.IsNullOrWhiteSpace(Actual.Descripcion) ||
                Actual.Duracion <= 0 || Actual.Precio <= 0)
            {
                Formulario = true;
                Mensaje = "Todos los campos son obligatorios y la duración y el precio deben ser mayores que cero.";
                return;
            }

            if (!Regex.IsMatch(Actual.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Formulario = true;
                Mensaje = "El nombre solo debe contener letras.";
                return;
            }

            try
            {
                if (Actual.Id_Servicio == 0)
                    this.IServiciosPresentacion.Insertar(Actual);
                else
                    this.IServiciosPresentacion.Actualizar(Actual);
            }
            catch (Exception)
            {
                Formulario = true;
                Mensaje = "No se pudo guardar el servicio.";
                return;
            }

            OnPostBtConsultar();
        }

        public void OnPostBtEditar(string data)
        {
            if (SinPermiso())
                return;

            try
            {
                var id = Convert.ToInt32(data);
                var encontrado = this.IServiciosPresentacion.Consultar()
                    .FirstOrDefault(x => x.Id_Servicio == id);

                if (encontrado == null)
                {
                    Mensaje = "El servicio no existe.";
                    OnPostBtConsultar();
                    return;
                }

                Actual = encontrado;
                Formulario = true;
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar el servicio.";
            }
        }

        public void OnPostBtPregBorrar(string data)
        {
            if (SinPermiso())
                return;

            try
            {
                var id = Convert.ToInt32(data);
                var encontrado = this.IServiciosPresentacion.Consultar()
                    .FirstOrDefault(x => x.Id_Servicio == id);

                if (encontrado == null)
                {
                    Mensaje = "El servicio no existe.";
                    OnPostBtConsultar();
                    return;
                }

                Actual = encontrado;
                OnPostBtConsultar();
                Borrar = true;
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar el servicio.";
            }
        }

        public void OnPostBtBorrar()
        {
            if (SinPermiso())
                return;

            try
            {
                this.IServiciosPresentacion.Borrar(Actual);
            }
            catch (Exception)
            {
                Mensaje = "No se pudo eliminar el servicio. Puede que ya tenga citas registradas.";
            }

            var mensaje = Mensaje;
            OnPostBtConsultar();
            Mensaje = mensaje;
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
