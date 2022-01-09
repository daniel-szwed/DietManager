using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Presentation.Commands;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public class AddNutritionFactViewModel : IAddNutritionFactViewModel, IUpdateNutritionFactViewModel
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private Models.NutritionFact _selectedItem;

        public AddNutritionFactViewModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public ObservableCollection<Models.NutritionFact> Items { get; set; }

        public Models.NutritionFact NutritionFact { get; private set; }

        public Models.NutritionFact SelectedItem {
            get => _selectedItem;
            set 
            {
                _selectedItem = value;
                NutritionFact = value;
            } 
        }

        public ICommand Submit => SelectedItem is null ? Add : Update;
        public string SubmitButtonText => SelectedItem is null ? "Add" : "Update";

        #region Commands
        public ICommand Add => new Command(parameters => OnAdd(parameters), parameters => CanAdd(parameters));

        public ICommand Update => new Command(async parameters => await OnUpdateAsync(parameters), parameters => CanAdd(parameters));
        #endregion

        #region CommandsImplementation
        public bool CanAdd(object parameters) => (parameters as object[]).Skip(1).All(x => x.TryParse(out _));

        private async void OnAdd(object parameters)
        {
            (object Name,
                object Kcal,
                object Protein,
                object Carbo,
                object Sugar,
                object Fat,
                object Satturated,
                object Rest) = parameters as object[];

            var ingredient = new Models.NutritionFact()
            {
                Name = Name.ToString(),
                Kcal = Kcal.ToFloat(),
                Protein = Protein.ToFloat(),
                Carbohydrates = Carbo.ToFloat(),
                Sugar = Sugar.ToFloat(),
                Fat = Fat.ToFloat(),
                Saturated = Satturated.ToFloat()
            };
            await mediator.Send(new AddNutritionFactCommand(mapper.Map<Domain.NutritionFact>(ingredient)));
            Items.Add(ingredient);
            Close();
        }

        private async Task OnUpdateAsync(object parameters)
        {
            (object Name,
                object Kcal,
                object Protein,
                object Carbo,
                object Sugar,
                object Fat,
                object Satturated,
                object Rest) = parameters as object[];

            SelectedItem.Name = Name.ToString();
            SelectedItem.Kcal = Kcal.ToFloat();
            SelectedItem.Protein = Protein.ToFloat();
            SelectedItem.Carbohydrates = Carbo.ToFloat();
            SelectedItem.Sugar = Sugar.ToFloat();
            SelectedItem.Fat = Fat.ToFloat();
            SelectedItem.Saturated = Satturated.ToFloat();

            await mediator.Send(new UpdateNutritionFactCommand(mapper.Map<Domain.NutritionFact>(SelectedItem)));
            Close();
        }
        #endregion

        private void Close()
        {
            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
    }

    public static class Extensions
    {
        public static float ToFloat(this object input) =>
            float.Parse(input.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);

        public static bool TryParse(this object input, out float output) => 
            float.TryParse(input.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out output);

        public static void Deconstruct<T>(
            this T[] input,
            out T first,
            out T second,
            out T third,
            out T fourth,
            out T fifth,
            out T sixth,
            out T seventh,
            out IList<T> rest)
        {
            first = input.Any() ? input[0] : default(T);
            second = input.Any() ? input[1] : default(T);
            third = input.Any() ? input[2] : default(T);
            fourth = input.Any() ? input[3] : default(T);
            fifth = input.Any() ? input[4] : default(T);
            sixth = input.Any() ? input[5] : default(T);
            seventh = input.Any() ? input[6] : default(T);
            rest = input.Skip(7).ToList();
        }
    }
}
