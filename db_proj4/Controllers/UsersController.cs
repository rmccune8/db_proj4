using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db_proj4.Models;
using db_proj4.Models.entities;
using System.Web.Security;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Controllers
{
    public class UsersController : Controller
    {
        UsersRepository UsersRepo;

        public UsersController()
        {
            UsersRepo = new UsersRepository();
        }

        public ActionResult Index()
        {
            var model = UsersRepo.BuildList();
            return View(model);
        }


        public ActionResult SubmitPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitPassword(Users User)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand("Users_InsertUser");

            db.AddInParameter(command, "@Type", System.Data.DbType.String, User.Type);
            db.AddInParameter(command, "@Pass", System.Data.DbType.String, User.Pass);

            db.ExecuteScalar(command);

            return RedirectToAction("Index", "Home");
        }
    }
}