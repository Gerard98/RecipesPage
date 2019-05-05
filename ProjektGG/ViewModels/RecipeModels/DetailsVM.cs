using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektGG.Models;
using ProjektGG.BL;

namespace ProjektGG.ViewModels.RecipeModels
{
    public class DetailsVM
    {

        public Recipe Recipe { get; set; }

        public string loggedUser
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public string IsRatedBefore(int id)
        {
            RecipeBL recipeBL = new RecipeBL();
            if (recipeBL.IsRatedBefore(id, loggedUser)) return "heart";
            else return "emptyHeart";
        }
    }
}