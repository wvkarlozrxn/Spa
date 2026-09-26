namespace Lib_spa.entidades
{
    public class Cargos
    {
        public int Id_Cargo { get; set; }
        public string? Cargo { get; set; }

        public List<Empleados>? Empleados { get; set; }
    }
}