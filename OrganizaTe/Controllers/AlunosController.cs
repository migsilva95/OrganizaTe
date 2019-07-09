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
        // Não está a ser utilizada de momento, fica preparado para updates futuros 
        public ActionResult Index()
        {
            // obtém os alunos
            var Alunos = db.Alunos
                           .ToList();

            return View(Alunos.ToList());
        }

        // GET: Alunos/Edit/5
        // Esta função verifica se o aluno existe e mostra a view da edição dela
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
        // Esta função dá save no Edit
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
        // Não está a ser utilizada de momento, fica preparado para updates futuros 
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
        // Não está a ser utilizada de momento, fica preparado para updates futuros 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alunos Alunos = db.Alunos.Find(id);
            db.Alunos.Remove(Alunos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Fecha os ficheiros e ligações à base de dados 
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
