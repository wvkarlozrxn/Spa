using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Facturas
    {
        public int Id_Factura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public int Id_Reserva { get; set; }

        [ForeignKey("Reservas")] public Reservas? _Reserva { get; set; }
        public List<Detalle_Facturas>? Detalle_Facturas { get; set; }
        public List<Pagos>? Pagos { get; set; }
    }
}