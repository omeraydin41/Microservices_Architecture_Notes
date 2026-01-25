using Microsoft.Extensions.Options;

namespace Microservices.Catalog.Api.Options
{
    public static class OptionExt
    {
      
        public static IServiceCollection AppOptionExt(this IServiceCollection services)
        {
            services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations().ValidateOnStart();


            //opsıyon olarak MongoOption clasını ekledık.AddOptions<MongoOption>()

            //MongoOption : BU CLASS APPSETTİNG.DEVELOPMENT İÇİNDEKİ MongoOption KISMINDAKİ CONNCETİON ALANLARININ PROPERTYLERİNİ TUTAR 

            //BindConfiguration : KİME KONFİGURE ETMEK İSTIYORUZ MongoOption BURAYA 

            //ValidateDataAnnotations() //MongoOption classı uzerınden REQUIRED(MUTLAKA GERKLİ ALAN) YAPMAMIZA YARAR 

            //ValidateOnStart(); : YUKARDA OLAN VAALİDASYONU HER UYGULAMA YAĞA KALKTUĞINDA GERÇEKLEŞTİR.
            //NE DEMEK : MongoOption classı içindeki değişkenlerin appsetting de karşılığı yoksa  HATA VERİR



            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);//bu işlemin valuesını aldık 
            /*DI  konteynerine giderek,  ayarları bir "paket" olarak sunan IOptions interfacesinden  
            oluşturulan MongoOption sınıfına (class) ait gerçek veriyi Value ile çekip,
            sisteme doğrudan kullanılabilir tek bir nesne olarak kaydetmektir.
            IOptions interfacesi KONFİGURASYON DOSYALARINI TİP GÜVENLİKLİ OKUMAYA YARAR
             */

            return services;
        }

    }
}
