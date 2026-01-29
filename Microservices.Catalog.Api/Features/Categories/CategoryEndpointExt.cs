using Microservices.Catalog.Api.Features.Categories.Create;

namespace Microservices.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {

        public static void AddCategoryGroupEndpointExt(this WebApplication app)
        {
            app.MapGroup("api/categories").CreateCategoryGroupItemEndpoint();
            // grup adı : "api/categories"  CreateCategoryGroupItemEndpoint methodu  CreateCategoryEndpoint clasından gelir
        }
    }
}
