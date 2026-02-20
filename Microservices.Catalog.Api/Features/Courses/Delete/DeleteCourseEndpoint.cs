
using Microservices.Catalog.Api.Features.Categories.GetById;

namespace Microservices.Catalog.Api.Features.Courses.Delete
{
    public record DeleteCourseCommand(Guid Id): IRequestByServiceResult;//genereic olmayanını döndük sebebi 204 nocontent döndüreceğiz
                                                                        //yani herhangi bir data dönmeyeceğiz sadece başarılı veya başarısız olduğunu göstermek istiyoruz.

    public class DeleteCourseCommandHandler(AppDbContext context ,IMapper mapper) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            //önce kurs varmı kontrol edilir 
            var hasCourse = await context.Courses.FindAsync([request.Id],cancellationToken: cancellationToken);
            if (hasCourse == null)
            {
                return ServiceResult.ErrorIsNotFound();
            }

            //eğer aranan kurs bulunursa 
            context.Courses.Remove(hasCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
    public static class DeleteCourseEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}"//kısıtlama belirterek id nin guid formatında olması gerektiğini söyledik
                , async (IMediator mediator, Guid id) =>
                (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult()).WithName("DeleteCourse");

            return group;

        }

    }
}
