namespace Lib_spa.DTOS
{
    public enum EstadoReserva
    {
        Pendiente = 1,
        Confirmada = 2,
        Completada = 3,
        Cancelada = 4
    }
    public class ReservaRequest
    {
        public int Id_Cliente { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_Servicio { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class ReservaRespuesta
    {
        public int Id_Reserva { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha { get; set; }
        public string Servicio { get; set; } = "";
        public string Empleado { get; set; } = "";
        public decimal Total { get; set; }
        public int Id_Factura { get; set; }
    }
}