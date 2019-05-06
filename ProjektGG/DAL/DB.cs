using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProjektGG.Models;

namespace ProjektGG.DAL
{
    public class DB : DbContext
    {

        public DB() : base("ProjektDB3")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Tbl_Users");
            modelBuilder.Entity<Recipe>().ToTable("Tbl_Recipes");
            modelBuilder.Entity<Rating>().ToTable("Tbl_Rating");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> usersDB { get; set; }
        public DbSet<Recipe> recipsesDB { get; set; }
        public DbSet<Rating> ratingDB { get; set; }

        public List<Recipe> GetRecipeList()
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = recipsesDB.ToList();
            return recipes;
        }

        public void AddRecipe(Recipe recipe)
        {
            recipsesDB.Add(recipe);
            SaveChanges();
        }

        public Boolean CheckLogin(string username, string password)
        {


            Boolean check = false;
            check = usersDB.ToList().Any(m =>
            {
                if (m.Username.Equals(username) && m.Password.Equals(password)) return true;
                else return false;

            });
            return check;
        }

        public List<Recipe> GetUserRecipes(string username)
        {
            List<Recipe> recipes = new List<Recipe>();
            recipes = recipsesDB.ToList().Where(m => m.Username.Equals(username)).ToList();
            return recipes;
        }

        public Boolean AddUser(User user)
        {
            Boolean check = false;
            check = usersDB.ToList().Any(m =>
            {
                if (m.Username.Equals(user.Username)) return true;
                else return false;

            });
            if (!check)
            {
                usersDB.Add(user);
                SaveChanges();
                return true;
            }
            return false;
            
        }

        public void EditRecipe(Recipe recipe)
        {

            using (var context = new DB())
            {
                string query = "Update Tbl_Recipes Set Name = '" +recipe.Name + "' ,Description = '"+recipe.Description+"', ImgName = '" +recipe.ImgName+"' where ID = " +recipe.ID;
                context.Database.ExecuteSqlCommand(query);
            }


            SaveChanges();

        }

        public void DeleteRecipe(Recipe recipe)
        {
            recipsesDB.Attach(recipe);
            recipsesDB.Remove(recipe);
            SaveChanges();
        }

        public Boolean CheckName(string name)
        {
            List<Recipe> recipes = GetRecipeList();
            return recipes.Any(m => m.Name.Equals(name));
            
        }

        public int GetRate(int id)
        {
            using(var context = new DB())
            {
                string query = "Select * From Tbl_Rating where RecipeID = " + id;
                int sum = context.ratingDB.SqlQuery(query).Count();
                return sum;
            }
            
        }

        public void Like(int id)
        {
            bool ratedBefore;
            string username = HttpContext.Current.User.Identity.Name;
            ratedBefore = ratingDB.ToList().Any(m =>
            {
                if ((m.Username.Equals(username)) && (m.RecipeID == id)) return true;
                else return false;
            });

            if (!ratedBefore)
            {
                Rating rating = new Rating();
                rating.Username = username;
                rating.RecipeID = id;
                ratingDB.Add(rating);
                SaveChanges();
            }
            else
            {
                Rating rating = new Rating();
                rating = ratingDB.ToList().Where(m => m.RecipeID == id && m.Username.Equals(username)).Single();
                ratingDB.Attach(rating);
                ratingDB.Remove(rating);
                SaveChanges();
            }
        }

        public bool IsRatedBefore(int id, string username)
        {
            return ratingDB.ToList().Any(m => m.RecipeID == id && m.Username.Equals(username));
        }
    }
}