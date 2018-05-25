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
        [ValidationSession("Pacient")]
        public ActionResult Index()
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();

            return View(pacient);
        }
        //GET: Pacient
        [ValidationSession("Pacient")]
        public ActionResult IstoricFisa()
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            return View(pacient);
        }
        [ValidationSession("Pacient")]
        public ActionResult DatePersonale()
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            return View(pacient);
        }
        [ValidationSession("Pacient")]
        [HttpGet]
        public ActionResult Programare()
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            return View(pacient);
        }
        [HttpPost]
        [ValidationSession("Pacient")]
        public ActionResult Programare(Programari model)
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            if (ModelState.IsValid)
            {
                model.DataOraEfectuare = DateTime.Now;
                model.StatusProgramare = "Efectuat";
                model.Pacient = pacient;
                model.Medic = pacient.Medic;
                model.Asistent = pacient.Medic.Asistent.First();
                db.Programari.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacient);
        }
        [HttpPost]
        [ValidationSession("Pacient")]
        public ActionResult EditareProgramare(Programari model)
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            var prog = db.Programari.OrderByDescending(x => x.DataConsultatiei).First().id_Programare;
            if (ModelState.IsValid)
            {
                db.Programari.First(x => x.id_Programare == prog).DataConsultatiei = model.DataConsultatiei;
                db.Programari.First(x => x.id_Programare == prog).DataModificarii = DateTime.Now;
                db.Programari.First(x => x.id_Programare == prog).StatusProgramare = "Modificat";
                db.Programari.First(x => x.id_Programare == prog).Pacient = pacient;
                db.Programari.First(x => x.id_Programare == prog).Medic = pacient.Medic;
                db.Programari.First(x => x.id_Programare == prog).Asistent = model.Asistent = pacient.Medic.Asistent.First();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacient);
        }
        [ValidationSession("Pacient")]
        public ActionResult StergeProgramare(Programari model)
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            var prog = db.Programari.OrderByDescending(x => x.DataConsultatiei).First().id_Programare;
            if (ModelState.IsValid)
            {
                db.Programari.Remove(db.Programari.First(x => x.id_Programare == prog));
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacient);
        }

        [HttpPost]
        [ValidationSession("Pacient")]
        public ActionResult Index(Programari model)
        {
            var user = (Utilizator)Session["Utilizator"];
            var pacient = db.Pacient.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            if (ModelState.IsValid)
            {
                model.DataOraEfectuare = DateTime.Now;
                model.StatusProgramare = "Efectuat";
                model.Pacient = pacient;
                model.Medic = pacient.Medic;
                model.Asistent = pacient.Medic.Asistent.First();
                db.Programari.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pacient);
        }
        [ValidationSession("Pacient")]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index","Login");
        }
    }

}
