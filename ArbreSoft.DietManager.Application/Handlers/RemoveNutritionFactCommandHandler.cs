using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class RemoveNutritionFactCommandHandler : IRequestHandler<RemoveNutritionFactCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveNutritionFactCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveNutritionFactCommand request, CancellationToken cancellationToken)
        {
            var nutritionFactId = new DbParamerer("Id", request.NutritionFact.Id.ToString().ToUpper());
            await unitOfWork.Repository<NutritionFact>().FromSqlRawAsync("DELETE FROM NUTRITIONFACT WHERE Id = @Id", string.Empty, nutritionFactId);

            return new Unit();
        }
    }
}
