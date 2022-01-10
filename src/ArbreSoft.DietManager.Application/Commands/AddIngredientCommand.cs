using ArbreSoft.DietManager.Domain;
using MediatR;
using System;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class AddIngredientCommand : IRequest<Ingredient>
    {
        public AddIngredientCommand(Guid mealId, Ingredient ingredient)
        {
            MealId = mealId;
            Ingredient = ingredient;
        }

        public Guid MealId { get; }
        public Ingredient Ingredient { get; }
    }
}
