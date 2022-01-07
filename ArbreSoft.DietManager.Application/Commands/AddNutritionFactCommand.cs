using ArbreSoft.DietManager.Domain;
using MediatR;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class AddNutritionFactCommand : IRequest<NutritionFact>
    {
        public AddNutritionFactCommand(NutritionFact nutritionFact)
        {
            NutritionFact = nutritionFact;
        }

        public NutritionFact NutritionFact { get; }
    }
}
