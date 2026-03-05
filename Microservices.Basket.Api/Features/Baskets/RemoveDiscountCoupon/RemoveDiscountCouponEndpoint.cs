using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservices.Shared;
using NewMicroservices.Shared.Extansions;
using NewMicroservices.Shared.Services;
using System.Net;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.RemoveDiscountCoupon
{

    public record RemoveDiscountCouponCommand:IRequestByServiceResult;

    public class RemoveDiscountCouponCommandHandler(IIdentityServices identityServices,IDistributedCache distributedCache,BasketService basketService) : 
                 IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            //var cacheKey = string.Format(BasketConst.BasketCacheKey,identityServices.GetUserId);
            var basketAsJson = await basketService.GetBasketFromChace(cancellationToken);
                //= await distributedCache.GetStringAsync(cacheKey,token:cancellationToken);

            //basket varmı yokmu onu kontrol etmeliyiz
            if (string.IsNullOrEmpty(basketAsJson))//Clean codeye gore önce olumsuz : basket datası yoksa 
            {
                return ServiceResult.Error("basket not fount", HttpStatusCode.NotFound);
            }
            //basket datası varsa alınması 

            var basket =JsonSerializer.Deserialize<Data.Basket>(basketAsJson);//basket Deserialize edildi.

            basket!.ClearDiscount();//basketi temizle //<Data.Basket uzerınden gelen yardımcı method

            //temızlenen basketın serialize edilerek  kaydedilmesi cache uzerine
            basketAsJson = JsonSerializer.Serialize(basket);
            await basketService.CreateBasketCacheAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/remove-discount-coupon",//silme isteği yolladık
                async ( IMediator mediator) =>
                       (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult())
                .WithName("RemoveDiscountCoupon")
                .MapToApiVersion(1, 0);
            return group;               
        }
    }
}
