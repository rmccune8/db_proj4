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
        public ActionResult AddApplicant(Applicants app, int Id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //User is added to Users database below.

            DbCommand command = db.GetStoredProcCommand("Applicants_InsertApplicant");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, Id);

            db.AddInParameter(command, "@Fname", System.Data.DbType.String, app.Fname);
            db.AddInParameter(command, "@Lname", System.Data.DbType.String, app.Lname);
            db.AddInParameter(command, "@School", System.Data.DbType.String, app.School);
            db.AddInParameter(command, "@Aemail", System.Data.DbType.String, app.Aemail);
            db.AddInParameter(command, "@Degree", System.Data.DbType.String, app.Degree);
            db.AddInParameter(command, "@DegreeField", System.Data.DbType.String, app.DegreeField);
            db.ExecuteScalar(command);

            return RedirectToAction("Create", "AddApplicant", new { _id = Id });
        }
    }
}
