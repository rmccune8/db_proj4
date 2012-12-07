using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace db_proj4.Models.entities
{
    public class Users
    {
        [Display(Name = "Userid")]
        public static int Userid { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Password")]
        public string Pass { get; set; }


    }
}