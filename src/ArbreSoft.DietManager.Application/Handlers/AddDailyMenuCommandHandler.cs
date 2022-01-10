using ArbreSoft.DietManager.Application.Commands;
using ArbreSoft.DietManager.Domain;
using ArbreSoft.DietManager.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ArbreSoft.DietManager.Application.Handlers
{
    public class AddDailyMenuCommandHandler : IRequestHandler<AddDailyMenuCommand, DailyMenu>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddDailyMenuCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<DailyMenu> Handle(AddDailyMenuCommand request, CancellationToken cancellationToken)
        {
            var dailyMenu = new DailyMenu();
            unitOfWork.Repository<DailyMenu>().Add(dailyMenu);
            await unitOfWork.SaveChangesAsync();

            return dailyMenu;
        }
    }
}
