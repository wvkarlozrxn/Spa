using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Detalle_Facturas
    {
        public int Id_Detalle_Factura { get; set; }
        public int Id_Factura { get; set; }
        public int Id_Servicio { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unidad { get; set; }
        public decimal Subtotal { get; set; }

        [ForeignKey("Facturas")] public Facturas? _Factura { get; set; }
        [ForeignKey("Servicios")] public Servicios? _Servicio { get; set; }
    }

}