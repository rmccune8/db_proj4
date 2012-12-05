using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db_proj4.Models.entities
{
    public class Jobs
    {
        [Display(Name = "Jobid")]
        public int Jobid { get; set; }

        [Display(Name = "Rid")]
        public int Rid { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Skills")]
        public string Skills { get; set; }

        [Display(Name = "Job Field")]
        public string Field { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Date { get; set; }

        [Display(Name = "Experience")]
        public string Experience { get; set; }

        [Display(Name = "Salary")]
        public int? Salary { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }
    }
}