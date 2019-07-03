using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class TurmasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Turmas
        public ActionResult Index()
        {
            // obtém as turmas
            var turmas = db.Turmas
                           .ToList();

            return View(turmas.ToList());
        }

        // GET: Turmas/Create
        public ActionResult Create()
        {
            return View(new Turmas());
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Turmas Turmas)
        {
            if (ModelState.IsValid)
            {
                if (db.Cursos.ToList().Any(e => e.ID != Turmas.ID))
                {
                    int idNovaTurma = 0;
                    try
                    {
                        idNovaTurma = db.Turmas.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovaTurma = 1;
                    }
                    Turmas.ID = idNovaTurma;
                    db.Turmas.Add(Turmas);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Turmas", new { id = Turmas.ID });
                }
            }

            return View(Turmas);
        }

        // GET: Turmas/Edit/5
        public ActionResult Edit(int id)
        {
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Turmas");
            }

            Turmas Turmas = db.Turmas.Find(id);
            if (Turmas == null)
            {
                return HttpNotFound();
            }
            return View(Turmas);
        }

        // POST: Turmas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ano,Turma,Horario,Semestre")] Turmas Turmas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Turmas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Turmas);
        }

        // GET: Turmas/Delete/5
        public ActionResult Delete(int id)
        {
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Turmas Turmas = db.Turmas.Find(id);
            if (Turmas == null)
            {
                return HttpNotFound();
            }
            return View(Turmas);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Turmas Turmas = db.Turmas.Find(id);
            db.Turmas.Remove(Turmas);
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
