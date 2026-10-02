using System.ComponentModel.DataAnnotations;

namespace Lib_spa.entidades
{
    public class Turnos
    {
        [Key]
        public int Id_Turno { get; set; }
        public int Dia { get; set; }
        public int Hora_Entrada { get; set; }
        public int Hora_Salida { get; set; }

        public List<Empleados_Turnos>? Empleados_Turnos { get; set; }
    }
}