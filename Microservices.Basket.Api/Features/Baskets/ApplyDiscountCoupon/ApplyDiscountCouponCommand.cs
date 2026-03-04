using NewMicroservices.Shared;

namespace Microservices.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public record ApplyDiscountCouponCommand(string Coupon,float DiscountRate):IRequestByServiceResult;
   
}
