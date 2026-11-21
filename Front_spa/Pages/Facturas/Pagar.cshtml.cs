using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Facturas
{
    public class PagarModel : PageModel
    {
        private IFacturasPresentacion IFacturasPresentacion;
        private IReservasPresentacion IReservasPresentacion;
        private IPagosPresentacion IPagosPresentacion;
        private IMetodos_Pagos_Presentacion IMetodos_Pagos_Presentacion;

        public PagarModel(IFacturasPresentacion IFacturasPresentacion,
            IReservasPresentacion IReservasPresentacion,
            IPagosPresentacion IPagosPresentacion,
            IMetodos_Pagos_Presentacion IMetodos_Pagos_Presentacion)
        {
            this.IFacturasPresentacion = IFacturasPresentacion;
            this.IReservasPresentacion = IReservasPresentacion;
            this.IPagosPresentacion = IPagosPresentacion;
            this.IMetodos_Pagos_Presentacion = IMetodos_Pagos_Presentacion;
        }

        [BindProperty]
        public string Metodo { get; set; } = "";

        public Lib_spa.entidades.Facturas? Factura { get; set; }

        public List<SelectListItem> Metodos { get; set; } = new()
        {
            new SelectListItem("Efectivo", "Efectivo"),
            new SelectListItem("Tarjeta", "Tarjeta"),
            new SelectListItem("Transferencia", "Transferencia")
        };

        public string Mensaje { get; set; } = "";

        public IActionResult OnGet(int id)
        {
            try
            {
                Factura = BuscarFactura(id);
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cargar la factura.";
                return Page();
            }

            if (Factura == null)
                return RedirectToPage("Index");

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                Factura = BuscarFactura(id);
                if (Factura == null)
                    return RedirectToPage("Index");

                if (!Metodos.Any(m => m.Value == Metodo))
                {
                    Mensaje = "Elige un método de pago.";
                    return Page();
                }

                var yaPagada = this.IPagosPresentacion.Consultar()
                    .Any(p => p.Id_Factura == Factura.Id_Factura);
                if (yaPagada)
                {
                    Mensaje = "Esta factura ya está pagada.";
                    return Page();
                }

                var reserva = this.IReservasPresentacion.Consultar()
                    .FirstOrDefault(r => r.Id_Reserva == Factura.Id_Reserva);
                if (reserva != null && reserva.Estado == (int)EstadoReserva.Cancelada)
                {
                    Mensaje = "La cita de esta factura está cancelada.";
                    return Page();
                }

                var pago = new Pagos();
                pago.Id_Factura = Factura.Id_Factura;
                pago.Fecha_Pago = DateTime.Now;
                pago = this.IPagosPresentacion.Insertar(pago);

                var detalle = new Metodos_Pagos();
                detalle.Id_Pago = pago.Id_Pago;
                detalle.Metodo_Pago = Metodo;
                detalle.Descripcion = "Pago de la factura " + Factura.Id_Factura;
                this.IMetodos_Pagos_Presentacion.Insertar(detalle);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo registrar el pago.";
                return Page();
            }
        }

        private Lib_spa.entidades.Facturas? BuscarFactura(int id)
        {
            return this.IFacturasPresentacion.Consultar()
                .FirstOrDefault(f => f.Id_Factura == id);
        }
    }
}
