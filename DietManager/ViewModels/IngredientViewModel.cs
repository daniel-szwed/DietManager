using DietManager.Commands;
using DietManager.Models;
using DietManager.Repositories;
using DietManager.Services;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace DietManager.ViewModels
{
    public class IngredientViewModel : BindableBase, IIngredientViewModel
    {
        private IIngredientBaseRepository _ingredientRepository;
        private IIngredientService _ingredientService;
        private IngredientBase _ingredietn;

        public IngredientViewModel(IIngredientBaseRepository ingredientRepository, IIngredientService ingredientService)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientService = ingredientService;
            Ingredients = new ObservableCollection<IngredientBase>(ingredientRepository.GetAllAsync().GetAwaiter().GetResult());
        }

        public ObservableCollection<IngredientBase> Ingredients { get; set; }

        public IngredientBase Ingredient
        {
            get { return _ingredietn; }
            set { _ingredietn = value; NotifyPropertyChanged(nameof(Ingredient)); }
        }

        #region Commands
        public ICommand SearchIngredient
        {
            get { return new EagerCommand((parameters) => OnSearchIngredient(parameters)); }
        }

        public ICommand AddIngredient
        {
            get { return new EagerCommand(p => OnAddIngredientAsync(p), p => CanAddIngredient(p)); }
        }

        public ICommand UpdateIngredient
        {
            get { return new EagerCommand(p => OnUpdateIngredientAsync(p), p => CanEditIngredient(p)); } 
        }

        public ICommand RemoveIngredient
        {
            get { return new EagerCommand(p => OnRemoveIngredientAsync(p), p => CanEditIngredient(p)); } 
        }
        #endregion

        #region Command Implementation
        private async void OnSearchIngredient(object obj)
        {
            string name = obj as string;
            Ingredient = await _ingredientService.SearchIngredientAsync(name);
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
                    if (!float.TryParse(param, NumberStyles.Any, CultureInfo.InvariantCulture, out floatValue))
                        return false;
                }
            }
            return true;
        }

        private async void OnAddIngredientAsync(object parameters)
        {
            object[] res = parameters as object[];
            var ingredient = new IngredientBase()
            {
                Name = res[0].ToString(),
                Kcal = float.Parse(res[1].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Protein = float.Parse(res[2].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Carbohydrates = float.Parse(res[3].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Sugar = float.Parse(res[4].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Fat = float.Parse(res[5].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Saturated = float.Parse(res[6].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture)
            };
            var result = await _ingredientRepository.Add(ingredient).SaveChangesAsync();
            if (result == 1)
                Ingredients.Add(ingredient);
        }

        private bool CanEditIngredient(object arg)
        {
            var ingredient = arg as IngredientBase;
            return arg is IngredientBase;
        }

        private async void OnUpdateIngredientAsync(object obj)
        {
            var ingredient = obj as IngredientBase;
            var result = await _ingredientRepository.Update(ingredient).SaveChangesAsync();
        }

        private async void OnRemoveIngredientAsync(object obj)
        {
            var ingredient = obj as IngredientBase;
            Ingredients.Remove(ingredient);
            var result = await _ingredientRepository.Remove(ingredient).SaveChangesAsync();
        }
        #endregion
    }
}
