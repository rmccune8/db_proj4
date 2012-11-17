using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace db_proj4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Job Loader!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
