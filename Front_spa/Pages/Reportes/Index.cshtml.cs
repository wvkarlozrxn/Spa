using ClosedXML.Excel;
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

    public class Tabla
    {
        public string Titulo = "";
        public List<string> Encabezados = new();
        public List<List<object>> Filas = new();
    }

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

        public void OnGet()
        {
        }

        public IActionResult OnPostBtPdf()
        {
            var tabla = ArmarTabla();
            if (tabla == null)
                return Page();

            var bytes = CrearPdf(tabla);
            return File(bytes, "application/pdf", NombreArchivo("pdf"));
        }

        public IActionResult OnPostBtExcel()
        {
            var tabla = ArmarTabla();
            if (tabla == null)
                return Page();

            var bytes = CrearExcel(tabla);
            return File(bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                NombreArchivo("xlsx"));
        }

        public string NombreArchivo(string extension)
        {
            return "Reporte_" + Tipo + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + "." + extension;
        }

        // ---------- Datos del reporte ----------

        public Tabla? ArmarTabla()
        {
            try
            {
                if (Desde != null && Hasta != null && Desde > Hasta)
                {
                    Mensaje = "La fecha inicial no puede ser mayor que la final.";
                    return null;
                }

                CargarListas();

                if (Tipo == "Facturas")
                    return TablaFacturas();
                if (Tipo == "Inventario")
                    return TablaInventario();
                return TablaCitas();
            }
            catch (Exception)
            {
                Mensaje = "No se pudo generar el reporte.";
                return null;
            }
        }

        public Tabla TablaCitas()
        {
            var tabla = new Tabla();
            tabla.Titulo = "Reporte de citas" + TextoRango();
            tabla.Encabezados = new List<string>
                { "Fecha y hora", "Clienta", "Servicio", "Empleado", "Estado", "Total" };

            var reservas = Datos.Reservas.Consultar()
                .Where(r => EnRango(r.Fecha))
                .OrderBy(r => r.Fecha);

            decimal suma = 0;
            foreach (var r in reservas)
            {
                var total = ListaFacturas.FirstOrDefault(f => f.Id_Reserva == r.Id_Reserva)?.Total ?? 0;
                suma = suma + total;

                tabla.Filas.Add(new List<object>
                {
                    r.Fecha,
                    NombreCliente(r.Id_Cliente),
                    NombreServicio(r.Id_Reserva),
                    NombreEmpleado(r.Id_Empleado),
                    ((EstadoReserva)r.Estado).ToString(),
                    total
                });
            }

            tabla.Filas.Add(new List<object> { "", "", "", "", "Suma", suma });
            return tabla;
        }

        public Tabla TablaFacturas()
        {
            var tabla = new Tabla();
            tabla.Titulo = "Reporte de facturas" + TextoRango();
            tabla.Encabezados = new List<string>
                { "N.°", "Fecha", "Clienta", "Servicio", "Total", "Estado" };

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

                tabla.Filas.Add(new List<object>
                {
                    f.Id_Factura,
                    f.Fecha,
                    NombreCliente(reserva?.Id_Cliente ?? 0),
                    NombreServicio(f.Id_Reserva),
                    f.Total,
                    pagada ? "Pagada" : "Pendiente"
                });
            }

            tabla.Filas.Add(new List<object> { "", "", "", "Total facturado", suma, "" });
            tabla.Filas.Add(new List<object> { "", "", "", "Total pagado", pagado, "" });
            return tabla;
        }

        public Tabla TablaInventario()
        {
            var tabla = new Tabla();
            tabla.Titulo = "Reporte de inventario";
            tabla.Encabezados = new List<string>
                { "Producto", "Cantidad", "Stock mínimo", "Precio de venta", "Estado" };

            var inventarios = Datos.Inventarios.Consultar();

            foreach (var p in Datos.Productos.Consultar().OrderBy(p => p.Nombre))
            {
                var inventario = inventarios.FirstOrDefault(i => i.Id_Inventario == p.Id_Inventario);
                var cantidad = inventario?.Cantidad ?? 0;
                var minimo = inventario?.Stock_min ?? 0;

                tabla.Filas.Add(new List<object>
                {
                    p.Nombre ?? "",
                    cantidad,
                    minimo,
                    p.Precio_Venta,
                    cantidad <= minimo ? "Stock bajo" : "Normal"
                });
            }

            return tabla;
        }

        // ---------- Archivos ----------

        public byte[] CrearPdf(Tabla tabla)
        {
            var documento = Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());
                    pagina.Margin(30);
                    pagina.DefaultTextStyle(x => x.FontSize(10));

                    pagina.Header().Column(columna =>
                    {
                        columna.Item().Text(tabla.Titulo).FontSize(18).Bold().FontColor("#4b2e83");
                        columna.Item().Text("Generado el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                            .FontSize(9).FontColor("#666666");
                    });

                    pagina.Content().PaddingTop(15).Table(t =>
                    {
                        t.ColumnsDefinition(columnas =>
                        {
                            foreach (var encabezado in tabla.Encabezados)
                                columnas.RelativeColumn();
                        });

                        t.Header(cabecera =>
                        {
                            foreach (var encabezado in tabla.Encabezados)
                                cabecera.Cell().Background("#6d4aa8").Padding(5)
                                    .Text(encabezado).FontColor("#ffffff").Bold();
                        });

                        foreach (var fila in tabla.Filas)
                        {
                            foreach (var valor in fila)
                                t.Cell().BorderBottom(1).BorderColor("#dddddd").Padding(5)
                                    .Text(Formato(valor));
                        }
                    });

                    pagina.Footer().AlignCenter().Text(texto =>
                    {
                        texto.Span("Página ");
                        texto.CurrentPageNumber();
                        texto.Span(" de ");
                        texto.TotalPages();
                    });
                });
            });

            return documento.GeneratePdf();
        }

        public byte[] CrearExcel(Tabla tabla)
        {
            var libro = new XLWorkbook();
            var hoja = libro.Worksheets.Add("Reporte");

            hoja.Cell(1, 1).Value = tabla.Titulo;
            hoja.Cell(1, 1).Style.Font.Bold = true;
            hoja.Cell(1, 1).Style.Font.FontSize = 14;

            // encabezados en la fila 3
            for (var c = 0; c < tabla.Encabezados.Count; c++)
            {
                var celda = hoja.Cell(3, c + 1);
                celda.Value = tabla.Encabezados[c];
                celda.Style.Font.Bold = true;
                celda.Style.Font.FontColor = XLColor.White;
                celda.Style.Fill.BackgroundColor = XLColor.FromHtml("#6d4aa8");
            }

            // filas desde la 4
            for (var f = 0; f < tabla.Filas.Count; f++)
            {
                for (var c = 0; c < tabla.Filas[f].Count; c++)
                {
                    var valor = tabla.Filas[f][c];
                    var celda = hoja.Cell(f + 4, c + 1);

                    if (valor is DateTime fecha)
                    {
                        celda.Value = fecha;
                        celda.Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                    }
                    else if (valor is decimal numero)
                    {
                        celda.Value = numero;
                        celda.Style.NumberFormat.Format = "#,##0";
                    }
                    else if (valor is int entero)
                    {
                        celda.Value = entero;
                    }
                    else
                    {
                        celda.Value = valor.ToString();
                    }
                }
            }

            hoja.Columns().AdjustToContents();

            var memoria = new MemoryStream();
            libro.SaveAs(memoria);
            return memoria.ToArray();
        }

        // Cómo se escribe cada valor en el PDF.
        public string Formato(object valor)
        {
            if (valor is DateTime fecha)
                return fecha.ToString("dd/MM/yyyy HH:mm");
            if (valor is decimal numero)
                return numero.ToString("N0");
            return valor.ToString() ?? "";
        }

        // ---------- Auxiliares ----------

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
