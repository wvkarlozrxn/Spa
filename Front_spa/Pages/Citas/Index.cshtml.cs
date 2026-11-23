using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Empleado = Lib_spa.entidades.Empleados;
using Factura = Lib_spa.entidades.Facturas;
using Servicio = Lib_spa.entidades.Servicios;

namespace Front_spa.Pages.Citas
{
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Reservas> Lista { get; set; } = new();
        public List<Reservas> ListaReservas { get; set; } = new();
        public List<Clientes> ListaClientes { get; set; } = new();
        public List<Empleado> ListaEmpleados { get; set; } = new();
        public List<Personas> ListaPersonas { get; set; } = new();
        public List<Servicio> ListaServicios { get; set; } = new();
        public List<Detalle_Reservas> ListaDetalles { get; set; } = new();
        public List<Factura> ListaFacturas { get; set; } = new();
        public List<Pagos> ListaPagos { get; set; } = new();
        public List<Cargos> ListaCargos { get; set; } = new();

        [BindProperty] public int FiltroEstado { get; set; }
        [BindProperty] public int FiltroEmpleado { get; set; }
        [BindProperty] public DateTime? FiltroFecha { get; set; }
        [BindProperty] public Reservas Actual { get; set; } = new();
        [BindProperty] public int IdServicio { get; set; }
        public bool Formulario { get; set; } = false;
        public bool Cancelar { get; set; } = false;
        public string Mensaje { get; set; } = "";

        // Listas para los select del formulario y del filtro
        public List<SelectListItem> Clientas
        {
            get
            {
                return ListaClientes.Select(c => new SelectListItem(
                    NombreCliente(c.Id_Cliente), c.Id_Cliente.ToString())).ToList();
            }
        }

        public List<SelectListItem> Empleadas
        {
            get
            {
                return ListaEmpleados
                    .Where(e => CargoDe(e.Id_Cargo) != "Recepcionista")
                    .Select(e => new SelectListItem(
                        NombreEmpleado(e.Id_Empleado), e.Id_Empleado.ToString())).ToList();
            }
        }

        public List<SelectListItem> Servicios
        {
            get
            {
                return ListaServicios.Select(s => new SelectListItem(
                    s.Nombre + " - " + s.Duracion + " min - " + s.Precio.ToString("N0"),
                    s.Id_Servicio.ToString())).ToList();
            }
        }

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        public void OnPostBtConsultar()
        {
            CargarDatos();
            Filtrar();
        }

        public void OnPostBtNuevo()
        {
            CargarDatos();
            Formulario = true;
            Actual = new Reservas();
            Actual.Fecha = DateTime.Today.AddDays(1).AddHours(9);
        }

        // Sirve para agendar una cita nueva o para reprogramar una existente.
        public void OnPostBtBorrar()
        {
            Datos.Reservas.Actualizar(new Reservas
            {
                Id_Reserva = Actual.Id_Reserva,
                Estado = (int)EstadoReserva.Cancelada
            });
        }

