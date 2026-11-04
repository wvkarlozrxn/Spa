using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Detalle_Facturas
    {
        [Key]
        public int Id_Detalle_Factura { get; set; }
        public int Factura { get; set; }
        public int Servicio { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unidad { get; set; }
        public decimal Subtotal { get; set; }

        [ForeignKey("Factura")] public Facturas? _Factura { get; set; }
        [ForeignKey("Servicio")] public Servicios? _Servicio { get; set; }
    }

}