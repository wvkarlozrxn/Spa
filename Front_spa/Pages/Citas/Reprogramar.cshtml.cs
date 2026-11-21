using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentaciones_Spa.interfaces;

namespace Front_spa.Pages.Citas
{
    public class ReprogramarModel : PageModel
    {
        private IReservasPresentacion IReservasPresentacion;
        private IClientesPresentacion IClientesPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private IServiciosPresentacion IServiciosPresentacion;
        private IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion;

        public ReprogramarModel(IReservasPresentacion IReservasPresentacion,
            IClientesPresentacion IClientesPresentacion,
            IEmpleadosPresentacion IEmpleadosPresentacion,
            IPersonasPresentacion IPersonasPresentacion,
            IServiciosPresentacion IServiciosPresentacion,
            IDetalle_Reservas_Presentacion IDetalle_Reservas_Presentacion)
        {
            this.IReservasPresentacion = IReservasPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IServiciosPresentacion = IServiciosPresentacion;
            this.IDetalle_Reservas_Presentacion = IDetalle_Reservas_Presentacion;
        }

        [BindProperty]
        public DateTime Fecha { get; set; }

        public string Clienta { get; set; } = "";
        public string Servicio { get; set; } = "";
        public string Empleado { get; set; } = "";
        public string Mensaje { get; set; } = "";

        public IActionResult OnGet(int id)
        {
            var reserva = Buscar(id);
            if (reserva == null || reserva.Estado != (int)EstadoReserva.Confirmada)
                return RedirectToPage("Index");

            Fecha = reserva.Fecha;
            CargarDatos(reserva);
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var reserva = Buscar(id);
            if (reserva == null || reserva.Estado != (int)EstadoReserva.Confirmada)
                return RedirectToPage("Index");

            CargarDatos(reserva);

            if (Fecha <= DateTime.Now)
            {
                Mensaje = "La fecha y hora deben ser futuras.";
                return Page();
            }

            try
            {
                var servicios = this.IServiciosPresentacion.Consultar();
                var detalles = this.IDetalle_Reservas_Presentacion.Consultar();

                var detalleActual = detalles.FirstOrDefault(d => d.Id_Reserva == id);
                var duracionActual = servicios
                    .FirstOrDefault(s => s.Id_Servicio == detalleActual?.Id_Servicio)?.Duracion ?? 60;

                var inicio = Fecha;
                var fin = inicio.AddMinutes(duracionActual);

                var otras = this.IReservasPresentacion.Consultar()
                    .Where(r => r.Id_Empleado == reserva.Id_Empleado &&
                                r.Id_Reserva != id &&
                                r.Estado != (int)EstadoReserva.Cancelada);

                foreach (var otra in otras)
                {
                    var detalle = detalles.FirstOrDefault(d => d.Id_Reserva == otra.Id_Reserva);
                    var duracion = servicios
                        .FirstOrDefault(s => s.Id_Servicio == detalle?.Id_Servicio)?.Duracion ?? 60;

                    var inicioOtra = otra.Fecha;
                    var finOtra = inicioOtra.AddMinutes(duracion);

                    if (inicio < finOtra && inicioOtra < fin)
                    {
                        Mensaje = "Ese empleado ya tiene una cita en ese horario.";
                        return Page();
                    }
                }

                reserva.Fecha = Fecha;
                this.IReservasPresentacion.Actualizar(reserva);

                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Mensaje = "No se pudo reprogramar la cita.";
                return Page();
            }
        }

        private Reservas? Buscar(int id)
        {
            try
            {
                return this.IReservasPresentacion.Consultar()
                    .FirstOrDefault(r => r.Id_Reserva == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void CargarDatos(Reservas reserva)
        {
            try
            {
                var personas = this.IPersonasPresentacion.Consultar();

                var cliente = this.IClientesPresentacion.Consultar()
                    .FirstOrDefault(c => c.Id_Cliente == reserva.Id_Cliente);
                Clienta = personas.FirstOrDefault(p => p.Id_Persona == cliente?.Persona)?.Nombre ?? "";

                var empleado = this.IEmpleadosPresentacion.Consultar()
                    .FirstOrDefault(e => e.Id_Empleado == reserva.Id_Empleado);
                Empleado = personas.FirstOrDefault(p => p.Id_Persona == empleado?.Id_Persona)?.Nombre ?? "";

                var detalle = this.IDetalle_Reservas_Presentacion.Consultar()
                    .FirstOrDefault(d => d.Id_Reserva == reserva.Id_Reserva);
                Servicio = this.IServiciosPresentacion.Consultar()
                    .FirstOrDefault(s => s.Id_Servicio == detalle?.Id_Servicio)?.Nombre ?? "";
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar los datos de la cita.";
            }
        }
    }
}
