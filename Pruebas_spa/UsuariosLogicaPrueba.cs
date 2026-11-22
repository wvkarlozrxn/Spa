using Lib_spa.DTOS;
using Lib_spa.entidades;
using Lib_spa.interfaces;
using presentacion_mst.Nucleo;

namespace presentacion_mst
{
    // Comprueba el inicio de sesión: usuario y contraseña.
    [TestClass]
    public class UsuariosLogicaPrueba
    {
        private IConexion conexion;
        private UsuariosAplicacion app;
        private Usuarios? entidad = null;

        public UsuariosLogicaPrueba()
        {
            this.conexion = Ayuda.NuevaConexion();
            this.app = new UsuariosAplicacion(this.conexion);
        }

        [TestMethod]
        public void Execute()
        {
            Insertar();
            ValidarCorrecto();
            ValidarIncorrecto();
            Borrar();
        }

        public void Insertar()
        {
            this.entidad = new Usuarios()
            {
                Usuario = "UsuarioLogica",
                Contraseña = "clave1234",
                Id_Rol = 1
            };
            this.entidad = this.app.Insertar(this.entidad);
        }

        // Con los datos correctos debe devolver el mismo usuario.
        private void ValidarCorrecto()
        {
            var datos = new UsuariosRequest()
            {
                Usuario = "UsuarioLogica",
                Contraseña = "clave1234"
            };
            var resultado = this.app.Validar(datos);

            if (resultado.Id_Usuario != this.entidad!.Id_Usuario)
                throw new Exception("Devolvió otro usuario");
            if (resultado.Id_Rol != 1)
                throw new Exception("Devolvió otro rol");
        }

        private void ValidarIncorrecto()
        {
            Ayuda.DebeFallar(() => this.app.Validar(new UsuariosRequest
            { Usuario = "UsuarioLogica", Contraseña = "otra" }), "contraseña equivocada");
            Ayuda.DebeFallar(() => this.app.Validar(new UsuariosRequest
            { Usuario = "NoExiste", Contraseña = "clave1234" }), "usuario que no existe");
            Ayuda.DebeFallar(() => this.app.Validar(new UsuariosRequest
            { Usuario = "", Contraseña = "clave1234" }), "usuario vacío");
            Ayuda.DebeFallar(() => this.app.Validar(new UsuariosRequest
            { Usuario = "UsuarioLogica", Contraseña = "" }), "contraseña vacía");
        }

        private void Borrar()
        {
            this.app.Borrar(this.entidad!);
        }
    }
}
