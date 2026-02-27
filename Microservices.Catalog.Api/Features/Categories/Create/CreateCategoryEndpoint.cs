using NewMicroservices.Shared.Filters;

namespace Microservices.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpoint
    {

        public static RouteGroupBuilder CreateCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            //http:localhost:5000/api/categories den sonra / alanı gelır ve yonlendirir
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator) =>
            (await mediator.Send(command)).ToGenericResult()).WithName("CreateCategory")//ToResult methodumuzn adı donuş olarak belirlemdi
            // "/" anlamı gurupları kontrol eden CategoryEndpointExt classındaki "api/categories" alanına denk 

            
            //hangi versiyonun kullanılacağını belirmek için 1.0 mı 2.0 mı 
            .MapToApiVersion(1,0)//major ve minor versiyonları verilir


                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();
            //end pointe fiter ekleyerek  hangi classın valıdasyona uğryacağı ve validasyon yapan classıda veriyoruz
            //validasyon yapan class generic olduğundan(ValidationFilter) her endpoinet teker teker yzılmalı ama dinamik olmasaydı direkt 
            //CategoryEndpointExt classından guruplanan endpoite verilebilirdi 


            return group;
        }

    }
}
