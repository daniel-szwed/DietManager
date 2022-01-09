using ArbreSoft.DietManager.Domain;
using MediatR;
using System.Collections.Generic;

namespace ArbreSoft.DietManager.Application.Queries
{
    public class GetAllNutritionFactsQuery : IRequest<IEnumerable<NutritionFact>>
    {
    }
}
