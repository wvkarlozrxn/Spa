using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Clientes
    {
        [Key]
        public int Id_Cliente { get; set; }
        public int Persona { get; set; }

        [ForeignKey("Persona")] public Personas? _Persona { get; set; }

        public List<Reservas>? Reservas { get; set; }
    }

}