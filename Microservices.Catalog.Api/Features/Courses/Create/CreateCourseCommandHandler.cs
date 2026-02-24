
namespace Microservices.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    //handler edilecek classı ve geriye dönecek değer olarak service result turunde  guid  belirttik
    //service result:işlemin başarılı olup olmadığını ve mesajı taşıyan shared classı
    {


        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            //category uzerınden course varmı konterol edeceğiz

            var hasCategory = await context.Categories.AnyAsync(x=>x.Id==request.CategoryId,cancellationToken);
            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error($"{request.CategoryId}Category not found",HttpStatusCode.NotFound);
                //service resultun failed methodunu kullanarak başarısız sonucu döndürüyoruz ve mesaj veriyoruz
            }

           //course yı adından kontrol etme
           var hasCourse = await context.Courses.AnyAsync(x=>x.Name.ToLower()==request.Name.ToLower(),cancellationToken);   
           //eğer true donerse kurs adı var ise 
           if (hasCourse)
            {
                return ServiceResult<Guid>.Error($"{request.Name}Course already exists",HttpStatusCode.BadRequest);
                //service resultun failed methodunu kullanarak başarısız sonucu döndürüyoruz ve mesaj veriyoruz
            }




                var newCourse = mapper.Map<Course>(request);//requesti course nesnesine mapledik

            newCourse.Created = DateTime.Now;//kurs oluşturulma tarihini şu an olarak atadık
            newCourse.Id=NewId.NextSequentialGuid();//kursun id sini sequential guid olarak atadık
            newCourse.Feature=new Feature()
            {
                Duration=10,//claculator ile süre hesaplanacak
                EducatorFullName = "ömer aydın",//get by token ile kullanıcı adı alınacak
                Rating = 0//kurs oluşturulurken rating 0 olacak
            };//kursun özelliklerini atadık

            context.Courses.Add(newCourse);//yeni kursu veritabanına ekledik

           

            await context.SaveChangesAsync(cancellationToken);//değişiklikleri asenkron olarak kaydettik


            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id,$"/api/courses/{newCourse.Id}");
            //service resultun success methodunu kullanarak başarılı sonucu döndürüyoruz ve yeni oluşturulan kursun id sini döndürüyoruz

        }
    }
}
