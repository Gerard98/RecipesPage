using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektGG.Models;
using ProjektGG.DAL;

namespace ProjektGG.BL
{
    public class RecipeBL
    {
        public Recipe Recipe { get; set; }

        public List<Recipe> GetRecipes()
        {
            DB db = new DB();
            List<Recipe> recipes = new List<Recipe>();
            recipes = db.GetRecipeList();
            return recipes;
        }

        public void AddRecipe(Recipe recipe)
        {
            DB db = new DB();
            db.AddRecipe(recipe);
        }

        public List<Recipe> GetUserRecipes(string username)
        {
            List<Recipe> recipes = new List<Recipe>();
            DB db = new DB();
            recipes = db.GetUserRecipes(username);
            return recipes;
        }

        public void DeleteRecipe(Recipe recipe)
        {
            DB db = new DB();
            db.DeleteRecipe(recipe);
        }

        public Boolean CheckName(string name)
        {
            DB db = new DB();
            return db.CheckName(name);
        }

        public void EditRecipe(Recipe recipe)
        {
            DB db = new DB();
            db.EditRecipe(recipe);
        }

        public void Like(int id)
        {
            DB db = new DB();
            db.Like(id);
        }

        public int GetRate(int id)
        {
            DB db = new DB();
            return db.GetRate(id);
        }

        public bool IsRatedBefore(int id, string username)
        {
            DB db = new DB();
            return db.IsRatedBefore(id, username);
        }

    }
}