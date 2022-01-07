using ArbreSoft.DietManager.Domain;
using MediatR;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class UpdateNutritionFactCommand : IRequest<NutritionFact>
    {
        public UpdateNutritionFactCommand(NutritionFact nutritionFact)
        {
            NutritionFact = nutritionFact;
        }

        public NutritionFact NutritionFact { get; }
    }
}
