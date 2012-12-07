using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace db_proj4.Models.entities
{
    public class Recruiters
    {
        public int Userid { get; set; }
        public int Rid { get; set; }
        [Display(Name = "Email")]
        public string Remail { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "Focus")]
        public string Focus { get; set; }
    }
}