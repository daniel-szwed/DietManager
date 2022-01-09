using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class RemoveMealCommandHandler : IRequestHandler<RemoveMealCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveMealCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveMealCommand request, CancellationToken cancellationToken)
        {
            var mealId = new DbParamerer("@Id", request.MealId.ToString().ToUpper());
            var meals = await unitOfWork.Repository<Meal>().FromSqlRawAsync("SELECT * FROM NUTRITIONFACTS WHERE Id = @Id", nameof(Meal.Childrens), mealId);
            var meal = meals.FirstOrDefault();
            unitOfWork.Repository<Meal>().Remove(meal);
            await unitOfWork.SaveChangesAsync();

            return new Unit();
        }
    }
}
