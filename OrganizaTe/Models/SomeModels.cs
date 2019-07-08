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

    public class CursoEListaTurmas
    {
        public Cursos Cursos { get; set; }
        public List<Turmas> Turmas { get; set; }
    }

    public class TurmaIdCurso
    {
        public int CursoId { get; set; }
        public Turmas Turmas { get; set; }
    }

    public class CadeirasDaTurma
    {
        public Turmas Turma { get; set; }
        public List<Cadeiras> Cadeiras { get; set; }
    }

    public class CadeirasToDropDown
    {
        public CadeirasTemTurmas CadeirasTemTurmas { get; set; }
        public List<Cadeiras> Cadeiras { get; set; }
    }

    public class CadeirasTurmasToDropDown
    {
        public Inscricoes Inscricoes { get; set; }
        public List<Cadeiras> Cadeiras { get; set; }
        public List<Turmas> Turmas { get; set; }
    }
}