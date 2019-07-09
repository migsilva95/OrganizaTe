using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class InscricoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inscricoes
        public ActionResult IndexCadeiras()
        {
            if (db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Alunos alunos = db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault();

            // obtém os cursos
            var Inscricoes = db.Inscricoes
                           .Where(i => i.AlunosFK == alunos.ID)
                           .ToList();

            return View(Inscricoes.ToList());
        }
        
        // GET: Inscricoes
        public ActionResult IndexHorarios()
        {
            if (db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Alunos alunos = db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault();

            // obtém os cursos
            var Inscricoes = db.Inscricoes
                           .Where(i => i.AlunosFK == alunos.ID)
                           .ToList();

            return View(Inscricoes.ToList());
        }

        // GET: Inscricoes/CreateInital
        public ActionResult CreateInital()
        {
            if (db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Cursos> Cursos = db.Cursos.ToList();
            
            return View(new ListCursos { Cursos = Cursos });
        }

        // POST: Inscricoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInital(ListCursos ListCursos)
        {
            return RedirectToAction("Create", "Inscricoes", new { id = ListCursos.idCurso });
        }


        // GET: Inscricoes/Create/1
        public ActionResult Create(int id)
        {
            if (db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (db.Cursos.Where(c => c.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Alunos Alunos = db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
            
            Inscricoes inscricoes = new Inscricoes { AlunosFK = Alunos.ID };

            return View(new CadeirasTurmasToDropDown
            {
                Inscricoes = inscricoes,
                Turmas = db.Turmas.Where(t => t.CursosFK == id).ToList(),
                Cadeiras = db.Cadeiras.Where(c => c.CursosFK == id).ToList()
            });
        }

        // POST: Inscricoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inscricoes Inscricoes)
        {
            if (ModelState.IsValid)
            {
                if (db.Inscricoes.ToList().Any(e => e.AlunosFK != Inscricoes.AlunosFK || e.TurmasFK != Inscricoes.TurmasFK || e.CadeirasFK != Inscricoes.CadeirasFK))
                {
                    int idNovaInscricao = 0;
                    try
                    {
                        idNovaInscricao = db.Inscricoes.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovaInscricao = 1;
                    }
                    Inscricoes.ID = idNovaInscricao;
                    db.Inscricoes.Add(Inscricoes);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Inscricoes");
                }
            }

            return View(new CadeirasTurmasToDropDown
            {
                Inscricoes = Inscricoes,
                Turmas = db.Turmas.ToList(),
                Cadeiras = db.Cadeiras.ToList()
            });
        }

        // GET: Inscricoes/Delete/5
        public ActionResult Delete(int id)
        {
            if (db.Inscricoes.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Inscricoes Inscricoes = db.Inscricoes.Where(p => p.ID == id).FirstOrDefault();
            if (Inscricoes == null)
            {
                return HttpNotFound();
            }
            return View(Inscricoes);
        }

        // POST: Inscricoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscricoes Inscricoes = db.Inscricoes.Where(p => p.ID == id).FirstOrDefault();
            db.Inscricoes.Remove(Inscricoes);
            db.SaveChanges();
            return RedirectToAction("IndexCadeiras");
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
