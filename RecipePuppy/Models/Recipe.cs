using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipePuppy.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public string ThumbNail { get; set; }

        public Recipe()
        {

        }

        public Recipe(JToken j)
        {
            Title = (string)j["title"];
            Image = (string)j["href"];
            Ingredients = (string)j["ingredients"];
            ThumbNail = (string)j["thumbnail"];
        }
    }
}