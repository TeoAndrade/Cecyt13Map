using Cecyt13Map.Permisos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cecyt13Map.Models;
using Cecyt13Map.Models.Viewmodel;
using System.Collections;

namespace Cecyt13Map.Controllers
{
    [ValidarSesionAtribute]
    public class HomeController : Controller
    {
        //Definicion de la cadena de conexion
        static string cadena = @"Data Source=DESKTOP-BC85JKD\SQL;Initial Catalog=Cecyt13Map;Integrated Security=True";
        
        //Metodo para poder visualizar la lista de edificios
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mapa()
        {
            
            return View();
        }

        public JsonResult Buscar(string buscar)
        {
            List<Busqueda> busquedas = new List<Busqueda>();
            using (var con=new SqlConnection(cadena))
            {
                con.Open();
                SqlCommand cmd=new SqlCommand("BuscarEscuela",con);
                cmd.Parameters.AddWithValue("consulta",buscar);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        busquedas.Add(new Busqueda()
                        {
                            value = Convert.ToInt32(reader["Cve_Escuela"]),
                            label = reader["Coincidencias"].ToString(),
                            Nombre = reader["Nom_Escuela"].ToString(),
                            Piso = reader["Piso"].ToString(),
                            Nom_Edificio = reader["Nom_Ubicacion"].ToString()
                        });
                    }
                }
            }
            return Json(busquedas,JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarUbicacion()
        {
            List<UbicacionViewModel> listaUbicacion = new List<UbicacionViewModel>();
            try
            {
                using (var con = new SqlConnection(cadena))
                {
                    string sql = "select Cve_Ubicacion,Nom_Ubicacion from Ubicacion";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaUbicacion.Add(new UbicacionViewModel()
                            {
                                IdUbi = Convert.ToInt32(reader["Cve_Ubicacion"]),
                                NomUbi = reader["Nom_Ubicacion"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                listaUbicacion = new List<UbicacionViewModel>();
            }
            return Json(new {data=listaUbicacion}, JsonRequestBehavior.AllowGet);
        }

        //Metodo para las redes de la pagina web
        public ActionResult Contact()
        {
            return View();
        }
        
        //Metodo para mostrar una tabla de los salones que pertenecen a un edificio indicado
        public JsonResult Salones(string edificios)
        {
            int edificiovalor = Convert.ToInt32(edificios);
            List<Escuela> lista = new List<Escuela>();
            try
            {
                using (var conexion = new SqlConnection(cadena))
                {

                    conexion.Open();
                    var cmd = new SqlCommand("sp_Lista_Salones", conexion);
                    cmd.Parameters.AddWithValue("@EdificioID", edificiovalor);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Escuela()
                            {
                                Nom_Escuela = dr["Nom_Escuela"].ToString(),
                                Piso = dr["Piso"].ToString(),
                            });
                        }
                    }

                }
            }
            catch
            {
                lista = new List<Escuela>();
            }
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}
