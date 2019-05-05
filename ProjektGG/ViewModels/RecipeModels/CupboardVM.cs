using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektGG.Models;

namespace ProjektGG.ViewModels.RecipeModels
{
    public class CupboardVM
    {
        public List<Recipe> Recipes { get; set; }

        public string loggedUser
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
    }
}