using AutoMapper;
using WebMarket.Model.Data;
using WebMarket.Model.Dto;

namespace WebMarket.Model.MappingProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>()
                .ForMember(d => d.CategoryId, o => {
                    o.MapFrom(source => source.CategoryId);
                })
                .ForMember(d => d.Name, o => {
                    o.MapFrom(source => source.Name);
                })
                .ReverseMap()
                .ForAllOtherMembers(o => o.UseDestinationValue());
        }
    }
}
