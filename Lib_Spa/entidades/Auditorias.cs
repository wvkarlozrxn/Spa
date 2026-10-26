using System.ComponentModel.DataAnnotations;

namespace Lib_spa.entidades
{
    public class Auditorias
    {
        [Key]

        public int Id_Auditoria { get; set; }
        public DateTime Fecha { get; set; }    

    }
}