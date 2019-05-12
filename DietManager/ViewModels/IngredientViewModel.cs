using DietManager.Commands;
using DietManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DietManager.ViewModels
{
    public class IngredientViewModel
    {
        public Command AddIngredient { get; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public IngredientViewModel()
        {
            AddIngredient = new Command(OnAddIngredient, CanAddIngredient);
            Ingredients = new ObservableCollection<Ingredient>()
            {
                new Ingredient() {Name = "test0", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test1", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test2", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test3", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test4", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test5", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test6", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test7", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test8", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
                new Ingredient() {Name = "test9", Kcal = 333f, Protein = 28f, Carbohydrates = 16f, Sugar = 6f, Fat = 12f, Saturated = 6f },
            };
        }

        public bool CanAddIngredient(object parameters)
        {
            float floatValue;
            object[] res = parameters as object[];
            if (res != null)
            {
                var sRes = res.OfType<string>().ToList();
                sRes.Remove(sRes.First());
                foreach (var param in sRes)
                {
                    if (!float.TryParse(param, out floatValue))
                        return false;
                }
            }
            return true;
        }

        private void OnAddIngredient(object parameters)
        {
            object[] res = parameters as object[];
            var ingredient = new Ingredient()
            {
                Name = res[0].ToString(),
                Kcal = float.Parse(res[1].ToString()),
                Protein = float.Parse(res[2].ToString()),
                Carbohydrates = float.Parse(res[3].ToString()),
                Sugar = float.Parse(res[4].ToString()),
                Fat = float.Parse(res[5].ToString()),
                Saturated = float.Parse(res[6].ToString())
            };
            Ingredients.Add(ingredient);
        }
    }
}
