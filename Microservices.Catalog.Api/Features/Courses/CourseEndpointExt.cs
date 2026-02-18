using Microservices.Catalog.Api.Features.Categories.Create;
using Microservices.Catalog.Api.Features.Courses.Create;
using Microservices.Catalog.Api.Features.Courses.GetAll;
using Microservices.Catalog.Api.Features.Courses.GetById;
using Microservices.Catalog.Api.Features.Courses.Update;

namespace Microservices.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)//her endpoint buraya eklenmeli 
        {
            app.MapGroup("api/courses").WithTags("Courses").
            //whithtags ile swagger da hangi başlık altında gözükeceğini belirtiyoruz

                CreateCourseGroupItemEndpoint().

                GetByIdCourseItemGroupEndPoint().

                GetAllCourseGroupItemEndpoint().

                UpdateCourseGroupItemEndpoint();


            //app.MapGroup("api/courses") ile "api/courses" alanına gelen istekleri bu grup altında toplayacağız
            //ve bu grup altında CreateCourseGroupItemEndpoint ve GetAllCourseGroupItemEndpoint endpointlerini ekleyeceğiz.
        }

    }
}
