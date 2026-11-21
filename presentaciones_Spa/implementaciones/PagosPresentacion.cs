using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class PagosPresentacion : IPagosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public PagosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Pagos Actualizar(Pagos entidad)
        {
            if (entidad.Id_Pago == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Pagos>(
                "Pagos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Pagos Borrar(Pagos entidad)
        {
            if (entidad.Id_Pago == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Pagos>(
                "Pagos/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Pagos> Buscar(Pagos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Pagos>>(
                "Pagos/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Pagos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Pagos>>(
                "Pagos/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Pagos Insertar(Pagos entidad)
        {
            if (entidad.Id_Pago != 0)
                throw new Exception("Ya se guardo");

            var task = this.IComunicaciones.Ejecutar<Pagos>(
                "Pagos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }
    }
}