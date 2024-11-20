using PROYECTOISW.Models;

namespace PROYECTOISW.Servicios
{
    public interface IServicioCorreo
    {
        string BuscarCorreo(string correo);
        bool ValidarCon(string correo,string token);
        void GuardarToken(string token, string correo);
        void ActualizarCon(Usuario usuario, string nuvaCon);

        void EnviarCorreo(string destino, string token);
    }
}
