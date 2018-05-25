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
        public ActionResult Index()
        {
            return View(db.Asistent.ToList());
        }

        // GET: Asistents/Details/5
        public ActionResult Details(int? id)
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

        // GET: Asistents/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ListaProgramari(int? id)
        {
            Asistent asistent = db.Asistent.Find(id);
            if (id == null)
            {
                return View(db.Programari.ToList());
            }
            else
            {
                return View(db.Programari.SqlQuery("select * from Programari where id_Asistent = @p0", asistent.id_Asistent));
            }
        }

        public ActionResult ManagementPacienti(int? id)
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

        public ActionResult ManagementProgramari()
        {
            return View();
        }

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

        // GET: Asistents/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Asistents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asistent asistent = db.Asistent.Find(id);
            db.Asistent.Remove(asistent);
            db.SaveChanges();
            return RedirectToAction("Index");
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
