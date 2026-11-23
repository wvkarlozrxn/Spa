using Front_spa.Seguridad;
using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Empleado = Lib_spa.entidades.Empleados;
using Factura = Lib_spa.entidades.Facturas;
using Servicio = Lib_spa.entidades.Servicios;

namespace Front_spa.Pages.Reportes
{
    [SoloAdministrador]
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Personas> ListaPersonas { get; set; } = new();
        public List<Clientes> ListaClientes { get; set; } = new();
        public List<Empleado> ListaEmpleados { get; set; } = new();
        public List<Servicio> ListaServicios { get; set; } = new();
        public List<Detalle_Reservas> ListaDetalles { get; set; } = new();
        public List<Factura> ListaFacturas { get; set; } = new();
        public List<Pagos> ListaPagos { get; set; } = new();

        [BindProperty] public string Tipo { get; set; } = "Citas";
        [BindProperty] public DateTime? Desde { get; set; }
        [BindProperty] public DateTime? Hasta { get; set; }
        public string Mensaje { get; set; } = "";

        public string Titulo { get; set; } = "";
        public List<string> Encabezados { get; set; } = new();
        public List<List<string>> Filas { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPostBtPdf()
        {
            if (Desde != null && Hasta != null && Desde > Hasta)
            {
                Mensaje = "La fecha inicial no puede ser mayor que la final.";
                return Page();
            }

            try
            {
                CargarListas();

                if (Tipo == "Facturas")
                    ArmarFacturas();
                else if (Tipo == "Inventario")
                    ArmarInventario();
                else
                    ArmarCitas();
            }
            catch (Exception)
            {
                Mensaje = "No se pudo generar el reporte.";
                return Page();
            }

            var bytes = CrearPdf();
            return File(bytes, "application/pdf", "Reporte_" + Tipo + ".pdf");
        }

        public void ArmarCitas()
        {
            Titulo = "Reporte de citas" + TextoRango();
            Encabezados = new List<string> { "Fecha y hora", "Clienta", "Servicio", "Empleado", "Estado", "Total" };

            var reservas = Datos.Reservas.Consultar()
                .Where(r => EnRango(r.Fecha))
                .OrderBy(r => r.Fecha);

            decimal suma = 0;
            foreach (var r in reservas)
            {
                var total = ListaFacturas.FirstOrDefault(f => f.Id_Reserva == r.Id_Reserva)?.Total ?? 0;
                suma = suma + total;

                Filas.Add(new List<string>
                {
                    r.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    NombreCliente(r.Id_Cliente),
                    NombreServicio(r.Id_Reserva),
                    NombreEmpleado(r.Id_Empleado),
                    ((EstadoReserva)r.Estado).ToString(),
                    total.ToString("N0")
                });
            }

            Filas.Add(new List<string> { "", "", "", "", "Suma", suma.ToString("N0") });
        }

        public void ArmarFacturas()
        {
            Titulo = "Reporte de facturas" + TextoRango();
            Encabezados = new List<string> { "N.°", "Fecha", "Clienta", "Servicio", "Total", "Estado" };

            var reservas = Datos.Reservas.Consultar();
            var facturas = ListaFacturas.Where(f => EnRango(f.Fecha)).OrderBy(f => f.Fecha);

            decimal suma = 0;
            decimal pagado = 0;
            foreach (var f in facturas)
            {
                var reserva = reservas.FirstOrDefault(r => r.Id_Reserva == f.Id_Reserva);
                var pagada = ListaPagos.Any(p => p.Id_Factura == f.Id_Factura);

                suma = suma + f.Total;
                if (pagada)
                    pagado = pagado + f.Total;

                Filas.Add(new List<string>
                {
                    f.Id_Factura.ToString(),
                    f.Fecha.ToString("dd/MM/yyyy"),
                    NombreCliente(reserva?.Id_Cliente ?? 0),
                    NombreServicio(f.Id_Reserva),
                    f.Total.ToString("N0"),
                    pagada ? "Pagada" : "Pendiente"
                });
            }

            Filas.Add(new List<string> { "", "", "", "Total facturado", suma.ToString("N0"), "" });
            Filas.Add(new List<string> { "", "", "", "Total pagado", pagado.ToString("N0"), "" });
        }

        public void ArmarInventario()
        {
            Titulo = "Reporte de inventario";
            Encabezados = new List<string> { "Producto", "Cantidad", "Stock mínimo", "Precio de venta", "Estado" };

            var inventarios = Datos.Inventarios.Consultar();

            foreach (var p in Datos.Productos.Consultar().OrderBy(p => p.Nombre))
            {
                var inventario = inventarios.FirstOrDefault(i => i.Id_Inventario == p.Id_Inventario);
                var cantidad = inventario?.Cantidad ?? 0;
                var minimo = inventario?.Stock_min ?? 0;

                Filas.Add(new List<string>
                {
                    p.Nombre ?? "",
                    cantidad.ToString(),
                    minimo.ToString(),
                    p.Precio_Venta.ToString("N0"),
                    cantidad <= minimo ? "Stock bajo" : "Normal"
                });
            }
        }

        public byte[] CrearPdf()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var documento = Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());
                    pagina.Margin(30);

