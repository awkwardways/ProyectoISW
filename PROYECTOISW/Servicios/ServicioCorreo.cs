using Microsoft.EntityFrameworkCore;
using PROYECTOISW.Models;

namespace PROYECTOISW.Servicios
{
    public class ServicioCorreo : IServicioCorreo
    {
        private readonly ProyectoiswContext _contexto;
        public ServicioCorreo(ProyectoiswContext contexto)
        {
            _contexto = contexto;
        }

        public string BuscarCorreo(string correo)
        {
            var encontrado = (from e in _contexto.Usuarios
                             where e.CorreoElectronico == correo
                             select e.CorreoElectronico).SingleOrDefault();
            return encontrado;
        }

        public void GuardarToken(string token, string correo)
        {
            _contexto.Usuarios
                .Where(c => c.CorreoElectronico == correo)
                .ExecuteUpdate(setters => setters.SetProperty(t => t.Token, token));
            _contexto.SaveChanges();
        }

        public bool ValidarCon(string correo, string token)
        {
            var encontrado = (from e in _contexto.Usuarios
                             where e.CorreoElectronico == correo && e.Token == token
                             select e.Token).SingleOrDefault();
            if (encontrado != null) return true; else return false;
        }

        public void ActualizarCon(Usuario usuario, string nuvaCon)
        {
            throw new NotImplementedException();
        }
    }
}
