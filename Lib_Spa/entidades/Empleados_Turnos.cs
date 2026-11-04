using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Empleados_Turnos
    {
        [Key]
        public int Id_Empleado_Turno { get; set; }
        public int Turno { get; set; }
        public int Empleado { get; set; }

        [ForeignKey("Turno")] public Turnos? _Turno { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
    }

}