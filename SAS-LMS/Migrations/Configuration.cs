namespace SAS_LMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SAS_LMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SAS_LMS.Models.ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var roleName = "Teacher";
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                var role = new IdentityRole { Name = roleName };
                var result = roleManager.Create(role);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var email = "headteacher@lexicon.se";
            if (!context.Users.Any(u => u.UserName == email))
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    EnrollmentDate = DateTime.Now
                };
                var result = userManager.Create(user, "Admin123!");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join("\n", result.Errors));
                }
            }

            var adminUser = userManager.FindByName("headteacher@lexicon.se");
            userManager.AddToRole(adminUser.Id, "Teacher");

            context.ActivityTypes.AddOrUpdate(
                p => p.Name,
                  new ActivityType { Name = "e-Learning" },
                  new ActivityType { Name = "Lecture" },
                  new ActivityType { Name = "Exercise" },
                  new ActivityType { Name = "Submission" }
                );

        }
    }
}
