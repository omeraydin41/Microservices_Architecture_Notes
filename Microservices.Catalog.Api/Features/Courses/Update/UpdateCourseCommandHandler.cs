
namespace Microservices.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context,IMapper mapper) 
        : IRequestHandler<UpdateCourseCommand, ServiceResult>
    // hani classı handler edecegimizi ve ne döndüreceğimizi belirtiyoruz
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            //ÖNCE KURS VAR MI BAKALIM

            var hasCourse = await context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);
            //find primary key gore arama yapar . aç tane key olduğunu bilmediğimizde key olarak Id var ve onu verdik new object[] { request.Id }
            if (hasCourse is null)
            {
                return ServiceResult.ErrorIsNotFound();
            }

            //KURS VARSA GÜNCELLEME İŞLEMİ YAPILIR
            hasCourse.Name = request.Name;
            hasCourse.Description = request.Description;
            hasCourse.Price = request.Price;
            hasCourse.ImageUrl = request.ImageUrl;
            hasCourse.CategoryId = request.CategoryId;

            context.Courses.Update(hasCourse);

            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
