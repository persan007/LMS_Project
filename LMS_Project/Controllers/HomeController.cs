using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Project.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadFiles(FileObjectModels Files)
        {
            var JsonObject = Json("Model Saved", JsonRequestBehavior.AllowGet);
            return JsonObject;
        }
    }
}