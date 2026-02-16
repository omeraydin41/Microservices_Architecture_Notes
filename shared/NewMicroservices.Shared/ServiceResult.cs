
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;//ProblemDetails için gerekli 
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;


namespace NewMicroservices.Shared
{
    public interface IRequestByServiceResult<T> :IRequest<ServiceResult<T>>;
    //mediatrın IRequest interface ini kullanarak ServiceResult<T> döndüren bir interface oluşturduk 

    //dinamik olmayanı 
    public interface IRequestByServiceResult : IRequest<ServiceResult>;








    //ServiceResult başarılı dönulduğunde sadece Status DOLDURULMALI Fail doldurulmaz .IsSucces ile kontrol edilir

    public class ServiceResult//public olursa diğer ASSAMBLYLERDEN erişilebilir
    {
        [JsonIgnore]//bu property serialize edilmeyerek json a dahil etme response(cevap) modelinde olmasın.
                    //nedeni başarılı olma surumunda zaten kod verliyor tekrar bodyde yazılmasına gerek yo 
        public HttpStatusCode Status { get; set; }//200,300,400,500 gibi kodlar

        public Microsoft.AspNetCore.Mvc.ProblemDetails? Fail { get; set; }//mesaj başarısız olduğunda bu fail kullanılacak dönmezse dolmayacak null kalacak 
        //refit değil Microsoft.AspNetCore.Mvc; kullanıyoruz :  refit ielrde değişebilir proje file editleyerek <FrameworkReference Include="Microsoft.AspNetCore.App" /> ekledik



        //yardımcı methodlar : lambda ile girildiğinden sadece GET VARDIR //ve response modelde görünmez
        [JsonIgnore] public bool IsSucces=>Fail is null;//Fail null ise IsSucces alını yani başarılı demek 
        [JsonIgnore] public bool IsFail => !IsSucces;//IsSucces dönmüyorsa eğer başarısız demek



