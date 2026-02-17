using Microservices.Catalog.Api.Features.Courses.Dtos;

namespace Microservices.Catalog.Api.Features.Courses.GetById
{
    public record GetcourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetcourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetcourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetcourseByIdQuery request, CancellationToken cancellationToken)
        {
            //kursları listelemeden önce böyle bir kategori varmı omu kontrol etmek lazım 
            var hasCourse = await context.Courses  
                 .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (hasCourse is null)
            {
                return ServiceResult<CourseDto>.Error("girilen id ye ait kurs bulunamadı ", HttpStatusCode.NotFound);
            }

            //eğer kurs varsa kategori biligisini alacağız 
            var category = await context.Categories.FindAsync
                (hasCourse.CategoryId, cancellationToken);
            //bu kategori olmak zorunda çünkü kursun kategorisi var ve o kategoriyi bulmak zorundayız



            hasCourse.Category = category!;//kategoriyi kursa atıyoruz//null olamayacağı için ! işareti koyduk

            var courseDto = mapper.Map<CourseDto>(hasCourse);

            return ServiceResult<CourseDto>.SuccessAsOk(courseDto);
        }
    }

    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCourseItemGroupEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new GetcourseByIdQuery(id));
                return result.ToGenericResult();
            })
            .WithName("GetByIdCourse"); // WithName buraya, MapGet'in dışına gelmeli

            return group;
        }
    }
}
