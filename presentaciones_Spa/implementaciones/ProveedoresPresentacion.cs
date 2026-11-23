using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class ProveedoresPresentacion : IProveedoresPresentacion
    {
        private IComunicaciones IComunicaciones;

        public ProveedoresPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Proveedores Actualizar(Proveedores entidad)
        {
            if (entidad.Id_Proveedor == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Proveedores>(
                "Proveedores/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Proveedores Borrar(Proveedores entidad)
        {
            if (entidad.Id_Proveedor == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Proveedores>(
                "Proveedores/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Proveedores> Buscar(Proveedores entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Proveedores>>(
                "Proveedores/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Proveedores> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Proveedores>>(
                "Proveedores/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Proveedores Insertar(Proveedores entidad)
        {
            if (entidad.Id_Proveedor != 0)
                throw new Exception("Ya se guardo");

            var task = this.IComunicaciones.Ejecutar<Proveedores>(
                "Proveedores/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

    }
}