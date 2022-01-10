using ArbreSoft.DietManager.Domain;
using MediatR;
using System;

namespace ArbreSoft.DietManager.Application.Queries
{
    public class GetDailySumQuery : IRequest<NutritionFact>
    {
        public GetDailySumQuery(Guid dailyMenuId)
        {
            DailyMenuId = dailyMenuId;
        }

        public Guid DailyMenuId { get; }
    }
}
