using OrganizaTe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OrganizaTe.Controllers
{
    public class CursosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cursos
        //Esta função vai buscar os cursos e lista-os
        public ActionResult Index()
        {
            // obtém os cursos
            var cursos = db.Cursos
                           .ToList();

            return View(cursos.ToList());
        }

        // GET: Cursos/Create
        // Esta função faz return da View create cursos
        public ActionResult Create()
        {
            return View(new Cursos());
        }

        // POST: Cursos/Create
        // Esta função verifica se o nome do curso já foi usado, se não cria um novo curso
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cursos Cursos)
        {
            if (ModelState.IsValid)
            {
                if (db.Cursos.ToList().Any(e => e.Nome != Cursos.Nome))
                {
                    int idNovoCurso = 0;
                    try
                    {
                        idNovoCurso = db.Cursos.Max(a => a.ID) + 1;
                    }
                    catch (Exception)
                    {
                        idNovoCurso = 1;
                    }
                    Cursos.ID = idNovoCurso;
                    db.Cursos.Add(Cursos);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cursos", new { id = Cursos.ID });
                }
            }

            return View(Cursos);
        }

        // GET: Cursos/Edit/5
        // Esta função verifica se o curso existe e mostra a view da edição desse curso
        public ActionResult Edit(int id)
        {
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Cursos");
            }

            Cursos Cursos = db.Cursos.Find(id);
            if (Cursos == null)
            {
                return HttpNotFound();
            }
            return View(Cursos);
        }

        // POST: Cursos/Edit/5
        // Esta função dá save no Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome")] Cursos Cursos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Cursos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Cursos);
        }

        // GET: Cursos/Delete/5
        // Esta função verifica se o curso existe e mostra a view para eliminar o curso
        public ActionResult Delete(int id)
        {
            if (db.Cursos.Where(p => p.ID == id).FirstOrDefault() == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Cursos Cursos = db.Cursos.Find(id);
            if (Cursos == null)
            {
                return HttpNotFound();
            }
            return View(Cursos);
        }
        // POST: Cursos/Delete/5
        // Função para apagar o cursos e tudo o que tiver a sua Foreign Key
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cursos Cursos = db.Cursos.Find(id);
            foreach (var turmas in Cursos.ListaDeTurmas.ToList())
            {
                var filePath = Server.MapPath("~/Images/" + turmas.Horario);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                foreach(var cadeirastemturmas in turmas.ListaDeCadeirasTemTurmas.ToList())
                {
                    db.CadeirasTemTurmas.Remove(cadeirastemturmas);
                }
                foreach (var inscricoes in turmas.ListaDeInscricoes.ToList())
                {
                    db.Inscricoes.Remove(inscricoes);
                }
                db.Turmas.Remove(turmas);
            }

            foreach (var Cadeiras in Cursos.ListaDeCadeiras.ToList())
            {
                foreach (var cadeirastemturmas in Cadeiras.ListaDeCadeirasTemTurmas.ToList())
                {
                    db.CadeirasTemTurmas.Remove(cadeirastemturmas);
                }
                foreach (var inscricoes in Cadeiras.ListaDeInscricoes.ToList())
                {
                    db.Inscricoes.Remove(inscricoes);
                }

                db.Cadeiras.Remove(Cadeiras);
            }
            db.Cursos.Remove(Cursos);
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
