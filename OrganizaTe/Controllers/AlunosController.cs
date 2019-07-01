using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class AlunosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Alunos
        public ActionResult Index()
        {
            // obtém os cursos
            var Alunos = db.Alunos
                           .ToList();

            return View(Alunos.ToList());
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alunos Alunos)
        {
            if (ModelState.IsValid)
            {
                if (db.Alunos.ToList().Any(e => e.Nome != Alunos.Nome))
                {
                    int idNovoAluno = 0;
                    try
                    {
                        idNovoAluno = db.Alunos.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovoAluno = 1;
                    }
                    Alunos.ID = idNovoAluno;
                    db.Alunos.Add(Alunos);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Alunos", new { id = Alunos.ID });
                }
            }

            return View(Alunos);
        }

        // GET: Alunos/Edit/5
        public ActionResult Edit(int id)
        {
            if (db.Alunos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Alunos");
            }

            Alunos Alunos = db.Alunos.Find(id);
            if (Alunos == null)
            {
                return HttpNotFound();
            }
            return View(Alunos);
        }

        // POST: Cursos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Email,DataNasc")] Alunos Alunos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Alunos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Alunos);
        }

        // GET: Alunos/Delete/5
        public ActionResult Delete(int id)
        {
            if (db.Alunos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Alunos Alunos = db.Alunos.Find(id);
            if (Alunos == null)
            {
                return HttpNotFound();
            }
            return View(Alunos);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alunos Alunos = db.Alunos.Find(id);
            db.Alunos.Remove(Alunos);
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
