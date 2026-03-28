using Asp.Versioning.Builder;
using Microservice.File.Api.Features.File.Delete;
using Microservice.File.Api.Features.File.Upload;

namespace Microservice.File.Api.Features.File
{
    public static class FileEndpointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files").WithTags("files").WithApiVersionSet(apiVersionSet)
            .UploadFileGroupItemEndpoint()
            .DeleteFileGroupItemEndpoint();
        }
    }
}
