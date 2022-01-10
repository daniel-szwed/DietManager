using AutoMapper;

namespace ArbreSoft.DietManager.Presentation.Profiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<Domain.Meal, Models.Meal>();
            CreateMap<Models.Meal, Domain.Meal>();
        }
    }
}
