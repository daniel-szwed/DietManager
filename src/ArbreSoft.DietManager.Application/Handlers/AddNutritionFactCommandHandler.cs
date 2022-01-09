using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class AddNutritionFactCommandHandler : IRequestHandler<AddNutritionFactCommand, NutritionFact>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddNutritionFactCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<NutritionFact> Handle(AddNutritionFactCommand request, CancellationToken cancellationToken)
        {
            unitOfWork.Repository<NutritionFact>().Add(request.NutritionFact);
            await unitOfWork.SaveChangesAsync();

            return request.NutritionFact;
        }
    }
}
