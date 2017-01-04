using System;
using System.Text;
using System.Web.Mvc;

namespace LMS_Project.Controllers
{ 
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}