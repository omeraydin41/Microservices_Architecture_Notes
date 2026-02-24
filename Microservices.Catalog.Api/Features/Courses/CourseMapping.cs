using Microservices.Catalog.Api.Features.Categories;
using Microservices.Catalog.Api.Features.Courses.Create;
using Microservices.Catalog.Api.Features.Courses.Dtos;

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

            CreateMap<Course,CourseDto>().ReverseMap();
            CreateMap<Feature,FeatureDto>().ReverseMap();//reverse map bu işlmeın tersıde olabılır            
        }
    }
}
/*Profile, AutoMapper kütüphanesinden gelen ve hangi sınıfın hangi sınıfa nasıl dönüştürüleceğini 
(mapping kurallarını) bir paket halinde sisteme tanıtan yapılandırma (konfigürasyon) sınıfıdır.*/