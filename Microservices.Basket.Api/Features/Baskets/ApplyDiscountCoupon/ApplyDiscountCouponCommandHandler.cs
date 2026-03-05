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
    public class ApplyDiscountCouponCommandHandler(IDistributedCache distributedCache,BasketService basketService) 
        : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>//handler edileceek class ve gerşye donş turü
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken) 
        {
            //indirim için öncesepettekı datayı çektik

            var basketAsJson =await basketService.GetBasketFromChace(cancellationToken);

            //data yoksa :
            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult<BasketDto>.Error("basket not found", HttpStatusCode.NotFound);
            }
            //bunu çevirme varkti:
            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            if (!basket.Items.Any())//içind bir data yok ıse
            {
                return ServiceResult<BasketDto>.Error("basket item not found", HttpStatusCode.NotFound);
            }

            basket.ApplyNewDiscount(request.Coupon, request.DiscountRate);

            basketAsJson = JsonSerializer.Serialize(basket);

            await basketService.CreateBasketCacheAsync(basket,cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
