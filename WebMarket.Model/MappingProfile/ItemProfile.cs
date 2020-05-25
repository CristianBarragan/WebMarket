using AutoMapper;
using WebMarket.Model.Domain;
using WebMarket.Model.Dto;

namespace WebMarket.Model.MappingProfile
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemDto, Item>()
                .ForMember(d => d.ItemId, o => {
                    o.MapFrom(source => source.ItemId);
                })
                .ForMember(d => d.Name, o => {
                    o.MapFrom(source => source.Name);
                })
                .ForMember(d => d.CategoryIds, o => {
                    o.MapFrom(source => source.CategoryIds);
                })
                .ForMember(d => d.CategoryNames, o => {
                    o.MapFrom(source => source.CategoryNames);
                })
                .ForMember(d => d.Quantity, o => {
                    o.MapFrom(source => source.Quantity);
                })
                .ForMember(d => d.Price, o => {
                    o.MapFrom(source => source.Price);

                })
                .ReverseMap()
                .ForAllOtherMembers(o => o.UseDestinationValue());
        }
    }
}
