using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Compras
    {
        [Key]
        public int Id_Compra { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Compra { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public int Proveedor { get; set; }

        [ForeignKey("Proveedor")] public Proveedores? _Proveedor { get; set; }


        public List<Detalle_Compras>? Detalle_Compras { get; set; }
    }
}