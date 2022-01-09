using ArbreSoft.DietManager.Domain;
using MediatR;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class AddMealCommand : IRequest<Meal>
    {
        public AddMealCommand(Meal meal)
        {
            Meal = meal;
        }

        public Meal Meal { get; }
    }
}
