using System;
using System.Collections.Generic;
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
    public class NutritionFactsBrowserViewModel : BindableBase, INutritionFactsBrowserViewModel
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private string _query = string.Empty;
        private NutritionFact _selectedItem;

        public NutritionFactsBrowserViewModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            Items = new ObservableCollection<NutritionFact>();
            InitAsync().GetAwaiter().GetResult();
        }

        private async Task InitAsync()
        {
            var nutritionFacts = await mediator.Send(new GetAllNutritionFactsQuery());
            foreach (var nutritionFact in nutritionFacts)
            {
                Items.Add(mapper.Map<NutritionFact>(nutritionFact));
            }
        }

        public ObservableCollection<NutritionFact> Items { get; set; }
        public IEnumerable<NutritionFact> Filtered => Items.Where(item => item.Name.Contains(_query, StringComparison.OrdinalIgnoreCase));

        public NutritionFact SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    NotifyPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public void OnSearchQueryChanged(object sender, string searchQuery)
        {
            _query = searchQuery;
            NotifyPropertyChanged(nameof(Filtered));
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
            new NutritionFactDialogView(Items).ShowDialog();
            NotifyPropertyChanged(nameof(Filtered));
        }

        private bool CanUpdate() => SelectedItem is not null;

        private void OnUpdate()
        {
            new NutritionFactDialogView(Items, SelectedItem).ShowDialog();
            var selectedId = SelectedItem.Id;
            NotifyPropertyChanged(nameof(Filtered));
            SelectedItem = Filtered.FirstOrDefault(item => item.Id == selectedId);
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
