using ArbreSoft.DietManager.Application.Queries;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class GetAllDailyMenuQueryHandler : IRequestHandler<GetAllDailyMenuQuery, IEnumerable<DailyMenu>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllDailyMenuQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DailyMenu>> Handle(GetAllDailyMenuQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork
                .Repository<DailyMenu>()
                .FromSqlRawAsync(
                    $"SELECT * FROM NUTRITIONFACTS WHERE TYPE = '{nameof(DailyMenu)}'", 
                    $"{nameof(NutritionFact.Childrens)}.{nameof(NutritionFact.Childrens)}");
        }
    }
}
