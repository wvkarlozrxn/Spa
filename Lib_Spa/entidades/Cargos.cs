using System.ComponentModel.DataAnnotations;

namespace Lib_spa.entidades
{
    public class Cargos
    {
        [Key]

        public int Id_Cargo { get; set; }
        public string? Cargo { get; set; }

        public List<Empleados>? Empleados { get; set; }
    }
}