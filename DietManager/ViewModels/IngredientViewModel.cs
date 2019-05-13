using DietManager.Commands;
using DietManager.Models;
using DietManager.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DietManager.ViewModels
{
    public class IngredientViewModel : IIngredientViewModel
    {
        private IIngredientRepository _ingredientRepository;

        public Command AddIngredient { get; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public IngredientViewModel(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
            AddIngredient = new Command(OnAddIngredientAsync, CanAddIngredient);
            Ingredients = new ObservableCollection<Ingredient>(ingredientRepository.GetAllAsync().GetAwaiter().GetResult());
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

        private async void OnAddIngredientAsync(object parameters)
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
            var result = await _ingredientRepository.AddIngredientAsync(ingredient);
            if(result == 1)
                Ingredients.Add(ingredient);
        }
    }
}
