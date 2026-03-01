using MediatR;
using NewMicroservices.Shared.Extansions;

namespace Microservices.Basket.Api.Features.Baskets.GetBasket
{
    public static class GetBasketQueryEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async//endpoint adı 
                (IMediator mediator)
                => (await mediator.Send(new GetBasketQuery()))//swaggerden ID DEĞİŞKENİ YOLLAMAK İÇİN 
                .ToGenericResult()//sahared içi donüş tipini belirlyen classımız
            )
                .WithName("Get Basket item")//sqagger adını verdik 
                .MapToApiVersion(1, 0);//kullandığı versiyonu verdik                     
            return group;
        }
    }
}
