using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProiectColectiv.Controllers
{
    public class PacientController : Controller
    {
        ProiectColectivEntities db = new ProiectColectivEntities();
        // GET: Pacient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IstoricFisa()
        {
            var pacienti = db.Pacient;
            return View(pacienti);
        }
        public ActionResult Programare()
        {
            return View();
        }
    }

}
