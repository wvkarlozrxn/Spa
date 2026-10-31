using aplicaciones_horarios.entidades;
using aplicaciones_horarios.interfaces;
using presentaciones_horarios.interfaces;

namespace aplicaciones_horarios.implementaciones
{
    public class AsignacionesPresentacion : IAsignacionesPresentacion
    {
        private IComunicaciones IComunicaciones;

        public AsignacionesPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Asignaciones Actualizar(Asignaciones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Asignaciones>(
                "Asignaciones/Actualizar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public Asignaciones Borrar(Asignaciones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se ha guardo");

            var task = this.IComunicaciones.Ejecutar<Asignaciones>(
                "Asignaciones/Borrar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Asignaciones> Buscar(Asignaciones entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Asignaciones>>(
                "Asignaciones/Buscar",
                entidad);
            task.Wait();
            return task.Result;
        }

        public List<Asignaciones> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Asignaciones>>(
                "Asignaciones/Consultar",
                null);
            task.Wait();
            return task.Result;
        }

        public Asignaciones Insertar(Asignaciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Asignaciones>(
                "Asignaciones/Insertar",
                entidad);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Asignaciones entidad)
        {
            return string.IsNullOrEmpty(entidad.Codigo) ||
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Lugar) ||
                entidad.Inicio == null ||
                entidad.Final == null ||
                string.IsNullOrEmpty(entidad.Notas);
        }
    }
}