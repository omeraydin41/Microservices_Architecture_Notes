using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NewMicroservices.Shared.Extansions
{
    public static class VersioningExt
    {
        //eğer versiyonlama yapılacaksa servis ve middleware tarafında yazılacak kodlar var.

        //servis tarafı 
        public static IServiceCollection AddVersioningExt(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1,0); //varsayılan versiyon 1
                options.AssumeDefaultVersionWhenUnspecified = true; //eğer versiyon belirtilmezse varsayılan versiyonu kullan
                options.ReportApiVersions = true; //endpointe istek atıldığınde hangi versiyonların olduğunu header kısmında gösterir
                options.ApiVersionReader = new UrlSegmentApiVersionReader();//versiyon bilgisini url üzerinden okumak için kullanılır.Headere göre daha anlaşılır.query en koü yöntem 
                                                                            //örneğin: api/v1/products gibi
                                                                            //options.ApiVersionReader =new ApiVersionReader.
                                                                            //Combine(
                                                                            //new HeaderApiVersionReader(),
                                                                            //new QueryStringApiVersionReader(),
                                                                            //new UrlSegmentApiVersionReader());//hem url hem header üzerinden versiyon bilgisini okumak için kullanılır.
                                                                                                                //Header kısmında x-api-version yazan kısımda versiyon bilgisi verilir
                                                                                                                //QueryString kısmında ise ?api-version=1 gibi versiyon bilgisi verilir

            }).//Swagger için doğru formatta gösterme işlemi : V1 - V2 gibi :MVC.api.explorler tarafında yapılan işlemler
            AddApiExplorer(options => { 
              options.GroupNameFormat = "'v'V"; //Swagger tarafında versiyonlama işlemi için gerekli format
              options.SubstituteApiVersionInUrl= true; //Swagger tarafında versiyonlama işlemi için gerekli format
            });



            return services;
        }


        //extensions olarka middleware tarafında kullanılacak kod :PROGRAM.CS de app den sonrası için yazılaccak kodlar 
        public static ApiVersionSet AddVersionSetExt(this WebApplication app)//app : program.cs içindeki webapplication için extansion method yazıyoruz
                                                                             //ext : dışarıdan sanki kendi metoduymuş gibi yeni fonksiyonlar eklemeni sağlayan özellik.
        {
            var apiVersionSet=app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion( 1,0))//versiyon 1.0 ekliyoruz
                .HasApiVersion(new ApiVersion( 2,0))//versiyon 2.0 ekliyoruz
                .ReportApiVersions()//versiyon bilgilerini header kısmında gösterir
                .Build();

            return apiVersionSet;
        }



    }
}
