using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class AddMealCommandHandler : IRequestHandler<AddMealCommand, Meal>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddMealCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Meal> Handle(AddMealCommand request, CancellationToken cancellationToken)
        {
            var dailyMenuId = new DbParamerer("Id", request.DailyMenuId);
            var dailyMenus = await unitOfWork.Repository<DailyMenu>()
                .FromSqlRawAsync("SELECT * FROM NUTRITIONFACTS WHERE Id = @Id", nameof(NutritionFact.Childrens), dailyMenuId);
            dailyMenus.First().Childrens.Add(request.Meal);
            await unitOfWork.SaveChangesAsync();

            return request.Meal;
        }
    }
}
