using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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
        [ValidationSession("Medic")]
        public ActionResult Index()
        {
            Utilizator user = (Utilizator)Session["Utilizator"];
            var medic = db.Medic.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();
            List<Programari> programari = medic.Programari.OrderBy(x => x.DataConsultatiei).ToList();

            medic.Programari.Clear();
            foreach (var item in programari)
            {
                medic.Programari.Add(item);
            }

            return View(medic);
        }
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
        public ActionResult DeleteConfirmed(int id)
        {
            Medic medic = db.Medic.Find(id);
            db.Medic.Remove(medic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
        public ActionResult StabilireDiagnostic()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidationSession("Medic")]
        public ActionResult AddDiagnostic([Bind(Include = "id_Diagnostic,Descriere")] Diagnostic diagnostic)
        {
            ViewData["modal"] = 1;
            var id = Convert.ToInt32(Request.RawUrl.Split('/').Last());
            if (diagnostic.Descriere != null && id != 0)
            {
                diagnostic.Programari = db.Programari.Find(id);
                diagnostic.id_Diagnostic = db.Diagnostic.Where(x => x.Programari.id_Programare == diagnostic.Programari.id_Programare).First().id_Diagnostic;
                db.Diagnostic.Where(x => x.id_Diagnostic == diagnostic.id_Diagnostic).First().Descriere = diagnostic.Descriere;
                
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
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
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

        [ValidationSession("Medic")]
        public ActionResult Asistent()
        {
            return View();
        }

        [HttpPost]
        [ValidationSession("Medic")]
        public ActionResult Asistent([Bind(Include = "CodUtilizator,Nume,Prenume,Parola")] Utilizator ut)
        {
            var user = (Utilizator)Session["Utilizator"];
            DatePersonale dp = new DatePersonale
            {
                CNP = Request.Form["DatePersonale.CNP"],
                Email = Request.Form["DatePersonale.Email"],
                Telefon = Request.Form["DatePersonale.Telefon"],
                Varsta = Convert.ToInt32(Request.Form["DatePersonale.Varsta"]),
                Sex = Request.Form["DatePersonale.Sex"],
                Adresa = Request.Form["DatePersonale.Adresa"],
            };

            if (dp.CNP != null && dp.Email != null && dp.Telefon != null && dp.Varsta != null && dp.Sex != null && dp.Adresa != null
                && ut.CodUtilizator != null && ut.Prenume != null && ut.Nume != null && ut.Parola != null)
            {
                var PAROLA = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(ut.Parola));
                ut.Parola = BitConverter.ToString(PAROLA).Replace("-", "").ToLower();

                db.DatePersonale.Add(dp);
                db.SaveChanges();

                ut.DatePersonale = dp;
                ut.Functie = "Asistent";
                ut.DataCreare = DateTime.Now;
                Medic m = db.Medic.Where(x => x.Utilizator.id_Utilizator == user.id_Utilizator).First();

                db.Utilizator.Add(ut);
                db.SaveChanges();

                Asistent asi = new Asistent
                {
                    Medic = m,
                    Utilizator = ut
                };
                db.Asistent.Add(asi);
                db.SaveChanges();

                ViewData["Info"] = "Asistentul a fost adaugat cu succes!";
                ViewData["InfoClasses"] = "general-modal-succes";
                return PartialView("GeneralModal");
            }
            return View();
        }
        [ValidationSession("Medic")]
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
        [ValidationSession("Medic")]
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
