using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjektGG.Models
{
    public class User
    {
        [Key]
        [Display(Name = "Nazwa Użytkownika")]
        [Required(ErrorMessage ="Proszę podać nazwę użytkownika!")]
        public string Username { get; set; }
        
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Proszę podać hasło!")]
        [StringLength (30,MinimumLength = 7 , ErrorMessage ="Hasło musi posiadać od 7 do 30 znaków!")]
        public string Password { get; set; }

        public List<int> IdRatedRecipes { get; set; } 

    }
}