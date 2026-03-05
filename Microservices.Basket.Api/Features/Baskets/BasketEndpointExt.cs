using Asp.Versioning.Builder;
using Microservices.Basket.Api.Features.Baskets.AddBasketItem;
using Microservices.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using Microservices.Basket.Api.Features.Baskets.DeleteBasketItem;
using Microservices.Basket.Api.Features.Baskets.GetBasket;
using Microservices.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace Microservices.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)//apiVersionSet : versiyonlama için gerekli
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")

             .WithApiVersionSet(apiVersionSet)//versiyonlama işlemi için tüm endpointlere burdan eklendi

             .AddBasketGroupItemEndpoint()

             .DeleteBasketGroupItemEndpoint()

             .GetBasketGroupItemEndpoint()

             .ApplyDiscountCouponGroupItemEndpoint()

             .RemoveDiscountCouponGroupItemEndpoint();

        }
    }
}
