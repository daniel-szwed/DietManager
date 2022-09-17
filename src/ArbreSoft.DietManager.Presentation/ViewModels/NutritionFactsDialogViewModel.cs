using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Presentation.Commands;
using ArbreSoft.DietManager.Presentation.Models;
using ArbreSoft.DietManager.Presentation.Validators;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public class NutritionFactsDialogViewModel : BindableBase, INutritionFactsDialogViewModel
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IEnumerable<IValidator> _nameValidators;
        private readonly IValidator[] _floatValidators;
        private NutritionFact _nutritionFact;

        public NutritionFactsDialogViewModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            _nameValidators = new IValidator[] { new StringNotEmptyValidator() };
            _floatValidators = new IValidator[] { new StringNotEmptyValidator(), new FloatParseValidator() };
        }

        public ObservableCollection<Models.NutritionFact> Items { get; set; }
        public Models.NutritionFact NutritionFact
        {
            get => _nutritionFact;
            set
            {
                if (_nutritionFact != value)
                {
                    _nutritionFact = value;
                    NotifyPropertyChanged(nameof(NutritionFact));
                }
            }
        }
        public IEnumerable<IValidator> NameValidators => _nameValidators;
        public IEnumerable<IValidator> FloatValidators => _floatValidators;
        public ICommand Submit => GetSubmitCommand();

        public string ButtonText => NutritionFact is null ? "Add" : "Update";

        public string ButtonIcon => NutritionFact is null ? "PlusIcon" : "UpdateIcon";

        #region Commands
        public ICommand Add => new Command(parameters => OnAdd(parameters), parameters => CanAdd(parameters));

        public ICommand Update => new Command(async parameters => await OnUpdateAsync(parameters), parameters => CanAdd(parameters));
        #endregion

        #region CommandsImplementation
        public bool CanAdd(object parameters) => true;//(parameters as object[]).Skip(1).All(x => x.TryParse(out _));

        private async void OnAdd(object parameters)
        {
            var nutritionFactWithId = await mediator.Send(new AddNutritionFactCommand(mapper.Map<Domain.NutritionFact>(NutritionFact)));
            Items.Add(mapper.Map<Models.NutritionFact>(nutritionFactWithId));
            Close();
        }

        private async Task OnUpdateAsync(object parameters)
        {
            var updated = await mediator.Send(new UpdateNutritionFactCommand(mapper.Map<Domain.NutritionFact>(NutritionFact)));
            var source = Items.FirstOrDefault(nf => nf.Id == NutritionFact.Id);
            var indexToRefresh = Items.IndexOf(source);
            if (indexToRefresh != -1)
            {
                //Items.RemoveAt(indexToRefresh);
                //Items.Insert(indexToRefresh, NutritionFact);
                Items[indexToRefresh] = NutritionFact;
            }

            Close();
        }
        #endregion

        private ICommand GetSubmitCommand()
        {
            if (NutritionFact is null)
            {
                NutritionFact = new();

                return Add;
            }
            else
            {
                return Update;
            }
        }

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
}
