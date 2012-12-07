using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using db_proj4.Models;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Controllers
{
    public class ApplicantsController : Controller
    {
        ApplicantsRepository appRepo;

        //public ApplicantsController(AccountController.getuserid)
        //{
        //    int currentuser = currentuser;
        //    appRepo = new ApplicantsRepository();
        //}
        public ActionResult Create(int Id)
        {
            ViewData["Userid"] = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Applicants app)
        {
            ApplicantsRepository.InsertApplicant(app.Userid, app.Fname, app.Lname, app.School, app.Aemail,
                app.Degree, app.DegreeField);

            return RedirectToAction("LogOn", "Account");
        }
    }
}
