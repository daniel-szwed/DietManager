using ArbreSoft.DietManager.Domain;
using MediatR;

namespace ArbreSoft.DietManager.Application.Commands
{
    public class AddDailyMenuCommand : IRequest<DailyMenu>
    {
    }
}
