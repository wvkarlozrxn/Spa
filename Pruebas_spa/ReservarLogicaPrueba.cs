using Lib_spa.DTOS;
using Lib_spa.entidades;
using Lib_spa.interfaces;
using Microsoft.EntityFrameworkCore;
using presentacion_mst.Nucleo;

namespace presentacion_mst
{
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

        [TestMethod]
        public void BorrarCitaConFacturaYPago()
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
                Fecha = DateTime.Now.AddYears(6)
            });

            var otra = Ayuda.NuevaConexion();
            var pago = new Pagos() { Fecha_Pago = DateTime.Now, Id_Factura = respuesta.Id_Factura };
            otra.Pagos!.Add(pago);
            otra.SaveChanges();
            otra.Metodos_Pagos!.Add(new Metodos_Pagos()
            { Descripcion = "Prueba", Metodo_Pago = "Efectivo", Id_Pago = pago.Id_Pago });
            otra.SaveChanges();

            var otraApp = new ReservasAplicacion(Ayuda.NuevaConexion());
            otraApp.Borrar(new Reservas() { Id_Reserva = respuesta.Id_Reserva });

            var verificar = Ayuda.NuevaConexion();
            if (verificar.Reservas!.Any(r => r.Id_Reserva == respuesta.Id_Reserva))
                throw new Exception("La reserva sigue en la base");
            if (verificar.Detalle_Reservas!.Any(d => d.Id_Reserva == respuesta.Id_Reserva))
                throw new Exception("Quedó el detalle de la reserva");
            if (verificar.Facturas!.Any(f => f.Id_Factura == respuesta.Id_Factura))
                throw new Exception("Quedó la factura");
            if (verificar.Detalle_Facturas!.Any(d => d.Id_Factura == respuesta.Id_Factura))
                throw new Exception("Quedó el detalle de la factura");
            if (verificar.Pagos!.Any(p => p.Id_Pago == pago.Id_Pago))
                throw new Exception("Quedó el pago");
            if (verificar.Metodos_Pagos!.Any(m => m.Id_Pago == pago.Id_Pago))
                throw new Exception("Quedó el método de pago");
        }

        [TestMethod]
        public void BorrarSinIdFalla()
        {
            Ayuda.DebeFallar(() => this.app.Borrar(new Reservas() { Id_Reserva = 0 }), "borrar sin id");
        }

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
