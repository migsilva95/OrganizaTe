using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaTe.Models
{
    public class Turmas
    {
        public Turmas()
        {
            ListaDeCadeirasTemTurmas = new HashSet<CadeirasTemTurmas>();
            ListaDeInscricoes = new HashSet<Inscricoes>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Ano { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Turma { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Horario { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Semestre { get; set; }

        // referente às cadeiras da turma
        public virtual ICollection<CadeirasTemTurmas> ListaDeCadeirasTemTurmas { get; set; }

        // referente às inscrições nas turmas
        public virtual ICollection<Inscricoes> ListaDeInscricoes { get; set; }
    }
}
