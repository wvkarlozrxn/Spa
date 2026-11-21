using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class PersonasPresentacion : IPersonasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public PersonasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Personas Actualizar(Personas entidad)
        {
            if (entidad.Id_Persona == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Personas>(
                "Personas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Personas Borrar(Personas entidad)
        {
            if (entidad.Id_Persona == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Personas>(
                "Personas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Personas> Buscar(Personas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Personas>>(
                "Personas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Personas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Personas>>(
                "Personas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Personas Insertar(Personas entidad)
        {
            if (entidad.Id_Persona != 0)
                throw new Exception("Ya se guardo");

            var task = this.IComunicaciones.Ejecutar<Personas>(
                "Personas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }
    }
}