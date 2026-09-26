using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Detalle_Reservas
    {
        public int Id_Detalle_Reserva { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int Id_Reserva { get; set; }
        public int Id_Servicio { get; set; }

        [ForeignKey("Reservas")] public Reservas? _Reserva { get; set; }
        [ForeignKey("Servicios")] public Servicios? _Servicio { get; set; }
    }

}