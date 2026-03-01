using Microservices.Basket.Api.Dto;
using NewMicroservices.Shared;

namespace Microservices.Basket.Api.Features.Baskets.GetBasket
{
    public record GetBasketQuery:IRequestByServiceResult<BasketDto>;
   
}
