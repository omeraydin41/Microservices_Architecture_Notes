using MediatR;
using Microservices.Basket.Api.Features.Baskets.AddBasketItem;
using Microsoft.AspNetCore.Mvc;
using NewMicroservices.Shared.Extansions;
using NewMicroservices.Shared.Filters;

namespace Microservices.Basket.Api.Features.Baskets.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/item/{id:guid}", async//DELETE isteği yolladık 
                (Guid id, IMediator mediator)
                => (await mediator.Send(new DeleteBasketItemCommand(id)))//swaggerden ID DEĞİŞKENİ YOLLAMAK İÇİN 
                .ToGenericResult()//sahared içi donüş tipini belirlyen classımız
            )
                .WithName("Delete basket item")//sqagger adını verdik 
                .MapToApiVersion(1, 0);//kullandığı versiyonu verdik                     
            return group;
        }
            
    }
}
