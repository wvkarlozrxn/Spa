using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public  class Empleados 
    {
        public int Id_Empleado { get; set; }
        public int Id_Persona { get; set; }
        public int Id_Cargo { get; set; }
        public DateTime Fecha_Contrato { get; set; }

        [ForeignKey("Cargos")] public Cargos? _Cargo { get; set; }
        [ForeignKey("Personas")] public Personas? _Persona { get; set; }

        public List<Reservas>? Reservas { get; set; }
        public List<Empleados_Turnos>? Empleados_Turnos { get; set; }
    }
}