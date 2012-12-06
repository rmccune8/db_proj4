using System;
using System.Collections.Generic;
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
        [Display(Name = "Userid")]
        public int Userid { get; set; }
        [Display(Name = "Fname")]
        public string Fname { get; set; }
        [Required]
        [Display(Name = "Lname")]
        public string Lname { get; set; }
        [Display(Name = "School:")]
        public string School { get; set; }
        [Display(Name = "Aemail")]
        public string Aemail { get; set; }
        [Display(Name = "Degree")]
        public string Degree { get; set; }
        [Display(Name = "DegreeField")]
        public string DegreeField { get; set; }
    }
}