
using Microservices.Catalog.Api.Features.Categories.Dtos;
using Microservices.Catalog.Api.Repostories;


namespace Microservices.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryQuery:IRequestByServiceResult<List<CategoryDto>>;
    //IRequest: MediatR kütüphanesinin bir parçasıdır ve bir sorgu (query) veya komut (command) nesnesi oluşturmak için kullanılır.
    //ServiceResult üzerinden list şeklinde CategoryDto döneceğiz


    //primary constuctounda AppDbContext alırız çünkü veritabanına erişmemiz gerekecek ve bu işlemi yaparken
    //MediatR'ın sağladığı IRequestHandler arayüzünü kullanarak sorguyu işlemek için bir sınıf oluştururuz.
    public class GetAllCategoryQueryHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {//IRequestHandler: MediatR kütüphanesinin bir parçasıdır ve belirli bir sorgu (query) veya komut (command) türünü işlemek için kullanılır.
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            
            var categories= await context.Categories.ToListAsync(cancellationToken : cancellationToken);

            var categoriesAsDto=mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);

        }
    }





    //tüm kategorileri listelem işlemi yapar 
    public static class GetAllCategoryEndpoint
    {

        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {

            group.MapGet("/", async (IMediator mediator) =>
            (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult()).WithName("GetAllCategory");//ToResult methodumuzn adı donuş olarak belirlemdi
            // "/" anlamı gurupları kontrol eden CategoryEndpointExt classındaki "api/categories" alanına denk 

            //end pointe fiter ekleyerek  hangi classın valıdasyona uğryacağı ve validasyon yapan classıda veriyoruz
            //validasyon yapan class generic olduğundan(ValidationFilter) her endpoinet teker teker yzılmalı ama dinamik olmasaydı direkt 
            //CategoryEndpointExt classından guruplanan endpoite verilebilirdi 


            return group;
        }

    }
}
