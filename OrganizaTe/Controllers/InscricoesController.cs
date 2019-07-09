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
        // Função para listar as cadeiras inscritas
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
        // Função para mostrar os horários das turmas em que estão inscritas
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
                            .ToList()
                            .GroupBy(i => i.TurmasFK)
                            .Select(i => i.First());

            return View(Inscricoes);
        }

        // GET: Inscricoes/CreateInital
        // Função para criar a View para escolher o curso que tem a cadeira que queremos nos inscrever 
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
        //Da redirect para a View do Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInital(ListCursos ListCursos)
        {
            return RedirectToAction("Create", "Inscricoes", new { id = ListCursos.idCurso });
        }


        // GET: Inscricoes/Create/1
        //Gera a View do create
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
        //Esta função guarda as inscrições na base de dados na tabela Inscricoes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CadeirasTurmasToDropDown CadeirasTurmasToDropDown)
        {
            if (ModelState.IsValid)
            {
                if (db.Inscricoes.ToList().Any(e => e.AlunosFK != CadeirasTurmasToDropDown.Inscricoes.AlunosFK || e.TurmasFK != CadeirasTurmasToDropDown.Inscricoes.TurmasFK || e.CadeirasFK != CadeirasTurmasToDropDown.Inscricoes.CadeirasFK))
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
                    CadeirasTurmasToDropDown.Inscricoes.ID = idNovaInscricao;
                    db.Inscricoes.Add(CadeirasTurmasToDropDown.Inscricoes);
                    db.SaveChanges();
                    return RedirectToAction("IndexCadeiras", "Inscricoes");
                }
            }

            return View(new CadeirasTurmasToDropDown
            {
                Inscricoes = CadeirasTurmasToDropDown.Inscricoes,
                Turmas = db.Turmas.ToList(),
                Cadeiras = db.Cadeiras.ToList()
            });
        }

        // GET: Inscricoes/Delete/5
        //Cria a View para apagar a inscrição
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
        //Função para apagar a inscrição
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inscricoes Inscricoes = db.Inscricoes.Where(p => p.ID == id).FirstOrDefault();
            db.Inscricoes.Remove(Inscricoes);
            db.SaveChanges();
            return RedirectToAction("IndexCadeiras");
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
