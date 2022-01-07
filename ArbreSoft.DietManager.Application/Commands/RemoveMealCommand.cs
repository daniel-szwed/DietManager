using ArbreSoft.DietManager.Domain;
using MediatR;
using System;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class RemoveMealCommand : IRequest
    {
        public RemoveMealCommand(Guid mealId)
        {
            MealId = mealId;
        }

        public Guid MealId { get; }
    }
}
