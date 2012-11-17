using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_proj4.Models.entities
{
    public class Users
    {
        public int Userid { get; set; }
        public string Type { get; set; }
        public string Pass { get; set; }
    }
}