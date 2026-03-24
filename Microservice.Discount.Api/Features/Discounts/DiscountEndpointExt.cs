using Asp.Versioning.Builder;
using Microservice.Discount.Api.Features.Discounts.CreateDiscount;
using Microservice.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace Microservice.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)//her endpoint kolay yonetılebılmek için buraya eklenecek
        {
            app.MapGroup("api/v{version:apiversion}/courses").WithTags("courses").WithApiVersionSet(apiVersionSet)
                .CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint();
        }
    }
}
