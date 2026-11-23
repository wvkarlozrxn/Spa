using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class Metodos_Pagos_Presentacion : IMetodos_Pagos_Presentacion
    {
        private IComunicaciones IComunicaciones;

        public Metodos_Pagos_Presentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Metodos_Pagos Actualizar(Metodos_Pagos entidad)
        {
            if (entidad.Id_Detalle_Pago == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Metodos_Pagos>(
                "Metodos_Pagos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Metodos_Pagos Borrar(Metodos_Pagos entidad)
        {
            if (entidad.Id_Detalle_Pago == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Metodos_Pagos>(
                "Metodos_Pagos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Metodos_Pagos> Buscar(Metodos_Pagos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Metodos_Pagos>>(
                "Metodos_Pagos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Metodos_Pagos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Metodos_Pagos>>(
                "Metodos_Pagos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Metodos_Pagos Insertar(Metodos_Pagos entidad)
        {
            if (entidad.Id_Detalle_Pago != 0)
                throw new Exception("Ya se guardo");

            var task = this.IComunicaciones.Ejecutar<Metodos_Pagos>(
                "Metodos_Pagos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }
    }
}