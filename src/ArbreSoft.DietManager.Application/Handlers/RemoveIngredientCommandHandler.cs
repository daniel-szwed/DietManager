using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class RemoveIngredientCommandHandler : IRequestHandler<RemoveIngredientCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public RemoveIngredientCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientId = new DbParamerer("IngredientId", request.IngredientId.ToString().ToUpper());
            var reposiotory = unitOfWork.Repository<Ingredient>().ExecuteNonQuery("DELETE FROM NUTRITIONFACTS WHERE ID = @IngredientId", ingredientId);

            return new Unit();
        }
    }
}
