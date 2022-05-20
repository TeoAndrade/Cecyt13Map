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
using System.Web.Mvc;

namespace Cecyt13Map.Controllers
{
    
    public class MenuController : Controller
    {
        //Definicion de la cadena de conexion
        static string cadena = @"Data Source=DESKTOP-BC85JKD\SQL;Initial Catalog=Cecyt13Map;Integrated Security=True";


        // GET: Menu
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
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
                return RedirectToAction("Mapa", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }

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