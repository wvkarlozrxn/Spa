namespace Lib_spa.entidades
{
    public class Proveedores
    {
        public int Id_Proveedor { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }

        public List<Compras>? Compras { get; set; }
    }

}