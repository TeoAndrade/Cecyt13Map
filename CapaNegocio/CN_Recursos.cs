using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        public static string GenerarClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }
        public static string Encriptar(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 contra = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] bytes = contra.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        public static bool EnviarCorreo(string correo, string asunto, string menseja)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(correo);
                mail.From = new MailAddress("ivantovar26.hist@gmail.com");
                mail.Subject = asunto;
                mail.Body = menseja;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("ivantovar26.hist@gmail.com", "uvihclibtueinzck"),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                smtp.Send(mail);
                resultado = true;

            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }
    }
}
