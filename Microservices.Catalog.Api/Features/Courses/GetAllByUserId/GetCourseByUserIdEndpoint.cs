using Microservices.Catalog.Api.Features.Courses.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson.Serialization.Serializers;
using static Microservices.Catalog.Api.Features.Courses.GetAllByUserId.GetCourseByUserIdEndpoint;

namespace Microservices.Catalog.Api.Features.Courses.GetAllByUserId
{
    // Dış sınıf ismini koruduk
    
    
        public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

        public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
        {
            public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request, CancellationToken cancellationToken)
            {
                var courses = await context.Courses.Where(x => x.UserId == request.Id).
                    ToListAsync(cancellationToken: cancellationToken);

                var categories = await context.Categories.ToListAsync(cancellationToken: cancellationToken);

                foreach (var course in courses)
                {
                    course.Category = categories.First(c => c.Id == course.CategoryId);
                }

                var coursesDto = mapper.Map<List<CourseDto>>(courses);
                return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesDto);
            }
        }
    

    public static class GetCourseByUserIdEndpoint
    {
        public static RouteGroupBuilder GetByUserIdCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet
            (   "/user/{userId:guid}",
                async (IMediator mediator, Guid Id) =>
                (await mediator.Send(new GetCourseByUserIdQuery(Id))).ToGenericResult()
            ).
                WithName("GetByUserIdCourses");
            return group;
        }
    }


}