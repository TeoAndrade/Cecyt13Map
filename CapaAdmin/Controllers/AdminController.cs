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
        //Vista de la tabla de administradores
        public ActionResult Admin()
        {
            return View();
        }
        
        //Vista de la tabla de los salones
        public ActionResult Salones()
        {
            return View();
        }
        
        //Vista de la tabla de los edificios
        public ActionResult Edificios()
        {
            return View();
        }
        
        //Vista de una pagina que te mostrara las coordenadas dentro de la imagen
        public ActionResult Coordenadas()
        {
            return View();
        }
        
        //Metodo de tipo Json para que se muestren la lista de administradores
        public JsonResult ListarAdmin()
        {
            List<Administradores> usas = new List<Administradores>();
            usas = new CN_Admin().Listar();

            return Json(new { data = usas }, JsonRequestBehavior.AllowGet);
        }

        //Metodo para guardar un administrador con datos ingresados y que posteriormente se ilustran en la tabla
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

        //Metodo para poder eliminar el administrador indicado
        [HttpPost]
        public JsonResult EliminarAdmin(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Admin().Eliminar(id, out mensaje);

            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }
        
        //Metodo de tipo Json para que se muestren la lista de edificios
        public JsonResult ListarEdificios()
        {
            List<Ubicacion> usas = new List<Ubicacion>();
            usas = new CN_Edificio().Listar();

            return Json(new { data = usas }, JsonRequestBehavior.AllowGet);
        }

        //Metodo para guardar un edificio con datos ingresados y que posteriormente se ilustran en la tabla
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

        //Metodo para poder eliminar el edificio indicado
        [HttpPost]
        public JsonResult EliminarEdificio(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Edificio().Eliminar(id, out mensaje);

            return Json(new { res = resultado, men = mensaje }, JsonRequestBehavior.AllowGet);
        }
        
        //Metodo de tipo Json para que se muestren la lista de salones
        public JsonResult ListarEscuela()
        {
            List<Escuela> usas = new List<Escuela>();
            usas = new CN_Escuela().Listar();

            return Json(new { data = usas }, JsonRequestBehavior.AllowGet);
        }

         //Metodo para guardar un salon con datos ingresados y que posteriormente se ilustran en la tabla
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

        //Metodo para poder eliminar el salon indicado
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
