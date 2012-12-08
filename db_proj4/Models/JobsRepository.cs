using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
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

        public static void InsertInterestedIn(int Jobid, int Appid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("InterestedIn_Insert");

            db.AddInParameter(command, "@Jobid", System.Data.DbType.Int32, Jobid);
            db.AddInParameter(command, "@Appid", System.Data.DbType.Int32, Appid);
            db.ExecuteScalar(command);
        }

        public static void DeleteInterestedIn(int Jobid, int Appid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand command = db.GetStoredProcCommand("InterestedIn_Delete");

            db.AddInParameter(command, "@Jobid", System.Data.DbType.Int32, Jobid);
            db.AddInParameter(command, "@Appid", System.Data.DbType.Int32, Appid);
            db.ExecuteScalar(command);
        }

        public List<Jobs> SearchResults(string SearchType, string SearchString)
        {
            var list = new List<Jobs>();
            DbCommand command;

            if (SearchType.Equals("Location"))
            {
                command = db.GetStoredProcCommand("Jobs_SearchLocation");
                db.AddInParameter(command, "@SearchString", System.Data.DbType.String, SearchString);
                db.ExecuteScalar(command);
            }
            else
            {
                command = db.GetStoredProcCommand("Jobs_SearchTitle");
                db.AddInParameter(command, "@SearchString", System.Data.DbType.String, SearchString);
                db.ExecuteScalar(command);
            }

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

        public static string GetCompany(int Jobid)
        {
            string connectionString = "Data Source=.\\MSSQLSERVER2;Initial Catalog=JobLoader;Integrated Security=True";
            string queryString = "SELECT R.Company " +
                                 "FROM Jobs J, Recruiters R " +
                                 "WHERE J.Rid = R.Rid AND J.Rid='" + Jobid + "'";
            string company = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    company = reader[0].ToString();
                }
                reader.Close();
            }

            return company;
        }

        public static int GetRid(string Username)
        {
            string connectionString = "Data Source=.\\MSSQLSERVER2;Initial Catalog=JobLoader;Integrated Security=True";
            string queryString = "SELECT R.Rid " +
                                 "FROM Users U, Recruiters R " +
                                 "WHERE U.Userid = R.Userid AND U.Username='" + Username + "'";
            int Rid = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Rid = (int)reader[0];
                }
                reader.Close();
            }

            return Rid;
        }

        public static string GetType(string Username)
        {
            string connectionString = "Data Source=.\\MSSQLSERVER2;Initial Catalog=JobLoader;Integrated Security=True";
            string queryString = "SELECT U.Type " +
                                "FROM Users U " +
                                "WHERE U.Username='" + Username + "'";
            string type = "";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    type = reader[0].ToString();
                }
                reader.Close();
            }

            return type;
        }

        public static int GetJobid(int Userid)
        {
            string connectionString = "Data Source=.\\MSSQLSERVER2;Initial Catalog=JobLoader;Integrated Security=True";
            string queryString = "SELECT J.Jobid " +
                                 "FROM Jobs J, Recruiters R " +
                                 "WHERE J.Rid = R.Rid AND R.Userid='" + Userid + "'";
            int Jobid = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Jobid = (int)reader[0];
                }
                reader.Close();
            }

            return Jobid;
        }

        public static int GetAppid(string Username)
        {
            string connectionString = "Data Source=.\\MSSQLSERVER2;Initial Catalog=JobLoader;Integrated Security=True";
            string queryString = "SELECT A.Appid " +
                                 "FROM Applicants A, Users U " +
                                 "WHERE A.Userid = U.Userid AND U.Username='" + Username + "'";
            int Appid = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Appid = (int)reader[0];
                }
                reader.Close();
            }

            return Appid;
        }

        public static bool ShowsInterest(int Jobid, int Appid)
        {
            bool InterestedIn = false;

            string connectionString = "Data Source=.\\MSSQLSERVER2;Initial Catalog=JobLoader;Integrated Security=True";
            string queryString = "SELECT I.Jobid " +
                                 "FROM Interested_In I " +
                                 "WHERE I.Jobid='" + Jobid + "' AND I.Appid='" + Appid + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
               
                    if (reader.HasRows == true)
                    {
                        InterestedIn = true;
                    }
                
                reader.Close();
            }

            return InterestedIn;
        }
    }
}
