using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class AddIngredientCommandHandler : IRequestHandler<AddIngredientCommand, Ingredient>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddIngredientCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Ingredient> Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            var mealReposotory = unitOfWork.Repository<Meal>();
            var mealId = new DbParamerer("Id", request.MealId);
            var meals = await mealReposotory.FromSqlRawAsync("SELECT * FROM NUTRITIONFACTS WHERE Id = @Id", nameof(NutritionFact.Childrens), mealId);
            meals.First().Childrens.Add(request.Ingredient);

            await unitOfWork.SaveChangesAsync();

            return request.Ingredient;
        }
    }
}
