namespace Lib_spa.entidades
{
    public class Turnos
    {
        public int Id_Turno { get; set; }
        public DateTime Dia { get; set; }
        public DateTime Hora_Entrada { get; set; }
        public DateTime Hora_Salida { get; set; }

        public List<Empleados_Turnos>? Empleados_Turnos { get; set; }
    }
}