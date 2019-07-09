using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrganizaTe.Models
{
    public class Cadeiras
    {
        public Cadeiras()
        {
            ListaDeInscricoes = new HashSet<Inscricoes>();
            ListaDeCadeirasTemTurmas = new HashSet<CadeirasTemTurmas>();
        }

        //Chave Primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //Nome
        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }
        
        // FK para a tabela dos Cursos
        [ForeignKey("Curso")]
        public int CursosFK { get; set; }
        public virtual Cursos Curso { get; set; }

        // referente às inscrições realizadas na cadeira
        public virtual ICollection<Inscricoes> ListaDeInscricoes { get; set; }

        // referente à turmas que essa cadeira está
        public virtual ICollection<CadeirasTemTurmas> ListaDeCadeirasTemTurmas { get; set; }
    }
}
