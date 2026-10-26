using System.ComponentModel.DataAnnotations;

namespace Lib_spa.entidades
{
    public class Permisos
    {
        [Key]

        public int Id_Permiso { get; set; }
        public string? Permiso { get; set; }

    }
}