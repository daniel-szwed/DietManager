using DietManager.Commands;
using DietManager.Models;
using DietManager.Repositories;
using DietManager.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DietManager.ViewModels
{
    public class IngredientViewModel : IIngredientViewModel, INotifyPropertyChanged
    {
        private IIngredientRepository _ingredientRepository;
        private IIngredientService _ingredientService;
        private Ingredient _ingredietn;

        public ObservableCollection<Ingredient> Ingredients { get; set; }

        public Ingredient Ingredient
        {
            get { return _ingredietn; }
            set { _ingredietn = value; NotifyPropertyChanged(nameof(Ingredient)); AddIngredient?.RaiseCanExecuteChanged(); }
        }
        
        public Command AddIngredient { get; }
        public Command UpdateIngredient { get; }
        public Command RemoveIngredient { get; }
        public Command SearchIngredient { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        
        public IngredientViewModel(IIngredientRepository ingredientRepository, IIngredientService ingredientService)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientService = ingredientService;
            Ingredients = new ObservableCollection<Ingredient>(ingredientRepository.GetAllAsync().GetAwaiter().GetResult());
            Ingredient = new Ingredient() { Name = "nazwa", Kcal = 0, Protein = 0, Carbohydrates = 0, Sugar = 0, Fat = 0, Saturated = 0 };
            AddIngredient = new Command(OnAddIngredientAsync, CanAddIngredient);
            UpdateIngredient = new Command(OnUpdateIngredientAsync, CanEditIngredient);
            RemoveIngredient = new Command(OnRemoveIngredientAsync, CanEditIngredient);
            SearchIngredient = new Command(OnSearchIngredient);
        }

        private async void OnSearchIngredient(object obj)
        {
            string name = obj as string;
            Ingredient = await _ingredientService.SearchIngredientAsync(name);
            AddIngredient.RaiseCanExecuteChanged();
        }

        private bool CanEditIngredient(object arg)
        {
            var ingredient = arg as Ingredient;
            return arg is Ingredient;
        }

        private async void OnRemoveIngredientAsync(object obj)
        {
            var ingredient = obj as Ingredient;
            Ingredients.Remove(ingredient);
            var result = await _ingredientRepository.RemoveAsync(ingredient);
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

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
