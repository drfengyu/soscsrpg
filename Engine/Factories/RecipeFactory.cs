using Engine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    public static class RecipeFactory
    {
        private static readonly List<Recipe> recipes = new List<Recipe>();
        static RecipeFactory() {
            Recipe granolaBar = new Recipe(1, "格兰诺拉麦片条");
            granolaBar.AddIngredient(3001,1);
            granolaBar.AddIngredient(3002,1);
            granolaBar.AddIngredient(3003,1);
            granolaBar.AddOutputItem(2001,1);
            recipes.Add(granolaBar);
        }

        public static Recipe RecipeByID(int id) {
            return recipes.FirstOrDefault(r => r.ID == id);
        }
    }
}
