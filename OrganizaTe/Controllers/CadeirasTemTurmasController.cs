using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class CadeirasTemTurmasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CadeirasTemTurmas/1
        public ActionResult Index(int id)
        {
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Turmas");
            }
           
            var cadeiras = db.CadeirasTemTurmas.Where(ctt => ctt.TurmasFK == id).Select(ctt => ctt.Cadeira);


            Turmas turmas = db.Turmas.Find(id);
            if (turmas == null)
            {
                return HttpNotFound();
            }

            return View(new CadeirasDaTurma { Cadeiras = cadeiras.ToList(), Turma = turmas });

        }

        // GET: CadeirasTemTurmas/Create
        public ActionResult Create(int id)
        {
            if (db.CadeirasTemTurmas.Where(p => p.TurmasFK == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Cursos Curso = db.Cursos.Where(c => c.ID == id).FirstOrDefault();

            List<Cadeiras> Cadeiras = db.Cadeiras.Where(p=> p.CursosFK == Curso.ID).ToList();

            CadeirasTemTurmas CadeirasTemTurmas = new CadeirasTemTurmas{ TurmasFK = id };

            return View(new CadeirasToDropDown
            {
                CadeirasTemTurmas = CadeirasTemTurmas,
                Cadeiras = Cadeiras
            });
        }

        // POST: CadeirasTemTurmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CadeirasToDropDown CadeirasToDropDown)
        {
            if (ModelState.IsValid)
            {
                if (db.CadeirasTemTurmas.ToList().Any(e => e.CadeirasFK != CadeirasToDropDown.CadeirasTemTurmas.CadeirasFK || e.TurmasFK != CadeirasToDropDown.CadeirasTemTurmas.TurmasFK))
                {
                    int idNovo = 0;
                    try
                    {
                        idNovo = db.CadeirasTemTurmas.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovo = 1;
                    }
                    CadeirasToDropDown.CadeirasTemTurmas.ID = idNovo;
                    db.CadeirasTemTurmas.Add(CadeirasToDropDown.CadeirasTemTurmas);
                    db.SaveChanges();
                    return RedirectToAction("Index", "CadeirasTemTurmas", new { id = CadeirasToDropDown.CadeirasTemTurmas.TurmasFK });
                }
            }

            List<Cadeiras> Cadeiras = db.Cadeiras.ToList();
            CadeirasToDropDown.Cadeiras = Cadeiras;
            return View(CadeirasToDropDown);
        }

        // GET: CadeirasTemTurmas/Delete/5
        public ActionResult Delete(int id)
        {
            if (db.CadeirasTemTurmas.Where(p => p.CadeirasFK == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CadeirasTemTurmas CadeirasTemTurmas = db.CadeirasTemTurmas.Where(p => p.CadeirasFK == id).FirstOrDefault();
            if (CadeirasTemTurmas == null)
            {
                return HttpNotFound();
            }
            return View(CadeirasTemTurmas);
        }

        // POST: CadeirasTemTurmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CadeirasTemTurmas CadeirasTemTurmas = db.CadeirasTemTurmas.Where(p => p.CadeirasFK == id).FirstOrDefault();
            int TurmasFK = CadeirasTemTurmas.TurmasFK;
            db.CadeirasTemTurmas.Remove(CadeirasTemTurmas);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = TurmasFK });
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