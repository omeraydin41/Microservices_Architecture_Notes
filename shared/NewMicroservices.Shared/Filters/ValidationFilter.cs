using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroservices.Shared.Filters
{
    public class ValidationFilter<T>/* validate edeceğimiz request farklı olacağından dinamık tanımladık*/ : IEndpointFilter
    {
        //mvc den gitseydik 5 tane filter vardır  /action /result /Authorization /Resource /Exception
        //minimal apilerde IEndpointFilter gidilir nedeni controller ve action methodlarının olmaması 1 tane filter var o da IEndpointFilter

        //kategori oluştuma isteği CreateCategoryEndpoint classına girmeden burda fitre olacak 
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)//IEndpointFilter interfacesinden gelen method
        {
            //InvokeAsync methodunda iki olay var kod CreateCategoryEndpoint endpointine girmeden önce ve girdikten sonra araya girme 





            //CreateCategoryEndpoint girmeden önce bi check yapamak istiyoruz 
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            //abstarct validatordan miras alınmış bir class varmı onu kontrol eder //CreateCategoryCommandValidator classı almıştır

            //bahsi geçen classı buldu CreateCategoryCommandValidator
            if (validator is null) //kontrolden sonra eğer miras alınmış class yoksa 
            {
                return await next(context);//CreateCategoryEndpoint'e bu request girebilir 
            }


            //eğer bu alan null değilse CreateCategoryEndpoint içindeki methoddaki ilk parametre yakalanmalı "CreateCategoryCommand command"
            //abstarctan miras almış olan class içindeki endpoint methodundan ilk parametreyi yakaladı 
            var requestModel = context.Arguments.OfType<T>().FirstOrDefault();//varsa al yoksa null

            if(requestModel is null)
            {
                return await next(context);//CreateCategoryEndpoint'e bu request girebilir
            }


            //ilk parametre beklenen tipteyse ve varsa bu VALİDATE EDİLEBİLİR
            var validateResult = await validator.ValidateAsync(requestModel);

            if (!validateResult.IsValid)
            {//gelen değer validasyondan geçmediyse ise Results üzerinden hata mesajı donecek : ToDictionary direkt istediği tipi verir

                return Results.ValidationProblem(validateResult.ToDictionary());
                 
            }
            //buraya kadar FAST FAİL yaklaşımı kullanıldı CLEAN CODE DE önce olumsuzlar ele alınır 

            






                //buraya kadar olan kodlar CreateCategoryEndpoint ine girmeden önce 
                return await next(context);
            //buraya kadar olan kodlar ise CreateCategoryEndpoint inden çıktıktan sonra 




        }
    }
}
