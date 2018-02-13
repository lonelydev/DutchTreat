using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Data
{
  /// <summary>
  /// Profile is a container around any mappings you want to setup. 
  /// 
  /// </summary>
  public class DutchMappingProfile : Profile
  {
    public DutchMappingProfile()
    {
      CreateMap<Order, OrderViewModel>()
        .ForMember(ov => ov.OrderId,
        map => map.MapFrom(o => o.Id))
        .ReverseMap();

      CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
    }
  }
}
