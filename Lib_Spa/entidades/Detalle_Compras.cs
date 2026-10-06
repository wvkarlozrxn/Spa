using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Detalle_Compras
    {
        [Key]
        public int Id_Detalle_Compra { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unidad { get; set; }
        public decimal Subtotal { get; set; }
        public int Id_Compra { get; set; }
        public int Id_Producto { get; set; }

        [ForeignKey("Id_Compra")] public Compras? _Compra { get; set; }
        [ForeignKey("Id_Producto")] public Productos? _Producto { get; set; }
    }

}