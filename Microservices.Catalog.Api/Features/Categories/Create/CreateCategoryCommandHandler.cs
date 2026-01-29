using MediatR;
using Microservices.Catalog.Api.Repostories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NewMicroservices.Shared;
using System.Net;

namespace Microservices.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context)//mediatR test surecını kolaylaştırır . test için appdbcontext e ihtiyacımız var
        : IRequestHandler<CreateCategoryCommand,ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            //category adı daha önce kullanılmış mı kontrol et :  mantıken etme yolu karşılaştıma : VAR OLAN LİSTE İLE İSTEĞİ KARŞILAŞTIMA
            //BİZ DE İSTEK İŞLEMİNİ İŞELYEN CLASSINDAYIZ :  İSTEK CLASSI : CreateCategoryCommand İLE CATEGORY KARŞILAŞTITILACAK
            var existCategory = context.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken: cancellationToken);
            //1.Name =Category(kategory listesi classı ) clasından gelir 2.Name : CreateCategoryCommand(ategorı oluşturma ) clsından gelr// any varmı 


            if (existCategory)//check etme 
            {//title detail                                //ERROR MESAJLARINDAN STRING STRING BADREQUEST OLAN ERROR MESAJINI DOLDURDUK /Shared CLASS LIB  ServiceResult CLACC İÇİ
                ServiceResult<CreateCategoryResponse>.Error("category name already exist",$"the category name '{request.Name}' already exist",
                    HttpStatusCode.BadRequest);
            }
        }
    }
}
