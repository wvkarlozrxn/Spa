using Front_spa.Pages.Reportes;
using Pagina = Front_spa.Pages.Reportes.IndexModel;

namespace presentacion_mst
{
    // Comprueba que los archivos del reporte se generan bien. No usa la base.
    [TestClass]
    public class ReportesPrueba
    {
        private Pagina pagina;
        private Tabla tabla;

        public ReportesPrueba()
        {
            this.pagina = new Pagina(null!);

            this.tabla = new Tabla();
            this.tabla.Titulo = "Reporte de prueba";
            this.tabla.Encabezados = new List<string> { "Fecha", "Nombre", "Total" };
            this.tabla.Filas.Add(new List<object> { new DateTime(2030, 1, 10, 10, 0, 0), "Ana", 150000m });
            this.tabla.Filas.Add(new List<object> { new DateTime(2030, 1, 11, 9, 30, 0), "Luisa", 90000m });
        }

        [TestMethod]
        public void GeneraPdf()
        {
            var bytes = this.pagina.CrearPdf(this.tabla);

            // todo PDF empieza con "%PDF"
            if (bytes.Length < 100 || bytes[0] != '%' || bytes[1] != 'P' || bytes[2] != 'D' || bytes[3] != 'F')
                throw new Exception("El archivo no es un PDF válido");
        }

        [TestMethod]
        public void GeneraExcel()
        {
            var bytes = this.pagina.CrearExcel(this.tabla);

            // un .xlsx es un zip: empieza con "PK"
            if (bytes.Length < 100 || bytes[0] != 'P' || bytes[1] != 'K')
                throw new Exception("El archivo no es un Excel válido");
        }

        [TestMethod]
        public void Formatos()
        {
            if (this.pagina.Formato(150000m) != "150.000" && this.pagina.Formato(150000m) != "150,000")
                throw new Exception("Los números deben llevar separador de miles");

            if (this.pagina.Formato(new DateTime(2030, 1, 10, 10, 5, 0)) != "10/01/2030 10:05")
                throw new Exception("La fecha tiene otro formato");
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
