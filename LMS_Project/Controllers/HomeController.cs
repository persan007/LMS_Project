using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LMS_Project.Repositories;
using System.Web.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using LMS_Project.Filters;

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

        public string GetAllLessons()
        {
            LessonModels[] arr = _repo.GetAllLessons();
            return JsonConvert.SerializeObject(arr, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            //_repo.UploadFiles(Request.Files, Server.MapPath("~/Resources/Tmp/"));
            return View();
        }

        public string GetAllRoleNames()
        {
            var roles = _repo.GetAllRoles();
            return JsonConvert.SerializeObject(roles, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects }); ;
        }

        public async Task<string> GetUserInformation()
        {
            var db = new ApplicationDbContext();
            var User_id = User.Identity.GetUserId();
            var rolesForUser = await System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRolesAsync(User_id);

            var CurrentUser = db.Users.Select(o => new
            {
                Id = o.Id,
                Firstname = o.Firstname,
                Lastname = o.Lastname,
                ProfileImage = o.ProfileImage ?? "http://placehold.it/100x100",
                Role = rolesForUser.ToList().FirstOrDefault()
            }).Where(o => o.Id == User_id);

            return JsonConvert.SerializeObject(CurrentUser, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        [HttpGet]
        [ValidateAngularAntiForgery]
        public string GetSchedule()
        {
            List<object> test = new List<object>() {
                new {
                    From = "10:30",
                    To = "12:00",
                    Day = "Tuesday",
                    LessonType = "English",
                    Color = "lightblue",
                    Teacher = "TLUG",
                    Classroom = "C320"
                },
                new {
                    From = "10:30",
                    To = "15:20",
                    Day = "Friday",
                    LessonType = "Programming",
                    Color = "pink",
                    Teacher = "ELÖV",
                    Classroom = "D220"
                },
                new {
                    From = "08:30",
                    To = "09:45",
                    Day = "Monday",
                    LessonType = "Math",
                    Color = "lightgreen",
                    Teacher = "POLV",
                    Classroom = "A332"
                }
            };

            return JsonConvert.SerializeObject(test, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddStudentsToSchoolClass()
        {
            var UserStore = new UserStore<ApplicationUser>(_context);
            var UserManager = new UserManager<ApplicationUser>(UserStore);

           List<object> students = new List<object>();

            //Lägger in alla elever i listan
            foreach (var user in UserManager.Users.ToList())
            {
                if ((UserManager.IsInRole(user.Id, "Student")) && ((user.SchoolClassModelId == null) || (user.SchoolClassModelId.Trim() == "")))
                {
                    students.Add(new { 
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        SSN = user.SSN
                    });
                }
            }

            ViewBag.Students = new SelectList(students, "Id", "Lastname");
           
            //------------------------------  
            //Lägger in alla skolklasser i listan
            List<object> schoolClasses = new List<object>();

            foreach (var schoolClass in _context.SchoolClasses.ToArray())
            {
                
                    schoolClasses.Add(new
                    {
                       Id = schoolClass.SchoolClassID,
                       Name = schoolClass.Name,
                       Students = schoolClass.Students
                    });
                
            }         

            ViewBag.SchoolClasses = new SelectList(schoolClasses, "SchoolClassID", "SchoolClassID");
               
            // Gör ett test
            string testSchoolClassId = _context.SchoolClasses.First().SchoolClassID;
            string testStudentSSN = _context.Users.First().SSN;
            string[] testAllSSN = new string[1];
            testAllSSN[0] = testStudentSSN;
            AddStudentsToSchoolClass(testSchoolClassId, testAllSSN);
            
            return View();
        }

        [AllowAnonymous]
        //[HttpPost]  
        public void AddStudentsToSchoolClass(string SchoolClassId, string[] StudentsSSN)  
        {
            
            List<ApplicationUser> studentsInClass = new List<ApplicationUser>();
            SchoolClassModels SchoolClass = _context.SchoolClasses.SingleOrDefault(sc => sc.SchoolClassID == SchoolClassId);

            //Lägger till student i skolklass
            foreach (string ssn in StudentsSSN)
            {
                ApplicationUser student = _context.Users.SingleOrDefault(s => s.SSN == ssn);
                SchoolClass.Students.Add(student);
            }

            _context.Entry(SchoolClass).State = EntityState.Modified;
            _context.SaveChanges();

            //ViewBag.SchoolClasses = new SelectList(schoolClasses, "SchoolClassID", "SchoolClassID");
            //ViewBag.SchoolClassStudents = new SelectList(schoolClassStudents, "Id", "Lastname");
            //ViewBag.Students = new SelectList(studentsInClass, "Id", "Lastname");


            //return View();             
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult RemoveStudentsFromSchoolClass()
        {
            // Gör ett test
            string testSchoolClassId = _context.SchoolClasses.First().SchoolClassID;
            string testStudentSSN = _context.Users.First().SSN;
            string[] testAllSSN = new string[1];
            testAllSSN[0] = testStudentSSN;
            RemoveStudentsFromSchoolClass(testSchoolClassId, testAllSSN);
            
            return View();
        }

        [AllowAnonymous]
        //[HttpPost]  
        private void RemoveStudentsFromSchoolClass(string SchoolClassId, string[] StudentsSSN)
        {
            SchoolClassModels SchoolClass = _context.SchoolClasses.SingleOrDefault(sc => sc.SchoolClassID == SchoolClassId);

            //Tar bort en student i skolklass
            foreach (string ssn in StudentsSSN)
            {
                ApplicationUser student = _context.Users.SingleOrDefault(s => s.SSN == ssn);
                SchoolClass.Students.Remove(student);
            }

            _context.Entry(SchoolClass).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AddFileObjects(bool InShared = false)
        {
            ApplicationUser user = _context.Users.Find(User.Identity.GetUserId());
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

            Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();
            foreach (HttpFileCollectionBase file in files)
            {
                BinaryReader reader = new BinaryReader(file[0].InputStream);
                data.Add(file[0].FileName, reader.ReadBytes((int)file[0].InputStream.Length));
            }

            List<FileObjectModels> fileObj = new List<FileObjectModels>();
            List<FileObjectUserModels> fileUserObj = new List<FileObjectUserModels>();

            foreach (HttpFileCollectionBase file in files)
            {
                var current = file[0];

                fileObj.Add(new FileObjectModels()
                {
                    Data = data[current.FileName],
                    Filename = current.FileName,
                    ContentType = current.ContentType,
                    CourseID = null
                });               
            }

            _context.FilesObjects.AddRange(fileObj);           
            _context.SaveChanges();

            foreach (HttpFileCollectionBase file in files)
            {
                var current = file[0];

                fileUserObj.Add(new FileObjectUserModels()
                {
                    UploadedTime = DateTime.Now,
                    FileObjectID = _context.FilesObjects.SingleOrDefault(f => f.Filename == current.FileName).FileObjectID,
                    Shared = InShared,
                    UserID = user.Id
                });
            }

            _context.FileObjectUsers.AddRange(fileUserObj);
            _context.SaveChanges();

            /*ApplicationUser student = _context.Users.SingleOrDefault(s => s.SSN == StudentSSN);
            FileObjectModels fileObject = _context.FilesObjects.SingleOrDefault(f => f.Filename == InFilename);

            FileObjectUserModels fileObjectUser = new FileObjectUserModels() {UserID = student.Id, FileObjectID = fileObject.FileObjectID, Shared = InShared, UploadedTime = DateTime.Now};

            if (_context.FileObjectUsers.SingleOrDefault(f => f.FileObjectID == fileObject.FileObjectID) == null)
            {
                _context.Entry(fileObjectUser).State = EntityState.Added;              
            }
            else 
            { 
                fileObjectUser = _context.FileObjectUsers.SingleOrDefault(f => f.FileObjectID == fileObject.FileObjectID);
                fileObjectUser.Shared = InShared;
                fileObjectUser.UploadedTime = DateTime.Now;
                _context.Entry(fileObjectUser).State = EntityState.Modified;                
            }
            _context.SaveChanges(); */                        
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetUserFileObject(string fileObjectId = "1")
        {
            FileObjectUserModels fileObjectUser = _context.FileObjectUsers.SingleOrDefault(f => f.FileObjectID == fileObjectId);
            
            bool shared = fileObjectUser.Shared;

            byte[] data = fileObjectUser.FileObjectModels.Data;
            string contentType = fileObjectUser.FileObjectModels.ContentType;
            string fileName = fileObjectUser.FileObjectModels.Filename;

            return View();
        }
    }
}