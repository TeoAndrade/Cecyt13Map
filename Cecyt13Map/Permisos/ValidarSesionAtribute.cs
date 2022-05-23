using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cecyt13Map.Permisos
{
    //Clase para poder validar que el usuario no entre a otros metodos sin haberse antes logueado
    public class ValidarSesionAtribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["usuario"] == null)
            {
                filterContext.Result = new RedirectResult("~/Menu/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
