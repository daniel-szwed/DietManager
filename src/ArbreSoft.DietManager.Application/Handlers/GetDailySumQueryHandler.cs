using ArbreSoft.DietManager.Application.Queries;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class GetDailySumQueryHandler : IRequestHandler<GetAllNutritionFactsQuery, IEnumerable<NutritionFact>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetDailySumQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NutritionFact>> Handle(GetAllNutritionFactsQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork
                .Repository<NutritionFact>()
                .FromSqlRawAsync($"SELECT * FROM NUTRITIONFACTS WHERE TYPE = '{nameof(NutritionFact)}'", string.Empty);
        }
    }
}
