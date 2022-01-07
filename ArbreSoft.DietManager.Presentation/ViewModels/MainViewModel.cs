using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System;
using ArbreSoft.DietManager.Presentation.Models;
using ArbreSoft.DietManager.Presentation.Commands;
using ArbreSoft.DietManager.Presentation.Views;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ArbreSoft.DietManager.Application.Commands;
using AutoMapper;
using System.Threading.Tasks;
using ArbreSoft.DietManager.Application.Queries;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public class MainViewModel : BindableBase, IMainViewModel
    {
        private readonly IServiceProvider provider;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        private Meal selectedMeal;
        private Ingredient selectedIngredient;
        private NutritionFact totalNutritionFacts;

        public MainViewModel(IServiceProvider provider)
        {
            this.provider = provider;
            mediator = provider.GetService<IMediator>();
            mapper = provider.GetService<IMapper>();
            Meals = new ObservableCollection<Meal>();
            IngredientBase = new ObservableCollection<NutritionFact>();
            InitAsync().GetAwaiter().GetResult();
        }

        private async Task InitAsync()
        {
            var nutritionFacts = await mediator.Send(new GetAllNutritionFactsQuery());
            foreach (var nutritionFact in nutritionFacts)
            {
                IngredientBase.Add(mapper.Map<NutritionFact>(nutritionFact));
            }

            var meals = await mediator.Send(new GetAllMealsQuery());
            foreach (var meal in meals)
            {
                Meals.Add(mapper.Map<Models.Meal>(meal));
            }

            CalcTotalNutritionFact();
        }

        public ObservableCollection<Meal> Meals { get; set; }
        public ObservableCollection<NutritionFact> IngredientBase { get; set; }
        
        public Meal SelectedMeal
        {
            get { return selectedMeal; }
            set { selectedMeal = value; NotifyPropertyChanged(nameof(SelectedMeal)); }
        }

        public Ingredient SelectedIngredient
        {
            get { return selectedIngredient; }
            set { selectedIngredient = value;
                if(selectedIngredient != null)
                    SelectedMeal = Meals.First(m => m.Ingregients.Contains(selectedIngredient));
                NotifyPropertyChanged(nameof(SelectedIngredient)); }
        }

        public NutritionFact TotalNutritionFacts
        {
            get { return totalNutritionFacts; }
            set { totalNutritionFacts = value; NotifyPropertyChanged(nameof(TotalNutritionFacts)); }
        }

        #region Commands
        public ICommand SaveToDataBase
        {
            get { return new EagerCommand(p => OnSaveToDataBase(p)); }
        }

        public ICommand ImportDiet
        {
            get { return new EagerCommand(p => OnImportDietAsync(p)); }
        }

        public ICommand ExportDiet
        {
            get { return new EagerCommand(p => OnExportDiet(p)); }
        }

        public ICommand ManageIngredients
        {
            get { return new EagerCommand((parameters) => OnManageIngredients(parameters)); }
        }

        public ICommand AddMeal
        {
            get { return new EagerCommand(p => OnAddMeal(p), p => CanAddMeal); }
        }

        public ICommand RemoveMeal
        {
            get { return new EagerCommand(p => OnRemoveMealAsync(p)); }
        }

        public ICommand AddIngredient
        {
            get { return new EagerCommand(p => OnAddIngredientAsync(p), p => CanAddIngredient(p)); }
        }

        public ICommand RefreshIngredients
        {
            get { return new EagerCommand(p => OnRefreshInfredients(p)); }
        }

        public ICommand RemoveIngredient
        {
            get { return new EagerCommand(p => OnRemoveIngredientAsync(p), p => CanRemoveIngredient(p)); }
        }
        #endregion

        #region Commands Implementation
        private void OnSaveToDataBase(object p)
        {
            //mealRepository.UpdateRange(Meals);
            //mealRepository.SaveChangesAsync();
        }

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
            CalcTotalNutritionFact();
        }

        private void OnExportDiet(object p)
        {
            //importExportService.ExportAsync(Meals);
        }

        private void OnManageIngredients(object parameters)
        {
            var window = new IngredientView(provider);
            window.Show();
        }
       
        public bool CanAddMeal { get { return true; } }
        
        private async void OnAddMeal(object p)
        {
            var meal = new Meal() { Name = p.ToString() };
            var mealWithId = await mediator.Send(new AddMealCommand(mapper.Map<Domain.Meal>(meal)));
            Meals.Add(mapper.Map<Meal>(mealWithId));
        }

        private async Task OnRemoveMealAsync(object p)
        {
            Meal meal = Meals.First(x => x.Id == (p as Meal).Id);
            await mediator.Send(new RemoveMealCommand(meal.Id));
            Meals.Remove(meal);
            CalcTotalNutritionFact();
        }

        private void OnRefreshInfredients(object p)
        {
            var ingredients = GetIngredientsAsync().GetAwaiter().GetResult();
            IngredientBase.Clear();
            ingredients.ToList().ForEach(i => IngredientBase.Add(i));
        }
        
        private bool CanAddIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = param[1] as NutritionFact;
            var ingredientAmount = param[2] as string;
            float ingrAmount;
            return meal != null && ingredient != null && float.TryParse(ingredientAmount, out ingrAmount);
        }

        private async Task OnAddIngredientAsync(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = new Ingredient(param[1] as NutritionFact);
            ingredient.Amount = float.Parse(param[2] as string);
            var test = await mediator.Send(new AddIngredientCommand(meal.Id, mapper.Map<Domain.Ingredient>(ingredient)));
            meal.Ingregients.Add(ingredient);
            //mealRepository.Update(meal);
            //mealRepository.SaveChangesAsync();
            CalcTotalNutritionFact();
        }

        private bool CanRemoveIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = param[1] as Ingredient;
            return meal?.Ingregients.Contains(ingredient) ?? false;
        }

        private async Task OnRemoveIngredientAsync(object p)
        {
            var param = p as object[];
            var meal = param[0] as Meal;
            var ingredient = param[1] as Ingredient;
            await mediator.Send(new RemoveIngredientCommand(meal.Id, ingredient.Id));
            meal.Ingregients.Remove(ingredient);
            CalcTotalNutritionFact();
        }
        #endregion

        private async Task<IEnumerable<NutritionFact>> GetIngredientsAsync()
        {
            var nutritionFacts = await mediator.Send(new GetAllNutritionFactsQuery());

            return mapper.Map<IEnumerable<Models.NutritionFact>>(nutritionFacts);
        }

        private void CalcTotalNutritionFact()
        {
            var result = new Domain.NutritionFact();

            foreach (var meal in mapper.Map<IEnumerable<Domain.Meal>>(Meals))
            {
                result += meal.Sum();
            }

            TotalNutritionFacts = mapper.Map<NutritionFact>(result);
        }

        internal void IncreaseAmount(Ingredient ingredient)
        {
            ingredient.Amount++;
            CalcTotalNutritionFact();
        }

        internal void DecreaseAmount(Ingredient ingredient)
        {
            ingredient.Amount--;
            CalcTotalNutritionFact();
        }
    }
}