        public void OnPostBtGuardar()
        {
            CargarDatos();
            Formulario = true;

            var nueva = Actual.Id_Reserva == 0;

            if (nueva && (Actual.Id_Cliente == 0 || Actual.Id_Empleado == 0 || IdServicio == 0))
            {
                Mensaje = "Elige la clienta, el empleado y el servicio.";
                return;
            }

            if (Actual.Fecha <= DateTime.Now)
            {
                Mensaje = "La fecha y hora deben ser futuras.";
                return;
            }

            try
            {
                var reserva = ListaReservas.FirstOrDefault(r => r.Id_Reserva == Actual.Id_Reserva);
                var idEmpleado = nueva ? Actual.Id_Empleado : reserva!.Id_Empleado;
                var duracion = nueva
                    ? ListaServicios.First(s => s.Id_Servicio == IdServicio).Duracion
                    : DuracionDe(reserva!.Id_Reserva);

                if (HayChoque(idEmpleado, Actual.Fecha, duracion, Actual.Id_Reserva))
                {
                    Mensaje = "Ese empleado ya tiene una cita en ese horario.";
                    return;
                }

                if (nueva)
                {
                    var datos = new ReservaRequest();
                    datos.Id_Cliente = Actual.Id_Cliente;
                    datos.Id_Empleado = Actual.Id_Empleado;
                    datos.Id_Servicio = IdServicio;
                    datos.Fecha = Actual.Fecha;
                    Datos.Reservas.Reservar(datos);
                }
                else
                {
                    reserva!.Fecha = Actual.Fecha;
                    Datos.Reservas.Actualizar(reserva);
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudo guardar la cita.";
                return;
            }

            Formulario = false;
            OnPostBtConsultar();
        }

        // Reprogramar: solo se puede cambiar la fecha de una cita confirmada.
        public void OnPostBtEditar(string data)
        {
            if (!BuscarCita(data))
                return;

            Formulario = true;
        }

        public void OnPostBtCompletar(string data)
        {
            CambiarEstado(Convert.ToInt32(data), EstadoReserva.Completada);
        }

        public void OnPostBtPregCancelar(string data)
        {
            if (!BuscarCita(data))
                return;

            Filtrar();
            Cancelar = true;
        }

        public void OnPostBtCancelar()
        {
            CambiarEstado(Actual.Id_Reserva, EstadoReserva.Cancelada);
        }

        public void CambiarEstado(int id, EstadoReserva nuevo)
        {
            try
            {
                var reserva = Datos.Reservas.Consultar()
                    .FirstOrDefault(r => r.Id_Reserva == id);

                if (reserva != null && reserva.Estado == (int)EstadoReserva.Confirmada)
                {
                    reserva.Estado = (int)nuevo;
                    Datos.Reservas.Actualizar(reserva);
                }
            }
            catch (Exception)
            {
                Mensaje = "No se pudo cambiar el estado de la cita.";
            }

            var mensaje = Mensaje;
            OnPostBtConsultar();
            Mensaje = mensaje;
        }

        // Dos citas del mismo empleado chocan si una empieza antes de que la otra termine.
        public bool HayChoque(int idEmpleado, DateTime inicio, int duracion, int idReservaActual)
        {
            var fin = inicio.AddMinutes(duracion);

            var otras = ListaReservas.Where(r => r.Id_Empleado == idEmpleado &&
                                                 r.Id_Reserva != idReservaActual &&
                                                 r.Estado != (int)EstadoReserva.Cancelada);

            foreach (var otra in otras)
            {
                var finOtra = otra.Fecha.AddMinutes(DuracionDe(otra.Id_Reserva));
                if (inicio < finOtra && otra.Fecha < fin)
                    return true;
            }

            return false;
        }

        public int DuracionDe(int idReserva)
        {
            var detalle = ListaDetalles.FirstOrDefault(d => d.Id_Reserva == idReserva);
            var servicio = ListaServicios.FirstOrDefault(s => s.Id_Servicio == detalle?.Id_Servicio);
            return servicio?.Duracion ?? 60;
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

        public string CargoDe(int idCargo)
        {
            return ListaCargos.FirstOrDefault(c => c.Id_Cargo == idCargo)?.Cargo ?? "";
        }

        public string NombreServicio(int idReserva)
        {
            var detalle = ListaDetalles.FirstOrDefault(d => d.Id_Reserva == idReserva);
            return ListaServicios.FirstOrDefault(s => s.Id_Servicio == detalle?.Id_Servicio)?.Nombre ?? "";
        }

        public Factura? FacturaDe(int idReserva)
        {
            return ListaFacturas.FirstOrDefault(f => f.Id_Reserva == idReserva);
        }

        public bool EstaPagada(int idFactura)
        {
            return ListaPagos.Any(p => p.Id_Factura == idFactura);
        }

        // Trae todas las listas que necesita la página.
        public void CargarDatos()
        {
            try
            {
                ListaReservas = Datos.Reservas.Consultar();
                ListaClientes = Datos.Clientes.Consultar();
                ListaEmpleados = Datos.Empleados.Consultar();
                ListaPersonas = Datos.Personas.Consultar();
                ListaServicios = Datos.Servicios.Consultar();
                ListaDetalles = Datos.Detalle_Reservas.Consultar();
                ListaFacturas = Datos.Facturas.Consultar();
                ListaPagos = Datos.Pagos.Consultar();
                ListaCargos = Datos.Cargos.Consultar();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las citas.";
            }
        }

        // Aplica los filtros de fecha, empleado y estado.
        public void Filtrar()
        {
            Lista = ListaReservas.OrderByDescending(r => r.Fecha).ToList();

            if (FiltroEstado != 0)
                Lista = Lista.Where(r => r.Estado == FiltroEstado).ToList();

            if (FiltroEmpleado != 0)
                Lista = Lista.Where(r => r.Id_Empleado == FiltroEmpleado).ToList();

            if (FiltroFecha != null)
                Lista = Lista.Where(r => r.Fecha.Date == FiltroFecha.Value.Date).ToList();
        }

        // Busca la cita confirmada y la deja en Actual.
        public bool BuscarCita(string data)
        {
            CargarDatos();

            var id = Convert.ToInt32(data);
            var reserva = ListaReservas.FirstOrDefault(r => r.Id_Reserva == id);

            if (reserva != null && reserva.Estado == (int)EstadoReserva.Confirmada)
            {
                Actual = reserva;
                return true;
            }

            Mensaje = "Solo se pueden modificar las citas confirmadas.";
            Filtrar();
            return false;
        }
    }
}
