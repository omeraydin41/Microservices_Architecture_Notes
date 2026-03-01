using MediatR;
using Microservices.Basket.Api.Const;
using Microservices.Basket.Api.Dto;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservices.Shared;
using NewMicroservices.Shared.Services;
using System.Net;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.GetBasket
{
    public class GetBasketQueryHandler(
        IDistributedCache distributedCache,//db de işlem yapmak için 
        IIdentityServices identityServices): //kullanıcının ID sını getirir
        IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>//GetBasketQuery alıp geriye ServiceResult turunde BasketDto donuyoruz 
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            //arama işlemi yapıldı             
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityServices.GetUserId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey,token:cancellationToken);

            //eğer null ise 
            if (string.IsNullOrEmpty( basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found",HttpStatusCode.NotFound);
            }
            
            //eğer null değilde data var ise data geriye donulur.
            var basket =JsonSerializer.Deserialize<BasketDto>(basketAsString)!;
            return  ServiceResult<BasketDto>.SuccessAsOk(basket);



        }
    }
}
