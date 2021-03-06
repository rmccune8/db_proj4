﻿using System;
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
        public ActionResult PostJob(Jobs job)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand("Jobs_InsertJob");

            db.AddInParameter(command, "@Rid", System.Data.DbType.Int32, JobsRepository.GetRid(User.Identity.Name));
            db.AddInParameter(command, "@Date", System.Data.DbType.DateTime, DateTime.Now);
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

   
        public ActionResult DeleteJob(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbCommand command = db.GetStoredProcCommand("Jobs_DeleteJob");

            db.AddInParameter(command, "@Jobid", System.Data.DbType.Int32, id);
            db.ExecuteScalar(command);

            return RedirectToAction("Index", "Jobs");
        }

        public ActionResult EditJob(int id)
        {
            //Database db = DatabaseFactory.CreateDatabase();

            //DbCommand command = db.GetStoredProcCommand("Jobs_ViewEditJob");
            
            //db.AddInParameter(command, "@Jobid", System.Data.DbType.Int32, id);
            //db.ExecuteScalar(command);

            //return RedirectToAction("EditJob", "Jobs");
            
            var model = jobRepo.ViewEditJob(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditJob(Jobs job)
        {
            JobsRepository.UpdateJob(job.Jobid, job.Field, job.Skills, job.Experience, job.Salary,
                job.Location, job.Title, job.Description);

            return RedirectToAction("Index");
        }

        public ActionResult ViewJob(int id)
        {
            var model = jobRepo.ViewEditJob(id);
            return View(model);
        }
        
        public ActionResult SearchIndex(string SearchType, string SearchString)
        {
            var results = jobRepo.SearchResults(SearchType, SearchString);

            return View(results);
        }

        public ActionResult ShowInterest(int id)
        {
            int Appid = JobsRepository.GetAppid(User.Identity.Name);
            JobsRepository.InsertInterestedIn(id, Appid);

            return RedirectToAction("Index");
        }

        public ActionResult WithdrawInterest(int id)
        {
            int Appid = JobsRepository.GetAppid(User.Identity.Name);
            JobsRepository.DeleteInterestedIn(id, Appid);

            return RedirectToAction("Index");
        }
    }
}
