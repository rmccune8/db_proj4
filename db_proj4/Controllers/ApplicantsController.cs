using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
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
        ApplicantsRepository appRepo = new ApplicantsRepository();
        // appRepo = new ApplicantsRepository();

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

        public ActionResult Edit(int id)
        {
            var model = appRepo.ViewEditApplicant(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Applicants app)
        {

            int Userid = app.Userid;
            string Fname = app.Fname, Lname = app.Lname, School = app.School, Aemail = app.Aemail,
                   Degree = app.Degree, DegreeField = app.DegreeField;

            ApplicantsRepository.UpdateApplicant(Userid, Fname, Lname, School, Aemail, Degree, DegreeField);

            return RedirectToAction("Index", "Jobs");
        }



        public ActionResult Pro()
        {
            string name = User.Identity.Name;
            var modelUser = appRepo.FindUserid(name);
            var model = appRepo.ViewEditApplicant(modelUser.Userid);
            return View(model);


        }

        [HttpPost]
        public ActionResult Pro(Applicants app)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand("Applicant_Update");

            db.AddInParameter(command, "@Appid", System.Data.DbType.Int32, 2);
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, 1);
            db.AddInParameter(command, "@Fname", System.Data.DbType.String, app.Fname);
            db.AddInParameter(command, "@Lname", System.Data.DbType.String, app.Lname);
            db.AddInParameter(command, "@School", System.Data.DbType.String, app.School);
            db.AddInParameter(command, "@Aemail", System.Data.DbType.String, app.Aemail);
            db.AddInParameter(command, "@Degree", System.Data.DbType.String, app.Degree);
            db.AddInParameter(command, "@DegreeField", System.Data.DbType.String, app.DegreeField);

            db.ExecuteScalar(command);

            return RedirectToAction("Index", "Jobs");
        }

        public ActionResult Delete(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand("Applicants_DeleteApplicant");
            string name = User.Identity.Name;
            var modelUser = appRepo.FindUserid(name);
            var modelUserAppid = appRepo.FindAppid(modelUser.Userid);
            db.AddInParameter(command, "@Appid", System.Data.DbType.Int32, modelUserAppid.Appid);
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, id);
            db.ExecuteScalar(command);

            return RedirectToAction("Index", "Jobs");
        }
    }
}
