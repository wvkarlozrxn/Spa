using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Reservas
    {
        public int Id_Reserva { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public int Estado { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_Cliente { get; set; }

      
        [ForeignKey("Empleados")] public Empleados? _Empleado { get; set; }
        [ForeignKey("Clientes")] public Clientes? _Cliente { get; set; }

        public List<Facturas>? Facturas { get; set; }
        public List<Detalle_Reservas>? Detalle_Reservas { get; set; }
    }

}