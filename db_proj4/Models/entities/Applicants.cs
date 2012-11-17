using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace db_proj4.Models.entities
{
    public class Applicants
    {
        public int Appid { get; set; }
        public int Userid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string School { get; set; }
        public string Aemail { get; set; }
        public string Degree { get; set; }
        public string DegreeField { get; set; }
    }
}