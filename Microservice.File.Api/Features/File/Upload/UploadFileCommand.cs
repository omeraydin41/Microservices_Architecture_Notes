using Microsoft.Extensions.FileProviders;
using NewMicroservices.Shared;

namespace Microservice.File.Api.Features.File.Upload
{
    public record UploadFileCommand(IFormFile File):IRequestByServiceResult<UploadFileCommandResponse>;
    //record :  Bir kez oluşturulduktan sonra içindeki veriyi değiştiremezsin.
}
