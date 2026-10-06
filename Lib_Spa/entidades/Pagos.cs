using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Pagos
    {
        [Key]
        public int Id_Pago { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public int Id_Factura { get; set; }

        [ForeignKey("Id_Factura")] public Facturas? _Factura { get; set; }

        public List<Metodos_Pagos>? Metodos_Pagos { get; set; }
    }
}