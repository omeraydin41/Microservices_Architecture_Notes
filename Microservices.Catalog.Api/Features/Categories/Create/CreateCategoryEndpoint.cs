using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewMicroservices.Shared.Extansions;

namespace Microservices.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {

        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            //http:localhost:5000/api/categories den sonra / alanı gelır ve yonlendirir
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) => ( await mediator.Send(command)).ToGenericResult());//ToResult methodumuzn adı donuş olarak belirlemdi
            // "/" anlamı gurupları kontrol eden CategoryEndpointExt classındaki "api/categories" alanına denk 

            return group;
        }

    }
}
