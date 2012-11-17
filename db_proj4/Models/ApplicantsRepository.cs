using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Models
{
    public class ApplicantsRepository
    {
        private Database db;

        public ApplicantsRepository()
        {
            db = DatabaseFactory.CreateDatabase();
        }

        public List<Applicants> BuildList()
        {
            var list = new List<Applicants>();

            DbCommand command = db.GetStoredProcCommand("Applicants_BuildList");

            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var applicant = new Applicants();

                    if (!reader.IsDBNull(reader.GetOrdinal("Aemail")))
                    {
                        applicant.Aemail = reader.GetString(reader.GetOrdinal("Aemail"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Appid")))
                    {
                        applicant.Appid = reader.GetInt32(reader.GetOrdinal("Appid"));
                    }

                    applicant.Degree = reader.GetString(reader.GetOrdinal("Degree"));
                    applicant.DegreeField = reader.GetString(reader.GetOrdinal("DegreeField"));
                    applicant.Fname = reader.GetString(reader.GetOrdinal("Fname"));
                    applicant.Lname = reader.GetString(reader.GetOrdinal("Lname"));
                    applicant.School = reader.GetString(reader.GetOrdinal("School"));
                    applicant.Userid = reader.GetInt32(reader.GetOrdinal("Userid"));
                    list.Add(applicant);
                }
            }
            return list;
        }
    }
}