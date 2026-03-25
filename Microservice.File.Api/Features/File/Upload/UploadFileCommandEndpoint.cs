using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewMicroservices.Shared.Extansions;

namespace Microservice.File.Api.Features.File.Upload
{
    public static class UploadFileCommandEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/",
                async (UploadFileCommand command,IMediator mediator)=>
                (await mediator.Send(command)).ToGenericResult()
            )
            .WithName("upload")
            .MapToApiVersion(1,0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)

                ;
            return group;
        }
    }
}
