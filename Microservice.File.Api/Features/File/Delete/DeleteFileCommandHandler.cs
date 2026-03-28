using MediatR;
using Microsoft.Extensions.FileProviders;
using NewMicroservices.Shared;

namespace Microservice.File.Api.Features.File.Delete
{
    public class DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>//DeleteFileCommand alıp geriye ServiceResult doneceğiz
    {
        public Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var fileInfo = fileProvider.GetFileInfo(Path.Combine("files", request.FileName));

            //ilgili dosya yoksa : CLEAN CODE ÖNCE OLUMSUZ DURUM 
            if (fileInfo.Exists)
            {//geriye hata donulur
                return Task.FromResult(ServiceResult.ErrorIsNotFound());
            }

            //dosya varsa silinme işlemi 
            System.IO.File.Delete(fileInfo.PhysicalPath!);//! dolayı fileInfo.PhysicalPath null olamaz

            return Task.FromResult(ServiceResult.SuccessAsNoContent());
        }
    }
}
