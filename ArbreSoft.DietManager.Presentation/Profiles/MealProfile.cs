using ArbreSoft.DietManager.Domain;
using AutoMapper;

namespace ArbreSoft.DietManager.Presentation.Profiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<ArbreSoft.DietManager.Domain.Meal, ArbreSoft.DietManager.Presentation.Models.Meal>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Ingregients, options => options.MapFrom(src => src.Childrens));


            CreateMap<ArbreSoft.DietManager.Presentation.Models.Meal, ArbreSoft.DietManager.Domain.Meal>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Childrens, options => options.MapFrom(src => src.Ingregients));
        }
    }
}
