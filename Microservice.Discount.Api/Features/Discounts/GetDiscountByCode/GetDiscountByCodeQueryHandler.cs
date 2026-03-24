

using NewMicroservice.Discount.Api.Repositories;
using NewMicroservices.Shared.Services;

namespace Microservice.Discount.Api.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext context,IIdentityServices ıdentityServices ) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            //db de varmı yokmu kontrol edilecek db nesnesi üzerinden
            var hasDiscount = await context.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken :cancellationToken);

            if (hasDiscount == null)
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("discount not fount",HttpStatusCode.NotFound);
            }

            //eğer var ise kontrol edilecek 

            if (hasDiscount.Expired < DateTime.Now)//indirim tarıhı geçerlimi 
            {
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("discount is Expired(günü geçmiş)",HttpStatusCode.BadRequest);
            }
            //tum durumlar dışında kod geçerli ise
            return ServiceResult<GetDiscountByCodeQueryResponse>
           .SuccessAsOk(new GetDiscountByCodeQueryResponse( hasDiscount.Code,hasDiscount.Rate));
            

        }
    }
}
