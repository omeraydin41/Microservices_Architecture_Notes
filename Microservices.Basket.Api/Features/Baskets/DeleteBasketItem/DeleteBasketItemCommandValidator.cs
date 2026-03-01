using FluentValidation;

namespace Microservices.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
    {
        public DeleteBasketItemCommandValidator() { RuleFor(x=>x.Id).NotEmpty().WithMessage("Course Id required");/*kurs ıd gerekli*/ }
    }
}
