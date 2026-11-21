using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Citas
{
    public class AgendarModel : PageModel
    {
        private IReservasPresentacion IReservasPresentacion;
        private IClientesPresentacion IClientesPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private IServiciosPresentacion IServiciosPresentacion;
        private ICargosPresentacion ICargosPresentacion;
        private IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion;

        public AgendarModel(IReservasPresentacion IReservasPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IEmpleadosPresentacion IEmpleadosPresentacion,
            IPersonasPresentacion IPersonasPresentacion,
            IServiciosPresentacion IServiciosPresentacion,
            ICargosPresentacion ICargosPresentacion,
            IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion)
        {
            this.IReservasPresentacion = IReservasPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IServiciosPresentacion = IServiciosPresentacion;
            this.ICargosPresentacion = ICargosPresentacion;
            this.IDetalle_Reservas_Presentacion = IDetalle_Reservas_Presentacion;
        }

        [BindProperty]
        public int IdCliente { get; set; }

        [BindProperty]
        public int IdEmpleado { get; set; }

        [BindProperty]
        public int IdServicio { get; set; }

        [BindProperty]
        public DateTime Fecha { get; set; }

        public List<SelectListItem> Clientas { get; set; } = new();
        public List<SelectListItem> Empleados { get; set; } = new();
        public List<SelectListItem> Servicios { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public void OnGet()
        {
            Fecha = DateTime.Today.AddDays(1).AddHours(9);
            CargarListas();
        }

        public IActionResult OnPost()
        {
            CargarListas();

            if (IdCliente == 0 || IdEmpleado == 0 || IdServicio == 0)
            {
                Mensaje = "Elige la clienta, el empleado y el servicio.";
                return Page();
            }

            if (Fecha <= DateTime.Now)
            {
                Mensaje = "La fecha y hora deben ser futuras.";
                return Page();
            }

            try
            {
                var servicios = this.IServiciosPresentacion.Consultar();
                var servicio = servicios.FirstOrDefault(s => s.Id_Servicio == IdServicio);
                if (servicio == null)
                {
                    Mensaje = "El servicio no existe.";
                    return Page();
                }

                var inicio = Fecha;
                var fin = inicio.AddMinutes(servicio.Duracion);

                var detalles = this.IDetalle_Reservas_Presentacion.Consultar();
                var reservasEmpleado = this.IReservasPresentacion.Consultar()
                    .Where(r => r.Id_Empleado == IdEmpleado &&
                                r.Estado != (int)EstadoReserva.Cancelada);

                foreach (var reserva in reservasEmpleado)
                {
                    var detalle = detalles.FirstOrDefault(d => d.Id_Reserva == reserva.Id_Reserva);
                    var servicioReserva = servicios.FirstOrDefault(s => s.Id_Servicio == detalle?.Id_Servicio);
                    var duracion = servicioReserva?.Duracion ?? 60;

                    var inicioReserva = reserva.Fecha;
                    var finReserva = inicioReserva.AddMinutes(duracion);

                    if (inicio < finReserva && inicioReserva < fin)
                    {
                        Mensaje = "Ese empleado ya tiene una cita en ese horario.";
                        return Page();
                    }
                }

                var datos = new ReservaRequest();
                datos.Id_Cliente = IdCliente;
                datos.Id_Empleado = IdEmpleado;
                datos.Id_Servicio = IdServicio;
                datos.Fecha = Fecha;
                this.IReservasPresentacion.Reservar(datos);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo agendar la cita.";
                return Page();
            }
        }

        private void CargarListas()
        {
            try
            {
                var personas = this.IPersonasPresentacion.Consultar();

                foreach (var cliente in this.IClientesPresentacion.Consultar())
                {
                    var nombre = personas.FirstOrDefault(p => p.Id_Persona == cliente.Persona)?.Nombre;
                    Clientas.Add(new SelectListItem(nombre, cliente.Id_Cliente.ToString()));
                }

                var cargos = this.ICargosPresentacion.Consultar();
                foreach (var empleado in this.IEmpleadosPresentacion.Consultar())
                {
                    var cargo = cargos.FirstOrDefault(c => c.Id_Cargo == empleado.Id_Cargo)?.Cargo;
                    if (cargo == "Recepcionista")
                        continue;

                    var nombre = personas.FirstOrDefault(p => p.Id_Persona == empleado.Id_Persona)?.Nombre;
                    Empleados.Add(new SelectListItem(nombre + " (" + cargo + ")", empleado.Id_Empleado.ToString()));
                }

                foreach (var servicio in this.IServiciosPresentacion.Consultar())
                {
                    var texto = servicio.Nombre + " - " + servicio.Duracion + " min - " + servicio.Precio.ToString("N0");
                    Servicios.Add(new SelectListItem(texto, servicio.Id_Servicio.ToString()));
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las listas.";
            }
        }
    }
}
