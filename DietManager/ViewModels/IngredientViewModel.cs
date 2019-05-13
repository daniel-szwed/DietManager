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
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public Command AddIngredient { get; }
        public Command UpdateIngredient { get; }
        public Command RemoveIngredient { get; }

        public IngredientViewModel(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
            Ingredients = new ObservableCollection<Ingredient>(ingredientRepository.GetAllAsync().GetAwaiter().GetResult());
            AddIngredient = new Command(OnAddIngredientAsync, CanAddIngredient);
            UpdateIngredient = new Command(OnUpdateIngredientAsync, CanUpdateIngredient);
            RemoveIngredient = new Command(OnRemoveIngredientAsync);
        }

        private bool CanUpdateIngredient(object arg)
        {
            var ingredient = arg as Ingredient;
            return arg is Ingredient;
        }

        private async void OnRemoveIngredientAsync(object obj)
        {
            var ingredient = obj as Ingredient;
            var result = await _ingredientRepository.RemoveAsync(ingredient);
            if (result == 1)
                Ingredients.Remove(ingredient);
        }

        private async void OnUpdateIngredientAsync(object obj)
        {
            var ingredient = obj as Ingredient;
            var result = await _ingredientRepository.UpdateAsync(ingredient);
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
            var result = await _ingredientRepository.AddAsync(ingredient);
            if(result == 1)
                Ingredients.Add(ingredient);
        }
    }
}
