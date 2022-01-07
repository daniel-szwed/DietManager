using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class RemoveIngredientCommandHandler : IRequestHandler<RemoveIngredientCommand, Meal>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveIngredientCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Meal> Handle(RemoveIngredientCommand request, CancellationToken cancellationToken)
        {
            var mealId = new DbParamerer("MealId", request.MealId.ToString().ToUpper());
            var ingredientId = new DbParamerer("IngredientId", request.IngredientId.ToString().ToUpper());
            var reposiotory = unitOfWork.Repository<Meal>();
            await reposiotory.ExecuteNonQuery($@"DELETE FROM NUTRITIONFACTS WHERE ID = @IngredientId", ingredientId);
            var meals = await reposiotory.FromSqlRawAsync($"SELECT * FROM NUTRITIONFACTS WHERE ID = @MealId", nameof(Meal.Childrens), mealId);

            return meals.FirstOrDefault();
        }
    }
}
