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

namespace ProiectColectiv.Controllers
{
    public class AsistentsController : Controller
    {
        private ProiectColectivEntities db = new ProiectColectivEntities();

        // GET: Asistents
        public async Task<ActionResult> Index()
        {
            return View(await db.Asistent.ToListAsync());
        }

        // GET: Asistents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistent asistent = await db.Asistent.FindAsync(id);
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

        public ActionResult ListaProgramari()
        {
            return View();
        }

        public ActionResult ManagementPacienti()
        {
            return View();
        }

        public ActionResult PaginaPacienti()
        {
            return View();
        }

        // POST: Asistents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_Asistent")] Asistent asistent)
        {
            if (ModelState.IsValid)
            {
                db.Asistent.Add(asistent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(asistent);
        }

        // GET: Asistents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistent asistent = await db.Asistent.FindAsync(id);
            if (asistent == null)
            {
                return HttpNotFound();
            }
            return View(asistent);
        }

        // POST: Asistents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_Asistent")] Asistent asistent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asistent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(asistent);
        }

        // GET: Asistents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asistent asistent = await db.Asistent.FindAsync(id);
            if (asistent == null)
            {
                return HttpNotFound();
            }
            return View(asistent);
        }

        // POST: Asistents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Asistent asistent = await db.Asistent.FindAsync(id);
            db.Asistent.Remove(asistent);
            await db.SaveChangesAsync();
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
