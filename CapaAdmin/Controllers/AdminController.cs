using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaAdmin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult Salones()
        {
            return View();
        }
        public ActionResult Edificios()
        {
            return View();
        }
        public ActionResult Coordenadas()
        {
            return View();
        }
        public JsonResult ListarAdmin()
        {
            List<Administradores> usas = new List<Administradores>();
            usas = new CN_Admin().Listar();

            return Json(new { data = usas }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarAdmin(Administradores obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.Cve_Admin == 0)
            {
                resultado = new CN_Admin().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Admin().Editar(obj, out mensaje);
            }
            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }
        //Lucero28<3

        [HttpPost]
        public JsonResult EliminarAdmin(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Admin().Eliminar(id, out mensaje);

            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarEdificios()
        {
            List<Ubicacion> usas = new List<Ubicacion>();
            usas = new CN_Edificio().Listar();

            return Json(new { data = usas }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEdificio(Ubicacion obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.Cve_Ubicacion== 0)
            {
                resultado = new CN_Edificio().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Edificio().Editar(obj, out mensaje);
            }
            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarEdificio(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Edificio().Eliminar(id, out mensaje);

            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarEscuela()
        {
            List<Escuela> usas = new List<Escuela>();
            usas = new CN_Escuela().Listar();

            return Json(new { data = usas }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarEscuela(Escuela obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.CveEscuela == 0)
            {
                resultado = new CN_Escuela().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = new CN_Escuela().Editar(obj, out mensaje);
            }
            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarEscuela(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Escuela().Eliminar(id, out mensaje);

            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}