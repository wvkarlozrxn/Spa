using Lib_spa.implementaciones;
using Lib_spa.interfaces;
using Lib_spa.nucleo;

namespace presentacion_mst.Nucleo
{
    public class Ayuda
    {
        public static IConexion NuevaConexion()
        {
            var conexion = new Conexion();
            conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
            return conexion;
        }

        public static void DebeFallar(Action accion, string caso)
        {
            try
            {
                accion();
            }
            catch (Exception ex)
            {
                if (ex is Microsoft.EntityFrameworkCore.DbUpdateException)
                    throw new Exception("Falló en la base y no en la validación: " + caso);

                return;
            }

            throw new Exception("Debió fallar: " + caso);
        }
    }
}
