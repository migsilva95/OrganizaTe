using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OrganizaTe.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrganizaTe.Startup))]
namespace OrganizaTe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // invocar o método para iniciar a configuração
            // dos ROLES +USERS
            iniciaAplicacao();
        }
        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Agentes, Condutores
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Aluno'
            if (!roleManager.RoleExists("Aluno"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Aluno";
                roleManager.Create(role);
            }

            try
            {
                // criar um utilizador 'Aluno'
                var user = new ApplicationUser();
                user.UserName = "aluno18904@ipt.pt";
                user.Email = "aluno18904@ipt.pt";

                string userPWD = "123_Asd";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Aluno-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Aluno");
                }

                // criar um utilizador 'Aluno'
                user = new ApplicationUser();
                user.UserName = "josealves@ipt.pt";
                user.Email = "josealves@ipt.pt";

                userPWD = "123_Asd";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Aluno-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Aluno");
                }
            }
            catch (System.Exception)
            { }

            // criar a Role 'AdminSite'
            if (!roleManager.RoleExists("AdminSite"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "AdminSite";
                roleManager.Create(role);
            }

            try
            {
                // criar um utilizador 'AdminSite'
                var user = new ApplicationUser();
                user.UserName = "miguelsilva@ipt.pt";
                user.Email = "miguelsilva@ipt.pt";

                string userPWD = "123_Asd";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-AdminSite-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "AdminSite");
                }


                user = new ApplicationUser();
                user.UserName = "pedrovinha@ipt.pt";
                user.Email = "pedrovinha@ipt.pt";

                userPWD = "123_Asd";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-AdminSite-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "AdminSite");
                }

                user = new ApplicationUser();
                user.UserName = "adminsite@ipt.pt";
                user.Email = "adminsite@ipt.pt";

                userPWD = "123_Asd";
                chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-AdminSite-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "AdminSite");
                }
            }
            catch (System.Exception)
            { }
        }

    }
}
