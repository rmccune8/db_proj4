﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Models
{
    public class JobsRepository
    {
        private Database db;

        public JobsRepository()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        public List<Jobs> BuildList()
        {
            var list = new List<Jobs>();

            DbCommand command = db.GetStoredProcCommand("Jobs_BuildList");
            
            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var job = new Jobs();

                    if (!reader.IsDBNull(reader.GetOrdinal("Date")))
                    {
                        job.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Field")))
                    {
                        job.Field = reader.GetString(reader.GetOrdinal("Field"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Title")))
                    {
                        job.Title = reader.GetString(reader.GetOrdinal("Title"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Location")))
                    {
                        job.Location = reader.GetString(reader.GetOrdinal("Location"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Jobid")))
                    {
                        job.Jobid = reader.GetInt32(reader.GetOrdinal("Jobid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Description")))
                    {
                        job.Description = reader.GetString(reader.GetOrdinal("Description"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Experience")))
                    {
                        job.Experience = reader.GetString(reader.GetOrdinal("Experience"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Rid")))
                    {
                        job.Rid = reader.GetInt32(reader.GetOrdinal("Rid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Salary")))
                    {
                        job.Salary = reader.GetInt32(reader.GetOrdinal("Salary"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Skills")))
                    {
                        job.Skills = reader.GetString(reader.GetOrdinal("Skills"));
                    }
                    

                    list.Add(job);
                }
            }
            return list;
        }

        public Jobs ViewEditJob(int id)
        {
            DbCommand command = db.GetStoredProcCommand("Jobs_ViewEditJob");
            db.AddInParameter(command, "@Jobid", System.Data.DbType.Int32, id);
            db.ExecuteScalar(command);
            var job = new Jobs();

            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("Date")))
                    {
                        job.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Field")))
                    {
                        job.Field = reader.GetString(reader.GetOrdinal("Field"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Title")))
                    {
                        job.Title = reader.GetString(reader.GetOrdinal("Title"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Location")))
                    {
                        job.Location = reader.GetString(reader.GetOrdinal("Location"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Jobid")))
                    {
                        job.Jobid = reader.GetInt32(reader.GetOrdinal("Jobid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Description")))
                    {
                        job.Description = reader.GetString(reader.GetOrdinal("Description"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Experience")))
                    {
                        job.Experience = reader.GetString(reader.GetOrdinal("Experience"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Rid")))
                    {
                        job.Rid = reader.GetInt32(reader.GetOrdinal("Rid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Salary")))
                    {
                        job.Salary = reader.GetInt32(reader.GetOrdinal("Salary"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Skills")))
                    {
                        job.Skills = reader.GetString(reader.GetOrdinal("Skills"));
                    }
                }
            }
            return job;
        }

        public static void UpdateJob(int Jobid, string Field, string Skills, string Experience,
                                     int? Salary, string Location, string Title, string Description)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("Jobs_EditJob");

            db.AddInParameter(command, "@Jobid", System.Data.DbType.Int32, Jobid);
            db.AddInParameter(command, "@Field", System.Data.DbType.String, Field);
            db.AddInParameter(command, "@Skills", System.Data.DbType.String, Skills);
            db.AddInParameter(command, "@Experience", System.Data.DbType.String, Experience);
            db.AddInParameter(command, "@Salary", System.Data.DbType.Int32, Salary);
            db.AddInParameter(command, "@Location", System.Data.DbType.String, Location);
            db.AddInParameter(command, "@Title", System.Data.DbType.String, Title);
            db.AddInParameter(command, "@Description", System.Data.DbType.String, Description);
            db.ExecuteScalar(command);
        }
    }
}