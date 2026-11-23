using Pagina = Front_spa.Pages.Reportes.IndexModel;

namespace presentacion_mst
{
    [TestClass]
    public class ReportesPrueba
    {
        private Pagina pagina;

        public ReportesPrueba()
        {
            this.pagina = new Pagina(null!);
            this.pagina.Titulo = "Reporte de prueba";
            this.pagina.Encabezados = new List<string> { "Fecha", "Nombre", "Total" };
            this.pagina.Filas.Add(new List<string> { "10/01/2030", "Ana", "150.000" });
            this.pagina.Filas.Add(new List<string> { "11/01/2030", "Luisa", "90.000" });
        }

        [TestMethod]
        public void GeneraPdf()
        {
            var bytes = this.pagina.CrearPdf();

            if (bytes.Length < 100 || bytes[0] != '%' || bytes[1] != 'P' || bytes[2] != 'D' || bytes[3] != 'F')
                throw new Exception("El archivo no es un PDF válido");
        }

        [TestMethod]
        public void FiltroDeFechas()
        {
            this.pagina.Desde = new DateTime(2030, 1, 10);
            this.pagina.Hasta = new DateTime(2030, 1, 20);

            if (!this.pagina.EnRango(new DateTime(2030, 1, 10, 23, 0, 0)))
                throw new Exception("El primer día debe entrar");
            if (!this.pagina.EnRango(new DateTime(2030, 1, 20, 8, 0, 0)))
                throw new Exception("El último día debe entrar");
            if (this.pagina.EnRango(new DateTime(2030, 1, 9, 23, 59, 0)))
                throw new Exception("Un día antes no debe entrar");
            if (this.pagina.EnRango(new DateTime(2030, 1, 21, 0, 0, 0)))
                throw new Exception("Un día después no debe entrar");
        }
    }
}
