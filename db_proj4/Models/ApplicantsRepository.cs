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

                    if (!reader.IsDBNull(reader.GetOrdinal("Degree")))
                    {
                        applicant.Degree = reader.GetString(reader.GetOrdinal("Degree"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DegreeField")))
                    {
                        applicant.DegreeField = reader.GetString(reader.GetOrdinal("DegreeField"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Fname")))
                    {
                        applicant.Fname = reader.GetString(reader.GetOrdinal("Fname"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Lname")))
                    {
                        applicant.Lname = reader.GetString(reader.GetOrdinal("Lname"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("School")))
                    {
                        applicant.School = reader.GetString(reader.GetOrdinal("School"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Userid")))
                    {
                        applicant.Userid = reader.GetInt32(reader.GetOrdinal("Userid"));
                    }
                    list.Add(applicant);
                }
            }
            return list;
        }
    }
}