using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectColectiv.Controllers
{
    public class ValidationSession : ActionFilterAttribute
    {
        private string functie;

        public ValidationSession(string functie)
        {
            this.functie = functie;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = (Utilizator)filterContext.HttpContext.Session["Utilizator"];
            if (user == null)
                filterContext.Result = new RedirectResult("/");
            else if (user.Functie != this.functie)
                filterContext.Result = new RedirectResult(string.Format("/{0}/Index", user.Functie));
        }
    }
}