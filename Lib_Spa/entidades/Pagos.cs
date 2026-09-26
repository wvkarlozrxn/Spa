using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Pagos
    {
        public int Id_Pago { get; set; }
        public DateTime Fecha_Pago { get; set; }
        public int Id_Factura { get; set; }

        [ForeignKey("Facturas")] public Facturas? _Factura { get; set; }

        public List<Metodos_Pagos>? Metodos_Pagos { get; set; }
    }
}