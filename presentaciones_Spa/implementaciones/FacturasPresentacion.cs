using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class FacturasPresentacion : IFacturasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public FacturasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Facturas Actualizar(Facturas entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Facturas Borrar(Facturas entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Facturas> Buscar(Facturas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Facturas>>(
                "Facturas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Facturas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Facturas>>(
                "Facturas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Facturas Insertar(Facturas entidad)
        {
            if (entidad.Id_Compra != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Facturas>(
                "Facturas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Facturas entidad)
        {
            /* return string.IsNullOrEmpty(entidad.) ||
                 string.IsNullOrEmpty(entidad.Nombre) ||
                 string.IsNullOrEmpty(entidad.Lugar) ||
                 entidad.Inicio == null ||
                 entidad.Final == null ||
                 string.IsNullOrEmpty(entidad.Notas);
            */
            return true;
        }
    }
}