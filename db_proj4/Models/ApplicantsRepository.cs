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

        public Applicants FindAppid(int Userid)
        {
            DbCommand command = db.GetStoredProcCommand("Applicants_FindAppid");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, Userid);
            db.ExecuteScalar(command);
            var result = new Applicants();
            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {

                    if (!reader.IsDBNull(reader.GetOrdinal("Appid")))
                    {
                        result.Appid = reader.GetInt32(reader.GetOrdinal("Appid"));
                    }

                }
                return result;
            }

        }

        public Users FindUserid(string userName)
        {
            DbCommand command = db.GetStoredProcCommand("Users_FindUserid");
            db.AddInParameter(command, "@Username", System.Data.DbType.String, userName);
            db.ExecuteScalar(command);
            var result = new Users();
            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {

                    if (!reader.IsDBNull(reader.GetOrdinal("Userid")))
                    {
                        result.Userid = reader.GetInt32(reader.GetOrdinal("Userid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Type")))
                    {
                        result.Type = reader.GetString(reader.GetOrdinal("Type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Pass")))
                    {
                        result.Pass = reader.GetString(reader.GetOrdinal("Pass"));
                    }

                }
                return result;
            }

        }
        public Applicants ViewEditApplicant(int id)
        {
            DbCommand command = db.GetStoredProcCommand("Applicants_BuildList");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, id);
            db.ExecuteScalar(command);
            var result = new Applicants();
            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {

                    if (!reader.IsDBNull(reader.GetOrdinal("Appid")))
                    {
                        result.Appid = reader.GetInt32(reader.GetOrdinal("Appid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Userid")))
                    {
                        result.Userid = reader.GetInt32(reader.GetOrdinal("Userid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Fname")))
                    {
                        result.Fname = reader.GetString(reader.GetOrdinal("Fname"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Lname")))
                    {
                        result.Lname = reader.GetString(reader.GetOrdinal("Lname"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Aemail")))
                    {
                        result.Aemail = reader.GetString(reader.GetOrdinal("Aemail"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("DegreeField")))
                    {
                        result.DegreeField = reader.GetString(reader.GetOrdinal("DegreeField"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Degree")))
                    {
                        result.Degree = reader.GetString(reader.GetOrdinal("Degree"));
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("School")))
                    {
                        result.School = reader.GetString(reader.GetOrdinal("School"));
                    }

                }

            }
            return result;
        }


        public static void InsertApplicant(int Userid, string Fname, string Lname, string School,
                                    string Aemail, string Degree, string DegreeField)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //User is added to Users database below.

            DbCommand command = db.GetStoredProcCommand("Applicants_InsertApplicant");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, Userid);
            db.AddInParameter(command, "@Fname", System.Data.DbType.String, Fname);
            db.AddInParameter(command, "@Lname", System.Data.DbType.String, Lname);
            db.AddInParameter(command, "@School", System.Data.DbType.String, School);
            db.AddInParameter(command, "@Aemail", System.Data.DbType.String, Aemail);
            db.AddInParameter(command, "@Degree", System.Data.DbType.String, Degree);
            db.AddInParameter(command, "@DegreeField", System.Data.DbType.String, DegreeField);
            db.ExecuteScalar(command);
        }


        public static void UpdateApplicant(int Userid, string Fname, string Lname, string Aemail, string School, string Degree, string DegreeField)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("Applicant_Update");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, Userid);
            db.AddInParameter(command, "@Fname", System.Data.DbType.String, Fname);
            db.AddInParameter(command, "@Lname", System.Data.DbType.String, Lname);
            db.AddInParameter(command, "@School", System.Data.DbType.String, School);
            db.AddInParameter(command, "@Aemail", System.Data.DbType.String, Aemail);
            db.AddInParameter(command, "@Degree", System.Data.DbType.String, Degree);
            db.AddInParameter(command, "@DegreeField", System.Data.DbType.String, DegreeField);
            db.ExecuteScalar(command);
        }

    }
}