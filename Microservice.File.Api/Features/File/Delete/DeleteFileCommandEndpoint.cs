using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewMicroservices.Shared.Extansions;

namespace Microservice.File.Api.Features.File.Delete
{
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{fileName}", async (string fileName, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteFileCommand(fileName));
                return result.ToGenericResult();
            }) // <--- MapDelete parantezi burada kapanmalı
            .WithName("delete") // "upload" yerine "DeleteFile" daha mantıklı bir isim olabilir
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .DisableAntiforgery();

            return group;
            /* DisableAntiforgery, .NET 8 ile gelen otomatik güvenlik kontrolünü (CSRF koruması) o endpoint için kapatmaya yarar. 

           * Özellikle dış kaynaklardan veya Swagger üzerinden dosya/form gönderirken alınan 400 Bad Request hatalarını çözmek için kullanılır. 

           * Eğer API'miz bir doğrulama token'ı beklemiyorsa, bu metodu ekleyerek isteğin sorunsuz işlenmesini sağlarız.*/
        }
    }
}