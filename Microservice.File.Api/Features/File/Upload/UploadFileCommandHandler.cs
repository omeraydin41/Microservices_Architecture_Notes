using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.FileProviders;
using NewMicroservices.Shared;
using System.Net;

namespace Microservice.File.Api.Features.File.Upload
{
    public class UploadFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
    {//bir dosyayı kaydederken dosyanın fiziksel lokasyonu lazım : fiziksel lokasyon anlık olarak tutulması lazım 
        public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            //gelen dosya dolumu değilmı onu kontrol edebılırız 

            if (request.File.Length==0)
            {
                return ServiceResult<UploadFileCommandResponse>.Error("invalid file","provided file is empty ",HttpStatusCode.BadRequest);//geriye UploadFileCommandResponse nesnesi donecek 
            }


            //gelen dosyalar aynı isimde olup bırbırını ezmesin diye random isim uretılmelı 
            var newFileName = 
                $"{Guid.NewGuid()}" +//resımler için random değer oluşturmada kullanılacak 
                $"{Path.GetExtension(request.File.FileName)}";//{Path.GetExtension(request.File.FileName) resmın uzantısını alır örn : .jpg .png

            var uploadPath = Path.Combine//pathları birleştirmeye yarar.
                (fileProvider.GetFileInfo("files")//pathın yerını aldık : files
                .PhysicalPath!//fiziksel path aldık buluta yuklendiğinde değişebılır.! sebebi boyle bır yer kesın var dedik null olamaz 
                , newFileName);//random isimi verdik 


            //bu isimi stram olarak kaydedilmesi

            await using var stream = new FileStream//strem kullanılırken USİNG kullanılır .HEMEN ÇALIŞIP KAPANSIN DİYE. YER AÇILSIN
                (uploadPath, //dosya adı 
                 FileMode.Create);//ve yenı dosya oluşsun 
            /* Stream : 1 GB'lık bir dosyayı bir kerede ışınlayamazsın. 
             * Veriyi küçük parçalara (byte'lara) böler, 
             * bu borunun içinden sırayla akıtarak hedefe ulaştırırsın.*/


            await request.File.CopyToAsync (stream,cancellationToken);//requestten gelen fileyı streama kopyaladık.

            var response = new UploadFileCommandResponse
                (newFileName//random file adı
                ,$"files/{newFileName}"//dosya yolu 
                ,request.File.FileName);//orjinal file adı

            return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(response,response.FilePath);
        }
    }
}
