using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Metodos_Pagos
    {
        public int Id_Detalle_Pago { get; set; }
        public string? Descripcion { get; set; }
        public string? Metodo_Pago { get; set; }
        public int Id_Pago { get; set; }

        [ForeignKey("Pagos")] public Pagos? _Pago { get; set; }
    }

}