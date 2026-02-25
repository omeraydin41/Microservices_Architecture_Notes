using FluentValidation;

namespace Microservices.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandValidator:AbstractValidator<AddBasketItemCommand>
    {
        public AddBasketItemCommandValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId field not empty.");
            RuleFor(x => x.CourseName).NotEmpty().WithMessage("CourseName field not empty.");
            RuleFor(x => x.CoursePrice).GreaterThan(0).WithMessage
                ("{PropertyName} must be greater than zero");
            
        }


    }
}
