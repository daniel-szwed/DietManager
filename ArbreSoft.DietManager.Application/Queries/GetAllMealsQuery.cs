using ArbreSoft.DietManager.Domain;
using MediatR;
using System.Collections.Generic;

namespace ArbreSoft.DietManager.Application.Queries
{
    public class GetAllMealsQuery : IRequest<IEnumerable<Meal>>
    {
    }
}
