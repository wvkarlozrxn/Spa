using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Empleados_Turnos
    {
        public int Id_Empleado_Turno { get; set; }
        public int Id_Turno { get; set; }
        public int Id_Empleado { get; set; }

        [ForeignKey("Turnos")] public Turnos? _Turno { get; set; }
        [ForeignKey("Empleados")] public Empleados? _Empleado { get; set; }
    }

}