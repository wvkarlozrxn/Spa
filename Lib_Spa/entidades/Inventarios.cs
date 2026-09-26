using System.ComponentModel.DataAnnotations.Schema;

namespace Lib_spa.entidades
{
    public class Inventarios
    {
        public int Id_Inventario { get; set; }
        public int Cantidad { get; set; }
        public int Stock_min { get; set; }

        public List<Productos>? Productos { get; set; }
    }

}