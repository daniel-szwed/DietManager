using AutoMapper;

namespace ArbreSoft.DietManager.Presentation.Profiles
{
    public class DailyMenuProfile : Profile
    {
        public DailyMenuProfile()
        {
            CreateMap<Domain.DailyMenu, Models.DailyMenu>();
            CreateMap<Models.DailyMenu, Domain.DailyMenu>();
        }
    }
}
