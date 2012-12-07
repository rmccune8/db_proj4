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
    public class RecruitersController : Controller
    {
        RecruitersRepository recRepo = new RecruitersRepository();

        public ActionResult Create(int Id)
        {
            ViewData["Userid"] = Id;
            return View();
        }


        [HttpPost]
        public ActionResult Create(Recruiters rec)
        {
            RecruitersRepository.InsertRecruiter(rec.Userid, rec.Company, rec.Focus, rec.Remail);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(int id)
        {
            var model = recRepo.ViewEditRecruiter(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Recruiters rec)
        {

            int Userid = rec.Userid;
            string Company = rec.Company, Focus = rec.Focus, Remail = rec.Remail;

            RecruitersRepository.UpdateRecruiter(Userid, Company, Focus, Remail);

            return RedirectToAction("Index", "Jobs");
        }



        public ActionResult Pro()
        {
            string name = User.Identity.Name;
            var modelUser = recRepo.FindUserid(name);
            var model = recRepo.ViewEditRecruiter(modelUser.Userid);
            return View(model);


        }


    }
}
