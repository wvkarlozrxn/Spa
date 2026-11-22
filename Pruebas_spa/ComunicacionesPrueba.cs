using Lib_spa.entidades;
using presentaciones_Spa.implementaciones;

namespace presentacion_mst
{
    // Estas pruebas necesitan la API encendida en http://localhost:5094/
    [TestClass]
    public class ComunicacionesPrueba
    {
        private Comunicaciones comunicaciones;

        public ComunicacionesPrueba()
        {
            this.comunicaciones = new Comunicaciones();
        }

        [TestMethod]
        public void Execute()
        {
            Consultar();
            RutaInexistente();
        }

        public void Consultar()
        {
            var task = this.comunicaciones.Ejecutar<List<Servicios>>(
                "Servicios/Consultar",
                null!);
            task.Wait();

            if (task.Result == null || task.Result.Count <= 0)
                throw new Exception("Lista vacia");
        }

        // Una ruta que no existe debe lanzar el error de comunicación.
        public void RutaInexistente()
        {
            try
            {
                var task = this.comunicaciones.Ejecutar<List<Servicios>>(
                    "RutaQueNoExiste/Consultar",
                    null!);
                task.Wait();
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException!.Message != "Error Comunicacion")
                    throw;
                return;
            }

            throw new Exception("Debió lanzar un error de comunicación");
        }
    }
}
