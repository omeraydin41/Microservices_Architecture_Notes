using MediatR;
using Microservices.Basket.Api.Const;
using Microservices.Basket.Api.Dto;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservices.Shared;
using NewMicroservices.Shared.Services;
using System.Net;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.DeleteBasketItem
{
    //db ye işlem için IDistributedCache kulllanılmalı
    public class DeleteBasketItemCommandHandler(IDistributedCache distributedCache,IIdentityServices identityServices) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            //silme işlemi için önde basket bulunmalı 

            Guid userId = identityServices.GetUserId;//user ıd alındı 

            //basket varmı kontrol edildi 
            var cacheKey = string.Format(BasketConst.BasketCacheKey, userId);
            var basketAsString = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

            //basket yoksa geriye hata donulmesi lazım 

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult.Error("basket not found",HttpStatusCode.NotFound);
            }
            //eğer basket varsa deserialize edilmesi lazım : 
            var currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);

            //silinecek olan basketı bulma işlemi 

            var basketItemToDelete=currentBasket!.BasketItems.FirstOrDefault(x=>x.Id==request.CourseId);
            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("basket ıtem not found",HttpStatusCode.NotFound);
            }
            
            currentBasket.BasketItems.Remove(basketItemToDelete);

            basketAsString=JsonSerializer.Serialize(currentBasket);

            await distributedCache.SetStringAsync(cacheKey,basketAsString,token:cancellationToken);
            
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
