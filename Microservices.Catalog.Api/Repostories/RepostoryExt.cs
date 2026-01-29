using Microservices.Catalog.Api.Options;
using MongoDB.Driver;

namespace Microservices.Catalog.Api.Repostories
{
    public static class RepostoryExt
    {
        public static IServiceCollection AppDatabaseServiceExt(this IServiceCollection services)
        {

            //APPDBCONTEXT İ DI AKTARMA : APPDB CONTEXT İÇİNDEKİ YARDIMCI METHOD(CREATE) HER KULLANILDIĞINDA NESEN ALINMALI 
            //SİNGELTON OLARAK AYAĞA KALKACAK //IMongoClient interface verdik,buna karşılık IMongoClient nesen orneği verdik //nesneyı verdığımız yer 
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var option = sp.GetRequiredService<MongoOption>();
                return new MongoClient(option.ConnectionString);
                //nedenı ConnectionString 1 tanedir 1 kere okumak yeterli

            });
            //mongoya her bağlanmak ıstediğimizde geriye AppDbContextdoneceğiz
           services.AddScoped(sp =>
            {
                //dı contaıneri interface uzerınden ekledik ust methodda burda geri alacağız 
                var mongoClient = sp.GetRequiredService<IMongoClient>();// GetRequiredService ile IMongoClient eriştik 

                var option = sp.GetRequiredService<MongoOption>();//MongoOption ı da aldık

                return AppDbContext.Create(mongoClient.GetDatabase(option.DatabaseName));//create extensıon methodumuzdur optionext içindeki
                                                                                         //cerate ile alınan database name appsettıngdev içindeki alan adını kullanacak 
            });
            return services;

        }
    }
}
