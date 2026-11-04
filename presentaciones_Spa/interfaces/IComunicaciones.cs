namespace presentaciones_Spa.interfaces
{
    public interface IComunicaciones
    {
        Task<T> Ejecutar<T>(string url, object entidad);
    }
}
