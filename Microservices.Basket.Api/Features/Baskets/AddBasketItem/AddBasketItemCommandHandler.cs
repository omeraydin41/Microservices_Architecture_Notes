using MediatR;
using Microservices.Basket.Api.Const;
using Microservices.Basket.Api.Data;
using Microservices.Basket.Api.Dto;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservices.Shared;
using NewMicroservices.Shared.Services;
using System.Text.Json;

namespace Microservices.Basket.Api.Features.Baskets.AddBasketItem
{
    public class AddBasketItemCommandHandler(IDistributedCache distributeCache,IIdentityServices identityServices) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {

            // TODO : change userId 

            Guid userId = identityServices.GetUserId;
            var basketCachKey= string.Format(BasketConst.BasketCacheKey,userId);
            // "basket:{0}" 0 alan ye userId gelecek
            //string.format : metinleri dinamik, düzenli ve okunabilir bir şekilde birleştirmek için kullanılan bir yöntemdir.


            //basket varmı yokmu kontrol edilir

            var basketAsString = await distributeCache.GetStringAsync(basketCachKey, cancellationToken);

            //eğer basket yoksa yenı bır basket oluşturulur ve cache'e eklenır

            Data.Basket currentBasket;
            var newBasketItem = 
                new BasketItem(request.CourseId,request.CourseName,request.ImageUrl,
                request.CoursePrice,null);//AddBasketItemCommand clasından gelen alanları kullanarak yeni bir BasketItemDto oluşturduk.

            if (string.IsNullOrEmpty(basketAsString))//eğer basket yoksa yenı bır basket oluşturulur ve cache'e eklenır
            {
                currentBasket = new Data.Basket(userId, [newBasketItem]);
                await CreateCacheAsync(currentBasket, basketCachKey, cancellationToken);//cache guncellendi
                return ServiceResult.SuccessAsNoContent();

            }
            //eğer basket varsa mevcut basket alınır ve yeni item eklenir sonra cache güncellenir
            
                currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);//bu işlemle cache'den gelen string'i BasketDto'ya dönüştürdük

                //aynı kursu birden fazla sepete eklememek için kontrol yapalım

                var existingBasketItem = currentBasket.Items.FirstOrDefault(x => x.Id == request.CourseId);
                //currentBasket : guncel basket uzerınden kontrol yapıldı 

                //eğer kurs sepetteyse silinip yenısı eklenmelı 

                if ( existingBasketItem is not null)
                {
                    currentBasket.Items.Remove(existingBasketItem);//var olan sılındı 

                }
                currentBasket.Items.Add(newBasketItem);//yeni item eklendi
                                                       //eğer boyle bır durum yoksa yani kurs sepette yoksa direkt yeni item eklenir

            //mevcut sepette ındırım varsa bunu uygulatmama lazım 

            currentBasket.ApplyAviableDiscount();//mevcut sepette ındırım varsa ygulayan yardımcı method


            await CreateCacheAsync(currentBasket,basketCachKey, cancellationToken);//cache guncellendi

            return ServiceResult.SuccessAsNoContent();
        }

        private async Task CreateCacheAsync(Data.Basket basket,string basketCachKey, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);//guncel basket stringe donusturuldu 
            await distributeCache.SetStringAsync(basketCachKey, basketAsString, cancellationToken);//cache guncellendi

        }
    }
}
