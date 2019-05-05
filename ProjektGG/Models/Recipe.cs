using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ProjektGG.BL;


namespace ProjektGG.Models
{
    public class Recipe
    {
        [Key]
        public int ID { get; set; }
        [Required (ErrorMessage = "Danie musi posiadać nazwę!")]
        [StringLength (100,MinimumLength = 5 , ErrorMessage ="Nazwa powinna posiadać od 5 do 50 liter!")]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display( Name = "Opis")]
        public string Description { get; set; }

        public string ImgName { get; set; }

        public string Username { get; set; }

        public int IsInCupboard { get; set; }

        public int Rate
        {
            get
            {
                RecipeBL recipeBL = new RecipeBL();
                return recipeBL.GetRate(ID);
            }
        }

    }
}