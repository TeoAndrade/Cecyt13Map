using Cecyt13Map.Models;
using Cecyt13Map.Permisos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using CapaNegocio;
using System.Web.Mvc;

namespace Cecyt13Map.Controllers
{
    
    public class MenuController : Controller
    {
        //Definicion de la cadena de conexion
        static string cadena = @"Data Source=DESKTOP-BC85JKD\SQL;Initial Catalog=Cecyt13Map;Integrated Security=True";


        //Metodo donde se loguea el usuario
        public ActionResult Login()
        {
            return View();
        }
        
        //Metodo donde se registra el usuario
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult CambiarContraseña()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        private Recursos recur=new Recursos();
        
        [HttpPost]
        public ActionResult CambiarContraseña(string correo)
        {
            Usuario usuario = new Usuario();
            usuario=recur.Listar().Where(u=>u.Correo==correo).FirstOrDefault();
            if (usuario == null)
            {
                ViewBag.Error = "Usuario no encontrado";
                return View();
            }
            else
            {
                TempData["Correo"]=usuario.Cve_Usuario;
                TempData["clave"] = CN_Recursos.GenerarClave();
                string asunto = "Cambio de contrseña";
                string mensaje = "<p>Su codigo es:!clave¡</p>";
                mensaje = mensaje.Replace("!clave¡", TempData["clave"].ToString());
                bool res = CN_Recursos.EnviarCorreo(correo, asunto, mensaje);
                if (res)
                {
                    ViewBag.Error = null;
                }
                else
                {
                    ViewBag.Error = "No se pudo enviar el correo";
                }
                return RedirectToAction("Reestablecer","Menu");
            }
        }

        [HttpPost]
        public ActionResult Reestablecer(string clave,string nuevacontra,string confirmar,string id)
        {
            if (clave == TempData["clave"].ToString() && nuevacontra==confirmar)
            {
                try
                {
                    using(SqlConnection conn = new SqlConnection(cadena))
                    {
                        string encriptar=CN_Recursos.Encriptar(nuevacontra);
                        SqlCommand cmd = new SqlCommand("ActualizarContraseña",conn);
                        cmd.Parameters.AddWithValue("Id", int.Parse(id));
                        cmd.Parameters.AddWithValue("Nueva", encriptar);
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        bool res=Convert.ToBoolean( cmd.ExecuteNonQuery());
                        if (res)
                        {
                            ViewBag.Error = null;
                            return RedirectToAction("Login", "Menu");
                        }
                        else
                        {
                            ViewBag.Error = "No se pudo actualizar la contraseña";
                            return View();
                        }
                    }
                }
                catch
                {
                    ViewBag.Error = "Hubo algun prblema interno";
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "No coincide alguno de los datos";
                return View();
            }
        }

        
        //Metodo para validar el usuario
        [HttpPost]
        public ActionResult Login(Usuario user)
        {
            user.Contraseña=ConvertirSha256(user.Contraseña);
            using(SqlConnection con=new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_Login", con);
                cmd.Parameters.AddWithValue("@correo", user.Correo);
                cmd.Parameters.AddWithValue("@contra", user.Contraseña);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                user.Cve_Usuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            if (user.Cve_Usuario != 0)
            {
                Session["usuario"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }
        
        //Metodo para cunado el usuario ingrese los datos requeridos
        [HttpPost]
        public ActionResult SignUp(Usuario user)
        {
            bool registro;
            string mensaje;

            //Verificacion de coincidencia de contraseñas y su encriptacion
            if (user.Contraseña == user.ConfirmarContra)
            {
                user.Contraseña = ConvertirSha256(user.Contraseña);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            using (SqlConnection con = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SP_Registrar", con);
                cmd.Parameters.AddWithValue("@correo", user.Correo);
                cmd.Parameters.AddWithValue("@contra", user.Contraseña);
                cmd.Parameters.AddWithValue("@nombre", user.Nom_Usuario);
                cmd.Parameters.Add("@Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                registro = Convert.ToBoolean(cmd.Parameters["@Registrado"].Value);
                mensaje = Convert.ToString(cmd.Parameters["@mensaje"].Value);
                con.Close();
            }
            ViewData["Mensaje"] = mensaje;
            if (registro)
            {
                return RedirectToAction("Login", "Menu");
            }
            else
            {
                return View();
            }
        }

        //Metodo para encriptar la contraseña
        public static string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 sha256 = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = sha256.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
