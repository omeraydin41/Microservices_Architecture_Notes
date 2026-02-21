using Asp.Versioning.Builder;
using Microservices.Catalog.Api.Features.Categories.Create;
using Microservices.Catalog.Api.Features.Courses.Create;
using Microservices.Catalog.Api.Features.Courses.Delete;
using Microservices.Catalog.Api.Features.Courses.GetAll;
using Microservices.Catalog.Api.Features.Courses.GetAllByUserId;
using Microservices.Catalog.Api.Features.Courses.GetById;
using Microservices.Catalog.Api.Features.Courses.Update;

namespace Microservices.Catalog.Api.Features.Courses
{
    public static class CourseEndpointExt
    {
        public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)//her endpoint buraya eklenmeli 
        {
            app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses").
            //whithtags ile swagger da hangi başlık altında gözükeceğini belirtiyoruz

                WithApiVersionSet(apiVersionSet).

                CreateCourseGroupItemEndpoint().

                GetByIdCourseItemGroupEndPoint().

                GetAllCourseGroupItemEndpoint().

                DeleteCourseGroupItemEndpoint().

                GetByUserIdCourseGroupItemEndpoint().

                UpdateCourseGroupItemEndpoint();


            //app.MapGroup("api/courses") ile "api/courses" alanına gelen istekleri bu grup altında toplayacağız
            //ve bu grup altında CreateCourseGroupItemEndpoint ve GetAllCourseGroupItemEndpoint endpointlerini ekleyeceğiz.
        }

    }
}
