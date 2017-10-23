namespace Galo_Personal_Site.Migrations
{
    using Galo_Personal_Site.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Galo_Personal_Site.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Galo_Personal_Blog.Models.ApplicationDbContext";
        }

        protected override void Seed(Galo_Personal_Site.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "glopez19@live.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "glopez19@live.com",
                    Email = "glopez19@live.com",
                    FirstName = "Galo",
                    LastName = "Lopez",
                    DisplayName = "Galo"
                }, "Cuentame1!");
            }

            var userId = userManager.FindByEmail("glopez19@live.com").Id;
            userManager.AddToRole(userId, "Admin");
            /*-----------------------------------------------------------------------------------------*/
            if (!context.Users.Any(u => u.Email == "bdavis@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "bdavis@coderfoundry.com",
                    Email = "bdavis@coderfoundry.com",
                    FirstName = "Bobby",
                    LastName = "Davis",
                    DisplayName = "Bobby Davis"
                }, "password1");
            }

            userId = userManager.FindByEmail("bdavis@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");
            /*-----------------------------------------------------------------------------------------*/
            if (!context.Users.Any(u => u.Email == "araynor@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "araynor@coderfoundry.com",
                    Email = "araynor@coderfoundry.com",
                    FirstName = "Antonio",
                    LastName = "Raynor",
                    DisplayName = "Antonio Raynor"
                }, "Abc&123!");
            }

            userId = userManager.FindByEmail("araynor@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");
            /*-----------------------------------------------------------------------------------------*/
            if (!context.Users.Any(u => u.Email == "ajensen@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ajensen@coderfoundry.com",
                    Email = "ajensen@coderfoundry.com",
                    FirstName = "Andrew",
                    LastName = "Jensen",
                    DisplayName = "Andrew Jensen"
                }, "Abc&123!");
            }

            userId = userManager.FindByEmail("ajensen@coderfoundry.com").Id;
            userManager.AddToRole(userId, "Moderator");
        }
    }
}
