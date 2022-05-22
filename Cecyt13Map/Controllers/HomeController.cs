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
        public ActionResult Index()
        {
            List<UbicacionViewModel> list = null;//cambiar el Cecyt13MapEntities por Cecyt13Map2Entities
            using (Cecyt13MapEntities db =new Cecyt13MapEntities())
            {
                    list=(from d in db.Ubicacion
                    select new UbicacionViewModel
                    {
                        IdUbi=d.Cve_Ubicacion,
                        NomUbi=d.Nom_Ubicacion
                    }).ToList();
            }
            List<SelectListItem> items = list.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NomUbi.ToString(),
                    Value = d.IdUbi.ToString(),
                    Selected = true
                };
            });
            ViewBag.Items = items;

            return View();
        }
        public ActionResult Mapa()
        {
            List<Salones>esc=null;
            using(BusquedaSalonEntities bus=new BusquedaSalonEntities())
            {
                Cecyt13MapEntities db = new Cecyt13MapEntities();
                esc = (from e in bus.Escuela
                       join u in db.Ubicacion 
                       on e.Id_Ubicacion equals u.Cve_Ubicacion
                     select new Salones
                     {
                         Cve_Salon=e.Cve_Escuela,
                         Nom_salon=e.Nom_Escuela,
                         Piso=e.Piso,
                         IdUbi=new UbicacionViewModel
                         {
                             IdUbi=u.Cve_Ubicacion,
                             NomUbi=u.Nom_Ubicacion
                         }
                     }).ToList();
                     
            }
            List<SelectListItem> items = esc.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nom_salon.ToString(),
                    Value = d.Cve_Salon.ToString(),
                    Selected = true
                };
            });
            ViewBag.Escuela=items;
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

        public ActionResult Contact()
        {
            return View();
        }
        public JsonResult Salones()
        {
            int edificio = 300;
            List<Escuela> lista = new List<Escuela>();
            using (var conexion = new SqlConnection(cadena))
            {
                
                conexion.Open();
                var cmd = new SqlCommand("sp_Lista_Salones", conexion);
                cmd.Parameters.AddWithValue("@EdificioID", edificio);
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
                return Json(new {data=lista}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}