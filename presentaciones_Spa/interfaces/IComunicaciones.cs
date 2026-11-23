namespace presentaciones_Spa.interfaces
{
    public interface IComunicaciones
    {
        string? Usuario { get; set; }

        Task<T> Ejecutar<T>(string url, object entidad);
    }
}