        //yardımcı statik factory methodlar sürekli new lemek zorunda kalmamak için
        public static ServiceResult SuccessAsNoContent()//generic method 
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NoContent
            };
        }

        public static ServiceResult ErrorIsNotFound()//FAİL OLDUĞUNDAN PROBLEMDETAİLS MANTIĞI İLE BODY DOLDURULMALI MESAJ TÜRÜ VB.
        {
            return new ServiceResult
            {
                Status = HttpStatusCode.NotFound,
                Fail=new Microsoft.AspNetCore.Mvc.ProblemDetails
                {
                    Title="Not found",//Not foundolduğunda response(cevap) body sine bakmaz zaten kaynak yok 
                    Detail ="request kayanğı bulunamadı "//açıklamayada gerek kalmaz bu durumda 
                }
            };
        }



        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Status = status,
                Fail = problemDetails//problem detilsi yukarıdan aldık 
            };
        }

        //Sadece Title ve  Detailsi doldurmak için kullanılır 
        public static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Status = status,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = (int)status//veya status.GetHashCode() de kullanılabilir/ProblemDetails içindede status alanı var 
                }
            };
        }


        //Sadece Title doldurmak için kullanılır
        public static ServiceResult Error(string title, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Status = status,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = (int)status//veya status.GetHashCode() de kullanılabilir/ProblemDetails içindede status alanı var 
                }
            };
        }



        //mail ve quantıty gibi VALİİDASYON  hataları donmek için kullanılır : BUNLAR ERRORUN İÇİNDEKİ SINIFIN İÇİNDE KEY BUNUN VALUESİ İSE OLARAK VARDIR 
        //örneğin quantity hatası : "quantity":["Miktar negatif olamaz."] BUNU YAKALAMAK DİCTONARY GEREKİR
        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {//ErrorFromValidation : validasyondan gelen hatalar 
            return new ServiceResult()
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title = "Vaildation errors occured.",
                    Detail = "please check the error property for more details.",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()//ProblemDetails içindede status alanı var
                }
            };
        }


        //REFİTTEN GELEN HATA MESAJLARI 
        //gelen hata mesajını dönüştürmemiz lazım 

        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {//exceptioniçinde problem details var onu alacağız

            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                    {
                        Title = exception.Message,

                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true//büyük küçük harf duyarsız


                });
            return new ServiceResult()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };

        }
    }



    //ERRORLA ALAKLAI OLAN GENERİC METHODLAR GENERİC CLASSTAN ALINIP <T> kladırılarak UST CLASSA DA KOPYALANDI 
    //ıkı kısımda da aynı method sısımlerı olduğunda hanggisini kullanmak istediğimzde 
    //diğerini VİRTUALLA EZMEK (POLİMORFİZİM YAPMAK)  YERINE GENERİC CLASSTA HATA METHODLARININ BAŞINA NEW ANAHTAR KELİMESİ EKLENDİ



    //BAŞARILI MESAJ DONULDUĞUNDE VE MESAJLA BERABER GÖVDEDE MESAJDA TUTULMAK İSTENİRSE KULLANILACAK YAPI 
    public class ServiceResult<T> : ServiceResult//GENERIC( kapsamlı daha genel ) HALİ //üst classtan miraas almak zorundadır
    {
        //üstteki T değişken ile yakalandı / gövdede tutulacak mesajın değişkeni olmazsa null olur ? dolayı 
        public T? Data { get; set; }//mesaj başarılı geldiğinde bu dolacak olmadığında kalıtım yolu ile üstteki Fail tutulacak 

        [JsonIgnore] public string? UrlAsCreated { get; set; }//201 dödüğünde doldurulacak 



        //200 kodu için 
        public static ServiceResult<T> SuccessAsOk(T data)//generic method 
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.OK,//mesaın kodu ok olduğundan sadece data gerklı gerisi önemsiz 
                Data=data
            };
        }


        //201 : CREATED DURUMU : response(cevabın) body sinin headerinde : LOCATİON KEYWORDU var : create olan nesneye ulşam URL Sİ burdadır 
        //örn api/product/5 id 5 olan 



        //201 kodu için /UrlAsCreated değikenını kullanacağız 
        public static ServiceResult<T> SuccessAsCreated(T data,string url)//generic method 
        {
            return new ServiceResult<T>
            {
                Status = HttpStatusCode.Created,//mesaj türü ne göre govdedeki değişkenler de değişir /oluşturuldu mesajı  için data ve erişim kodu lazım 
                Data = data,
                UrlAsCreated=url
            };
        }


       

        public new static ServiceResult<T> Error(ProblemDetails problemDetails,HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Status = status,
                Fail = problemDetails//problem detilsi yukarıdan aldık 
            };
        }

        //Sadece Title ve  Detailsi doldurmak için kullanılır 
        public new static ServiceResult<T> Error(string title,string description, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Status=status,
                Fail=new ProblemDetails
                {
                    Title=title,
                    Detail=description,
                    Status=(int)status//veya status.GetHashCode() de kullanılabilir/ProblemDetails içindede status alanı var 
                }
            };
        }


        //Sadece Title doldurmak için kullanılır
        public new static ServiceResult<T> Error(string title, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Status = status,
                Fail = new ProblemDetails
                {
                    Title = title,
                    Status = (int)status//veya status.GetHashCode() de kullanılabilir/ProblemDetails içindede status alanı var 
                }
            };
        }



        //mail ve quantıty gibi VALİİDASYON  hataları donmek için kullanılır : BUNLAR ERRORUN İÇİNDEKİ SINIFIN İÇİNDE KEY BUNUN VALUESİ İSE OLARAK VARDIR 
        //örneğin quantity hatası : "quantity":["Miktar negatif olamaz."] BUNU YAKALAMAK DİCTONARY GEREKİR
        public new static ServiceResult<T> ErrorFromValidation(IDictionary<string,object?> errors)
        {//ErrorFromValidation : validasyondan gelen hatalar 
            return new ServiceResult<T>()
            {
                Status = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails
                {
                    Title ="Vaildation errors occured.",
                    Detail = "please check the error property for more details.",
                    Extensions=errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()//ProblemDetails içindede status alanı var
                }
            };
        }


        //REFİTTEN GELEN HATA MESAJLARI 
        //gelen hata mesajını dönüştürmemiz lazım 

        public new  static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {//exceptioniçinde problem details var onu alacağız

            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new Microsoft.AspNetCore.Mvc.ProblemDetails()
                    {
                        Title = exception.Message,

                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(exception.Content,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true//büyük küçük harf duyarsız


                });
            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };

        }





    }

    /*ServiceResult (Düz): Genelde veri dönmesi gerekmeyen işlemler için kullanılır. SAYI DONEBİLİR AMA GÖVEDE MESAJ YOKTUR.
    * Mesela bir "Şifre Sıfırlama Maili Gönder" işlemi. Veri dönmez, 
    * sadece "Başarılı mı?" sorusuna yanıt verir.
    ServiceResult<T> (Generic): Bir veri listesi veya nesne dönüleceği zaman kullanılır.GÖVEDE MESAJ YUKLU OLDUĞUNDA 
    * <T> yerine User, Product veya List<Order> gelebilir. 
    * Bu sayede her farklı veri tipi için ayrı ayrı sınıf yazmak zorunda kalmazsın.*/



    //ProblemDetails HERKES İÇİN BELİRLENMİŞ HATA alanları 
    /*{
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        "title": "Not Found",
        "status": 404,
        "detail": "The requested resource was not found",
        "instance": "/api/products/5",
        "errors": {  // Bu kısım Extensions içine girer
        "id": ["Ürün bulunamadı."]
        "email": ["Email adresi geçersiz."]
        "quantity": ["Miktar negatif olamaz."]
     }
    */
}
