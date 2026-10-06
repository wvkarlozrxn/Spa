using System.ComponentModel.DataAnnotations;

namespace Lib_spa.entidades
{
    public class Servicios
    {
        [Key]
        public int Id_Servicio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int Duracion { get; set; }
        public decimal Precio { get; set; }

        public List<Detalle_Reservas>? Detalle_Reservas { get; set; }
        public List<Detalle_Facturas>? Detalle_Facturas { get; set; }
    }

}