using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Factura = Lib_spa.entidades.Facturas;
using Servicio = Lib_spa.entidades.Servicios;

namespace Front_spa.Pages.Facturas
{
    public class IndexModel : PageModel
    {
        public Datos Datos;

        public IndexModel(Datos Datos)
        {
            this.Datos = Datos;
        }

        public List<Factura> Lista { get; set; } = new();
        public List<Reservas> ListaReservas { get; set; } = new();
        public List<Clientes> ListaClientes { get; set; } = new();
        public List<Personas> ListaPersonas { get; set; } = new();
        public List<Servicio> ListaServicios { get; set; } = new();
        public List<Detalle_Reservas> ListaDetalles { get; set; } = new();
        public List<Pagos> ListaPagos { get; set; } = new();

        [BindProperty] public string FiltroPago { get; set; } = "";
        [BindProperty] public Factura Actual { get; set; } = new();
        [BindProperty] public string Metodo { get; set; } = "";
        public bool Formulario { get; set; } = false;
        public string Mensaje { get; set; } = "";

        public List<string> Metodos { get; set; } = new() { "Efectivo", "Tarjeta", "Transferencia" };

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Lista = Datos.Facturas.Consultar().OrderByDescending(f => f.Fecha).ToList();
                ListaReservas = Datos.Reservas.Consultar();
                ListaClientes = Datos.Clientes.Consultar();
                ListaPersonas = Datos.Personas.Consultar();
                ListaServicios = Datos.Servicios.Consultar();
                ListaDetalles = Datos.Detalle_Reservas.Consultar();
                ListaPagos = Datos.Pagos.Consultar();

                if (FiltroPago == "Pagada")
                    Lista = Lista.Where(f => EstaPagada(f.Id_Factura)).ToList();

                if (FiltroPago == "Pendiente")
                    Lista = Lista.Where(f => !EstaPagada(f.Id_Factura)).ToList();
            }
            catch (Exception)
            {
                Mensaje = "No se pudieron cargar las facturas.";
            }
        }

        public void OnPostBtPagar(string data)
        {
            OnPostBtConsultar();

            var id = Convert.ToInt32(data);
            var factura = Lista.FirstOrDefault(f => f.Id_Factura == id);

            if (factura == null)
            {
                Mensaje = "La factura no existe.";
                return;
            }

            if (EstaPagada(id) || EstaCancelada(factura.Id_Reserva))
            {
                Mensaje = "Esta factura no se puede pagar.";
                return;
            }

            Actual = factura;
            Formulario = true;
        }

        public void OnPostBtGuardar()
        {
            OnPostBtConsultar();

            var factura = Lista.FirstOrDefault(f => f.Id_Factura == Actual.Id_Factura);
            Formulario = true;

            if (factura == null)
            {
                Formulario = false;
                Mensaje = "La factura no existe.";
                return;
            }

            Actual = factura;

            if (!Metodos.Contains(Metodo))
            {
                Mensaje = "Elige un método de pago.";
                return;
            }

            if (EstaPagada(factura.Id_Factura) || EstaCancelada(factura.Id_Reserva))
            {
                Formulario = false;
                Mensaje = "Esta factura no se puede pagar.";
                return;
            }

            try
            {
                var pago = new Pagos();
                pago.Id_Factura = factura.Id_Factura;
                pago.Fecha_Pago = DateTime.Now;
                pago = Datos.Pagos.Insertar(pago);

                var detalle = new Metodos_Pagos();
                detalle.Id_Pago = pago.Id_Pago;
                detalle.Metodo_Pago = Metodo;
                detalle.Descripcion = "Pago de la factura " + factura.Id_Factura;
                Datos.Metodos_Pagos.Insertar(detalle);
            }
            catch (Exception)
            {
                Mensaje = "No se pudo registrar el pago.";
                return;
            }

            Formulario = false;
            OnPostBtConsultar();
        }

        public Reservas? ReservaDe(int idReserva)
        {
            return ListaReservas.FirstOrDefault(r => r.Id_Reserva == idReserva);
        }

        public string NombreCliente(int idReserva)
        {
            var cliente = ListaClientes.FirstOrDefault(c => c.Id_Cliente == ReservaDe(idReserva)?.Id_Cliente);
            return ListaPersonas.FirstOrDefault(p => p.Id_Persona == cliente?.Persona)?.Nombre ?? "";
        }

        public string NombreServicio(int idReserva)
        {
            var detalle = ListaDetalles.FirstOrDefault(d => d.Id_Reserva == idReserva);
            return ListaServicios.FirstOrDefault(s => s.Id_Servicio == detalle?.Id_Servicio)?.Nombre ?? "";
        }

        public bool EstaPagada(int idFactura)
        {
            return ListaPagos.Any(p => p.Id_Factura == idFactura);
        }

        public bool EstaCancelada(int idReserva)
        {
            return ReservaDe(idReserva)?.Estado == (int)EstadoReserva.Cancelada;
        }
    }
}
