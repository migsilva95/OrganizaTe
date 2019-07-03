using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrganizaTe.Models
{
    public class AlunosRegister
    {
        public Alunos Alunos { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
    
    public class CursoEListaCadeiras
    {
        public Cursos Cursos { get; set; }
        public List<Cadeiras> Cadeiras { get; set; }
    }

    public class CadeiraIdCurso
    {
        public int CursoId { get; set; }
        public Cadeiras Cadeiras { get; set; }
    }
}