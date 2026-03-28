using NewMicroservices.Shared;

namespace Microservice.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName):IRequestByServiceResult;
   
}
