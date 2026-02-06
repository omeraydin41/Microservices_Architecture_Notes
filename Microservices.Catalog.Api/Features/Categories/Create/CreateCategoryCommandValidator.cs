using FluentValidation;

namespace Microservices.Catalog.Api.Features.Categories.Create
{

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>//kinin için validasyon yapacağız= CreateCategoryCommand
    {

        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("peroperty name it cant empty").
                Length(4, 25).WithMessage("must be between 4 and 25");
        }
    }
}
