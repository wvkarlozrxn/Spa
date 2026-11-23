using System.Text.RegularExpressions;
using Front_spa.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proveedor = Lib_spa.entidades.Proveedores;

namespace Front_spa.Pages.Proveedores
{
    [SoloAdministrador]
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Proveedor> Lista { get; set; } = new();
        [BindProperty] public string Buscar { get; set; } = "";
        [BindProperty] public Proveedor Actual { get; set; } = new();
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
                Lista = Datos.Proveedores.Consultar();

                if (!string.IsNullOrWhiteSpace(Buscar))
                    Lista = Lista.Where(x => (x.Nombre ?? "").Contains(Buscar.Trim(),
                        StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los proveedores.";
            }
        }

        public void OnPostBtNuevo()
        {
            Formulario = true;
            Actual = new Proveedor();
        }

        public void OnPostBtGuardar()
        {
            if (string.IsNullOrWhiteSpace(Actual.Nombre) ||
                string.IsNullOrWhiteSpace(Actual.Telefono) ||
                string.IsNullOrWhiteSpace(Actual.Correo) ||
                string.IsNullOrWhiteSpace(Actual.Direccion))
            {
                Formulario = true;
                Mensaje = "Todos los campos son obligatorios.";
                return;
            }

            if (!Regex.IsMatch(Actual.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                Formulario = true;
                Mensaje = "El nombre solo debe contener letras.";
                return;
            }

            if (!Regex.IsMatch(Actual.Telefono, @"^\d+$"))
            {
                Formulario = true;
                Mensaje = "El teléfono solo debe contener números.";
                return;
            }

            if (!Actual.Correo.Contains("@"))
            {
                Formulario = true;
                Mensaje = "El correo no es válido.";
                return;
            }

            try
            {
                if (Actual.Id_Proveedor == 0)
                    Datos.Proveedores.Insertar(Actual);
                else
                    Datos.Proveedores.Actualizar(Actual);
            }
            catch (Exception)
            {
                Formulario = true;
                Mensaje = "No se pudo guardar el proveedor.";
                return;
            }

            OnPostBtConsultar();
        }

        public void OnPostBtEditar(string data)
        {
            if (!BuscarProveedor(data))
                return;

            Formulario = true;
        }

        public void OnPostBtPregBorrar(string data)
        {
            if (!BuscarProveedor(data))
                return;

            OnPostBtConsultar();
            Borrar = true;
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Datos.Proveedores.Borrar(Actual) == null)
                    Mensaje = "No se pudo eliminar el proveedor. Puede que tenga compras registradas.";
            }
            catch (Exception)
            {
                Mensaje = "No se pudo eliminar el proveedor. Puede que tenga compras registradas.";
            }

            var mensaje = Mensaje;
            OnPostBtConsultar();
            Mensaje = mensaje;
        }

        public bool BuscarProveedor(string data)
        {
            try
            {
                var id = Convert.ToInt32(data);
                var encontrado = Datos.Proveedores.Consultar()
                    .FirstOrDefault(x => x.Id_Proveedor == id);

                if (encontrado != null)
                {
                    Actual = encontrado;
                    return true;
                }

                Mensaje = "El proveedor no existe.";
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar el proveedor.";
            }

            OnPostBtConsultar();
            return false;
        }
    }
}
