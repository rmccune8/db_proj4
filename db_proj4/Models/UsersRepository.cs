using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Models
{
    public static class Crypto
    {
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
    public class UsersRepository
    {
        private Database db;

        public UsersRepository()
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

        public List<Users> BuildList()
        {
            var list = new List<Users>();

            DbCommand command = db.GetStoredProcCommand("Users_BuildList");

            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var User = new Users();
                    if (!reader.IsDBNull(reader.GetOrdinal("Type")))
                    {
                        User.Type = reader.GetString(reader.GetOrdinal("Type"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Pass")))
                    {
                        User.Pass = Crypto.Hash(reader.GetString(reader.GetOrdinal("Pass")));

                    }

                    list.Add(User);
                }
            }
            return list;
        }
    }
}