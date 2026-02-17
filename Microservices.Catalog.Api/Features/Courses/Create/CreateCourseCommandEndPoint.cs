using Microsoft.AspNetCore.Mvc;
using NewMicroservices.Shared.Filters;

namespace Microservices.Catalog.Api.Features.Courses.Create
{
    public static class CreateCourseCommandEndPoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            //http:localhost:5000/api/categories den sonra / alanı gelır ve yonlendirir
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
            (await mediator.Send(command)).ToGenericResult()).WithName("CreateCourse")//ToResult methodumuzn adı donuş olarak belirlemdi
            // "/" anlamı gurupları kontrol eden CategoryEndpointExt classındaki "api/categories" alanına denk 

            .Produces<Guid>(StatusCodes.Status201Created)//201 olaursa guid değer don geriye
            .Produces(StatusCodes.Status404NotFound)//GENERİC OLARAK DÖNUŞU YOK .
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)


                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();
            //end pointe fiter ekleyerek  hangi classın valıdasyona uğryacağı ve validasyon yapan classıda veriyoruz
            //validasyon yapan class generic olduğundan(ValidationFilter) her endpoinet teker teker yzılmalı ama dinamik olmasaydı direkt 
            //CategoryEndpointExt classından guruplanan endpoite verilebilirdi 


            return group;
        }
    }
}
