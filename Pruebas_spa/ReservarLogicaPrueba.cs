using Lib_spa.DTOS;
using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.EntityFrameworkCore;
using presentacion_mst.Nucleo;

namespace presentacion_mst
{
    // Comprueba que Reservar crea la cita, la factura y sus detalles.
    // Usa un cliente, un empleado y un servicio que ya existan en la base.
    [TestClass]
    public class ReservarLogicaPrueba
    {
        private IConexion conexion;
        private ReservasAplicacion app;

        public ReservarLogicaPrueba()
        {
            this.conexion = Ayuda.NuevaConexion();
            this.app = new ReservasAplicacion(this.conexion);
        }

        [TestMethod]
        public void Invalidos()
        {
            Ayuda.DebeFallar(() => this.app.Reservar(new ReservaRequest
            { Id_Cliente = 0, Id_Empleado = 1, Id_Servicio = 1, Fecha = DateTime.Now.AddDays(1) }), "sin clienta");
            Ayuda.DebeFallar(() => this.app.Reservar(new ReservaRequest
            { Id_Cliente = 1, Id_Empleado = 0, Id_Servicio = 1, Fecha = DateTime.Now.AddDays(1) }), "sin empleado");
            Ayuda.DebeFallar(() => this.app.Reservar(new ReservaRequest
            { Id_Cliente = 1, Id_Empleado = 1, Id_Servicio = 0, Fecha = DateTime.Now.AddDays(1) }), "sin servicio");
            Ayuda.DebeFallar(() => this.app.Reservar(new ReservaRequest
            { Id_Cliente = 1, Id_Empleado = 1, Id_Servicio = 999999, Fecha = DateTime.Now.AddDays(1) }), "servicio que no existe");
        }

        [TestMethod]
        public void FechaPasada()
        {
            var cliente = this.conexion.Clientes!.FirstOrDefault();
            var empleado = this.conexion.Empleados!.FirstOrDefault();
            var servicio = this.conexion.Servicios!.FirstOrDefault();
            if (cliente == null || empleado == null || servicio == null)
            {
                Assert.Inconclusive("Faltan datos en la base.");
                return;
            }

            Ayuda.DebeFallar(() => this.app.Reservar(new ReservaRequest
            {
                Id_Cliente = cliente.Id_Cliente,
                Id_Empleado = empleado.Id_Empleado,
                Id_Servicio = servicio.Id_Servicio,
                Fecha = DateTime.Now.AddDays(-1)
            }), "fecha pasada");
        }

        [TestMethod]
        public void Reservar()
        {
            var cliente = this.conexion.Clientes!.FirstOrDefault();
            var empleado = this.conexion.Empleados!.FirstOrDefault();
            var servicio = this.conexion.Servicios!.FirstOrDefault();
            if (cliente == null || empleado == null || servicio == null)
            {
                Assert.Inconclusive("Faltan datos en la base.");
                return;
            }

            var respuesta = this.app.Reservar(new ReservaRequest
            {
                Id_Cliente = cliente.Id_Cliente,
                Id_Empleado = empleado.Id_Empleado,
                Id_Servicio = servicio.Id_Servicio,
                Fecha = DateTime.Now.AddYears(5)
            });

            try
            {
                Verificar(respuesta, servicio);
            }
            finally
            {
                Limpiar(respuesta);
            }
        }

        // La cita debe quedar confirmada y con su factura y sus detalles.
        private void Verificar(ReservaRespuesta respuesta, Servicios servicio)
        {
            if (respuesta.Estado != (int)EstadoReserva.Confirmada)
                throw new Exception("La cita no quedó confirmada");

            if (respuesta.Total != servicio.Precio)
                throw new Exception("El total no es el precio del servicio");

            var otra = Ayuda.NuevaConexion();

            if (!otra.Detalle_Reservas!.Any(d => d.Id_Reserva == respuesta.Id_Reserva))
                throw new Exception("No se guardó el detalle de la reserva");

            var factura = otra.Facturas!.FirstOrDefault(f => f.Id_Factura == respuesta.Id_Factura);
            if (factura == null)
                throw new Exception("No se guardó la factura");
            if (factura.Id_Reserva != respuesta.Id_Reserva)
                throw new Exception("La factura es de otra reserva");

            if (!otra.Detalle_Facturas!.Any(d => d.Id_Factura == respuesta.Id_Factura))
                throw new Exception("No se guardó el detalle de la factura");
        }

        // Borra lo que creó la prueba, de los detalles hacia la reserva.
        private void Limpiar(ReservaRespuesta respuesta)
        {
            var otra = Ayuda.NuevaConexion();

            otra.Detalle_Facturas!.RemoveRange(
                otra.Detalle_Facturas!.Where(d => d.Id_Factura == respuesta.Id_Factura).ToList());
            otra.SaveChanges();

            otra.Facturas!.RemoveRange(
                otra.Facturas!.Where(f => f.Id_Factura == respuesta.Id_Factura).ToList());
            otra.SaveChanges();

            otra.Detalle_Reservas!.RemoveRange(
                otra.Detalle_Reservas!.Where(d => d.Id_Reserva == respuesta.Id_Reserva).ToList());
            otra.SaveChanges();

            otra.Reservas!.RemoveRange(
                otra.Reservas!.Where(r => r.Id_Reserva == respuesta.Id_Reserva).ToList());
            otra.SaveChanges();
        }
    }
}
