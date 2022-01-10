using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Application.Queries;
using ArbreSoft.DietManager.Presentation.Commands;
using ArbreSoft.DietManager.Presentation.Models;
using ArbreSoft.DietManager.Presentation.Views;
using AutoMapper;
using MediatR;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public class NutritionFactsViewModel : INutritionFactViewModel
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private string _searchQuery;
        private string _query;

        public NutritionFactsViewModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            Items = new ObservableCollection<NutritionFact>();
            Filtered = new ObservableCollection<NutritionFact>();
            InitAsync().GetAwaiter().GetResult();
        }

        private async Task InitAsync()
        {
            var nutritionFacts = await mediator.Send(new GetAllNutritionFactsQuery());
            foreach (var nutritionFact in nutritionFacts)
            {
                Items.Add(mapper.Map<NutritionFact>(nutritionFact));
                Filtered.Add(mapper.Map<NutritionFact>(nutritionFact));
            }
        }

        public ObservableCollection<NutritionFact> Items { get; set; }
        public ObservableCollection<NutritionFact> Filtered { get; set; }

        public NutritionFact SelectedItem { get; set; }

        public void OnSearchQueryChanged(object sender, string searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                Filtered.Clear();
                foreach (var item in Items.Where(item => item.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
                {
                    Filtered.Add(item);
                }
            }
            else
            {
                Filtered.Clear();
                foreach (var item in Items)
                {
                    Filtered.Add(item);
                }
            }
        }

        #region Commands
        public ICommand Add => new Command(_ => OnAdd());
        public ICommand Update => new Command(_ => OnUpdate(), _ => CanUpdate());
        public ICommand SearchOnline => new Command((parameters) => OnSearchIngredient(parameters));
        public ICommand Remove => new Command(parameters => OnRemoveAsync(parameters), _ => CanUpdate());
        #endregion

        #region Command Implementation
        private void OnAdd()
        {
            new AddNutritionFactView(Items).ShowDialog();
            OnSearchQueryChanged(null, string.Empty);
        }

        private bool CanUpdate() => SelectedItem is not null;

        private void OnUpdate()
        {
            new AddNutritionFactView(Items, SelectedItem).Show();
        }

        private async void OnRemoveAsync(object obj)
        {
            var ingredient = obj as NutritionFact;
            await mediator.Send(new RemoveNutritionFactCommand(mapper.Map<Domain.NutritionFact>(ingredient)));
            Items.Remove(ingredient);
        }

        private async void OnSearchIngredient(object obj)
        {
            var parameters = obj as object[];
            var sender = parameters[0] as Button;
            var name = parameters[1].ToString();
            //Ingredient = await _ingredientService.SearchIngredientAsync(name);
            sender.MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
        }


        #endregion
    }
}
