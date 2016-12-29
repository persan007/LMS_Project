namespace LMS_Project.Migrations
{
    using LMS_Project.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Create handlers //
            var UserStore = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(UserStore);

            // Create a temp user //
            var TmpUser = new ApplicationUser() { UserName = "test@test.com", Email = "test@test.com", PhoneNumber = "0701234567" };
            var TmpUser2 = new ApplicationUser() { UserName = "bill@test.com", Email = "bill@test.com", Firstname = "Bill", Lastname = "Billsson", Persnr = "1212121222", PhoneNumber = "0732222222" };

            // Set password for user 'TmpUser' to 'Test@123' //
            UserManager.Create(TmpUser, "Test@123");
            UserManager.Create(TmpUser2, "Test2@123");

            // Create roll Teacher //
            if (!context.Roles.Any(o => o.Name == "Teacher"))
            {
                var RoleStore = new RoleStore<IdentityRole>(context);
                var RoleManager = new RoleManager<IdentityRole>(RoleStore);

                var Role = new IdentityRole() { Name = "Teacher" };

                RoleManager.Create(Role);
            }

            // Create roll Student //
            if (!context.Roles.Any(o => o.Name == "Student"))
            {
                var RoleStore = new RoleStore<IdentityRole>(context);
                var RoleManager = new RoleManager<IdentityRole>(RoleStore);

                var Role = new IdentityRole() { Name = "Student" };

                RoleManager.Create(Role);
            }

            // Set temp user role to teacher //
            UserManager.AddToRole(TmpUser.Id, "Teacher");

            // Save all changes //
            context.SaveChanges();
        }
    }
}
