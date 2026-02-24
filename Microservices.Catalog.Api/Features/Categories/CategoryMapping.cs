using AutoMapper;
using Microservices.Catalog.Api.Features.Categories.Dtos;

namespace Microservices.Catalog.Api.Features.Categories
{
    public class CategoryMapping:Profile//kategoriyi ilgilendiren tüm maplama işlemleri burdan yapıllır 
    {
            public CategoryMapping()
            {
               CreateMap<Category,CategoryDto>().ReverseMap();
        }

    }
}
