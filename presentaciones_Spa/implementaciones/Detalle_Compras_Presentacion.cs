using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class Detalle_Compras_Presentacion : IDetalle_Compras_Presentacion
    {
        private IComunicaciones IComunicaciones;

        public Detalle_Compras_Presentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Detalle_Compras Actualizar(Detalle_Compras entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalle_Compras>(
                "Detalle_Compras/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Detalle_Compras Borrar(Detalle_Compras entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Detalle_Compras>(
                "Detalle_Compras/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Detalle_Compras> Buscar(Detalle_Compras entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalle_Compras>>(
                "Detalle_Compras/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Detalle_Compras> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalle_Compras>>(
                "Detalle_Compras/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Detalle_Compras Insertar(Detalle_Compras entidad)
        {
            if (entidad.Id_Compra != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalle_Compras>(
                "Detalle_Compras/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Detalle_Compras entidad)
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