using MediatR;
using Microservices.Basket.Api.Const;
using Microservices.Basket.Api.Dto;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservices.Shared;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributeCache) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
           Guid userId=Guid.NewGuid();//normalde userId yi token dan alırız ama şimdilik böyle yapalım
            var basketCachKey= string.Format(BasketConst.BasketCacheKey,userId);
            // "basket:{0}" 0 alan ye userId gelecek
            //string.format : metinleri dinamik, düzenli ve okunabilir bir şekilde birleştirmek için kullanılan bir yöntemdir.


            //basket varmı yokmu kontrol edilir

            var basketAsString = await distributeCache.GetStringAsync(basketCachKey, cancellationToken);

            //eğer basket yoksa yenı bır basket oluşturulur ve cache'e eklenır

            BasketDto? currentBasket;
            var newBasketItem = 
                new BasketItemDto(request.CourseId,request.CourseName,request.ImageUrl,
                request.CoursePrice,null);//AddBasketItemCommand clasından gelen alanları kullanarak yeni bir BasketItemDto oluşturduk.

            if (string.IsNullOrEmpty(basketAsString))//eğer basket yoksa yenı bır basket oluşturulur ve cache'e eklenır
            {
                currentBasket = new BasketDto(userId, [newBasketItem]);
            }
            else//eğer basket varsa mevcut basket alınır ve yeni item eklenir sonra cache güncellenir
            {
                currentBasket = JsonSerializer.Deserialize<BasketDto>(basketAsString);//bu işlemle cache'den gelen string'i BasketDto'ya dönüştürdük

                //aynı kursu birden fazla sepete eklememek için kontrol yapalım

                var existingBasketItem = currentBasket.BasketItems.FirstOrDefault(x => x.Id == request.CourseId);
                //currentBasket : guncel basket uzerınden kontrol yapıldı 

                //eğer kurs sepetteyse silinip yenısı eklenmelı 

                if ( existingBasketItem is not null)
                {
                    currentBasket.BasketItems.Remove(existingBasketItem);//var olan sılındı 

                    currentBasket.BasketItems.Add(newBasketItem);//yeni item eklendi
                }
                //eğer boyle bır durum yoksa yani kurs sepette yoksa direkt yeni item eklenir
                else
                {
                    currentBasket.BasketItems.Add(newBasketItem);//yeni item eklendi
                }


            }
                basketAsString= JsonSerializer.Serialize(currentBasket);//guncel basket stringe donusturuldu 

                await distributeCache.SetStringAsync(basketCachKey, basketAsString, cancellationToken);//cache guncellendi

                return ServiceResult.SuccessAsNoContent();
        }

    }
}
