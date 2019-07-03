using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class CursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cursos
        public ActionResult Index()
        {
            // obtém os cursos
            var cursos = db.Cursos
                           .ToList();

            return View(cursos.ToList());
        }

        // GET: Cursos/Create
        public ActionResult Create()
        {
            return View(new Cursos());
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cursos Cursos)
        {
            if (ModelState.IsValid)
            {
                if (db.Cursos.ToList().Any(e => e.Nome != Cursos.Nome))
                {
                    int idNovoCurso = 0;
                    try
                    {
                        idNovoCurso = db.Cursos.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovoCurso = 1;
                    }
                    Cursos.ID = idNovoCurso;
                    db.Cursos.Add(Cursos);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cursos", new { id = Cursos.ID });
                }
            }

            return View(Cursos);
        }

        // GET: Cursos/Edit/5
        public ActionResult Edit(int id)
        {
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Cursos");
            }

            Cursos Cursos = db.Cursos.Find(id);
            if (Cursos == null)
            {
                return HttpNotFound();
            }
            return View(Cursos);
        }

        // POST: Cursos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome")] Cursos Cursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Cursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Cursos);
        }

        // GET: Cursos/Delete/5
        public ActionResult Delete(int id)
        {
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Cursos Cursos = db.Cursos.Find(id);
            if (Cursos == null)
            {
                return HttpNotFound();
            }
            return View(Cursos);
        }

        // POST: Cursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cursos Cursos = db.Cursos.Find(id);
            db.Cursos.Remove(Cursos);
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
