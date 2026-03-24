namespace Microservice.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryValidator:AbstractValidator<GetDiscountByCodeQuery>//valide edilecek classı verdik 
    {
        public GetDiscountByCodeQueryValidator()//contructor(yapıcı method) // nesne alındığında otomatık olarak çalışacak olan yapı 
        {
            RuleFor(x=>x.Code).NotEmpty().WithMessage("Code is not empty");
        
        
        }
    }
}
