using LMS_Project.Models;
using LMS_Project.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using LMS_Project.Filters;

namespace LMS_Project.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private Repository _repo;
        private ApplicationDbContext _context;

        public DataController()
        {
            this._repo = new Repository();
            this._context = new ApplicationDbContext();
        }

        [HttpGet]
        public string GetAllRoleNames()
        {
            var roles = _repo.GetAllRoleNames();
            return JsonConvert.SerializeObject(roles, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        [HttpGet]
        public async Task<string> GetUserInformation()
        {
            var User_id = User.Identity.GetUserId();
            var rolesForUser = await System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRolesAsync(User_id);

            var CurrentUser = _context.Users.Select(o => new
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

        [HttpGet]
        public string GetAllLessons()
        {
            LessonModels[] arr = _repo.GetAllLessons();
            return JsonConvert.SerializeObject(arr, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }

        [HttpGet]
        public string GetUrlByFilename(string fileName)
        {
            return _repo.GetUrlByFilename(fileName);
        }

        [HttpGet]
        public string[] GetAllFilenames()
        {
            return _repo.GetAllFilenames();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            _repo.UploadFiles(hfc, Server.MapPath("~/Resources/Tmp/"));
            return View();
        }

        public ActionResult DownloadFile(string fileName)
        {
            //Retrieve file with corrisponding filename
            var file = _repo.db.FilesObjects.Where(f => f.Filename == fileName).First();

            //Return file
            return File(file.Data, file.ContentType, file.Filename);
        }
    }
}