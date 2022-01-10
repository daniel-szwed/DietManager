using AutoMapper;

namespace ArbreSoft.DietManager.Presentation.Profiles
{
    public class NutritionFactProfile : Profile
    {
        public NutritionFactProfile()
        {
            CreateMap<Domain.NutritionFact, Models.NutritionFact>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.Kcal, options => options.MapFrom(src => src.KiloCalories))
                .ForMember(dest => dest.Protein, options => options.MapFrom(src => src.Proteins))
                .ForMember(dest => dest.Carbohydrates, options => options.MapFrom(src => src.TotalCarbohydreates))
                .ForMember(dest => dest.Sugar, options => options.MapFrom(src => src.Sugars))
                .ForMember(dest => dest.Fat, options => options.MapFrom(src => src.TotalFats))
                .ForMember(dest => dest.Saturated, options => options.MapFrom(src => src.SaturatedFats));

            CreateMap<Models.NutritionFact, Domain.NutritionFact>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.KiloCalories, options => options.MapFrom(src => src.Kcal))
                .ForMember(dest => dest.Proteins, options => options.MapFrom(src => src.Protein))
                .ForMember(dest => dest.TotalCarbohydreates, options => options.MapFrom(src => src.Carbohydrates))
                .ForMember(dest => dest.Sugars, options => options.MapFrom(src => src.Sugar))
                .ForMember(dest => dest.TotalFats, options => options.MapFrom(src => src.Fat))
                .ForMember(dest => dest.SaturatedFats, options => options.MapFrom(src => src.Saturated));

            CreateMap<Models.Nutritionix, Models.NutritionFact>()
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.food_name))
                .ForMember(dest => dest.Kcal, options => options.MapFrom(src => src.nf_calories))
                .ForMember(dest => dest.Protein, options => options.MapFrom(src => src.nf_protein))
                .ForMember(dest => dest.Carbohydrates, options => options.MapFrom(src => src.nf_total_carbohydreate))
                .ForMember(dest => dest.Sugar, options => options.MapFrom(src => src.nf_sugar))
                .ForMember(dest => dest.Fat, options => options.MapFrom(src => src.nf_total_fat))
                .ForMember(dest => dest.Saturated, options => options.MapFrom(src => src.nf_saturated_fat));
        }
    }
}
