using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaTe.Models
{
    public class Alunos
    {
        public Alunos()
        {
            ListaDeInscricoes = new HashSet<Inscricoes>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public DateTime DataNasc { get; set; }

        // referente às inscrições dos alunos nas cadeiras e turmas
        public virtual ICollection<Inscricoes> ListaDeInscricoes { get; set; }
    }
}
