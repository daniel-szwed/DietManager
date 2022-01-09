using ArbreSoft.DietManager.Domain;
using MediatR;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class RemoveNutritionFactCommand : IRequest
    {
        public RemoveNutritionFactCommand(NutritionFact nutritionFact)
        {
            NutritionFact = nutritionFact;
        }

        public NutritionFact NutritionFact { get; }
    }
}
