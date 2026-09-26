using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Personas
    {
        public int Id_Persona { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Cedula { get; set; }

        public List<Clientes>? Clientes { get; set; }
        public List<Empleados>? Empleados { get; set; }
    }
}