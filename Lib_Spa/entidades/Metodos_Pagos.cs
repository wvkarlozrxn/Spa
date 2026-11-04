using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Metodos_Pagos
    {
        [Key]
        public int Id_Detalle_Pago { get; set; }
        public string? Descripcion { get; set; }
        public string? Metodo_Pago { get; set; }
        public int Pago { get; set; }

        [ForeignKey("Pago")] public Pagos? _Pago { get; set; }
    }

}