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
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var curso = db.Cursos.Where(p => p.ID == id).FirstOrDefault();

            // obtém as turmas de uma cadeira
            var turmas = db.Turmas
                           .Include(p => p.Curso)
                           .Where(p => p.CursosFK == id);

            return View(new CursoEListaTurmas { Turmas = turmas.ToList(), Cursos = curso });
        }

        // GET: Turmas/Details/5
        public ActionResult Details(int id)
        {
            if (db.Turmas.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Turmas");
            }

            Turmas turmas = db.Turmas.Find(id);
            if (turmas == null)
            {
                return HttpNotFound();
            }
            return View(turmas);
        }

        // GET: Turmas/Create
        public ActionResult Create(int id)
        {
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new TurmaIdCurso
            {
                CursoId = id,
                Turmas = new Turmas()
            });
        }

        // POST: Turmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TurmaIdCurso TurmaIdCurso, HttpPostedFileBase uploadHorario)
        {
            if (ModelState.IsValid)
            {
                if (db.Turmas.ToList().Any(e => e.ID != TurmaIdCurso.Turmas.ID))
                {
                    // validar se o horario foi fornecido
                    if (uploadHorario != null)
                    {
                        Cursos curso = db.Cursos.Where(p => p.ID == TurmaIdCurso.CursoId).FirstOrDefault();
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
                        TurmaIdCurso.Turmas.ConcatText = "Ano: " + TurmaIdCurso.Turmas.Ano + "Turma: " + TurmaIdCurso.Turmas.Turma + "Semestre: " + TurmaIdCurso.Turmas.Semestre;
                        uploadHorario.SaveAs(pathImagens);
                    }
                    int idNovaTurma = 0;
                    try
                    {
                        idNovaTurma = db.Turmas.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovaTurma = 1;
                    }
                    TurmaIdCurso.Turmas.ID = idNovaTurma;
                    TurmaIdCurso.Turmas.CursosFK = TurmaIdCurso.CursoId;
                    db.Turmas.Add(TurmaIdCurso.Turmas);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Turmas", new { id = TurmaIdCurso.CursoId });
                }
            }

            return View(TurmaIdCurso.Turmas);
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
            int cursoid = Turmas.CursosFK;
            var filePath = Server.MapPath("~/Images/" + Turmas.Horario);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            db.Turmas.Remove(Turmas);
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
