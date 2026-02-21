using Asp.Versioning.Builder;
using Microservices.Catalog.Api.Features.Categories.Create;
using Microservices.Catalog.Api.Features.Categories.GetAll;
using Microservices.Catalog.Api.Features.Categories.GetById;

namespace Microservices.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt//her endpoint buraya eklenmeli 
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)//apiVersionSet : versiyonlama için gerekli
        {
            app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")

             .WithApiVersionSet(apiVersionSet)//versiyonlama işlemi için tüm endpointlere burdan eklendi

             .CreateCategoryGroupItemEndpoint()// grup adı : "api/categories"  CreateCategoryGroupItemEndpoint methodu  CreateCategoryEndpoint clasından gelir


             .GetAllCategoryGroupItemEndpoint()// grup adı : "api/categories"  GetAllCategoryGroupItemEndpoint methodu  GetAllCategoryEndpoint clasından gelir


             .GetByIdCategoryGroupItemEndpoint();  // grup adı : "api/categories"  GetByIdCategoryGroupItemEndpoint methodu  GetByIdCategoryEndpoint clasından gelir ıd ye gore kategori getirmek için

        }
    }
}
