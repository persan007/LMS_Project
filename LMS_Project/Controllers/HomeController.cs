using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LMS_Project.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LMS_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Repository _repo = new Repository();
        private ApplicationDbContext _context;

        public HomeController()
        {
            this._context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        public ActionResult DownloadFile(string fileName)
        {
            //Retrieve file with corrisponding filename
            var file = _repo.db.FilesObjects.Where(f => f.Filename == fileName).First();
            //Return file
            return File(file.Data, file.ContentType, file.Filename);
        }

        public string GetUrlByFilename(string fileName)
        {
            return _repo.GetUrlByFilename(fileName);
        }

        public string[] GetAllFilenames()
        {
            return _repo.GetAllFilenames();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            _repo.UploadFiles(Request.Files, Server.MapPath("~/Resources/Tmp/"));
            return View();
        }

        public string GetUserInformation()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            var db = new ApplicationDbContext();
            var User_id = User.Identity.GetUserId();
            var CurrentUser = db.Users.Select(o => new
            {
                Id = o.Id,
                Firstname = o.Firstname,
                Lastname = o.Lastname,
                ProfileImage = o.ProfileImage ?? "http://placehold.it/100x100"
            }).Where(o => o.Id == User_id);

            return JsonConvert.SerializeObject(CurrentUser, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddSchoolClass()
        {                       
            var UserStore = new UserStore<ApplicationUser>(_context);
            var UserManager = new UserManager<ApplicationUser>(UserStore);
            
            List<ApplicationUser> students = new  List<ApplicationUser>();
            
            foreach (var user in UserManager.Users.ToList())
            {
                if (UserManager.IsInRole(user.Id, "Student"))
                {
                    students.Add(user);
                }
            }
           
            ViewBag.Students = new SelectList(students, "Id","Lastname");

            //------------------------------
                        
            List<SchoolClassModels> schoolClasses = new List<SchoolClassModels>();
            schoolClasses = _context.SchoolClasses.ToList();
            
            ViewBag.SchoolClasses = new SelectList(schoolClasses, "SchoolClassID", "Name");

            //------------------------------

            schoolClasses.ElementAt(0).Students.Add(students.First());
            List<ApplicationUser> schoolClassStudents = new List<ApplicationUser>();
            var schoolClassName = "Chemistry";
            
            foreach (var schoolClass in _context.SchoolClasses.ToList())
            {
                if (schoolClass.Name == schoolClassName)
                {
                    schoolClassStudents = schoolClass.Students.ToList();
                }
            }


            ViewBag.SchoolClassStudents = new SelectList(schoolClassStudents, "Id", "Lastname");
            
            return View();

            //var db = new ApplicationDbContext();
            //List<ApplicationUser> users = db.Users.ToList();
            //var roleStore = new RoleStore<IdentityRole>(_context);
            //var roleManager = new RoleManager<IdentityRole>(roleStore);           
            //IdentityRole role = roleManager.FindByName("Student");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddSchoolClass(string SchoolClasses, string Students)
        {
             
            return View();           
        }
    }
}