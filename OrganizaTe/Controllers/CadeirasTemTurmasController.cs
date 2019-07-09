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
        //Esta função vai buscar as cadeiras da turma e lista-os
        public ActionResult Index(int id)
        {
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                //Retorna a lista de turmas
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
        // Esta função faz return da View para adicionar uma cadeira a uma turma
        public ActionResult Create(int id)
        {
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Turmas Turmas = db.Turmas.Where(t => t.ID == id).FirstOrDefault();

            Cursos Cursos = db.Cursos.Where(c => c.ID == Turmas.CursosFK).FirstOrDefault();

            List<Cadeiras> Cadeiras = db.Cadeiras.Where(c=> c.CursosFK == Cursos.ID).ToList();

            CadeirasTemTurmas CadeirasTemTurmas = new CadeirasTemTurmas{ TurmasFK = id };

            return View(new CadeirasToDropDown
            {
                CadeirasTemTurmas = CadeirasTemTurmas,
                Cadeiras = Cadeiras
            });
        }

        // POST: CadeirasTemTurmas/Create
        // Esta função verifica se a cadeira já existe na turma se não liga-a 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CadeirasToDropDown CadeirasToDropDown)
        {
            if (ModelState.IsValid)
            {
                if (db.CadeirasTemTurmas.Where(p => p.CadeirasFK == CadeirasToDropDown.CadeirasTemTurmas.CadeirasFK && p.TurmasFK == CadeirasToDropDown.CadeirasTemTurmas.TurmasFK).FirstOrDefault() == null)
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
        // Esta função verifica se a cadeira já existe na turma, se sim mostra a view para eliminar a mesma
        public ActionResult Delete(int idCadeira, int idTurma)
        {
            if (db.CadeirasTemTurmas.Where(p => p.CadeirasFK == idCadeira && p.TurmasFK == idTurma).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CadeirasTemTurmas CadeirasTemTurmas = db.CadeirasTemTurmas.Where(p => p.CadeirasFK == idCadeira && p.TurmasFK == idTurma).FirstOrDefault();
            if (CadeirasTemTurmas == null)
            {
                return HttpNotFound();
            }
            return View(CadeirasTemTurmas);
        }

        // POST: CadeirasTemTurmas/Delete/5
        // Função para apagar o a ligação entre uma cadeira e uma turma
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idCadeira, int idTurma)
        {
            CadeirasTemTurmas CadeirasTemTurmas = db.CadeirasTemTurmas.Where(p => p.CadeirasFK == idCadeira && p.TurmasFK == idTurma).FirstOrDefault();
            int TurmasFK = CadeirasTemTurmas.TurmasFK;
            db.CadeirasTemTurmas.Remove(CadeirasTemTurmas);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = TurmasFK });
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