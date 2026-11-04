using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Detalle_Reservas
    {
        [Key]
        public int Id_Detalle_Reserva { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int Reserva { get; set; }
        public int Servicio { get; set; }

        [ForeignKey("Reserva")] public Reservas? _Reserva { get; set; }
        [ForeignKey("Servicio")] public Servicios? _Servicio { get; set; }
    }

}