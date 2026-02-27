using MediatR;
using NewMicroservices.Shared.Extansions;
using NewMicroservices.Shared.Filters;

namespace Microservices.Basket.Api.Features.Baskets.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            //http:localhost:5000/api/categories den sonra / alanı gelır ve yonlendirir
            group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
            (await mediator.Send(command)).ToGenericResult()).
            
            WithName("AddBasketItem")//ToResult methodumuzn adı donuş olarak belirlemdi
                                                                                        // "/" anlamı gurupları kontrol eden CategoryEndpointExt classındaki "api/categories" alanına denk 


            //hangi versiyonun kullanılacağını belirmek için 1.0 mı 2.0 mı 
            .MapToApiVersion(1, 0)//major ve minor versiyonları verilir


                .AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();
            //end pointe fiter ekleyerek  hangi classın valıdasyona uğryacağı ve validasyon yapan classıda veriyoruz
            //validasyon yapan class generic olduğundan(ValidationFilter) her endpoinet teker teker yzılmalı ama dinamik olmasaydı direkt 
            //CategoryEndpointExt classından guruplanan endpoite verilebilirdi 


            return group;
        }

    }
}
