using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class TurmasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Turmas/1
        public ActionResult Index(int id)
        {
            //Verifica se o curso existe
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Manda o curso que está no ID
            var curso = db.Cursos.Where(p => p.ID == id).FirstOrDefault();

            //Obtém as turmas de um curso
            var turmas = db.Turmas
                           .Include(p => p.Curso)
                           .Where(p => p.CursosFK == id);

            //Retorna a View das turmas
            return View(new CursoEListaTurmas { Turmas = turmas.ToList(), Cursos = curso });
        }

        // GET: Turmas/Details/5
        public ActionResult Details(int id)
        {
            //Verifica se a turma existe
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                //Retorna a lista de turmas
                return RedirectToAction("Index", "Turmas");
            }
            //Vai a procura da turma
            Turmas turmas = db.Turmas.Find(id);
            if (turmas == null)
            {
                return HttpNotFound();
            }
            //Retorna a View da turma clicada
            return View(turmas);
        }

        // GET: Turmas/Create
        public ActionResult Create(int id)
        {
            //Verifica se o curso existe
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                //Retorna a lista de turmas
                return RedirectToAction("Index", "Home");
            }

            //Retorna a View do create da turma 
            return View(new TurmaIdCurso
            {
                //Mete na nova turma o no curso certo e cria a nova turma
                CursoId = id,
                Turmas = new Turmas()
            });
        }

        // POST: Turmas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TurmaIdCurso TurmaIdCurso, HttpPostedFileBase uploadHorario)
        {
            if (ModelState.IsValid)
            {
                //Verifica se a turma já existe 
                if (db.Turmas.ToList().Any(e => e.ID != TurmaIdCurso.Turmas.ID))
                {
                    //Vai buscar o curso
                    Cursos curso = db.Cursos.Where(p => p.ID == TurmaIdCurso.CursoId).FirstOrDefault();
                    // validar se o horario foi fornecido
                    if (uploadHorario != null)
                    {
                        string pathImagens = "";
                        string pasta = "";
                        string nomeImagem = curso.Nome + "_" + TurmaIdCurso.Turmas.Ano + TurmaIdCurso.Turmas.Turma + TurmaIdCurso.Turmas.Semestre + Path.GetExtension(uploadHorario.FileName);

                        // criar o caminho completo até ao sítio onde o ficheiro
                        // será guardado
                        pathImagens = Path.Combine(Server.MapPath("~/Images/"), nomeImagem);

                        pasta = Path.GetDirectoryName(pathImagens);

                        Directory.CreateDirectory(pasta);

                        // guardar o nome do ficheiro na BD
                        TurmaIdCurso.Turmas.Horario = nomeImagem;
                        uploadHorario.SaveAs(pathImagens);
                    }

                    int idNovaTurma = 0;
                    try
                    {
                        //Gera o novo ID
                        idNovaTurma = db.Turmas.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovaTurma = 1;
                    }
                    TurmaIdCurso.Turmas.ConcatText = "Ano: " + TurmaIdCurso.Turmas.Ano + " Turma: " + TurmaIdCurso.Turmas.Turma + " Semestre: " + TurmaIdCurso.Turmas.Semestre;
                    TurmaIdCurso.Turmas.ID = idNovaTurma;
                    TurmaIdCurso.Turmas.CursosFK = TurmaIdCurso.CursoId;
                    //Adiciona nova turma
                    db.Turmas.Add(TurmaIdCurso.Turmas);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Turmas", new { id = TurmaIdCurso.CursoId });
                }
            }

            //Da return das turmas
            return View(TurmaIdCurso.Turmas);
        }

        // GET: Turmas/Edit/5
        public ActionResult Edit(int id)
        {
            //Verifica se a turma existe
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                //Retorna a lista de turmas
                return RedirectToAction("Index", "Turmas");
            }

            //Verifica se a turma existe
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
        public ActionResult Edit(Turmas Turmas, HttpPostedFileBase uploadHorario)
        {
            if (ModelState.IsValid)
            {
                // validar se o horario foi fornecido
                if (uploadHorario != null)
                {
                    Cursos curso = db.Cursos.Where(p => p.ID == Turmas.CursosFK).FirstOrDefault();
                    string pathImagens = "";
                    string pasta = "";
                    string nomeImagem = curso.Nome + "_" + Turmas.Ano + Turmas.Turma + Turmas.Semestre + Path.GetExtension(uploadHorario.FileName);
                    
                    // criar o caminho completo até ao sítio onde o ficheiro
                    // será guardado
                    pathImagens = Path.Combine(Server.MapPath("~/Images/"), nomeImagem);

                    pasta = Path.GetDirectoryName(pathImagens);

                    Directory.CreateDirectory(pasta);

                    // guardar o nome do ficheiro na BD
                    Turmas.ConcatText = "Ano: " + Turmas.Ano + "Turma: " + Turmas.Turma + "Semestre: " + Turmas.Semestre;
                    Turmas.Horario = nomeImagem;
                    uploadHorario.SaveAs(pathImagens);
                }
                db.Entry(Turmas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Turmas.CursosFK });
            }
            return View(Turmas);
        }

        // GET: Turmas/Delete/5
        public ActionResult Delete(int id)
        {
            //Verifica se a turma existe
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                //Retorna a lista de Turmas
                return RedirectToAction("Index", "Home");
            }

            //Verifica se existe 
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
            //Vai à procura da Turma
            Turmas Turmas = db.Turmas.Find(id);
            int cursoid = Turmas.CursosFK;
            var filePath = Server.MapPath("~/Images/" + Turmas.Horario);
            //Verifica se existe o ficheiro 
            if (System.IO.File.Exists(filePath))
            {
                //Apaga o ficheiro do horario da pasta
                System.IO.File.Delete(filePath);
            }
            //Apaga a Turma da tabela
            db.Turmas.Remove(Turmas);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = cursoid });
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
