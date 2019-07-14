using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipePuppy.Models
{
    public class RecipeAPIDAL
    {
        public static string APICall(string URL)
        {
            HttpWebRequest request = WebRequest.CreateHttp(URL);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string APIText = rd.ReadToEnd();

            return APIText;
        }

        public static List<Recipe> GetNewRecipe(string ing1, string ing2, string ing3)
        {
            string URL = $"http://www.recipepuppy.com/api/?i={ing1},{ing2},{ing3}";

            string APIText = APICall(URL);

            JToken json = JToken.Parse(APIText);

            List<JToken> jsonRecipes = json["results"].ToList();

            List<Recipe> Recipes = new List<Recipe>();

            foreach(JToken j in jsonRecipes)
            {
                Recipe r = new Recipe(j);
                Recipes.Add(r);
            }

            return Recipes;
        }
        public static List<Recipe> GetNewRecipeTitle(string title)
        {
            string URL = $"http://www.recipepuppy.com/api/?q={title}";

            string APIText = APICall(URL);

            JToken json = JToken.Parse(APIText);

            List<JToken> jsonRecipes = json["results"].ToList();

            List<Recipe> Recipes = new List<Recipe>();

            foreach (JToken j in jsonRecipes)
            {
                Recipe r = new Recipe(j);
                Recipes.Add(r);
            }

            return Recipes;
        }
    }
}