using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using db_proj4.Models;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Controllers
{
    public class JobsController : Controller
    {
      JobsRepository jobRepo;

        public JobsController()
        {
            jobRepo = new JobsRepository();
        }

        public ActionResult Index()
        {
            var model = jobRepo.BuildList();
            return View(model);
        }

        
        public ActionResult PostJob()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostJob(JobsRepository jr)
        {
            Database db = DatabaseFactory.CreateDatabase();
            var job = new Jobs();

            DbCommand command = db.GetStoredProcCommand("Jobs_InsertJob");

            db.AddInParameter(command, "@Field", System.Data.DbType.String, job.Field);
            db.AddInParameter(command, "@Skills", System.Data.DbType.String, job.Skills);
            db.AddInParameter(command, "@Experience", System.Data.DbType.String, job.Experience);
            db.AddInParameter(command, "@Salary", System.Data.DbType.Int32, job.Salary);
            db.AddInParameter(command, "@Location", System.Data.DbType.String, job.Location);
            db.AddInParameter(command, "@Title", System.Data.DbType.String, job.Title);
            db.AddInParameter(command, "@Description", System.Data.DbType.String, job.Description);
            db.ExecuteScalar(command);

            return RedirectToAction("Index", "Jobs");
        }
    }
}
