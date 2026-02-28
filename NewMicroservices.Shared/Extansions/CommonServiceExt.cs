using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NewMicroservices.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NewMicroservices.Shared.Extansions
{
    public static class CommonServiceExt//TÜM YARDIMCI VE GLOBAL SERVİSLER BURAYA EKLENİR BURDAN PROGRAM.CS İÇİNE YAZILIR 
    {
        //IServiceCollection geriyedönüş tipimiz
        public static IServiceCollection AddCommonServiceExt(this IServiceCollection services,Type assembly)
        {
            services.AddHttpContextAccessor();//yüm servislere erişmek için 

            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(assembly));//verilen classı tarar ve bulur 

            services.AddFluentValidationAutoValidation();//otomatık validasyonu yapacak 

            services.AddValidatorsFromAssemblyContaining(assembly);//asemmbly contai olarak verdik 

            services.AddScoped<IIdentityServices,IdentityServicesFake>();

            services.AddAutoMapper(assembly);//verilen assemblyi tarar ve bulur


            return services;//amacımız her mıcreoservicede assembly dışardan vermek 
        }
    }
}
