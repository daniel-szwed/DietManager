using ArbreSoft.DietManager.Domain;
using MediatR;
using System;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class RemoveIngredientCommand : IRequest
    {
        public RemoveIngredientCommand(Guid ingredientId)
        {
            IngredientId = ingredientId;
        }

        public Guid IngredientId { get; }
    }
}
