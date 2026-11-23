using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Roles
    {
        [Key]

        public int Id_Rol { get; set; }
        [Column("Descripcion")]
        public string? Rol { get; set; }

    }
}