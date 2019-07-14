using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RecipePuppy.Models;

namespace RecipePuppy.Controllers
{
    public class HomeController : Controller
    {
        RecipeDBEntities db = new RecipeDBEntities();
        public bool removed = false;

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RecipeList(string title)
        {
            return View();
        }

        [HttpPost]

        public ActionResult RecipeList(string ing1, string ing2, string ing3, int num)
        {
            List<Recipe> Recipes = RecipeAPIDAL.GetNewRecipe(ing1, ing2, ing3);

            Session["Number"] = num;

            return View(Recipes);
        }

        public ActionResult RecipeListTitle(string title)
        {
            List<Recipe> Recipes = RecipeAPIDAL.GetNewRecipeTitle(title);

            return View("RecipeList",Recipes);
        }
        public ActionResult Ingredients() 
        {

          return View();
        }
        public ActionResult RecipeFavorite(string title, string image, string ingredients, string thumbNail)
        {
            User u = (User)Session["User"];
            User favU = db.Users.Where(login => login.UserID == u.UserID).ToList().First();
            Favorite fav = new Favorite();

            if (removed)
            {
                removed = false;
            }
            else
            {
                Session["Success"] = "";
            }

            fav.Title = title;
            fav.Image = image;
            fav.Ingredients = ingredients;
            fav.Thumbnail = thumbNail;
            fav.FavUserID = u.UserID;
            fav.User = favU;
            
            db.Favorites.Add(fav);
            db.SaveChanges();
            return RedirectToAction("FavoriteList");
        }

        public ActionResult FavoriteList()
        {
            User u = (User)Session["User"];
            List<Favorite> favs = db.Favorites.Where(fav => fav.FavUserID == u.UserID).ToList();


            return View(favs);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNewUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();

            return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            List < User > users = db.Users.ToList();
            User user = new User();
            for (int i = 0; i < users.Count; i++)
            {
                if (u.UserName == users[i].UserName)
                {
                    if (u.Password == users[i].Password)
                    {
                        Session["UserError"] = "";
                        user = users[i];
                        Session["User"] = user;
                        return View("Ingredients");
                    }
                }
                else
                {
                    continue;
                }
            }
            Session["UserError"] = "Incorrect username or password";
            return RedirectToAction("Login");
        }

        public ActionResult DeleteFav(int id) 
        {
            Favorite fav = db.Favorites.Find(id);
            db.Favorites.Remove(fav);
            Session["Success"] = "Successfully removed favorite.";
            removed = true;
            db.SaveChanges();
            return RedirectToAction("FavoriteList");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

    }
}