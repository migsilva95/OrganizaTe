using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizaTe.Models
{
    public class CadeirasTemTurmas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } // Chave Primária

        // FK para a tabela das Turmas
        [ForeignKey("Turmas")]
        public int TurmasFK { get; set; }
        public virtual Turmas Turma { get; set; }

        // FK para a tabela das Turmas
        [ForeignKey("Cadeiras")]
        public int CadeirasFK { get; set; }
        public virtual Cadeiras Cadeira { get; set; }
    }

    internal class CadeirasTemTurmasConfiguration : IEntityTypeConfiguration<CadeirasTemTurmas>
    {
        public void Configure(EntityTypeBuilder<CadeirasTemTurmas> builder)
        {
            builder.HasKey(t => t.ID);
        }
    }
}
