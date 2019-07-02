namespace OrganizaTe.Migrations
{
    using OrganizaTe.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OrganizaTe.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OrganizaTe.Models.ApplicationDbContext context)
        {
            //*********************************************************************
            // adiciona ALUNOS
            var alunos = new List<Alunos> {
                new Alunos {Nome="Jose Alves",DataNasc=DateTime.Parse("02-07-2000")}
            };
            alunos.ForEach(aa => context.Alunos.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();

            //*********************************************************************
            // adiciona CURSOS
            var cursos = new List<Cursos> {
                new Cursos {Nome ="Licenciatura de Engenharia Informática"}
                };
            cursos.ForEach(cc => context.Cursos.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            //*********************************************************************
            // adiciona CADEIRAS
            var cadeiras = new List<Cadeiras> {
                new Cadeiras {Nome="Tecnologias de Internet 1",CursosFK=1},
                new Cadeiras {Nome="Tecnologias de Internet 2",CursosFK=1}
            };
            cadeiras.ForEach(cc => context.Cadeiras.AddOrUpdate(c => new { c.Nome, c.CursosFK }, cc));
            context.SaveChanges();

            //*********************************************************************
            // adiciona TURMAS
            var turmas = new List<Turmas> {
                new Turmas {Ano="1",Turma="A",Semestre="2",Horario="../Home/1A2.jpg"}  // NOT DECIDE YET
            };
            turmas.ForEach(tt => context.Turmas.AddOrUpdate(t => new { t.Ano, t.Turma, t.Semestre }, tt));
            context.SaveChanges();

            //*********************************************************************
            // adiciona INSCRICOES
            var inscricoes = new List<Inscricoes> {
                new Inscricoes {AlunosFK=1,TurmasFK=1,CadeirasFK=1}
            };
            inscricoes.ForEach(ii => context.Inscricoes.AddOrUpdate(i => new { i.AlunosFK, i.TurmasFK, i.CadeirasFK }, ii));
            context.SaveChanges();

            //*********************************************************************
            // adiciona CADEIRASTEMTURMAS
            var cadeirastemturmas = new List<CadeirasTemTurmas> {
                new CadeirasTemTurmas {TurmasFK=1,CadeirasFK=1}
            };
            cadeirastemturmas.ForEach(cc => context.CadeirasTemTurmas.AddOrUpdate(c => new { c.TurmasFK, c.CadeirasFK }, cc));
            context.SaveChanges();
        }
    }
}
