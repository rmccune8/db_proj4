using System;
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
                    list.Add(job);
                }
            }
            return list;
        }
    }
}