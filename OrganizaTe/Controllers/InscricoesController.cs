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
            return View(new Inscricoes());
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
