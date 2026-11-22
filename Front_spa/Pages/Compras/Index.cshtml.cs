using Front_spa.Seguridad;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Compra = Lib_spa.entidades.Compras;
using Producto = Lib_spa.entidades.Productos;
using Proveedor = Lib_spa.entidades.Proveedores;

namespace Front_spa.Pages.Compras
{
    [SoloAdministrador]
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Compra> Lista { get; set; } = new();
        public List<Proveedor> ListaProveedores { get; set; } = new();
        public List<Producto> ListaProductos { get; set; } = new();
        public List<Detalle_Compras> ListaDetalles { get; set; } = new();
        [BindProperty] public int FiltroProveedor { get; set; }
        [BindProperty] public Compra Actual { get; set; } = new();
        [BindProperty] public List<Detalle_Compras> Lineas { get; set; } = new();
        public bool Formulario { get; set; } = false;
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Lista = Datos.Compras.Consultar().OrderByDescending(x => x.Fecha).ToList();
                ListaProveedores = Datos.Proveedores.Consultar();
                ListaProductos = Datos.Productos.Consultar();
                ListaDetalles = Datos.Detalle_Compras.Consultar();

                if (FiltroProveedor != 0)
                    Lista = Lista.Where(x => x.Id_Proveedor == FiltroProveedor).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las compras.";
            }
        }

        public void OnPostBtNuevo()
        {
            Formulario = true;
            Actual = new Compra();
            Lineas = new List<Detalle_Compras>();
            Lineas.Add(new Detalle_Compras());
            CargarListas();
        }

        public void OnPostBtAgregar()
        {
            Formulario = true;
            Lineas.Add(new Detalle_Compras());
            CargarListas();
        }

        public void OnPostBtQuitar(string data)
        {
            Formulario = true;
            var posicion = Convert.ToInt32(data);

            if (Lineas.Count > 1 && posicion < Lineas.Count)
                Lineas.RemoveAt(posicion);

            CargarListas();
        }

        public void OnPostBtGuardar()
        {
            Formulario = true;
            CargarListas();

            if (Actual.Id_Proveedor == 0)
            {
                Mensaje = "Selecciona un proveedor.";
                return;
            }

            if (Lineas.Any(x => x.Id_Producto == 0 || x.Cantidad <= 0 || x.Precio_Unidad <= 0))
            {
                Mensaje = "Cada línea necesita un producto, una cantidad y un precio mayores que cero.";
                return;
            }

            try
            {
                // 1. calcula los subtotales y el total
                decimal total = 0;
                foreach (var linea in Lineas)
                {
                    linea.Subtotal = linea.Cantidad * linea.Precio_Unidad;
                    total = total + linea.Subtotal;
                }

                // 2. guarda la compra
                Actual.Compra = total;
                Actual.Total = total;
                Actual.Estado = 1;
                var compra = Datos.Compras.Insertar(Actual);

                // 3. guarda cada línea y suma la cantidad al inventario
                var inventarios = Datos.Inventarios.Consultar();
                foreach (var linea in Lineas)
                {
                    linea.Id_Compra = compra.Id_Compra;
                    Datos.Detalle_Compras.Insertar(linea);

                    var producto = ListaProductos.First(x => x.Id_Producto == linea.Id_Producto);
                    var inventario = inventarios.First(x => x.Id_Inventario == producto.Id_Inventario);
                    inventario.Cantidad = inventario.Cantidad + linea.Cantidad;
                    Datos.Inventarios.Actualizar(inventario);
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudo guardar la compra.";
                return;
            }

            Formulario = false;
            Mensaje = "";
            OnPostBtConsultar();
        }

        public string NombreProveedor(int idProveedor)
        {
            return ListaProveedores.FirstOrDefault(x => x.Id_Proveedor == idProveedor)?.Nombre ?? "";
        }

        public string NombreProducto(int idProducto)
        {
            return ListaProductos.FirstOrDefault(x => x.Id_Producto == idProducto)?.Nombre ?? "";
        }

        // Resume los productos de una compra: "2 x Aceite, 1 x Crema"
        public string Resumen(int idCompra)
        {
            return string.Join(", ", ListaDetalles
                .Where(x => x.Id_Compra == idCompra)
                .Select(x => x.Cantidad + " x " + NombreProducto(x.Id_Producto)));
        }

        public void CargarListas()
        {
            try
            {
                ListaProveedores = Datos.Proveedores.Consultar();
                ListaProductos = Datos.Productos.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las listas.";
            }
        }
    }
}
