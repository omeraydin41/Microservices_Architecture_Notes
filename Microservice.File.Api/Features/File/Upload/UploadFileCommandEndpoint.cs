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
                async (IFormFile file,IMediator mediator)=>
                (await mediator.Send(new UploadFileCommand(file))).ToGenericResult()
            )
            .WithName("upload")
            .MapToApiVersion(1,0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .DisableAntiforgery();
            /* DisableAntiforgery, .NET 8 ile gelen otomatik güvenlik kontrolünü (CSRF koruması) o endpoint için kapatmaya yarar. 
             * Özellikle dış kaynaklardan veya Swagger üzerinden dosya/form gönderirken alınan 400 Bad Request hatalarını çözmek için kullanılır. 
             * Eğer API'miz bir doğrulama token'ı beklemiyorsa, bu metodu ekleyerek isteğin sorunsuz işlenmesini sağlarız.*/
            return group;
        }
    }
}
