using Microsoft.EntityFrameworkCore;
using PROYECTOISW.Models;
//Agregar 
using System.Net;
using System.Net.Mail;

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

        public void EnviarCorreo(string destino, string token)
        {
            try
            {
                MailMessage msg = new MailMessage("hernandez.granados.johan.ipn@gmail.com", destino);
                msg.IsBodyHtml = true;
                msg.Subject = "Recuperación de Contraseña";
                string body = $"Debido a su peticion de restablecimiento de contraseña le hemos enviado este correo.<br>Su código de ruperacion es: <br><b>{token}</b>";
                msg.Body = body;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("hernandez.granados.johan.ipn@gmail.com", "kzvl kqnd krhb uwyn");
                smtp.Send(msg);
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
