namespace Microservices.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>//ilgili classı validate ettik gneric olarak <T>
    {

       public CreateCourseCommandValidator()
       {//COMMAND CLASSINDAKI PARAMETRELERI DENETLEYİCİ KURRALLAR YAZDIK 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");


            //zorunlu olmamasının sebebei öğretmen resım yuklmezse bizde defoult resım yuklerız
            //RuleFor(x => x.PictureUrl).NotEmpty().WithMessage("PictureUrl is required")
            //    .MaximumLength(2000).WithMessage("PictureUrl must not exceed 2000 characters");

            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
                

        }
    }
}
