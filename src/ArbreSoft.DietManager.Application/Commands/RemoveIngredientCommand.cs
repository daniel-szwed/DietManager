using ArbreSoft.DietManager.Domain;
using MediatR;
using System;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class RemoveIngredientCommand : IRequest<Meal>
    {
        public RemoveIngredientCommand(Guid mealId, Guid ingredientId)
        {
            MealId = mealId;
            IngredientId = ingredientId;
        }

        public Guid MealId { get; }
        public Guid IngredientId { get; }
    }
}
