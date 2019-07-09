using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OrganizaTe.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // vamos colocar, aqui, as instruções relativas às tabelas 
        // descrever os nomes das tabelas na Base de Dados
        public virtual DbSet<Alunos> Alunos { get; set; } // tabela Alunos
        public virtual DbSet<Cadeiras> Cadeiras { get; set; } // tabela Cadeiras
        public virtual DbSet<CadeirasTemTurmas> CadeirasTemTurmas { get; set; } // tabela CadeirasTemTurmas
        public virtual DbSet<Cursos> Cursos { get; set; } // tabela Cursos
        public virtual DbSet<Inscricoes> Inscricoes { get; set; } // tabela Inscricoes
        public virtual DbSet<Turmas> Turmas { get; set; } // tabela Turmas

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}