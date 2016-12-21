namespace LMS_Project.Migrations
{
    using LMS_Project.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_Project.Database_Access.DbContextLayer>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS_Project.Database_Access.DbContextLayer context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user1 = new ApplicationUser { UserName = "test@test.com", Email = "test@test.com" };
            var user2 = new ApplicationUser { UserName = "test2@test.com", Email = "test2@test.com" };
            userManager.Create(user1, "Test@123");
            userManager.Create(user2, "Test@123");
            context.SaveChanges();
        }
    }
}
