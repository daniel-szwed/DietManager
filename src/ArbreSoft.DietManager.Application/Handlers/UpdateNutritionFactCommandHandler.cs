using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class UpdateNutritionFactCommandHandler : IRequestHandler<UpdateNutritionFactCommand, NutritionFact>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateNutritionFactCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<NutritionFact> Handle(UpdateNutritionFactCommand request, CancellationToken cancellationToken)
        {
            var nutritionFactId = new DbParamerer("Id", request.NutritionFact.Id);
            var nutritionFacts = await unitOfWork.Repository<NutritionFact>().FromSqlRawAsync("SELECT * FROM NUTRITIONFACTS WHERE Id = @Id", string.Empty, nutritionFactId);

            var nutritionFact = nutritionFacts.FirstOrDefault();

            nutritionFact.Name = request.NutritionFact.Name;
            nutritionFact.KiloCalories = request.NutritionFact.KiloCalories;
            nutritionFact.Proteins = request.NutritionFact.Proteins;
            nutritionFact.TotalCarbohydreates = request.NutritionFact.TotalCarbohydreates;
            nutritionFact.Sugars = request.NutritionFact.Sugars;
            nutritionFact.TotalFats = request.NutritionFact.TotalFats;
            nutritionFact.SaturatedFats = request.NutritionFact.SaturatedFats;

            await unitOfWork.SaveChangesAsync();

            return nutritionFact;
        }
    }
}
