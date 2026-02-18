namespace Microservices.Catalog.Api.Features.Courses.Update
{
    public record UpdateCourseCommand
        (Guid Id,string Name,string Description,
        decimal Price,string? ImageUrl,Guid CategoryId): IRequestByServiceResult;//geriye generic bir şey donmedik 
    //imageurl olmak zorunda depğil ? derleyiciye ve ekip arkadaşlarına mesaj
}
