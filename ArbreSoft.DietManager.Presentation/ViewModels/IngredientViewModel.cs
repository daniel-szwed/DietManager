using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Application.Queries;
using ArbreSoft.DietManager.Presentation.Commands;
using ArbreSoft.DietManager.Presentation.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public class IngredientViewModel : BindableBase, IIngredientViewModel
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private NutritionFact _ingredient;

        public IngredientViewModel(IServiceProvider provider)
        {
            mediator = provider.GetService<IMediator>();
            mapper = provider.GetService<IMapper>();
            Ingredients = new ObservableCollection<NutritionFact>();
            InitAsync().GetAwaiter().GetResult();
        }

        private async Task InitAsync()
        {
            var nutritionFacts = await mediator.Send(new GetAllNutritionFactsQuery());
            foreach (var nutritionFact in nutritionFacts)
            {
                Ingredients.Add(mapper.Map<NutritionFact>(nutritionFact));
            }
        }

        public ObservableCollection<NutritionFact> Ingredients { get; set; }

        public NutritionFact Ingredient
        {
            get { return _ingredient; }
            set { _ingredient = value; NotifyPropertyChanged(nameof(Ingredient)); }
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
            var parameters = obj as object [];
            var sender = parameters[0] as Button;
            var name = parameters[1].ToString();
            //Ingredient = await _ingredientService.SearchIngredientAsync(name);
            sender.MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
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
            var ingredient = new NutritionFact()
            {
                Name = res[0].ToString(),
                Kcal = float.Parse(res[1].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Protein = float.Parse(res[2].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Carbohydrates = float.Parse(res[3].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Sugar = float.Parse(res[4].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Fat = float.Parse(res[5].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture),
                Saturated = float.Parse(res[6].ToString(), NumberStyles.Any, CultureInfo.InvariantCulture)
            };
            await mediator.Send(new AddNutritionFactCommand(mapper.Map<Domain.NutritionFact>(ingredient)));
            Ingredients.Add(ingredient);
        }

        private bool CanEditIngredient(object arg)
        {
            var ingredient = arg as NutritionFact;
            return arg is NutritionFact;
        }

        private async void OnUpdateIngredientAsync(object obj)
        {
            var ingredient = obj as NutritionFact;
            await mediator.Send(new UpdateNutritionFactCommand(mapper.Map<Domain.NutritionFact>(ingredient)));
        }

        private async void OnRemoveIngredientAsync(object obj)
        {
            var ingredient = obj as NutritionFact;
            await mediator.Send(new RemoveNutritionFactCommand(mapper.Map<Domain.NutritionFact>(ingredient)));
            Ingredients.Remove(ingredient);
        }
        #endregion
    }
}
