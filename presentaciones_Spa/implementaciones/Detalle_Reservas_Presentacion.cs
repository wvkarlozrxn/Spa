using Lib_spa.entidades;
using presentaciones_Spa.interfaces;



namespace presentaciones_Spa.implementaciones
{
    public class Detalle_Reservas_Presentacion : IDetalle_Reservas_Presentacion
    {
        private IComunicaciones IComunicaciones;

        public Detalle_Reservas_Presentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Detalle_Reservas Actualizar(Detalle_Reservas entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalle_Reservas>(
                "Detalle_Reservas/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Detalle_Reservas Borrar(Detalle_Reservas entidad)
        {
            if (entidad.Id_Compra == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Detalle_Reservas>(
                "Detalle_Reservas/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Detalle_Reservas> Buscar(Detalle_Reservas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalle_Reservas>>(
                "Detalle_Reservas/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Detalle_Reservas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Detalle_Reservas>>(
                "Detalle_Reservas/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Detalle_Reservas Insertar(Detalle_Reservas entidad)
        {
            if (entidad.Id_Compra != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Detalle_Reservas>(
                "Detalle_Reservas/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Detalle_Reservas entidad)
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