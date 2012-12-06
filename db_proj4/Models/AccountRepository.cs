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