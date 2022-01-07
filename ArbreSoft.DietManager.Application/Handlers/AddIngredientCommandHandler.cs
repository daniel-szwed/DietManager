using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class AddIngredientCommandHandler : IRequestHandler<AddIngredientCommand, Meal>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddIngredientCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Meal> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            var mealReposotory = unitOfWork.Repository<Meal>();
            var mealId = new DbParamerer("Id", request.MealId);

            var meal = (await mealReposotory.FromSqlRawAsync("SELECT * FROM NUTRITIONFACTS WHERE Id = @Id", nameof(Meal.Childrens), mealId)).FirstOrDefault();
            meal.Childrens.Add(request.Ingredient);

            await unitOfWork.SaveChangesAsync();

            return meal;
        }
    }
}
