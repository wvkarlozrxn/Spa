using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Citas
{
    public class IndexModel : PageModel
    {
        private IReservasPresentacion IReservasPresentacion;
        private IClientesPresentacion IClientesPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private IServiciosPresentacion IServiciosPresentacion;
        private IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion;
        private IFacturasPresentacion IFacturasPresentacion;
        private IPagosPresentacion IPagosPresentacion;

        public IndexModel(IReservasPresentacion IReservasPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IEmpleadosPresentacion IEmpleadosPresentacion,
            IPersonasPresentacion IPersonasPresentacion,
            IServiciosPresentacion IServiciosPresentacion,
            IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion,
            IFacturasPresentacion IFacturasPresentacion,
            IPagosPresentacion IPagosPresentacion)
        {
            this.IReservasPresentacion = IReservasPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IServiciosPresentacion = IServiciosPresentacion;
            this.IDetalle_Reservas_Presentacion = IDetalle_Reservas_Presentacion;
            this.IFacturasPresentacion = IFacturasPresentacion;
            this.IPagosPresentacion = IPagosPresentacion;
        }

        public List<Reservas> Lista { get; set; } = new();
        public List<Clientes> Clientes { get; set; } = new();
        public List<Lib_spa.entidades.Empleados> Empleados { get; set; } = new();
        public List<Personas> Personas { get; set; } = new();
        public List<Lib_spa.entidades.Servicios> Servicios { get; set; } = new();
        public List<Detalle_Reservas> Detalles { get; set; } = new();
        public List<Lib_spa.entidades.Facturas> Facturas { get; set; } = new();
        public List<Pagos> Pagos { get; set; } = new();
        public string Mensaje { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public int FiltroEstado { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FiltroEmpleado { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FiltroFecha { get; set; }

        public List<SelectListItem> ListaEmpleados { get; set; } = new();

        public void OnGet()
        {
            try
            {
                Lista = this.IReservasPresentacion.Consultar()
                    .OrderByDescending(r => r.Fecha).ToList();
                Clientes = this.IClientesPresentacion.Consultar();
                Empleados = this.IEmpleadosPresentacion.Consultar();
                Personas = this.IPersonasPresentacion.Consultar();
                Servicios = this.IServiciosPresentacion.Consultar();
                Detalles = this.IDetalle_Reservas_Presentacion.Consultar();
                Facturas = this.IFacturasPresentacion.Consultar();
                Pagos = this.IPagosPresentacion.Consultar();

                foreach (var empleado in Empleados)
                    ListaEmpleados.Add(new SelectListItem(
                        NombreEmpleado(empleado.Id_Empleado),
                        empleado.Id_Empleado.ToString()));

                if (FiltroEstado != 0)
                    Lista = Lista.Where(r => r.Estado == FiltroEstado).ToList();

                if (FiltroEmpleado != 0)
                    Lista = Lista.Where(r => r.Id_Empleado == FiltroEmpleado).ToList();

                if (FiltroFecha != null)
                    Lista = Lista.Where(r => r.Fecha.Date == FiltroFecha.Value.Date).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las citas.";
            }
        }

        public IActionResult OnPostCompletar(int id)
        {
            return CambiarEstado(id, EstadoReserva.Completada);
        }

        public IActionResult OnPostCancelar(int id)
        {
            return CambiarEstado(id, EstadoReserva.Cancelada);
        }

        private IActionResult CambiarEstado(int id, EstadoReserva nuevo)
        {
            try
            {
                var reserva = this.IReservasPresentacion.Consultar()
                    .FirstOrDefault(r => r.Id_Reserva == id);

                if (reserva != null && reserva.Estado == (int)EstadoReserva.Confirmada)
                {
                    reserva.Estado = (int)nuevo;
                    this.IReservasPresentacion.Actualizar(reserva);
                }

                return RedirectToPage();
            }
            catch (Exception)
            {
                OnGet();
                Mensaje = "No se pudo cambiar el estado de la cita.";
                return Page();
            }
        }

        public string NombreCliente(int idCliente)
        {
            var cliente = Clientes.FirstOrDefault(c => c.Id_Cliente == idCliente);
            if (cliente == null)
                return "";
            return Personas.FirstOrDefault(p => p.Id_Persona == cliente.Persona)?.Nombre ?? "";
        }

        public string NombreEmpleado(int idEmpleado)
        {
            var empleado = Empleados.FirstOrDefault(e => e.Id_Empleado == idEmpleado);
            if (empleado == null)
                return "";
            return Personas.FirstOrDefault(p => p.Id_Persona == empleado.Id_Persona)?.Nombre ?? "";
        }

        public string NombreServicio(int idReserva)
        {
            var detalle = Detalles.FirstOrDefault(d => d.Id_Reserva == idReserva);
            if (detalle == null)
                return "";
            return Servicios.FirstOrDefault(s => s.Id_Servicio == detalle.Id_Servicio)?.Nombre ?? "";
        }

        public Lib_spa.entidades.Facturas? FacturaDe(int idReserva)
        {
            return Facturas.FirstOrDefault(f => f.Id_Reserva == idReserva);
        }

        public bool EstaPagada(int idFactura)
        {
            return Pagos.Any(p => p.Id_Factura == idFactura);
        }
    }
}
