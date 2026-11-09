using Lib_spa.DTOS;
using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private IConexion IConexion;

        public ReservasAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad.Id_Reserva == 0)
                throw new Exception("No se guardo");

            if (entidad.Estado == 0 ||
                entidad.Id_Empleado == 0 ||
                entidad.Id_Cliente == 0 )
                throw new Exception("Faltan valores O son inválidos");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            var entry = this.IConexion!.Entry<Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Reservas Borrar(Reservas entidad)
        {
            if (entidad.Id_Reserva == 0)
                throw new Exception("No se guardo");

            this.IConexion.Reservas!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Reservas> Consultar()
        {
            return this.IConexion.Reservas!
                .ToList();
        }

        public Reservas Insertar(Reservas entidad)
        {
            if (entidad.Id_Reserva != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Estado == 0 ||
                entidad.Id_Empleado == 0 ||
                entidad.Id_Cliente == 0)
                throw new Exception("Faltan valores O son inválidos");

            if (entidad.Fecha == null)
                entidad.Fecha = DateTime.Now;

            this.IConexion.Reservas!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
        public ReservaRespuesta Reservar(ReservaRequest datos)
        {
            if (datos.Id_Cliente == 0 || datos.Id_Empleado == 0 || datos.Id_Servicio == 0) 
                throw new Exception("Faltan valores O son inválidos");

            var servicio = this.IConexion.Servicios!.Find(datos.Id_Servicio)
                ?? throw new Exception("El servicio no existe");

            var empleado = this.IConexion.Empleados!.Find(datos.Id_Empleado)
                ?? throw new Exception("El empleado no existe");

            var personaEmpleado = this.IConexion.Personas!.Find(empleado.Id_Persona);

            if (this.IConexion.Clientes!.Find(datos.Id_Cliente) == null)
                throw new Exception("El cliente no existe");

            var inicio = datos.Fecha;
            if (inicio <= DateTime.Now)
                throw new Exception("La fecha debe ser futura");


            using var tx = this.IConexion.Database.BeginTransaction();

            var reserva = new Reservas
            {
                Fecha = inicio,
                Estado = (int)EstadoReserva.Confirmada,
                Id_Empleado = datos.Id_Empleado,
                Id_Cliente = datos.Id_Cliente
            };
            this.IConexion.Reservas!.Add(reserva);
            this.IConexion.SaveChanges();

            this.IConexion.Detalle_Reservas!.Add(new Detalle_Reservas
            {
                Cantidad = 1,
                Precio = servicio.Precio,
                Id_Reserva = reserva.Id_Reserva,
                Id_Servicio = servicio.Id_Servicio
            });
            this.IConexion.SaveChanges();

            var factura = new Facturas
            {
                Fecha = DateTime.Now,
                Subtotal = servicio.Precio,
                Descuento = 0,
                Total = servicio.Precio,
                Id_Reserva = reserva.Id_Reserva
            };
            this.IConexion.Facturas!.Add(factura);
            this.IConexion.SaveChanges();

            this.IConexion.Detalle_Facturas!.Add(new Detalle_Facturas
            {
                Id_Factura = factura.Id_Factura,
                Id_Servicio = servicio.Id_Servicio,
                Cantidad = 1,
                Precio_Unidad = servicio.Precio,
                Subtotal = servicio.Precio
            });
            this.IConexion.SaveChanges();

            tx.Commit();

            return new ReservaRespuesta
            {
                Id_Reserva = reserva.Id_Reserva,
                Estado = reserva.Estado,
                Fecha = inicio,
                Servicio = servicio.Nombre ?? "",
                Empleado = personaEmpleado?.Nombre ?? "",
                Total = factura.Total,
                Id_Factura = factura.Id_Factura
            };
        }
    }
}