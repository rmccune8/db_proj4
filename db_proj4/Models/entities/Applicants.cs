using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;
namespace db_proj4.Models.entities
{
    public class Applicants
    {
        [Display(Name = "Appid")]
        public int Appid { get; set; }

        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Display(Name = "Userid")]
        public int Userid { get; set; }

        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Display(Name = "School")]
        public string School { get; set; }

        [Display(Name = "Email")]
        public string Aemail { get; set; }

        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [Display(Name = "Degree Field")]
        public string DegreeField { get; set; }
    }
}