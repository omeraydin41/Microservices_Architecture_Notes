using AutoMapper;
using Microservices.Basket.Api.Dto;

namespace Microservices.Basket.Api.Features.Baskets
{
    public class BasketMapping : Profile//basketi ilgilendiren tüm maplama işlemleri burdan yapıllır 
    {
        public BasketMapping()
        {
            CreateMap<BasketDto,Data.Basket>().ReverseMap();
            CreateMap<BasketItemDto, Data.BasketItem>().ReverseMap();
        }

    }
}
