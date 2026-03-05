using Microservices.Basket.Api.Const;
using Microsoft.Extensions.Caching.Distributed;
using NewMicroservices.Shared.Services;
using System.Text.Json;
using System.Threading;

namespace Microservices.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityServices identityServices,IDistributedCache distributedCache)
    {
        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityServices.GetUserId);

        public  Task<string?> GetBasketFromChace(CancellationToken cancellationToken)
        {
             
            return  distributedCache.GetStringAsync(GetCacheKey(), cancellationToken);
        }
        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);//guncel basket stringe donusturuldu 
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);//cache guncellendi

        }
    }
}
