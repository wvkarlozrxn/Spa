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
            if (entidad.Id_Compra == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Metodos_Pagos>(
                "Metodos_Pagos/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Metodos_Pagos Borrar(Metodos_Pagos entidad)
        {
            if (entidad.Id_Compra == 0)
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
            if (entidad.Id_Compra != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Metodos_Pagos>(
                "Metodos_Pagos/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Metodos_Pagos entidad)
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