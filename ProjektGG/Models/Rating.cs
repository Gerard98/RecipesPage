using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektGG.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjektGG.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public int RecipeID { get; set; }

    }
}