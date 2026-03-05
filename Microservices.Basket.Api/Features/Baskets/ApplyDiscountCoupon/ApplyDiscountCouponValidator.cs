using FluentValidation;

namespace Microservices.Basket.Api.Features.Baskets.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponValidator: AbstractValidator<ApplyDiscountCouponCommand>//hangı classıın valide edileceği 
    {
        public ApplyDiscountCouponValidator()
        {
            RuleFor(x=>x.Coupon).NotEmpty().WithMessage(
                "(property - name) is required");
            RuleFor(x => x.DiscountRate).GreaterThan(0).WithMessage(
                "(property - name) must be grader than zero");

        }
    }
}
