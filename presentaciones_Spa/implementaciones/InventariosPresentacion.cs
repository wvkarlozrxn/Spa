using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class InventariosPresentacion : IInventariosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public InventariosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Inventarios Actualizar(Inventarios entidad)
        {
            if (entidad.Id_Inventario == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Inventarios>(
                "Inventarios/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Inventarios Borrar(Inventarios entidad)
        {
            if (entidad.Id_Inventario == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Inventarios>(
                "Inventarios/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Inventarios> Buscar(Inventarios entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Inventarios>>(
                "Inventarios/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Inventarios> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Inventarios>>(
                "Inventarios/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Inventarios Insertar(Inventarios entidad)
        {
            if (entidad.Id_Inventario != 0)
                throw new Exception("Ya se guardo");

            var task = this.IComunicaciones.Ejecutar<Inventarios>(
                "Inventarios/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

    }
}