                    pagina.Header().Text(Titulo).FontSize(18).Bold();

                    pagina.Content().PaddingTop(15).Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columnas =>
                        {
                            foreach (var encabezado in Encabezados)
                                columnas.RelativeColumn();
                        });

                        tabla.Header(cabecera =>
                        {
                            foreach (var encabezado in Encabezados)
                                cabecera.Cell().Background("#6d4aa8").Padding(5)
                                    .Text(encabezado).FontColor("#ffffff").Bold();
                        });

                        foreach (var fila in Filas)
                        {
                            foreach (var valor in fila)
                                tabla.Cell().Padding(5).Text(valor);
                        }
                    });
                });
            });

            return documento.GeneratePdf();
        }

        public void CargarListas()
        {
            ListaPersonas = Datos.Personas.Consultar();
            ListaClientes = Datos.Clientes.Consultar();
            ListaEmpleados = Datos.Empleados.Consultar();
            ListaServicios = Datos.Servicios.Consultar();
            ListaDetalles = Datos.Detalle_Reservas.Consultar();
            ListaFacturas = Datos.Facturas.Consultar();
            ListaPagos = Datos.Pagos.Consultar();
        }

        public bool EnRango(DateTime fecha)
        {
            if (Desde != null && fecha.Date < Desde.Value.Date)
                return false;
            if (Hasta != null && fecha.Date > Hasta.Value.Date)
                return false;
            return true;
        }

        public string TextoRango()
        {
            if (Desde == null && Hasta == null)
                return "";

            var desde = Desde == null ? "inicio" : Desde.Value.ToString("dd/MM/yyyy");
            var hasta = Hasta == null ? "hoy" : Hasta.Value.ToString("dd/MM/yyyy");
            return " (" + desde + " a " + hasta + ")";
        }

        public string NombrePersona(int idPersona)
        {
            return ListaPersonas.FirstOrDefault(p => p.Id_Persona == idPersona)?.Nombre ?? "";
        }

        public string NombreCliente(int idCliente)
        {
            var cliente = ListaClientes.FirstOrDefault(c => c.Id_Cliente == idCliente);
            return NombrePersona(cliente?.Persona ?? 0);
        }

        public string NombreEmpleado(int idEmpleado)
        {
            var empleado = ListaEmpleados.FirstOrDefault(e => e.Id_Empleado == idEmpleado);
            return NombrePersona(empleado?.Id_Persona ?? 0);
        }

        public string NombreServicio(int idReserva)
        {
            var detalle = ListaDetalles.FirstOrDefault(d => d.Id_Reserva == idReserva);
            return ListaServicios.FirstOrDefault(s => s.Id_Servicio == detalle?.Id_Servicio)?.Nombre ?? "";
        }
    }
}
