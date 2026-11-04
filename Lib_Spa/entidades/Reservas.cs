using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Reservas
    {
        [Key]
        public int Id_Reserva { get; set; }
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
        public int Empleado { get; set; }
        public int Cliente { get; set; }

      
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }

        public List<Facturas>? Facturas { get; set; }
        public List<Detalle_Reservas>? Detalle_Reservas { get; set; }
    }

}