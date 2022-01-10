using ArbreSoft.DietManager.Domain;
using MediatR;
using System.Collections.Generic;

namespace ArbreSoft.DietManager.Application.Queries
{
    public class GetAllDailyMenuQuery : IRequest<IEnumerable<DailyMenu>>
    {
    }
}
