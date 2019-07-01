using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class CadeirasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cadeiras
        public ActionResult Index()
        {
            // obtém as Cadeiras
            var Cadeiras = db.Cadeiras
                           .Include(c => c.Curso)
                           .ToList();

            return View(Cadeiras.ToList());
        }

        // POST: Cadeiras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cadeiras Cadeiras)
        {
            if (ModelState.IsValid)
            {
                if (db.Cadeiras.ToList().Any(e => e.Nome != Cadeiras.Nome))
                {
                    int idNovaCadeira = 0;
                    try
                    {
                        idNovaCadeira = db.Cadeiras.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovaCadeira = 1;
                    }
                    Cadeiras.ID = idNovaCadeira;
                    db.Cadeiras.Add(Cadeiras);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cadeiras", new { id = Cadeiras.ID });
                }
            }

            return View(Cadeiras);
        }

        // GET: Cadeiras/Edit/5
        public ActionResult Edit(int id)
        {
            if (db.Cadeiras.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Cadeiras");
            }

            Cadeiras Cadeiras = db.Cadeiras.Find(id);
            if (Cadeiras == null)
            {
                return HttpNotFound();
            }
            return View(Cadeiras);
        }

        // POST: Cadeiras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,CursosFK")] Cadeiras Cadeiras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Cadeiras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Cadeiras);
        }

        // GET: Cadeiras/Delete/5
        public ActionResult Delete(int id)
        {
            if (db.Cadeiras.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Cadeiras Cadeiras = db.Cadeiras.Find(id);
            if (Cadeiras == null)
            {
                return HttpNotFound();
            }
            return View(Cadeiras);
        }

        // POST: Cadeiras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cadeiras Cadeiras = db.Cadeiras.Find(id);
            db.Cadeiras.Remove(Cadeiras);
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
