using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using db_proj4.Models.entities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace db_proj4.Models
{
    public class AccountsRepository
    {
        private Database db;

        public AccountsRepository()
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

        public List<Accounts> BuildList()
        {
            var list = new List<Accounts>();

            DbCommand command = db.GetStoredProcCommand("Users_BuildList");

            using (var reader = db.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var account = new Accounts();
                    if (!reader.IsDBNull(reader.GetOrdinal("Userid")))
                    {
                        account.Userid = reader.GetInt32(reader.GetOrdinal("Userid"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Username")))
                    {
                        account.Username = reader.GetString(reader.GetOrdinal("Username"));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Pass")))
                    {
                        account.Pass = reader.GetString(reader.GetOrdinal("Pass"));
                    }
                    list.Add(account);
                }
            }
            return list;
        }
    }
}