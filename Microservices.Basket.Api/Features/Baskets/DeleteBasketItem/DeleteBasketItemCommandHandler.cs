using MediatR;
using NewMicroservices.Shared;
using System.Net;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.DeleteBasketItem
{
    //db ye işlem için IDistributedCache kulllanılmalı
    public class DeleteBasketItemCommandHandler(BasketService basketService) : 
        IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
            //silme işlemi için önde basket bulunmalı 


            //basket varmı kontrol edildi 

            var basketAsJson =await  basketService.GetBasketFromChace(cancellationToken);

            //basket yoksa geriye hata donulmesi lazım 

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("basket not found",HttpStatusCode.NotFound);
            }
            //eğer basket varsa deserialize edilmesi lazım : 
            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            //silinecek olan basketı bulma işlemi 

            var basketItemToDelete=currentBasket!.Items.FirstOrDefault(x=>x.Id==request.Id);
            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("basket ıtem not found",HttpStatusCode.NotFound);
            }
            
            currentBasket.Items.Remove(basketItemToDelete);

            basketAsJson=JsonSerializer.Serialize(currentBasket);


            await basketService.CreateBasketCacheAsync(currentBasket, cancellationToken);
            
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
