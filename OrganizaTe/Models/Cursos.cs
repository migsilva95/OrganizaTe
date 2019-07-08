using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaTe.Models
{
    public class Cursos
    {
        public Cursos()
        {
            ListaDeCadeiras = new HashSet<Cadeiras>();
            ListaDeTurmas = new HashSet<Turmas>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // Chave Primária

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatório!")]
        public string Nome { get; set; }

        // referente à quantidade de cadeiras no curso
        public virtual ICollection<Cadeiras> ListaDeCadeiras { get; set; }

        // referente à quantidade de turmas no curso
        public virtual ICollection<Turmas> ListaDeTurmas { get; set; }
    }
}
