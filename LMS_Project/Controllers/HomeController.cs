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

        public string[] GetAllFilenames()
        {
            List<FileObjectModels> files = db.FilesObjects.ToList();
            string[] filenames = new string[files.Count()];
            int index = 0;
            foreach(var file in files)
            {
                filenames[index] = file.Filename;
                index++;
            }
            return filenames;
        }

        public string GetUrlByFilename(string fileName = "ppap.png")
        {
            FileObjectModels file = db.FilesObjects.Single(f => f.Filename == fileName);

            if (file != null)
                return string.Format("data:{0};base64,{1}", file.ContentType, Convert.ToBase64String(file.Data));

            return null; 
        }

        public string DeBlobber(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public byte[] FileToBlob(string filepath)
        {
            //A stream of bytes that represents the binary file
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);

            //The reader reads the binary data from the file stream
            BinaryReader reader = new BinaryReader(fs);

            //Bytes from the binary reader stored in BlobValue array
            byte[] BlobValue = reader.ReadBytes((int)fs.Length);

            //Close stream
            fs.Close();
            //Close reader
            reader.Close();

            return BlobValue;
        }

        //[HttpPost]
        public ActionResult DownloadFiles(string fileName = "ppap.png")
        {
            //Retrieve file with corrisponding filename
            var file = db.FilesObjects.Where(f => f.Filename == fileName).First();
            //Get file blob data
            var blob = file.Data;
            //Return generated file
            return File(blob, file.ContentType, file.Filename);
        }

        [HttpPost]
        public void UploadFiles()
        {
            for(int i = 0; i < Request.Files.Count; i++)
            {
                //Get file, filename and path
                var file = Request.Files[i];
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/Resources/Tmp/"), fileName);
                file.SaveAs(path);

                //Get Blob value from file via filepath
                byte[] bytes = FileToBlob(path);

                //Create relevant model
                FileObjectModels tmpModel = new FileObjectModels();
                tmpModel.Filename = fileName;
                tmpModel.Data = bytes;
                tmpModel.ContentType = file.ContentType;

                //Check if model already exists
                bool dataExists = db.FilesObjects.Any(m => m.Data.Equals(bytes));
                if(!dataExists)
                {
                    db.FilesObjects.Add(tmpModel);
                    db.SaveChanges();
                }
                //Get path to file
                string fullPath = Request.MapPath("~/Resources/Tmp/" + fileName);

                //Check if file exists, if it does delete it
                if(System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);
            }
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