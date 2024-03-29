﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using CapaEntidad;

using CapaNegocio;

namespace CapaAdmin.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        //Metodo para cambiar la contraseña del usuario
        public ActionResult CambiarClave()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Administradores admin = new Administradores();
            admin = new CN_Admin().Listar().Where(a => a.Correo == correo && a.Contraseña == CN_Recursos.Encriptar(clave)).FirstOrDefault();
            if (admin == null)
            {
                ViewBag.Error = "Correo o contraseña no correcta";
                return View();
            }
            else
            {
                if (admin.Reestablecer)
                {
                    TempData["idAdmin"] = admin.Cve_Admin;

                    return RedirectToAction("CambiarClave");
                }
                FormsAuthentication.SetAuthCookie(admin.Correo, false);
                ViewBag.Error = null;
                return RedirectToAction("Admin", "Admin");


            }

        }
        /*ivana2615
         dwalkerw_n927j@chyju.com*/
        [HttpPost]
        public ActionResult CambiarClave(string idAdmin, string claveActual, string claveNueva, string claveConfirmada)
        {
            Administradores admin = new Administradores();
            admin = new CN_Admin().Listar().Where(a => a.Cve_Admin == int.Parse(idAdmin)).FirstOrDefault();
            if (admin.Contraseña != CN_Recursos.Encriptar(claveActual))
            {
                TempData["idAdmin"] = idAdmin;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (claveNueva != claveConfirmada)
            {
                TempData["idAdmin"] = idAdmin;
                ViewData["vclave"] = claveActual;
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            ViewData["vclave"] = "";

            claveNueva = CN_Recursos.Encriptar(claveNueva);
            string mensaje = string.Empty;
            bool res = new CN_Admin().CambiarClave(int.Parse(idAdmin), claveNueva, out mensaje);
            if (res)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["idAdmin"] = idAdmin;
                ViewBag.Error = mensaje;
                return View();
            }

        }
        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {

            Administradores admin = new Administradores();
            admin = new CN_Admin().Listar().Where(a => a.Correo == correo).FirstOrDefault();
            if (admin == null)
            {
                ViewBag.Error = "No se encontro un usuario relacionado a un correo";
                return View();
            }
            string mensaje = string.Empty;
            bool res = new CN_Admin().Reestablecer(admin.Cve_Admin, correo, out mensaje);
            if (res)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }

        }
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}