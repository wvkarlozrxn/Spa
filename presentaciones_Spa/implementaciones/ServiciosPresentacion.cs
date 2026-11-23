using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class ServiciosPresentacion : IServiciosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public ServiciosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Servicios Actualizar(Servicios entidad)
        {
            if (entidad.Id_Servicio == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Servicios>(
                "Servicios/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Servicios Borrar(Servicios entidad)
        {
            if (entidad.Id_Servicio == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Servicios>(
                "Servicios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Servicios> Buscar(Servicios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Servicios>>(
                "Servicios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Servicios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Servicios>>(
                "Servicios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Servicios Insertar(Servicios entidad)
        {
            if (entidad.Id_Servicio != 0)
                throw new Exception("Ya se guardo");

            var task = this.IComunicaciones.Ejecutar<Servicios>(
                "Servicios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }
    }
}