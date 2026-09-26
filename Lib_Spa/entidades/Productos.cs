using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Productos
    {
        public int Id_Producto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio_Compra { get; set; }
        public decimal Precio_Venta { get; set; }
        public int Id_Inventario { get; set; }

        [ForeignKey("Inventarios")] public Inventarios? _Inventario { get; set; }
        public List<Detalle_Compras>? Detalle_Compras { get; set; }
    }

}