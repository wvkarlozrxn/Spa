using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Detalle_Compras
    {
        public int Id_Detalle_Compra { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unidad { get; set; }
        public decimal Subtotal { get; set; }
        public int Id_Compra { get; set; }
        public int Id_Producto { get; set; }

        [ForeignKey("Compras")] public Compras? _Compra { get; set; }
        [ForeignKey("Productos")] public Productos? _Producto { get; set; }
    }

}