using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Clientes
    {
        public int Id_Cliente { get; set; }
        public int Id_Persona { get; set; }

        [ForeignKey("Personas")] public Personas? _Persona { get; set; }

        public List<Reservas>? Reservas { get; set; }
    }

}