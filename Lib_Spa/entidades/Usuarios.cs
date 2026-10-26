using System.ComponentModel.DataAnnotations;

namespace Lib_spa.entidades
{
    public class Usuarios
    {
        [Key]

        public int Id_Usuario { get; set; }
        public string? Nombre { get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }

    }
}