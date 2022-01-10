using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class UpdateIngredientWeightCommandHandler : IRequestHandler<UpdateIngredientWeightCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateIngredientWeightCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateIngredientWeightCommand request, CancellationToken cancellationToken)
        {
            var ingredientId = new DbParamerer("Id", request.IngredientId);
            var ingredients = await unitOfWork.Repository<Ingredient>().FromSqlRawAsync(
                "SELECT * FROM NUTRITIONFACTS WHERE Id = @Id",
                string.Empty,
                ingredientId);

            ingredients.First().Weight += request.Delta;
            await unitOfWork.SaveChangesAsync();

            return new Unit();
        }
    }
}
