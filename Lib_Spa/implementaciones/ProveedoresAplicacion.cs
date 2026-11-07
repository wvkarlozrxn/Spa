using Lib_spa.entidades;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lib_spa.interfaces
{
    public class ProveedoresAplicacion : IProveedoresAplicacion
    {
        private IConexion IConexion;

        public ProveedoresAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        public Proveedores Actualizar(Proveedores entidad)
        {
            if (entidad.Id_Proveedor == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (!Regex.IsMatch(entidad.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre solo debe contener letras");

            if (!Regex.IsMatch(entidad.Telefono, @"^\d+$") || !Regex.IsMatch(entidad.Telefono, @"^\d+$"))
                throw new Exception("Este campo solo debe contener números");

            var entry = this.IConexion!.Entry<Proveedores>(entidad);
            entry.State = EntityState.Modified;
            this.IConexion!.SaveChanges();
            return entidad;
        }

        public Proveedores Borrar(Proveedores entidad)
        {
            if (entidad.Id_Proveedor == 0)
                throw new Exception("No se guardo");

            this.IConexion.Proveedores!.Remove(entidad!);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Proveedores> Consultar()
        {
            return this.IConexion.Proveedores!
                .ToList();
        }

        public Proveedores Insertar(Proveedores entidad)
        {
            if (entidad.Id_Proveedor != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Telefono) ||
                string.IsNullOrEmpty(entidad.Direccion))
                throw new Exception("Faltan valores");

            if (!Regex.IsMatch(entidad.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre solo debe contener letras");

            if (!Regex.IsMatch(entidad.Telefono, @"^\d+$") || !Regex.IsMatch(entidad.Telefono, @"^\d+$"))
                throw new Exception("Este campo solo debe contener números");

            this.IConexion.Proveedores!.Add(entidad!);
            this.IConexion.SaveChanges();

            return entidad;
        }
    }
}