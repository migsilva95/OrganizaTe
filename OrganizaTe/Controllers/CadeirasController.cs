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

        // GET: Cadeiras/1
        public ActionResult Index(int id)
        {
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var curso = db.Cursos.Where(p => p.ID == id).FirstOrDefault();

            // obtém as cadeiras de um curso
            var cadeiras = db.Cadeiras
                           .Include(p => p.Curso)
                           .Where(p => p.CursosFK == id);
            
            return View(new CursoEListaCadeiras { Cadeiras = cadeiras.ToList(), Cursos = curso });
        }

        // GET: Cadeiras/Create
        public ActionResult Create(int id)
        {
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new CadeiraIdCurso
            {
                CursoId = id,
                Cadeiras = new Cadeiras()
            });
        }

        // POST: Cadeiras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CadeiraIdCurso CadeiraIdCurso)
        {
            if (ModelState.IsValid)
            {
                if (db.Cadeiras.ToList().Any(e => e.Nome != CadeiraIdCurso.Cadeiras.Nome))
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
                    CadeiraIdCurso.Cadeiras.ID = idNovaCadeira;
                    CadeiraIdCurso.Cadeiras.CursosFK = CadeiraIdCurso.CursoId;
                    db.Cadeiras.Add(CadeiraIdCurso.Cadeiras);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cadeiras", new { id = CadeiraIdCurso.Cadeiras.CursosFK });
                }
            }

            return View(CadeiraIdCurso);
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
                return RedirectToAction("Index", new { id = Cadeiras.CursosFK });
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
            int cursoid = Cadeiras.CursosFK;
            db.Cadeiras.Remove(Cadeiras);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = cursoid });
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
