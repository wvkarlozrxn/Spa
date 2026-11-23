using Lib_spa.DTOS;
using Lib_spa.entidades;
using Front_spa.Pages.Citas;

namespace presentacion_mst
{
    [TestClass]
    public class ChoqueHorarioPrueba
    {
        private IndexModel pagina;
        private DateTime diez;

        public ChoqueHorarioPrueba()
        {
            this.diez = new DateTime(2030, 1, 10, 10, 0, 0);

            this.pagina = new IndexModel(null!);
            this.pagina.ListaServicios.Add(new Lib_spa.entidades.Servicios
            { Id_Servicio = 1, Duracion = 60 });
            this.pagina.ListaDetalles.Add(new Detalle_Reservas
            { Id_Reserva = 1, Id_Servicio = 1 });
            this.pagina.ListaReservas.Add(new Reservas
            { Id_Reserva = 1, Id_Empleado = 1, Fecha = this.diez, Estado = (int)EstadoReserva.Confirmada });
        }

        [TestMethod]
        public void ChocaCuandoSeCruza()
        {
            if (!this.pagina.HayChoque(1, this.diez.AddMinutes(30), 60, 0))
                throw new Exception("Debió chocar a las 10:30");

            if (!this.pagina.HayChoque(1, this.diez.AddMinutes(-30), 60, 0))
                throw new Exception("Debió chocar a las 9:30");

            if (!this.pagina.HayChoque(1, this.diez, 30, 0))
                throw new Exception("Debió chocar a las 10:00");
        }

        [TestMethod]
        public void NoChocaEnLosBordes()
        {
            if (this.pagina.HayChoque(1, this.diez.AddMinutes(-60), 60, 0))
                throw new Exception("No debió chocar a las 9:00");

            if (this.pagina.HayChoque(1, this.diez.AddMinutes(60), 60, 0))
                throw new Exception("No debió chocar a las 11:00");
        }

        [TestMethod]
        public void NoChocaConOtroEmpleado()
        {
            if (this.pagina.HayChoque(2, this.diez, 60, 0))
                throw new Exception("No debió chocar con otro empleado");
        }

        [TestMethod]
        public void IgnoraCitasCanceladas()
        {
            this.pagina.ListaReservas[0].Estado = (int)EstadoReserva.Cancelada;

            if (this.pagina.HayChoque(1, this.diez, 60, 0))
                throw new Exception("Una cita cancelada no debe chocar");
        }

        [TestMethod]
        public void IgnoraLaPropiaCita()
        {
            if (this.pagina.HayChoque(1, this.diez.AddMinutes(15), 60, 1))
                throw new Exception("La cita chocó consigo misma");
        }
    }
}
