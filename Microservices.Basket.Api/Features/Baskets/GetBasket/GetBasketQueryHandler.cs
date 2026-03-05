using AutoMapper;
using MediatR;
using Microservices.Basket.Api.Dto;
using NewMicroservices.Shared;
using System.Net;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(       
        IMapper mapper,BasketService basketService): //kullanıcının ID sını getirir
        IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>//GetBasketQuery alıp geriye ServiceResult turunde BasketDto donuyoruz 
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //arama işlemi yapıldı             
           ;
            var basketAsString = await basketService.GetBasketFromChace(cancellationToken);

            //eğer null ise 
            if (string.IsNullOrEmpty( basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found",HttpStatusCode.NotFound);
            }
            
            //eğer null değilde data var ise data geriye donulur.
            var basket =JsonSerializer.Deserialize<Data.Basket>(basketAsString)!;

            var basketDto=mapper.Map<BasketDto>(basket);

            return  ServiceResult<BasketDto>.SuccessAsOk(basketDto);
        }
    }
}
