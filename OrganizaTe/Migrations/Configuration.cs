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
                new Alunos {Nome="Jose Alves",DataNasc=DateTime.Parse("02-07-1998")},
                new Alunos {Nome="Tania Vieira",DataNasc=DateTime.Parse("02-07-2000")},
                new Alunos {Nome="Antonio Rocha",DataNasc=DateTime.Parse("02-07-1999")},
                new Alunos {Nome="Andre Silveira",DataNasc=DateTime.Parse("02-07-1999")},
                new Alunos {Nome="Lurdes Vieira",DataNasc=DateTime.Parse("02-07-1999")},
                new Alunos {Nome="Claudia Pinto",DataNasc=DateTime.Parse("02-07-1998")}
            };
            alunos.ForEach(aa => context.Alunos.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();

            //*********************************************************************
            // adiciona CURSOS
            var cursos = new List<Cursos> {
                new Cursos {Nome="Licenciatura de Engenharia Informatica"},
                new Cursos {Nome="Licenciatura de Engenharia Electrot�cnica e Computadores"},
                new Cursos {Nome="Licenciatura de Fotografia"}
            };
                
            cursos.ForEach(cc => context.Cursos.AddOrUpdate(c => c.Nome, cc));
            context.SaveChanges();

            //*********************************************************************
            // adiciona CADEIRAS
            var cadeiras = new List<Cadeiras> {
                //-------------1� Ano 1� Semestre------------------
                new Cadeiras {Nome="Algebra",CursosFK=1},
                new Cadeiras {Nome="An�lise I",CursosFK=1},
                new Cadeiras {Nome="Sistemas Digitais",CursosFK=1},
                new Cadeiras {Nome="Introdu��o � Tecnologia",CursosFK=1},
                new Cadeiras {Nome="Introdu��o � Programa��o",CursosFK=1},
                //-------------1� Ano 2� Semestre------------------
                new Cadeiras {Nome="An�lise II",CursosFK=1},
                new Cadeiras {Nome="Introdu��o � Electr�nica Digital",CursosFK=1},
                new Cadeiras {Nome="Programa��o Orientada a Objetos",CursosFK=1},
                new Cadeiras {Nome="Logica e Computa��o",CursosFK=1},
                new Cadeiras {Nome="Tecnologia da Internet I",CursosFK=1},
                //-------------2� Ano 1� Semestre------------------
                new Cadeiras {Nome="Arquitectura de Computadores I",CursosFK=1},
                new Cadeiras {Nome="Base de Dados I",CursosFK=1},
                new Cadeiras {Nome="Estruturas de Dados e Algoritmos",CursosFK=1},
                new Cadeiras {Nome="Introdu��o �s Telecomunica��es",CursosFK=1},
                new Cadeiras {Nome="Probabilida e Estatistica",CursosFK=1},
                //-------------2� Ano 2� Semestre------------------
                new Cadeiras {Nome="Sistemas Operativos",CursosFK=1},
                new Cadeiras {Nome="Tecnologia da Internet II",CursosFK=1},
                new Cadeiras {Nome="Base de Dados II",CursosFK=1},
                new Cadeiras {Nome="Microprocessadores",CursosFK=1},
                new Cadeiras {Nome="Redes de Dados I",CursosFK=1},
                //-------------3� Ano 1� Semestre------------------
                new Cadeiras {Nome="Analise de Sistemas",CursosFK=1},
                new Cadeiras {Nome="Arquitectura de Computadores II",CursosFK=1},
                new Cadeiras {Nome="Gest�o e Seguran�a de Redes Inform�ticas",CursosFK=1},
                new Cadeiras {Nome="Redes de Dados II",CursosFK=1},
                new Cadeiras {Nome="Sistemas Distribu�dos",CursosFK=1},
                //-------------3� Ano 2� Semestre------------------
                new Cadeiras {Nome="Empreendorismo",CursosFK=1},
                new Cadeiras {Nome="Projeto de Redes",CursosFK=1},
                new Cadeiras {Nome="Projecto de Sistemas de Informa��o",CursosFK=1},
                new Cadeiras {Nome="Projeto Final",CursosFK=1},
                new Cadeiras {Nome="Sistemas de Informa��o nas Organiza��es",CursosFK=1}
            };
            cadeiras.ForEach(cc => context.Cadeiras.AddOrUpdate(c => new { c.Nome, c.CursosFK }, cc));
            context.SaveChanges();

            //*********************************************************************
            // adiciona TURMAS
            var turmas = new List<Turmas> {
                //---------------------------- 1� Semestre -----------------------------
                //------------------------------- 1� Ano -------------------------------
                new Turmas {Ano="1", Turma="A", Horario="1A1.jpg", Semestre="1", CursosFK=1},
                new Turmas {Ano="1", Turma="B", Horario="1B1.jpg", Semestre="1", CursosFK=1},
                new Turmas {Ano="1", Turma="C", Horario="1C1.jpg", Semestre="1", CursosFK=1},
                //------------------------------- 2� Ano -------------------------------
                new Turmas {Ano="2", Turma="A", Horario="2A1.jpg", Semestre="1", CursosFK=1},
                new Turmas {Ano="2", Turma="B", Horario="2B1.jpg", Semestre="1", CursosFK=1},
                new Turmas {Ano="2", Turma="C", Horario="2C1.jpg", Semestre="1", CursosFK=1},
                //------------------------------- 3� Ano -------------------------------
                new Turmas {Ano="3", Turma="A", Horario="3A1.jpg", Semestre="1", CursosFK=1},
                new Turmas {Ano="3", Turma="B", Horario="3B1.jpg", Semestre="1", CursosFK=1},
                //---------------------------- 2� Semestre -----------------------------
                //------------------------------- 1� Ano -------------------------------
                new Turmas {Ano="1", Turma="A", Horario="1A2.jpg", Semestre="2", CursosFK=1},
                new Turmas {Ano="1", Turma="B", Horario="1B2.jpg", Semestre="2", CursosFK=1},
                new Turmas {Ano="1", Turma="C", Horario="1C2.jpg", Semestre="2", CursosFK=1},
                //------------------------------- 2� Ano -------------------------------
                new Turmas {Ano="2", Turma="A", Horario="2A2.jpg", Semestre="2", CursosFK=1},
                new Turmas {Ano="2", Turma="B", Horario="2B2.jpg", Semestre="2", CursosFK=1},
                new Turmas {Ano="2", Turma="C", Horario="2C2.jpg", Semestre="2", CursosFK=1},
                //------------------------------- 3� Ano -------------------------------
                new Turmas {Ano="3", Turma="A", Horario="3A2.jpg", Semestre="2", CursosFK=1},
                new Turmas {Ano="3", Turma="B", Horario="3B2.jpg", Semestre="2", CursosFK=1},
                new Turmas {Ano="3", Turma="C", Horario="3C2.jpg", Semestre="2", CursosFK=1}                
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
