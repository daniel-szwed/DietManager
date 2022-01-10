using ArbreSoft.DietManager.Application.Queries;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class GetTotalNutritionFactQueryHandler : IRequestHandler<GetDailySumQuery, NutritionFact>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetTotalNutritionFactQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<NutritionFact> Handle(GetDailySumQuery request, CancellationToken cancellationToken)
        {
            var dailyMenuId = new DbParamerer("Id", request.DailyMenuId);
            var dailyMenus = await unitOfWork.Repository<DailyMenu>().FromSqlRawAsync(
                "SELECT * FROM NUTRITIONFACTS WHERE Id = @Id",
                $"{nameof(NutritionFact.Childrens)}.{nameof(NutritionFact.Childrens)}",
                dailyMenuId);

            return dailyMenus.First().Sum().ToFixed(1);
        }
    }
}
