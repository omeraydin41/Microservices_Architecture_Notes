using MediatR;
using Microservices.Basket.Api.Const;
using Microservices.Basket.Api.Dto;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservices.Shared;
using NewMicroservices.Shared.Services;
using System.Net;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(IIdentityServices identityServices,IDistributedCache distributedCache) 
        : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>//handler edileceek class ve gerşye donş turü
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken) 
        {
            //indirim için öncesepettekı datayı çektik
            var cacheKey = string.Format(BasketConst.BasketCacheKey, identityServices.GetUserId);
            var basketAsJson=await distributedCache.GetStringAsync(cacheKey,token:cancellationToken);

            //data yoksa :
            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("basket not found", HttpStatusCode.NotFound);
            }
            //bunu çevirme varkti:
            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(cacheKey, basketAsJson, token:cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
