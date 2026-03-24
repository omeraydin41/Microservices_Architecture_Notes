using Microservice.Discount.Api.Features.Discounts.CreateDiscount;

namespace Microservice.Discount.Api.Features.Discounts
{
    public class DiscountMapping:Profile
    {
       public DiscountMapping()
        {
            CreateMap<CreateDiscountCommand,Discount>();
        }
    }
}
