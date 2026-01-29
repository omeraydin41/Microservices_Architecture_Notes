using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {

        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            //http:localhost:5000/api/categories den sonra / alanı gelır ve yonlendirir
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            {// "/" anlamı gurupları kontrol eden CategoryEndpointExt classındaki "api/categories" alanına denk 

                var result = await mediator.Send(command);

                return new ObjectResult(result)
                {
                    StatusCode = result.Status.GetHashCode(),

                };

            });
            return group;
        }

    }
}
