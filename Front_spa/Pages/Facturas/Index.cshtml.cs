using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Facturas
{
    public class IndexModel : PageModel
    {
        private IFacturasPresentacion IFacturasPresentacion;
        private IReservasPresentacion IReservasPresentacion;
        private IClientesPresentacion IClientesPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private IServiciosPresentacion IServiciosPresentacion;
        private IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion;
        private IPagosPresentacion IPagosPresentacion;

        public IndexModel(IFacturasPresentacion IFacturasPresentacion,
            IReservasPresentacion IReservasPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IPersonasPresentacion IPersonasPresentacion,
            IServiciosPresentacion IServiciosPresentacion,
            IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion,
            IPagosPresentacion IPagosPresentacion)
        {
            this.IFacturasPresentacion = IFacturasPresentacion;
            this.IReservasPresentacion = IReservasPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IServiciosPresentacion = IServiciosPresentacion;
            this.IDetalle_Reservas_Presentacion = IDetalle_Reservas_Presentacion;
            this.IPagosPresentacion = IPagosPresentacion;
        }

        public List<Lib_spa.entidades.Facturas> Lista { get; set; } = new();
        public List<Reservas> Reservas { get; set; } = new();
        public List<Clientes> Clientes { get; set; } = new();
        public List<Personas> Personas { get; set; } = new();
        public List<Lib_spa.entidades.Servicios> Servicios { get; set; } = new();
        public List<Detalle_Reservas> Detalles { get; set; } = new();
        public List<Pagos> Pagos { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            try
            {
                Lista = this.IFacturasPresentacion.Consultar()
                    .OrderByDescending(f => f.Fecha).ToList();
                Reservas = this.IReservasPresentacion.Consultar();
                Clientes = this.IClientesPresentacion.Consultar();
                Personas = this.IPersonasPresentacion.Consultar();
                Servicios = this.IServiciosPresentacion.Consultar();
                Detalles = this.IDetalle_Reservas_Presentacion.Consultar();
                Pagos = this.IPagosPresentacion.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las facturas.";
            }
        }

        public Reservas? ReservaDe(int idReserva)
        {
            return Reservas.FirstOrDefault(r => r.Id_Reserva == idReserva);
        }

        public string NombreCliente(int idReserva)
        {
            var reserva = ReservaDe(idReserva);
            var cliente = Clientes.FirstOrDefault(c => c.Id_Cliente == reserva?.Id_Cliente);
            if (cliente == null)
                return "";
            return Personas.FirstOrDefault(p => p.Id_Persona == cliente.Persona)?.Nombre ?? "";
        }

        public string NombreServicio(int idReserva)
        {
            var detalle = Detalles.FirstOrDefault(d => d.Id_Reserva == idReserva);
            if (detalle == null)
                return "";
            return Servicios.FirstOrDefault(s => s.Id_Servicio == detalle.Id_Servicio)?.Nombre ?? "";
        }

        public bool EstaPagada(int idFactura)
        {
            return Pagos.Any(p => p.Id_Factura == idFactura);
        }
    }
}
