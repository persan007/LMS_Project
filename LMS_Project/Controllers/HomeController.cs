using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LMS_Project.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        public string GetUrl(string fileName = "ppap.png")
        {
            var file = db.FilesObjects.Where(f => f.Filename == fileName).First();
            var blob = file.Data;
            var imgSrc = String.Format("data:" + file.ContentType + ";base64,{0}", blob);
            return imgSrc;
        }

        //[HttpPost]
        public ActionResult DownloadFiles(string fileName = "ppap.png")
        {
            var file = db.FilesObjects.Where(f => f.Filename == fileName).First();
            var blob = file.Data;
            return File(blob, file.ContentType, file.Filename);
        }

        [HttpPost]
        public JsonResult UploadFiles()
        {
            for(int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                string fileName = Path.GetFileName(file.FileName);

                string path = Path.Combine(Server.MapPath("~/Resources/Tmp/"), fileName);
                file.SaveAs(path);
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                br.Close();
                fs.Close();

                FileObjectModels tmpModel = new FileObjectModels();
                tmpModel.Filename = fileName;
                tmpModel.Data = bytes;
                tmpModel.ContentType = file.ContentType;

                bool dataExists = db.FilesObjects.Any(m => m.Data.Equals(bytes));
                
                if(!dataExists)
                {
                    db.FilesObjects.Add(tmpModel);
                    db.SaveChanges();
                }

                string fullPath = Request.MapPath("~/Resources/Tmp/" + fileName);
                if(System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }


            var JsonObject = Json("Model Saved", JsonRequestBehavior.AllowGet);
            return JsonObject;
        }

        public string GetUserInformation()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var db = new ApplicationDbContext();
            var User_Id = User.Identity.GetUserId();
            var TmpUser = db.Users.Single(o => o.Id == User_Id);

            return JsonConvert.SerializeObject(TmpUser, Formatting.None, new JsonSerializerSettings() { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }
    }
}