using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektGG.ViewModels.RecipeModels;
using ProjektGG.Models;
using ProjektGG.BL;

namespace ProjektGG.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Przepisy()
        {
            RecipeListVM recipeListVM = new RecipeListVM();
            RecipeBL recipeBL = new RecipeBL();
            recipeListVM.Recipes = recipeBL.GetRecipes();

            List<Recipe> recipes = GetSessionRecipes();

            foreach(var item in recipeListVM.Recipes)
            {
                if(recipes.Any(m=> m.ID == item.ID)) item.IsInCupboard = 1;
            }

            return View(recipeListVM);
        }

        [Authorize]
        public ActionResult Create()
        {
            CreateVM createVM = new CreateVM();
            
            return View(createVM);
        }

        [HttpPost]
        public ActionResult Create(CreateVM create)
        {
            if (ModelState.IsValid)
            {
                RecipeBL recipe = new RecipeBL();

                HttpPostedFileBase file = Request.Files["FileUpload"];



                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/png"))
                    {
                        //Save the File.
                        string filePath = Server.MapPath("~/Images/") + file.FileName;
                        file.SaveAs(filePath);
                        create.Recipe.ImgName = file.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("FormatError", "System obsługuje tylko pliki jpg i png!!");
                        return View(create);
                    }
                }
                else
                {
                    create.Recipe.ImgName = "BrakZdjecia.jpg";
                }
               
                create.Recipe.Username = HttpContext.User.Identity.Name;
                recipe.AddRecipe(create.Recipe);
                return RedirectToAction("Przepisy");
            }
            else return View(create);

        }

        public ActionResult Details(int id)
        {
            RecipeBL recipe = new RecipeBL();
            DetailsVM detailsVM = new DetailsVM();
            detailsVM.Recipe = recipe.GetRecipes().Where(m => m.ID == id).Single();

            return View(detailsVM);
        }
        

        [Authorize]
        public ActionResult UserRecipes()
        {
            UserRecipesVM userRecipesVM = new UserRecipesVM();
            RecipeBL recipe = new RecipeBL();
            string username = HttpContext.User.Identity.Name;
            userRecipesVM.Recipes = recipe.GetUserRecipes(username);
            
            return View(userRecipesVM);
        }
        
        public ActionResult Edit(int id)
        {
            RecipeBL recipe = new RecipeBL();
            EditVM editVM = new EditVM();
            editVM.Recipe = recipe.GetRecipes().Where(m => m.ID == id).Single();

            return View(editVM);
        }

        [HttpPost]
        public ActionResult Edit(EditVM edit)
        {
            RecipeBL recipeBL = new RecipeBL();
            
            HttpPostedFileBase file = Request.Files["FileUpload"];

            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/png"))
                {
                    //Save the File.
                    string filePath = Server.MapPath("~/Images/") + file.FileName;
                    file.SaveAs(filePath);
                    edit.Recipe.ImgName = file.FileName;
                }
                else
                {
                    ModelState.AddModelError("FormatError", "System obsługuje tylko pliki jpg i png!!");
                    return View(edit);
                }
            }
            recipeBL.EditRecipe(edit.Recipe);
            
            return RedirectToAction("UserRecipes");
        }

        public ActionResult Delete(int id)
        {
            RecipeBL recipe = new RecipeBL();
            DeleteVM delete = new DeleteVM();
            delete.Recipe = recipe.GetRecipes().Where(m => m.ID == id).Single();
            return View(delete);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            RecipeBL recipeBL = new RecipeBL();
            Recipe recipe = new Recipe();
            recipe = recipeBL.GetRecipes().Where(m => m.ID == id).Single();
            recipeBL.DeleteRecipe(recipe);
            return RedirectToAction("UserRecipes");
        }

        [Authorize]
        public ActionResult Like(int id)
        {
            RecipeBL recipeBL = new RecipeBL();
            recipeBL.Like(id);
            return RedirectToAction("Przepisy");
        }

        private List<Recipe> GetSessionRecipes()
        {
            List<Recipe> recipes;

            if (Session["UserCupboard"] == null)
            {
                recipes = new List<Recipe>();
            }
            else
            {
                recipes = (List<Recipe>)Session["UserCupboard"];
            }

            return recipes;
        }
        private void SaveSessionRecipes(List<Recipe> recipes)
        {
            Session["UserCupboard"] = recipes;
        }

        public ActionResult Cupboard()
        {
            CupboardVM cupboard = new CupboardVM();
            cupboard.Recipes = GetSessionRecipes();
            return View(cupboard);
        }

        public ActionResult AddToCupboard(int id)
        {
            RecipeBL recipeBL = new RecipeBL();
            Recipe recipe = recipeBL.GetRecipes().Where(m => m.ID == id).Single();
            List<Recipe> recipes = GetSessionRecipes();

            recipes.Add(recipe);
            SaveSessionRecipes(recipes);
            return RedirectToAction("Przepisy");
        }

        public ActionResult DeleteFromCupboard(int id)
        {
            RecipeBL recipeBL = new RecipeBL();
            List<Recipe> recipes = GetSessionRecipes();
            Recipe recipe = recipes.Where(m => m.ID == id).Single();

            recipes.Remove(recipe);
            SaveSessionRecipes(recipes);
            return RedirectToAction("Cupboard");
        }

        public ActionResult DeleteCupboard()
        {
            List<Recipe> recipes = GetSessionRecipes();
            recipes = null;
            SaveSessionRecipes(recipes);
            return RedirectToAction("Przepisy");
        }

    }
}