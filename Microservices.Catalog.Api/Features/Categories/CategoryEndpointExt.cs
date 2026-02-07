using Microservices.Catalog.Api.Features.Categories.Create;
using Microservices.Catalog.Api.Features.Categories.GetAll;

namespace Microservices.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {

        public static void AddCategoryGroupEndpointExt(this WebApplication app)//her endpoint buraya eklenmeli 
        {
            app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint().
            // grup adı : "api/categories"  CreateCategoryGroupItemEndpoint methodu  CreateCategoryEndpoint clasından gelir

                GetAllCategoryGroupItemEndpoint();
            // grup adı : "api/categories"  GetAllCategoryGroupItemEndpoint methodu  GetAllCategoryEndpoint clasından gelir
        }
    }
}
