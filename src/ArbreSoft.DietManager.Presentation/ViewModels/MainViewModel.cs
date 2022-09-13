using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ArbreSoft.DietManager.Presentation.Commands;
using ArbreSoft.DietManager.Presentation.Views;
using MediatR;
using ArbreSoft.DietManager.Application.Commands;
using AutoMapper;
using System.Threading.Tasks;
using ArbreSoft.DietManager.Application.Queries;
using System.ComponentModel;
using System;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public class MainViewModel : IMainViewModel, INotifyPropertyChanged
    {
        public IMediator Mediator { get; init; }
        private readonly IMapper mapper;

        private Models.Ingredient selectedIngredient;
        private Models.NutritionFact _totalNutritionFacts;
        private Models.Meal _selectedMeal;

        public event PropertyChangedEventHandler PropertyChanged;
        public MainViewModel(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            this.mapper = mapper;
            InitAsync().GetAwaiter().GetResult();
        }

        private async Task InitAsync()
        {
            var dailyMenus = await Mediator.Send(new GetAllDailyMenuQuery());
            if (!dailyMenus.Any())
            {
                var dailyMenu = await Mediator.Send(new AddDailyMenuCommand());
                DailyMenu = mapper.Map<Models.DailyMenu>(dailyMenu);
            }
            else
            {
                DailyMenu = mapper.Map<Models.DailyMenu>(dailyMenus.First());
            }

            await CalcTotalNutritionFactAsync();
        }

        public Models.DailyMenu DailyMenu { get; set; }
        
        public Models.Meal SelectedMeal { 
            get => _selectedMeal; 
            set
            {
                _selectedMeal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedMeal)));
            }
        }

        public Models.Ingredient SelectedIngredient
        {
            get =>  selectedIngredient; 
            set 
            { 
                selectedIngredient = value;
                if(selectedIngredient is not null)
                {
                    SelectedMeal = DailyMenu.Childrens.FirstOrDefault(m => m.Childrens.Contains(selectedIngredient));
                }
            }
        }

        public Models.NutritionFact TotalNutritionFacts 
        { 
            get => _totalNutritionFacts;
            set 
            {
                _totalNutritionFacts = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalNutritionFacts)));
            } 
        }

        #region Commands
        public ICommand ImportDiet
        {
            get { return new Command(parameters => OnImportDietAsync(parameters)); }
        }

        public ICommand ExportDiet
        {
            get { return new Command(parameters => OnExportDiet(parameters)); }
        }

        public ICommand BrowseNutritionFacts
        {
            get { return new Command((parameters) => OnManageIngredients(parameters)); }
        }

        public ICommand AddMeal
        {
            get { return new Command(parameters => OnAddMealAsync(parameters)); }
        }

        public ICommand AddIngredient
        {
            get { return new Command(async parameters => await OnAddIngredientAsync(parameters), parameters => CanAddIngredient(parameters)); }
        }

        public ICommand RemoveIngredient
        {
            get { return new Command(async parameters => await OnRemoveIngredientAsync (parameters), parameters => CanRemoveIngredient(parameters)); }
        }
        #endregion

        #region Commands Implementation
        private async void OnImportDietAsync(object p)
        {
            //var meals = await importExportService.ImportAsync<Meal>();
            //Meals.ToList().ForEach(m => mealRepository.Remove(m));
            //Meals.Clear();
            //meals.ToList().ForEach(m => {
            //    m.Ingregients.ToList().ForEach(i => ingredientRepository.Add(i));
            //    mealRepository.Add(m);
            //    Meals.Add(m);
            //});
            //mealRepository.SaveChangesAsync();
            await CalcTotalNutritionFactAsync ();
        }

        private void OnExportDiet(object p)
        {
            //importExportService.ExportAsync(Meals);
        }

        private void OnManageIngredients(object parameters)
        {
            new NutritionFactsBrowserView().Show();
        }
       
        private async void OnAddMealAsync(object p)
        {
            var dialog = new InputDialog("Diet Manager", "Meal name");
            if (dialog.ShowDialog() == true)
            {
                var meal = new Models.Meal(dialog.ResponseText);
                var mealWithId = await Mediator.Send(new AddMealCommand(DailyMenu.Id, mapper.Map<Domain.Meal>(meal)));
                DailyMenu.Childrens.Add(mapper.Map<Models.Meal>(mealWithId));
            }
        }

        private bool CanAddIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Models.Meal;
            var ingredient = param[1] as Models.NutritionFact;
            var ingredientAmount = param[2] as string;

            return meal != null && ingredient != null && float.TryParse(ingredientAmount, out _);
        }

        private async Task OnAddIngredientAsync(object p)
        {
            var param = p as object[];
            var meal = param[0] as Models.Meal;
            var ingredient = new Models.Ingredient(param[1] as Models.NutritionFact);
            ingredient.Weight = float.Parse(param[2] as string);
            await Mediator.Send(new AddIngredientCommand(meal.Id, mapper.Map<Domain.Ingredient>(ingredient)));
            meal.Childrens.Add(ingredient);
            await CalcTotalNutritionFactAsync();
        }

        private bool CanRemoveIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Models.Meal;
            var ingredient = param[1] as Models.Ingredient;

            return meal?.Childrens.Contains(ingredient) ?? false;
        }

        private async Task OnRemoveIngredientAsync(object p)
        {
            var param = p as object[];
            var meal = param[0] as Models.Meal;
            var ingredient = param[1] as Models.Ingredient;
            await Mediator.Send(new RemoveIngredientCommand(ingredient.Id));
            meal.Childrens.Remove(ingredient);
            await CalcTotalNutritionFactAsync ();
        }
        #endregion

        private async Task<IEnumerable<Models.NutritionFact>> GetIngredientsAsync()
        {
            var nutritionFacts = await Mediator.Send(new GetAllNutritionFactsQuery());

            return mapper.Map<IEnumerable<Models.NutritionFact>>(nutritionFacts);
        }

        private async Task CalcTotalNutritionFactAsync()
        {
            var totalNutritionFact = await Mediator.Send(new GetDailySumQuery(DailyMenu.Id));
            TotalNutritionFacts = mapper.Map<Models.NutritionFact>(totalNutritionFact);
        }

        internal async Task IncreaseAmountAsync(Models.Ingredient ingredient)
        {
            await Mediator.Send(new UpdateIngredientWeightCommand(ingredient.Id, +1));
            ingredient.Weight++;
            await CalcTotalNutritionFactAsync();
        }

        internal async Task DecreaseAmountAsync(Models.Ingredient ingredient)
        {
            await Mediator.Send(new UpdateIngredientWeightCommand(ingredient.Id, -1));
            ingredient.Weight--;
            await CalcTotalNutritionFactAsync();
        }
    }
}