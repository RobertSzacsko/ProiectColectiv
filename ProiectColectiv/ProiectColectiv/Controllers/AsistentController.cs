using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProiectColectiv;
using System.Data.SqlClient;

namespace ProiectColectiv.Controllers
{
    public class AsistentController : Controller
    {
        private ProiectColectivEntities db = new ProiectColectivEntities();

        // GET: Asistents
        [ValidationSession("Asistent")]
        public ActionResult Index()
        {
            return RedirectToAction("ListaProgramari");
        }
        [ValidationSession("Asistent")]
        // GET: Asistents/Create
        public ActionResult Create()
        {
            return View();
        }
        [ValidationSession("Asistent")]
        public ActionResult ListaProgramari()
        {
            var user = (Utilizator)Session["Utilizator"];
            var asistent = db.Asistent.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            return (View(asistent));
        }
        [HttpGet]
        [ValidationSession("Asistent")]
        public ActionResult ConfirmaProgramare(int? idprog)
        {
            var user = (Utilizator)Session["Utilizator"];
            var asistent = db.Asistent.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            
            
            db.Programari.First(x => x.id_Programare == idprog).StatusProgramare = "Stabilit";
            db.SaveChanges();
            return RedirectToAction("ListaProgramari");
        }
        [ValidationSession("Asistent")]
        public ActionResult ManagementProgramari(int? id)
        {
            Pacient pacient =  db.Pacient.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (pacient == null)
            {
                return HttpNotFound();
            }
            return View(pacient);
        }
        [ValidationSession("Asistent")]
        public ActionResult ManagementPacienti([Bind(Include = "id_Pacient")] Pacient newPacient)
        {
            /*if (ModelState.IsValid)
            {
                db.Pacient.Add(newPacient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            return View(newPacient);
        }
        [ValidationSession("Asistent")]
        // POST: Asistents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Asistent")] Asistent asistent)
        {
            if (ModelState.IsValid)
            {
                db.Asistent.Add(asistent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asistent);
        }
        [ValidationSession("Asistent")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistent asistent = db.Asistent.Find(id);
            if (asistent == null)
            {
                return HttpNotFound();
            }
            return View(asistent);
        }
        [ValidationSession("Asistent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Asistent")] Asistent asistent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asistent);
        }
        [ValidationSession("Asistent")]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }
    }
}
