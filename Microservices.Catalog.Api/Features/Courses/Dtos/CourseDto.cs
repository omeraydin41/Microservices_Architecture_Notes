namespace Microservices.Catalog.Api.Features.Courses.Dtos
{
   public record class CourseDto(
         Guid Id,
         string Name ,
         string Description ,
         decimal Price,
         string ImageUrl,
         CategoryDto Category,
         FeatureDto Feature);
         


   
}
