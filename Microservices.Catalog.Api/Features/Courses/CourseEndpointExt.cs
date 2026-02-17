using Microservices.Catalog.Api.Features.Categories.Create;
using Microservices.Catalog.Api.Features.Courses.Create;

namespace Microservices.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app)//her endpoint buraya eklenmeli 
        {
            app.MapGroup("api/courses").WithTags("Courses").CreateCourseGroupItemEndpoint();
            //whithtags ile swagger da hangi başlık altında gözükeceğini belirtiyoruz
        }

    }
}
