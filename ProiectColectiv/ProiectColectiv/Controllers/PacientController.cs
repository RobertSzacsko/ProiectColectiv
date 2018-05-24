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
        //GET: Pacient
        public ActionResult IstoricFisa()
        {
            //var pacient = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.First(x => x.Utilizator.Nume == "Popa" && x.Utilizator.Prenume == "Valeria");
            return View(pacient);
        }

        public ActionResult DatePersonale()
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            return View(pacient);
        }


        public ActionResult Programare()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index","Login");
        }
    }

}
