using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class Detalle_Facturas_Presentacion : IDetalle_Facturas_Presentacion
    {
        private IComunicaciones IComunicaciones;

        public Detalle_Facturas_Presentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Detalle_Facturas Actualizar(Detalle_Facturas entidad)
        {
            if (entidad.Id_Detalle_Factura == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalle_Facturas>(
                "Detalle_Facturas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Detalle_Facturas Borrar(Detalle_Facturas entidad)
        {
            if (entidad.Id_Detalle_Factura == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Detalle_Facturas>(
                "Detalle_Facturas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Detalle_Facturas> Buscar(Detalle_Facturas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalle_Facturas>>(
                "Detalle_Facturas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Detalle_Facturas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalle_Facturas>>(
                "Detalle_Facturas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Detalle_Facturas Insertar(Detalle_Facturas entidad)
        {
            if (entidad.Id_Detalle_Factura != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalle_Facturas>(
                "Detalle_Facturas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Detalle_Facturas entidad)
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