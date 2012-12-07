using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Models
{
    public class RecruitersRepository
    {
        private Database db;

        public RecruitersRepository()
        {
            db = DatabaseFactory.CreateDatabase();
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
        public Recruiters ViewEditRecruiter(int id)
        {
            DbCommand command = db.GetStoredProcCommand("Recruiters_BuildList");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, id);
            db.ExecuteScalar(command);
            var result = new Recruiters();
            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {

                    if (!reader.IsDBNull(reader.GetOrdinal("Rid")))
                    {
                        result.Rid = reader.GetInt32(reader.GetOrdinal("Rid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Userid")))
                    {
                        result.Userid = reader.GetInt32(reader.GetOrdinal("Userid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Company")))
                    {
                        result.Company = reader.GetString(reader.GetOrdinal("Company"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Focus")))
                    {
                        result.Focus = reader.GetString(reader.GetOrdinal("Focus"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Remail")))
                    {
                        result.Remail = reader.GetString(reader.GetOrdinal("Remail"));
                    }
                }

            }
            return result;
        }


        public static void InsertRecruiter(int Userid, string Company, string Focus, string Remail)
        {
            Database db = DatabaseFactory.CreateDatabase();
            //User is added to Users database below.

            DbCommand command = db.GetStoredProcCommand("Recruiters_InsertRecruiter");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, Userid);
            db.AddInParameter(command, "@Company", System.Data.DbType.String, Company);
            db.AddInParameter(command, "@Focus", System.Data.DbType.String, Focus);
            db.AddInParameter(command, "@Remail", System.Data.DbType.String, Remail);
            db.ExecuteScalar(command);
        }


        public static void UpdateRecruiter(int Userid, string Company, string Focus, string Remail)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("Recruiter_Update");
            db.AddInParameter(command, "@Userid", System.Data.DbType.Int32, Userid);
            db.AddInParameter(command, "@Company", System.Data.DbType.String, Company);
            db.AddInParameter(command, "@Focus", System.Data.DbType.String, Focus);
            db.AddInParameter(command, "@Remail", System.Data.DbType.String, Remail);
            db.ExecuteScalar(command);
        }

    }
}