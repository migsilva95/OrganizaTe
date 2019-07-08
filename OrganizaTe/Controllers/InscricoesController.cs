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

        // GET: Cursos/Create
        public ActionResult Create()
        {
            Alunos Alunos = db.Alunos.Where(a => a.Email == User.Identity.Name).FirstOrDefault();

            ViewBag.Cadeiras = db.Cadeiras.ToList();
            ViewBag.Turmas = db.Turmas.ToList();

            Inscricoes inscricoes = new Inscricoes { AlunosFK = Alunos.ID };

            return View(new CadeirasTurmasToDropDown
            {
                Inscricoes = inscricoes,
                Turmas = db.Turmas.ToList(),
                Cadeiras = db.Cadeiras.ToList()
        });
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
