using MediatR;
using System;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class UpdateIngredientWeightCommand : IRequest
    {
        public UpdateIngredientWeightCommand(Guid ingredientId, int delta)
        {
            IngredientId = ingredientId;
            Delta = delta;
        }

        public Guid IngredientId { get; }
        public int Delta { get; }
    }
}
