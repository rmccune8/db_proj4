using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db_proj4.Models.entities
{
    public class Accounts
    {
        [Display(Name = "Userid")]
        public int Userid { get; set; }
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Pass")]
        public string Pass { get; set; }
    }
}