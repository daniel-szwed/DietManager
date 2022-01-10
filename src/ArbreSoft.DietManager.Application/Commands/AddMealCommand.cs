using ArbreSoft.DietManager.Domain;
using MediatR;
using System;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class AddMealCommand : IRequest<Meal>
    {
        public AddMealCommand(Guid dailyMenuId, Meal meal)
        {
            DailyMenuId = dailyMenuId;
            Meal = meal;
        }

        public Guid DailyMenuId { get; }
        public Meal Meal { get; }
    }
}
