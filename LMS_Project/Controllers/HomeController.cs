using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        public Byte[] DownloadFiles(string fileName = "ppap.png")
        {
            var blob = db.FilesObjects.Where(f => f.Filename == fileName).First().Data;
            return blob;
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
    }
}