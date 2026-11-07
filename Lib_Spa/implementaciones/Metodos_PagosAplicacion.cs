using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;

namespace Lib_spa.interfaces
{
    public class Metodos_PagosAplicacion : IMetodos_PagosAplicacion
    {
        private IConexion IConexion;

        public Metodos_PagosAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Metodos_Pagos Actualizar(Metodos_Pagos entidad)
        {
            if (entidad.Id_Detalle_Pago == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Descripcion) ||
                string.IsNullOrEmpty(entidad.Metodo_Pago) ||
                entidad.Id_Pago <= 0)
                throw new Exception("Faltan valores O son inválidos");

            var entry = this.IConexion!.Entry<Metodos_Pagos>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Metodos_Pagos Borrar(Metodos_Pagos entidad)
        {
            if (entidad.Id_Detalle_Pago == 0)
                throw new Exception("No se guardo");

            this.IConexion.Metodos_Pagos!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Metodos_Pagos> Consultar()
        {
            return this.IConexion.Metodos_Pagos!
                .ToList();
        }

        public Metodos_Pagos Insertar(Metodos_Pagos entidad)
        {
            if (entidad.Id_Detalle_Pago != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Descripcion) ||
                string.IsNullOrEmpty(entidad.Metodo_Pago) ||
                entidad.Id_Pago <= 0)
                throw new Exception("Faltan valores O son inválidos ");

            this.IConexion.Metodos_Pagos!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}