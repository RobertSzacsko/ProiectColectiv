using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProiectColectiv;

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
            foreach(var item in programari)
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

        public ActionResult VizualizareFisa(int ?id)
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
