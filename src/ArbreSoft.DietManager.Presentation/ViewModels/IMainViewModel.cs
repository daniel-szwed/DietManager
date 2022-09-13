using MediatR;
using System.Windows.Input;

namespace ArbreSoft.DietManager.Presentation.ViewModels
{
    public interface IMainViewModel
    {
        public IMediator Mediator { get; }
        ICommand BrowseNutritionFacts { get; }
    }
}
