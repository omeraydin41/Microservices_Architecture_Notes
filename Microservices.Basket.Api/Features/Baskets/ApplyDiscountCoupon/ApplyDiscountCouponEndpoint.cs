using MediatR;
using Microservices.Basket.Api.Features.Baskets.AddBasketItem;
using NewMicroservices.Shared.Extansions;
using NewMicroservices.Shared.Filters;

namespace Microservices.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {

        public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/apply-discount-coupon",//put isteği // var olan basketu guncellem işlemi yapıyoruz
                async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())// commandı gönder 
                .WithName("ApplyDiscountCoupon")//swagger adı 
                .MapToApiVersion(1, 0)//kullanılacak versiyon
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponValidator>>();//valide edilecek class
            return group;

        }
    }
}
