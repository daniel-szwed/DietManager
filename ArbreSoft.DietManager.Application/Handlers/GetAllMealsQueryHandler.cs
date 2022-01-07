using ArbreSoft.DietManager.Application.Queries;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, IEnumerable<Meal>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetAllMealsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Meal>> Handle(GetAllMealsQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork
                .Repository<Meal>()
                .FromSqlRawAsync($"SELECT * FROM NUTRITIONFACTS WHERE TYPE = '{nameof(Meal)}'", nameof(Meal.Childrens));
        }
    }
}
