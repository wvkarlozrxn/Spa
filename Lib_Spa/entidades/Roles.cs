using System.ComponentModel.DataAnnotations;

namespace Lib_spa.entidades
{
    public class Roles
    {
        [Key]

        public int Id_Rol { get; set; }
        public string? Rol { get; set; }

    }
}