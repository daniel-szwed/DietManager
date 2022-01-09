using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ArbreSoft.DietManager.Presentation.Commands;
using ArbreSoft.DietManager.Presentation.Views;
using MediatR;
using ArbreSoft.DietManager.Application.Commands;
using AutoMapper;
using System.Threading.Tasks;
using ArbreSoft.DietManager.Application.Queries;
using static ArbreSoft.DietManager.Domain.Extensions;
using System.ComponentModel;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public class MainViewModel : IMainViewModel, INotifyPropertyChanged
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        private Models.Ingredient selectedIngredient;
        private Models.NutritionFact _totalNutritionFacts;

        public event PropertyChangedEventHandler PropertyChanged;
        public MainViewModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            Meals = new ObservableCollection<Models.Meal>();
            IngredientBase = new ObservableCollection<Models.NutritionFact>();
            InitAsync().GetAwaiter().GetResult();
        }

        private async Task InitAsync()
        {
            var nutritionFacts = await mediator.Send(new GetAllNutritionFactsQuery());
            foreach (var nutritionFact in nutritionFacts)
            {
                IngredientBase.Add(mapper.Map<Models.NutritionFact>(nutritionFact));
            }

            var meals = await mediator.Send(new GetAllMealsQuery());
            foreach (var meal in meals)
            {
                Meals.Add(mapper.Map<Models.Meal>(meal));
            }

            CalcTotalNutritionFact();
        }

        public ObservableCollection<Models.Meal> Meals { get; set; }
        public ObservableCollection<Models.NutritionFact> IngredientBase { get; set; }
        
        public Models.Meal SelectedMeal { get; set; }

        public Models.Ingredient SelectedIngredient
        {
            get =>  selectedIngredient; 
            set 
            { 
                selectedIngredient = value;
                if(selectedIngredient is not null)
                {
                    SelectedMeal = Meals.FirstOrDefault(m => m.Ingregients.Contains(selectedIngredient));
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
        public ICommand SaveToDataBase
        {
            get { return new Command(parameters => OnSaveToDataBase(parameters)); }
        }

        public ICommand ImportDiet
        {
            get { return new Command(parameters => OnImportDietAsync(parameters)); }
        }

        public ICommand ExportDiet
        {
            get { return new Command(parameters => OnExportDiet(parameters)); }
        }

        public ICommand ManageIngredients
        {
            get { return new Command((parameters) => OnManageIngredients(parameters)); }
        }

        public ICommand AddMeal
        {
            get { return new Command(parameters => OnAddMeal(parameters)); }
        }

        public ICommand RemoveMeal
        {
            get { return new Command(async parameters => await OnRemoveMealAsync(parameters)); }
        }

        public ICommand AddIngredient
        {
            get { return new Command(async parameters => await OnAddIngredientAsync(parameters), parameters => CanAddIngredient(parameters)); }
        }

        public ICommand RefreshIngredients
        {
            get { return new Command(parameters => OnRefreshInfredients(parameters)); }
        }

        public ICommand RemoveIngredient
        {
            get { return new Command(async parameters => await OnRemoveIngredientAsync (parameters), parameters => CanRemoveIngredient(parameters)); }
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
            new NutritionFactView().Show();
        }
       
        private async void OnAddMeal(object p)
        {
            var dialog = new InputDialog("Diet Manager", "Meal name");
            if (dialog.ShowDialog() == true)
            {
                var meal = new Models.Meal(dialog.ResponseText);
                var mealWithId = await mediator.Send(new AddMealCommand(mapper.Map<Domain.Meal>(meal)));
                Meals.Add(mapper.Map<Models.Meal>(mealWithId));
            }
        }

        private async Task OnRemoveMealAsync(object p)
        {
            Models.Meal meal = Meals.First(x => x.Id == (p as Models.Meal).Id);
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
            ingredient.Amount = float.Parse(param[2] as string);
            await mediator.Send(new AddIngredientCommand(meal.Id, mapper.Map<Domain.Ingredient>(ingredient)));
            meal.Ingregients.Add(ingredient);
            CalcTotalNutritionFact();
        }

        private bool CanRemoveIngredient(object p)
        {
            var param = p as object[];
            var meal = param[0] as Models.Meal;
            var ingredient = param[1] as Models.Ingredient;

            return meal?.Ingregients.Contains(ingredient) ?? false;
        }

        private async Task OnRemoveIngredientAsync(object p)
        {
            var param = p as object[];
            var meal = param[0] as Models.Meal;
            var ingredient = param[1] as Models.Ingredient;
            await mediator.Send(new RemoveIngredientCommand(meal.Id, ingredient.Id));
            meal.Ingregients.Remove(ingredient);
            CalcTotalNutritionFact();
        }
        #endregion

        private async Task<IEnumerable<Models.NutritionFact>> GetIngredientsAsync()
        {
            var nutritionFacts = await mediator.Send(new GetAllNutritionFactsQuery());

            return mapper.Map<IEnumerable<Models.NutritionFact>>(nutritionFacts);
        }

        private void CalcTotalNutritionFact()
        {
            var result = new Domain.NutritionFact();

            foreach (var meal in mapper.Map<IEnumerable<Domain.Meal>>(Meals))
            {
                result += meal.Sum().ToFixed(1);
            }

            TotalNutritionFacts = mapper.Map<Models.NutritionFact>(result);
        }

        internal void IncreaseAmount(Models.Ingredient ingredient)
        {
            ingredient.Amount++;
            CalcTotalNutritionFact();
        }

        internal void DecreaseAmount(Models.Ingredient ingredient)
        {
            ingredient.Amount--;
            CalcTotalNutritionFact();
        }
    }
}