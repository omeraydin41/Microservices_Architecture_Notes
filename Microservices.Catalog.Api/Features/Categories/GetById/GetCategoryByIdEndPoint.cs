
using Microservices.Catalog.Api.Repostories;

namespace Microservices.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;//service result turunde categorydto donecek 

    public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            //ilgili kategori yoksa geriye not foun donemek için önce kategoriyi bulmaya çalışalım
            var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);//cancellationToken işlem iptali için 

            //eğer bulunan kategori null ise
            if (hasCategory == null)//SERVİCERESULT CLASSINDAN HATA MESAJLARINDAN BİRİ OLAN ERROR KULLANARAK GERİ DÖNECEĞİZ KENDİ YAZDIĞIMIZ CLASS OLAN 
            {
                return ServiceResult<CategoryDto>.Error("Category not found", $"{request.Id} category was not found"
                    , HttpStatusCode.NotFound);
            }


            //KAREGORİ BULUNDUĞUNDA KATEGORİ DTO YA MAPLEME YAPALIM VE GERİ DÖNELİM
            var categoryAsDto = mapper.Map<CategoryDto>(hasCategory);
            return ServiceResult<CategoryDto>.SuccessAsOk(categoryAsDto);//BAŞARILI OLAN DATA SERVİCE RESULT CLASSINDAN SuccessAsOk İLE DONULDU BİZ YAZDIK 


        }
    }


    public static class GetCategoryByEndPoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}"//kısıtlama belirterek id nin guid formatında olması gerektiğini söyledik
                , async (IMediator mediator, Guid id) =>
                (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult()).WithName("GetByIdCategoryGroup")

             .MapToApiVersion(1, 0);//major ve minor versiyonları verilir

            return group;

        }


    }
}