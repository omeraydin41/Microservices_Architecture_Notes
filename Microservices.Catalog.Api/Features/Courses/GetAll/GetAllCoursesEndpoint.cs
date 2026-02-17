using Microservices.Catalog.Api.Features.Courses.Create;
using Microservices.Catalog.Api.Features.Courses.Dtos;
using NewMicroservices.Shared.Filters;

namespace Microservices.Catalog.Api.Features.Courses.GetAll
{
    public record GetAllCoursesQuery : IRequestByServiceResult<List<CourseDto>>;

    public class GetAllCoursesHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult <List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            //kategorileri lite şeklinde aldık
           var courses = await context.Courses.ToListAsync(cancellationToken : cancellationToken);
            
            //kursalrı lıste şeklinde aldık 
           var categories= await context.Categories.ToListAsync(cancellationToken : cancellationToken);

            //kurslarda tek tek dolaşıp kategorileri eşleştirelim

            foreach (var course in courses)
            {
                course.Category=context.Categories.First();
                course.Category = categories.First(c => c.Id == course.CategoryId);
                //course un kategorisi kategoriler listesinden course un categoryid sine eşit olan kategoriyi bulup atıyoruz.
            }

            var coursesDto=mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesDto);

        }
    }

    public static class GetAllCoursesEndpoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCoursesQuery());
                return result.ToGenericResult();
            })
            .WithName("GetAllCourses");


            return group;
        }
    }
}
