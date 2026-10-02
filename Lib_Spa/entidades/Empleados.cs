using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public  class Empleados 
    {
        [Key]
        public int Id_Empleado { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Cargo { get; set; }
        public DateTime Fecha_Contrato { get; set; }

        [ForeignKey("Id_Cargo")] public Cargos? _Cargo { get; set; }
        [ForeignKey("Id_Persona")] public Personas? _Persona { get; set; }

        public List<Reservas>? Reservas { get; set; }
        public List<Empleados_Turnos>? Empleados_Turnos { get; set; }
    }
}