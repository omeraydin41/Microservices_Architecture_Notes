using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroservices.Shared.Extansions
{
    public static class CommonServiceExt
    {
        //IServiceCollection geriyedönüş tipimiz
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services,Type assembly)
        {
            services.AddHttpContextAccessor();//yüm servislere erişmek için 

            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));//verilen classı tarar ve bulur 

            services.AddFluentValidationAutoValidation();//otomatık validasyonu yapacak 

            services.AddValidatorsFromAssemblyContaining(assembly);//asemmbly contai olarak verdik 


            return services;//amacımız her mıcreoservicede assembly dışardan vermek 
        }
    }
}
