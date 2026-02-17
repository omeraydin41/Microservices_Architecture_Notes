using Microservices.Catalog.Api.Features.Courses.Create;

namespace Microservices.Catalog.Api.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()//
        {
            CreateMap<CreateCourseCommand,Course>();
            //CreateCourseCommand classı Course classına mapleniyor yani dönüştürülüyor.
            //Bu sayede CreateCourseCommand içindeki verileri Course nesnesine kolayca aktarabiliriz.

            //iki classtakı alan adları uyuşmak zorunda değil ama ben aynı isimde yaparsam otomatik mapler .
            //Farklı isimde yaparsam manuel mapleme yapmam gerekir .
        }
    }
}
/*Profile, AutoMapper kütüphanesinden gelen ve hangi sınıfın hangi sınıfa nasıl dönüştürüleceğini 
(mapping kurallarını) bir paket halinde sisteme tanıtan yapılandırma (konfigürasyon) sınıfıdır.*/