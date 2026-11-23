using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Factura = Lib_spa.entidades.Facturas;
using Servicio = Lib_spa.entidades.Servicios;

namespace Front_spa.Pages.Facturas
{
    public class ImprimirModel : PageModel
    {
        public Datos Datos;

        public ImprimirModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public Factura Factura { get; set; } = new();
        public Personas Clienta { get; set; } = new();
        public string Empleado { get; set; } = "";
        public string Metodo { get; set; } = "";
        public bool Pagada { get; set; } = false;
        public List<Detalle_Facturas> Lineas { get; set; } = new();
        public List<Servicio> ListaServicios { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            try
            {
                var factura = Datos.Facturas.Consultar().FirstOrDefault(f => f.Id_Factura == id);
                if (factura == null)
                    return RedirectToPage("Index");

                Factura = factura;

                var reserva = Datos.Reservas.Consultar()
                    .FirstOrDefault(r => r.Id_Reserva == factura.Id_Reserva);
                var personas = Datos.Personas.Consultar();

                var cliente = Datos.Clientes.Consultar()
                    .FirstOrDefault(c => c.Id_Cliente == reserva?.Id_Cliente);
                Clienta = personas.FirstOrDefault(p => p.Id_Persona == cliente?.Persona) ?? new Personas();

                var empleado = Datos.Empleados.Consultar()
                    .FirstOrDefault(e => e.Id_Empleado == reserva?.Id_Empleado);
                Empleado = personas.FirstOrDefault(p => p.Id_Persona == empleado?.Id_Persona)?.Nombre ?? "";

                ListaServicios = Datos.Servicios.Consultar();
                Lineas = Datos.Detalle_Facturas.Consultar()
                    .Where(d => d.Id_Factura == id).ToList();

                var pago = Datos.Pagos.Consultar().FirstOrDefault(p => p.Id_Factura == id);
                if (pago != null)
                {
                    Pagada = true;
                    Metodo = Datos.Metodos_Pagos.Consultar()
                        .FirstOrDefault(m => m.Id_Pago == pago.Id_Pago)?.Metodo_Pago ?? "";
                }
            }
            catch (Exception)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public string NombreServicio(int idServicio)
        {
            return ListaServicios.FirstOrDefault(s => s.Id_Servicio == idServicio)?.Nombre ?? "";
        }
    }
}
