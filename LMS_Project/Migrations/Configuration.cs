namespace LMS_Project.Migrations
{
    using LMS_Project.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
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

            //Create schoolclass
            var TempSchoolClass = new SchoolClassModels() { SchoolClassID = "1", Name = "SchoolClass 9a", Students = new List<ApplicationUser>() };
            context.SchoolClasses.Add(TempSchoolClass);
            context.SaveChanges();

            // Create a temp user //
            var TmpUser = new ApplicationUser() { UserName = "test@test.com", Email = "test@test.com", PhoneNumber = "0701234567", 
                Firstname = "Test", Lastname = "User", ProfileImage = null, SSN = "185201052663",
             SchoolClassModelId=TempSchoolClass.SchoolClassID};

            var TmpUser2 = new ApplicationUser()
            {
                UserName = "test2@test.com",
                Email = "test2@test.com",
                PhoneNumber = "0701234567",
                Firstname = "Test2",
                Lastname = "User",
                ProfileImage = null,
                SSN = "185201052663",
                //SchoolClassModelId = TempSchoolClass.SchoolClassID
            };


            // Set password for user 'TmpUser' to 'Test@123' //
            UserManager.Create(TmpUser, "Test@123");

            UserManager.Create(TmpUser2, "Test@123");

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
            UserManager.AddToRole(TmpUser.Id, "Student");
            UserManager.AddToRole(TmpUser2.Id, "Student");

            // Save all changes //
            context.SaveChanges();

           
            
        }
    }
}
