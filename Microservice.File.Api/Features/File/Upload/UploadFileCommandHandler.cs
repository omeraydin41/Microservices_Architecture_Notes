using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.FileProviders;
using NewMicroservices.Shared;

namespace Microservice.File.Api.Features.File.Upload
{
    public class UploadFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {//bir dosyayı kaydederken dosyanın fiziksel lokasyonu lazım : fiziksel lokasyon anlık olarak tutulması lazım 
        public Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
