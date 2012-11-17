using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db_proj4.Models;

namespace db_proj4.Controllers
{
    public class ApplicantsController : Controller
    {
        ApplicantsRepository appRepo;

        public ApplicantsController()
        {
            appRepo = new ApplicantsRepository();
        }

        public ActionResult ApplicantsList()
        {
            var model = appRepo.BuildList();
            return View(model);
        }

    }
}
