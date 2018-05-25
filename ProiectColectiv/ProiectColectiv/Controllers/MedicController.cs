using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProiectColectiv;
using ProiectColectiv.Models;

namespace ProiectColectiv.Controllers
{
    public class MedicController : Controller
    {
        private ProiectColectivEntities db = new ProiectColectivEntities();

        // GET: Medic
        public ActionResult Index()
        {
            //Utilizator user = (Utilizator)Session["Utilizator"];
            var user = db.Utilizator.Find(3);
            var medic = db.Medic.First(x => x.Utilizator.id_Utilizator == user.id_Utilizator);
            List<Programari> programari = medic.Programari.OrderBy(x => x.DataConsultatiei).ToList();

            medic.Programari.Clear();
            foreach (var item in programari)
            {
                medic.Programari.Add(item);
            }

            return View(medic);
        }

        // GET: Medic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medic medic = db.Medic.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }

        // GET: Medic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Medic")] Medic medic)
        {
            if (ModelState.IsValid)
            {
                db.Medic.Add(medic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medic);
        }

        // GET: Medic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medic medic = db.Medic.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }

        // POST: Medic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Medic")] Medic medic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medic);
        }

        // GET: Medic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medic medic = db.Medic.Find(id);
            if (medic == null)
            {
                return HttpNotFound();
            }
            return View(medic);
        }

        // POST: Medic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medic medic = db.Medic.Find(id);
            db.Medic.Remove(medic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult VizualizareFisa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FisaMedicala fisa = db.FisaMedicala.Find(id);
            if (fisa == null)
            {
                return HttpNotFound();
            }
            return View(fisa);
        }

        [ChildActionOnly]
        public ActionResult StabilireDiagnostic()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddDiagnostic([Bind(Include = "id_Diagnostic,Descriere")] Diagnostic diagnostic)
        {
            ViewData["modal"] = 1;
            var id = Convert.ToInt32(Request.RawUrl.Split('/').Last());
            if (diagnostic.Descriere != null && id != 0)
            {
                diagnostic.Programari = db.Programari.Find(id);
                db.Diagnostic.Add(diagnostic);
                db.SaveChanges();
                ViewData["Info"] = "Diagnosticul a fost salvat cu succes!";
                ViewData["InfoClasses"] = "general-modal-succes";

                return PartialView("GeneralModal");
            }
            ViewData["Info"] = "A aparut o eroare! Te rog incearca din nou!";
            ViewData["InfoClasses"] = "general-modal-eroare";

            return PartialView("GeneralModal");
        }

        [HttpGet]
        public ActionResult EmitereReteta(int? id)
        {
            FisaMedicala fisaMedicala = db.FisaMedicala.Find(id);

            if (id == null || fisaMedicala == null)
            {
                ViewData["Info"] = "A aparut o eroare! Te rog incearca din nou!";
                ViewData["InfoClasses"] = "general-modal-eroare";

                return PartialView("GeneralModal");
            }

            return View("EmitereReteta");
        }

        [HttpPost, ActionName("EmitereReteta")]
        [ValidateAntiForgeryToken]
        public ActionResult Emitere([Bind(Include = "Reteta,Medicamente")] RetetaMedic rm)
        {
            var id = Convert.ToInt32(Request.UrlReferrer.ToString().Split('/').Last());
            var nume = Request.Form["Medicamente.Nume"];
            var cantitate = Request.Form["Medicamente.Cantitate"];
            var administratie = Request.Form["Medicamente.Administrare"];
            List<Medicamente> newlist = MapRequest(nume, cantitate, administratie);

            if (rm.Reteta.Tip != null && rm.Reteta.Durata != null
                && rm.Medicamente.Nume != null && rm.Medicamente.Cantitate != null && rm.Medicamente.Administrare != null)
            {
                Retete ret = new Retete
                {
                    Medicamente = newlist,
                    Tip = rm.Reteta.Tip,
                    Durata = rm.Reteta.Durata,
                    FisaMedicala = db.FisaMedicala.Find(id)

                };
                foreach (var item in newlist)
                {
                    db.Medicamente.Add(item);
                }
                db.Retete.Add(ret);
                db.SaveChanges();
                ViewData["Info"] = "Reteta a fost trimisa cu succes pacientului!";
                ViewData["InfoClasses"] = "general-modal-succes";

                return PartialView("GeneralModal");
            }
            return View();
        }

        private List<Medicamente> MapRequest(string nume, string cantitate, string administratie)
        {
            string[] numes = nume.Split(new char[] { ',' }, StringSplitOptions.None);
            string[] cantitates = cantitate.Split(new char[] { ',' }, StringSplitOptions.None);
            string[] administraties = administratie.Split(new char[] { ',' }, StringSplitOptions.None);
            List<Medicamente> list = new List<Medicamente>();

            for (int i = 0; i < administraties.Count(); i++)
            {
                list.Add(new Medicamente
                {
                    Nume = numes[i],
                    Cantitate = Convert.ToInt32(cantitates[i]),
                    Administrare = administraties[i],
                });
            }

            return list;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
