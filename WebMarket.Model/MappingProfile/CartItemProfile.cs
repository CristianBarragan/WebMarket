using AutoMapper;
using WebMarket.Model.Domain;
using WebMarket.Model.Dto;

namespace WebMarket.Model.MappingProfile
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItemDto, CartItem>()
                .ForMember(d => d.OrderId, o => {
                    o.MapFrom(source => source.OrderId);
                })
                .ForMember(d => d.ProductId, o => {
                    o.MapFrom(source => source.ProductId);
                })
                .ForMember(d => d.Name, o => {
                    o.MapFrom(source => source.Name);
                })
                .ForMember(d => d.Quantity, o => {
                    o.MapFrom(source => source.Quantity);
                })
                .ForMember(d => d.Total, o => {
                    o.MapFrom(source => source.Total);
                })                
                .ReverseMap()
                .ForAllOtherMembers(o => o.UseDestinationValue());
        }
    }
}
