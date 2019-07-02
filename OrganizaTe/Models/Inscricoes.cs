﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaTe.Models
{
    public class Inscricoes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // Chave Primária
        
        // FK para a tabela dos Alunos
        [ForeignKey("Aluno")]
        public int AlunosFK { get; set; }
        public virtual Alunos Aluno { get; set; }

        // FK para a tabela das Turmas
        [ForeignKey("Turma")]
        public int TurmasFK { get; set; }
        public virtual Turmas Turma { get; set; }

        // FK para a tabela das Turmas
        [ForeignKey("Cadeira")]
        public int CadeirasFK { get; set; }
        public virtual Cadeiras Cadeira { get; set; }
    }
}
