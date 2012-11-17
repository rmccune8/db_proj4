using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_proj4.Models.entities
{
    public class Recruiters
    {
        public int Userid { get; set; }
        public int Rid { get; set; }
        public string Remail { get; set; }
        public string Company { get; set; }
        public string Focus { get; set; }
    }
}