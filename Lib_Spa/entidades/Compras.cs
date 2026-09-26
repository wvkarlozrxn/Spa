using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Compras
    {
        public int Id_Compra { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Compra { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public int Id_Proveedor { get; set; }

        [ForeignKey("Proveedores")] public Proveedores? _Proveedore { get; set; }


        public List<Detalle_Compras>? Detalle_Compras { get; set; }
    }
}