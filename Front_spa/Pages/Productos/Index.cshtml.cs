using Front_spa.Seguridad;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Producto = Lib_spa.entidades.Productos;

namespace Front_spa.Pages.Productos
{
    [SoloAdministrador]
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Producto> Lista { get; set; } = new();
        public List<Inventarios> ListaInventarios { get; set; } = new();
        [BindProperty] public string Buscar { get; set; } = "";
        [BindProperty] public Producto Actual { get; set; } = new();
        [BindProperty] public Inventarios Inventario { get; set; } = new();
        [BindProperty] public IFormFile? Archivo { get; set; }
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
                Lista = Datos.Productos.Consultar();
                ListaInventarios = Datos.Inventarios.Consultar();

                if (!string.IsNullOrWhiteSpace(Buscar))
                    Lista = Lista.Where(x => (x.Nombre ?? "").Contains(Buscar.Trim(),
                        StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los productos.";
            }
        }

        public void OnPostBtNuevo()
        {
            Formulario = true;
            Actual = new Producto();
            Inventario = new Inventarios();
        }

        public void OnPostBtGuardar()
        {
            if (string.IsNullOrWhiteSpace(Actual.Nombre) ||
                string.IsNullOrWhiteSpace(Actual.Descripcion))
            {
                Formulario = true;
                Mensaje = "El nombre y la descripción son obligatorios.";
                return;
            }

            if (Actual.Precio_Compra <= 0 || Actual.Precio_Venta <= 0)
            {
                Formulario = true;
                Mensaje = "Los precios deben ser mayores que cero.";
                return;
            }

            if (Inventario.Cantidad <= 0 || Inventario.Stock_min <= 0)
            {
                Formulario = true;
                Mensaje = "La cantidad y el stock mínimo deben ser mayores que cero.";
                return;
            }

            if (Archivo != null)
            {
                if (!Archivo.ContentType.StartsWith("image/"))
                {
                    Formulario = true;
                    Mensaje = "El archivo debe ser una imagen.";
                    return;
                }

                if (Archivo.Length > 1000000)
                {
                    Formulario = true;
                    Mensaje = "La imagen no debe pesar más de 1 MB.";
                    return;
                }
            }

            try
            {
                if (Archivo != null)
                {
                    var memoria = new MemoryStream();
                    Archivo.CopyToAsync(memoria).Wait();
                    Actual.Imagen = Convert.ToBase64String(memoria.ToArray());
                    memoria.Dispose();
                }
                else if (Actual.Id_Producto != 0)
                {
                    Actual.Imagen = Datos.Productos.Consultar()
                        .FirstOrDefault(x => x.Id_Producto == Actual.Id_Producto)?.Imagen;
                }

                if (Actual.Id_Producto == 0)
                {
                    Inventario = Datos.Inventarios.Insertar(Inventario);
                    Actual.Id_Inventario = Inventario.Id_Inventario;
                    Datos.Productos.Insertar(Actual);
                }
                else
                {
                    Datos.Inventarios.Actualizar(Inventario);
                    Datos.Productos.Actualizar(Actual);
                }
            }
            catch (Exception)
            {
                Formulario = true;
                Mensaje = "No se pudo guardar el producto.";
                return;
            }

            OnPostBtConsultar();
        }

        public void OnPostBtEditar(string data)
        {
            if (!BuscarProducto(data))
                return;

            Formulario = true;
        }

        public void OnPostBtPregBorrar(string data)
        {
            if (!BuscarProducto(data))
                return;

            OnPostBtConsultar();
            Borrar = true;
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Datos.Productos.Borrar(Actual) == null)
                    Mensaje = "No se pudo eliminar el producto. Puede que tenga compras registradas.";
                else
                    Datos.Inventarios.Borrar(Inventario);
            }
            catch (Exception)
            {
                Mensaje = "No se pudo eliminar el producto. Puede que tenga compras registradas.";
            }

            var mensaje = Mensaje;
            OnPostBtConsultar();
            Mensaje = mensaje;
        }

        public Inventarios? InventarioDe(int idInventario)
        {
            return ListaInventarios.FirstOrDefault(i => i.Id_Inventario == idInventario);
        }

        public bool BuscarProducto(string data)
        {
            try
            {
                var id = Convert.ToInt32(data);
                var producto = Datos.Productos.Consultar()
                    .FirstOrDefault(x => x.Id_Producto == id);
                var inventario = Datos.Inventarios.Consultar()
                    .FirstOrDefault(x => x.Id_Inventario == producto?.Id_Inventario);

                if (producto != null && inventario != null)
                {
                    Actual = producto;
                    Inventario = inventario;
                    return true;
                }

                Mensaje = "El producto no existe.";
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar el producto.";
            }

            OnPostBtConsultar();
            return false;
        }
    }
}
