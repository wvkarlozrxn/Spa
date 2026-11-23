using Lib_spa.entidades;
using presentaciones_Spa.interfaces;


namespace presentaciones_Spa.implementaciones
{
    public class AuditoriasPresentacion : IAuditoriasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public AuditoriasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Auditorias Actualizar(Auditorias entidad)
        {
            if (entidad.Id_Auditoria == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Auditorias>(
                "Auditorias/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Auditorias Borrar(Auditorias entidad)
        {
            if (entidad.Id_Auditoria == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Auditorias>(
                "Auditorias/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Auditorias> Buscar(Auditorias entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Auditorias>>(
                "Auditorias/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Auditorias> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Auditorias>>(
                "Auditorias/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Auditorias Insertar(Auditorias entidad)
        {
            if (entidad.Id_Auditoria != 0)
                throw new Exception("Ya se guardo");

            var task = this.IComunicaciones.Ejecutar<Auditorias>(
                "Auditorias/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

    }
}